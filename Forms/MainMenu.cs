using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using ZOPZ_SNIFF.Api;
using ZOPZ_SNIFF.Config;
using ZOPZ_SNIFF.Config.Configs;
using ZOPZ_SNIFF.Json.Info;
using ZOPZ_SNIFF.Json.Sniffer;
using ZOPZ_SNIFF.Json.Xbox;
using ZOPZ_SNIFF.Menus;
using ZOPZ_SNIFF.Utils;

namespace ZOPZ_SNIFF.Forms
{
    public partial class MainMenu : Form
    {
        private SnifferHandler snifferHandler { get; set; }
        private NotificationForm? notification { get; set; }

        private UserInfo userInfo = Configuration.UserInfoConfig!;
        private Sniffer sniffer = Configuration.SnifferConfig!;
        private InfoStorage InfoStorage = Configuration.InfoStorage!;

        private static BindingList<UnfilteredEntry> _unfilteredEntries = new();
        private static BindingList<FilteredEntry> _filteredEntries = new();

        private ConcurrentDictionary<string, bool> _processingPackets = new();
        private static ConcurrentDictionary<string, List<Packet>> _packets = new();

        private static Image? _protectedImage { get; set; }
        private static Image? _unprotectedImage { get; set; }

        public MainMenu()
        {
            InitializeComponent();
            PopulateAdapters();
            XboxGamertagAI();
            snifferHandler = new SnifferHandler();
            snifferHandler.Captured += (e) =>
            {
                Invoke(() => ProcessPacket(e));
            };
            _protectedImage = FileHandler.GetImage("Protected.png");
            _unprotectedImage = FileHandler.GetImage("Unprotected.png");
            FilteredGamesDGV.DataSource = _filteredEntries;
            OtherInfoDGV.DataSource = _unfilteredEntries;

        }

        private void ShowNotification(string message)
        {
            if (notification == null || notification.IsDisposed)
            {
                notification = new NotificationForm()
                {
                    StartPosition = FormStartPosition.Manual,
                    Owner = this,
                    TopMost = true
                };
            }
            notification.SetMessage(message);
            UpdateNotificationPosition();
            notification.Show();
            timer1.Start();
        }

        private void UpdateNotificationPosition()
        {
            if (notification != null && !notification.IsDisposed)
            {
                int x = ClientSize.Width - notification.Width - 10;
                int y = ClientSize.Height - notification.Height - 10;
                notification.Location = new Point(Left + x, Top + y);
            }
        }

        private void PopulateAdapters()
        {
            List<string> deviceDescriptions = new();
            foreach (ILiveDevice device in CaptureDeviceList.Instance)
            {
                string description = device.Description ?? "Unknown device";
                NetworkInterface? networkInterface = FindNetworkInterface(description);
                if (networkInterface != null)
                {
                    string ipv4Address = GetIPAddress(networkInterface, AddressFamily.InterNetwork);
                    deviceDescriptions.Add(ipv4Address != "N/A" ? $"{networkInterface.Name} ({ipv4Address})" : $"{networkInterface.Name} - No IPv4 Address");
                }
                else
                    deviceDescriptions.Add($"{description} - No Network Interface Found");
            }
            PopulateComboBox(deviceDescriptions);
        }

        private void PopulateComboBox(IEnumerable<string> items)
        {
            foreach (string item in items)
                AdapterCB.Items.Add(item);
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Adapter))
            {
                AdapterCB.Text = userInfo.Adapter;
            }
        }

        private string GetAdapterCBText() => AdapterCB.Text;

        private NetworkInterface? FindNetworkInterface(string description)
        {
            return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(ni => ni.OperationalStatus == OperationalStatus.Up && (ni.Description.Contains(description, StringComparison.OrdinalIgnoreCase) || ni.Name.Contains(description, StringComparison.OrdinalIgnoreCase)));
        }

        private string GetIPAddress(NetworkInterface networkInterface, AddressFamily addressFamily)
        {
            UnicastIPAddressInformation? ipInfo = networkInterface.GetIPProperties().UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == addressFamily);
            return ipInfo != null && addressFamily == AddressFamily.InterNetwork ? $"{ipInfo.Address}/{ConvertSubnetMaskToCIDR(ipInfo.IPv4Mask)}" : "No IP Address Assigned";
        }


        private string ConvertSubnetMaskToCIDR(IPAddress subnetMask) => subnetMask.GetAddressBytes().Sum(b => Convert.ToString(b, 2).Count(bit => bit == '1')).ToString();



        private List<string> CheckPacketForFilterMatches(Packet packet, IpInfo? geolocation)
        {
            string dstIp = packet.Extract<IPPacket>().DestinationAddress.ToString();
            List<string> matchedFilters = new List<string>();
            ushort dstPort = snifferHandler.ExtractPort(packet).Item2;
            byte[] payload = snifferHandler.GetPacketDataPayload(packet);
            foreach (Filter filter in Program.api.Filters)
            {
                foreach (FilterOption option in filter.Options)
                {
                    if (IsMatch(option, dstPort, payload, packet.TotalPacketLength, geolocation) && !string.IsNullOrEmpty(filter.Name))
                    {
                        matchedFilters.Add(filter.Name);
                    }
                }
            }
            return matchedFilters;
        }

        private bool IsMatch(FilterOption option, ushort dstPort, byte[] payload, int totalPacketLength, IpInfo? geolocation)
        {
            if (option.IpRegex != null && !string.IsNullOrEmpty(option.IpRegex) && new Regex(option.IpRegex).IsMatch(geolocation!.Address))
            {
                return true;
            }
            if (geolocation != null && !string.IsNullOrEmpty(option.Isp) && option.Isp == geolocation.Provider)
            {
                return true;
            }
            if (snifferHandler.IsPayloadMatching(option.Payloads, payload))
            {
                return true;
            }
            return snifferHandler.IsPortInRange(option.Ports, dstPort) && snifferHandler.IsLengthValid(option, totalPacketLength);
        }


        private void AddPacketToHostDictionary(Packet packet)
        {
            string srcIp = packet.Extract<IPPacket>().DestinationAddress.ToString();
            if (_packets.TryGetValue(srcIp, out List<Packet>? packets))
                packets.Add(packet);
            else
                _packets[srcIp] = new List<Packet>() { packet };
        }

        private async void ProcessPacket(Packet packet)
        {
            string? dstIp = packet.Extract<IPPacket>().DestinationAddress.ToString();
            while (_processingPackets.ContainsKey(dstIp))
            {
                await Task.Delay(100);
            }
            (ushort sourcePort, ushort dstPort) = snifferHandler.ExtractPort(packet);
            IpInfo? geolocation = await GetOrCacheGeolocation(dstIp);
            if (string.IsNullOrEmpty(geolocation?.Address)) return;
            AddPacketToHostDictionary(packet);
            List<string> matchedFilters = CheckPacketForFilterMatches(packet, geolocation);
            IEnumerable<string> enabledFilters = sniffer?.EnabledFilters?.Where(x => matchedFilters.Any(y => x == y)) ?? Enumerable.Empty<string>();
            if (!enabledFilters.Any())
            {
                UnfilteredEntry? existingEntry = _unfilteredEntries.FirstOrDefault(x => x.IPAddress == dstIp);
                if (existingEntry != null)
                {
                    existingEntry.Packets++;
                    int index = _unfilteredEntries.IndexOf(existingEntry);
                    if (index >= 0)
                    {
                        _unfilteredEntries[index] = existingEntry;
                    }
                }
                else
                {
                    _unfilteredEntries.Add(new UnfilteredEntry()
                    {
                        IPAddress = dstIp,
                        City = string.IsNullOrWhiteSpace(geolocation?.City) ? "Unknown City" : geolocation?.City,
                        State = string.IsNullOrWhiteSpace(geolocation?.Region) ? "Unknown Region" : geolocation?.Region,
                        Country = string.IsNullOrWhiteSpace(geolocation?.Country) ? "Unknown Country" : geolocation?.Country,
                        Port = dstPort,
                        Packets = 1,
                        ISP = string.IsNullOrWhiteSpace(geolocation?.Provider) ? "Unknown ISP" : geolocation?.Provider,
                        Label = Program.api.LookupUsername(dstIp)
                    });
                }
            }
            else
            {
                FilteredEntry? existingEntry = _filteredEntries.FirstOrDefault(x => x.IPAddress == dstIp);
                if (existingEntry != null)
                {
                    existingEntry.Packets++;
                    int index = _filteredEntries.IndexOf(existingEntry);
                    if (index >= 0)
                    {
                        _filteredEntries[index] = existingEntry;
                    }
                }
                else
                {
                    _filteredEntries.Add(new FilteredEntry()
                    {
                        IPAddress = dstIp,
                        City = string.IsNullOrWhiteSpace(geolocation?.City) ? "Unknown City" : geolocation?.City,
                        State = string.IsNullOrWhiteSpace(geolocation?.Region) ? "Unknown Region" : geolocation?.Region,
                        Country = string.IsNullOrWhiteSpace(geolocation?.Country) ? "Unknown Country" : geolocation?.Country,
                        Port = dstPort,
                        Packets = 1,
                        ISP = string.IsNullOrWhiteSpace(geolocation?.Provider) ? "Unknown ISP" : geolocation?.Provider,
                        ProtectedFlag = ProtectedISPs.Contains(geolocation?.Address ?? "Unknown ISP") ? _protectedImage : _unprotectedImage,
                        GeoFlag = FileHandler.GetImage($"{geolocation?.IsoCode}.png"),
                        Filters = string.Join(", ", enabledFilters),
                        Label = Program.api.LookupUsername(dstIp)
                    });
                }
            }
        }

        private async Task<IpInfo?> GetOrCacheGeolocation(string dstIp)
        {
            if (InfoStorage.IpInfos.TryGetValue(dstIp, out IpInfo? cachedInfo))
            {
                return cachedInfo;
            }
            try
            {
                _processingPackets.TryAdd(dstIp, true);
                IpInfo? geolocation = await Info.GetAddressInfo(dstIp);
                if (geolocation != null && !string.IsNullOrEmpty(geolocation.Address))
                {
                    InfoStorage.IpInfos[dstIp] = geolocation;
                    Configuration.SaveConfiguration(InfoStorage);
                }
                return geolocation;
            }
            finally
            {
                _processingPackets.TryRemove(dstIp, out _);
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void XboxGamertagAI()
        {
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.XboxToken))
            {
                XboxAPI xbox = new XboxAPI(userInfo.XboxToken);
                guna2DataGridView4.Rows.Clear();
                Root? profile = await xbox.GetProfile();
                if (profile != null && profile.ProfileUsers != null)
                {
                    await xbox.GetSessionInfo(profile.ProfileUsers.First().Id);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (userInfo != null)
            {
                userInfo.Adapter = GetAdapterCBText();
                Configuration.SaveConfiguration(userInfo);
            }
            if (string.IsNullOrEmpty(GetAdapterCBText()))
            {
                ShowNotification("Adapter not set, please set your adapter.");
            }
            setdatagridviewstyle();
            if (snifferHandler.GetState())
            {
                snifferHandler.SetActive(false);
                label3.Text = "Status: Idle";
                guna2Button1.Text = "Start Sniffing";
                Program.api.customRpc?.UpdateRichPresence(string.Empty, "Status: Idle");
            }
            else
            {
                guna2Button1.Text = "Stop Sniffing";
                setdatagridviewstyle();
                string? selectedInterface = AdapterCB.SelectedItem?.ToString()?.Split(" (", StringSplitOptions.None)[0];
                if (string.IsNullOrEmpty(selectedInterface))
                {
                    MessageBox.Show("Please select a valid network adapter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CaptureDeviceList.Instance.OfType<LibPcapLiveDevice>().FirstOrDefault(dev => dev.Interface.FriendlyName.Contains(selectedInterface, StringComparison.OrdinalIgnoreCase)) is LibPcapLiveDevice newDevice)
                {
                    snifferHandler.Initialize(newDevice);
                    snifferHandler.SetActive(true);
                    Program.api.customRpc?.UpdateRichPresence(string.Empty, "Status: Sniffing");
                    label3.Text = "Status: Sniffing";
                }
                else
                    MessageBox.Show("No valid device selected. Please check your adapter settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setdatagridviewstyle()
        {
            FilteredGamesDGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            FilteredGamesDGV.AllowUserToAddRows = false;
            foreach (DataGridViewRow row in FilteredGamesDGV.Rows.OfType<DataGridViewRow>())
            {
                row.Height = 35;
            }
            FilteredGamesDGV.RowTemplate.Height = 35;
            FilteredGamesDGV.Invalidate();
            FilteredGamesDGV.Refresh();


            OtherInfoDGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            OtherInfoDGV.AllowUserToAddRows = false;
            foreach (DataGridViewRow row in OtherInfoDGV.Rows.OfType<DataGridViewRow>())
            {
                row.Height = 35;
            }
            OtherInfoDGV.RowTemplate.Height = 35;
            OtherInfoDGV.Invalidate();
            OtherInfoDGV.Refresh();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            AdapterCB.Items.Clear();
            PopulateAdapters();
            Program.api.LoadFilters();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            using (Geo menu = new Geo())
            {
                menu.StartPosition = FormStartPosition.Manual;
                menu.Location = new Point(Location.X + (Width - menu.Width) / 2, Location.Y + (Height - menu.Height) / 2);
                menu.ShowDialog(this);
                Enabled = true;
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (FilteredGamesDGV)
            {
                FilteredGamesDGV.Rows.Clear();
                ShowNotification("All Cleared");
            }
        }

        private void copyTooClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilteredGamesDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow? selectedRow = FilteredGamesDGV.SelectedRows[0];
                if (selectedRow.Cells.Count > 5)
                    Clipboard.SetText($"{selectedRow.Cells[4]?.Value?.ToString()} {selectedRow.Cells[5]?.Value?.ToString()}");
                else
                    ShowNotification("No row selected. Please select a row to copy.");
            }
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            XboxAPI? api = new XboxAPI(guna2TextBox1.Text);
            Root? profile = await api.GetProfile();
            if (userInfo != null)
            {
                userInfo.XboxToken = guna2TextBox1.Text;
                Configuration.SaveConfiguration(userInfo);
            }
            using (Xboxpartyoptions form = new Xboxpartyoptions(guna2TextBox1.Text.ToString()))
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(Location.X + (Width - form.Width) / 2, Location.Y + (Height - form.Height) / 2);
                form.ShowDialog(this);
                Enabled = true;
            }
        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Show();
            guna2TextBox2.Hide();
            guna2Button3.Hide();
            guna2Button4.Show();
            //psnhideshowect
            guna2TextBox3.Hide();
            guna2Button6.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Hide();
            guna2TextBox2.Show();

            guna2Button3.Show();
            guna2Button4.Hide();

            //psnhideshowect
            guna2TextBox3.Hide();
            guna2Button6.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox2.Text) && !string.IsNullOrEmpty(userInfo.Recroomtoken))
            {
                guna2TextBox2.Text = userInfo.Recroomtoken;
            }
            else if (!string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                userInfo.Recroomtoken = guna2TextBox2.Text;
                Configuration.SaveConfiguration(userInfo);
            }
            using (Rec form = new Rec(userInfo.Recroomtoken))
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(Location.X + (Width - form.Width) / 2, Location.Y + (Height - form.Height) / 2);
                form.ShowDialog(this);
                Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            guna2ContextMenuStrip1.RenderMode = ToolStripRenderMode.System;
            guna2ContextMenuStrip1.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.ForeColor = Color.White;
            guna2ContextMenuStrip1.Padding = new Padding(0);
            foreach (ToolStripItem item in guna2ContextMenuStrip1.Items.OfType<ToolStripItem>())
            {
                item.Padding = new Padding(0);
                item.BackColor = Color.FromArgb(25, 25, 25);
                item.ForeColor = Color.White;
                item.Margin = new Padding(0);
            }
            guna2ContextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            guna2ContextMenuStrip2.RenderMode = ToolStripRenderMode.System;
            guna2ContextMenuStrip2.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip2.ForeColor = Color.White;
            guna2ContextMenuStrip2.Padding = new Padding(0);
            foreach (ToolStripItem item in guna2ContextMenuStrip2.Items.OfType<ToolStripItem>())
            {
                item.Padding = new Padding(0);
                item.BackColor = Color.FromArgb(25, 25, 25);
                item.ForeColor = Color.White;
                item.Margin = new Padding(0);
            }
            guna2ContextMenuStrip2.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            DataGridViewColumnCollection? view = FilteredGamesDGV.Columns;
            if (view["GeoFlag"] is DataGridViewColumn geoflag)
            {
                geoflag.HeaderText = "";
                geoflag.Width = 20;
                geoflag.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["ProtectedFlag"] is DataGridViewColumn protectedFlag)
            {
                protectedFlag.HeaderText = "";
                protectedFlag.Width = 20;
                protectedFlag.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["Label"] is DataGridViewColumn label)
            {
                label.HeaderText = "Label";
                label.Width = 90;
                label.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["Filters"] is DataGridViewColumn filters)
            {
                filters.HeaderText = "Filter Name";
                filters.Width = 90;
                filters.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            if (view["IPAddress"] is DataGridViewColumn ipAddress)
            {
                ipAddress.HeaderText = "IP Address";
                ipAddress.Width = 90;
                ipAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["Port"] is DataGridViewColumn port)
            {
                port.HeaderText = "Port";
                port.Width = 50;
                port.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["Country"] is DataGridViewColumn country)
            {
                country.HeaderText = "Country";
                country.Width = 90;
                country.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["State"] is DataGridViewColumn state)
            {
                state.HeaderText = "Region";
                state.Width = 80;
                state.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["City"] is DataGridViewColumn city)
            {
                city.HeaderText = "City";
                city.Width = 80;
                city.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["ISP"] is DataGridViewColumn isp)
            {
                isp.HeaderText = "ISP";
                isp.Width = 200;
                isp.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (view["Packets"] is DataGridViewColumn packets)
            {
                packets.HeaderText = "Packets";
                packets.Width = 80;
                packets.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["Label"] is DataGridViewColumn otherLabel)
            {
                otherLabel.HeaderText = "Label";
                otherLabel.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["IPAddress"] is DataGridViewColumn otherIpAddress)
            {
                otherIpAddress.HeaderText = "IP Address";
                otherIpAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["Port"] is DataGridViewColumn otherPort)
            {
                otherPort.HeaderText = "Port";
                otherPort.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["Country"] is DataGridViewColumn otherCountry)
            {
                otherCountry.HeaderText = "Country";
                otherCountry.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["State"] is DataGridViewColumn otherState)
            {
                otherState.HeaderText = "Region";
                otherState.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["City"] is DataGridViewColumn otherCity)
            {
                otherCity.HeaderText = "City";
                otherCity.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["ISP"] is DataGridViewColumn otherIsp)
            {
                otherIsp.HeaderText = "ISP";
                otherIsp.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (OtherInfoDGV.Columns["Packets"] is DataGridViewColumn otherPackets)
            {
                otherPackets.HeaderText = "Packets";
                otherPackets.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (userInfo != null)
            {
                if (!string.IsNullOrEmpty(userInfo.XboxToken))
                    guna2TextBox1.Text = userInfo.XboxToken;
                if (!string.IsNullOrEmpty(userInfo.Recroomtoken))
                    guna2TextBox2.Text = userInfo.Recroomtoken;
                if (!string.IsNullOrEmpty(userInfo.PSNToken))
                    guna2TextBox3.Text = userInfo.PSNToken;
                Program.api.customRpc?.SetRPC(userInfo.ShowDiscordRPC);
                if (userInfo.ShowDiscordRPC)
                    Program.api.customRpc?.UpdateRichPresence(string.Empty, "Status: Idle");
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            using (SettingsMenu menu = new SettingsMenu())
            {
                menu.StartPosition = FormStartPosition.Manual;
                menu.Location = new Point(Location.X + (Width - menu.Width) / 2, Location.Y + (Height - menu.Height) / 2);
                menu.ShowDialog(this);
                Enabled = true;
            }
        }

        private void copyEntireRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilteredGamesDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow? selectedRow = FilteredGamesDGV.SelectedRows[0];
                List<string> rowValues = new List<string>();
                foreach (DataGridViewCell cell in selectedRow.Cells.OfType<DataGridViewCell>())
                {
                    object? value = cell.Value;
                    string? item = value != null ? value.ToString() : string.Empty;
                    rowValues.Add(item ?? "N/A");
                }
                Clipboard.SetText(string.Join("\t", rowValues));
            }
            else
                ShowNotification("No row selected. Please select a row to copy.");
        }

        private void clearSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilteredGamesDGV.SelectedCells.Count > 0)
            {
                DataGridViewRow? selectedRow = FilteredGamesDGV.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                {
                    FilteredGamesDGV.Rows.Remove(selectedRow);
                    FilteredGamesDGV.Refresh();
                }
            }
            else
                ShowNotification("Please select a valid row from the DataGridView.");
        }

        private void pingCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilteredGamesDGV.SelectedCells.Count > 0)
            {
                DataGridViewRow? selectedRow = FilteredGamesDGV.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                {
                    object? value = selectedRow.Cells[4].Value;
                    string? ipAddress = value != null ? value.ToString() : null;
                    if (!string.IsNullOrWhiteSpace(ipAddress))
                    {
                        Process.Start("CMD.exe", "/K mode con lines=25 cols=60 & ping " + ipAddress + " -t");
                    }
                    else
                        ShowNotification("Please select a valid IP address from the DataGridView.");
                }
            }
            else
                ShowNotification("Please select a valid row from the DataGridView.");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (notification != null)
                notification.Hide();
            timer1.Stop();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            UpdateNotificationPosition();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            UpdateNotificationPosition();
        }

        private string? GetSelectedIP()
        {
            string? ipAddress = string.Empty;
            if (FilteredGamesDGV.SelectedCells.Count > 0)
            {

                DataGridViewRow? selectedRow = FilteredGamesDGV.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                {
                    object? value = selectedRow.Cells[4].Value;
                    ipAddress = value != null ? value.ToString() : string.Empty;
                }
            }
            return ipAddress;
        }

        private string? GetSelectedIP2()
        {
            string? ipAddress = string.Empty;
            if (OtherInfoDGV.SelectedCells.Count > 0)
            {
                DataGridViewRow? selectedRow = OtherInfoDGV.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                {
                    object? value = selectedRow.Cells[1].Value;
                    ipAddress = value != null ? value.ToString() : string.Empty;
                }
            }
            return ipAddress;
        }

        private void packetAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? ip = GetSelectedIP() != null ? GetSelectedIP() : GetSelectedIP2();
            if (!string.IsNullOrEmpty(ip))
            {
                using (PacketAnalyzer menu = new PacketAnalyzer(_packets[ip]))
                {
                    menu.StartPosition = FormStartPosition.Manual;
                    menu.Location = new Point(Location.X + (Width - menu.Width) / 2, Location.Y + (Height - menu.Height) / 2);
                    menu.ShowDialog(this);
                    Enabled = true;
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (OtherInfoDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow? selectedRow = OtherInfoDGV.SelectedRows[0];
                if (selectedRow != null && selectedRow.Cells.Count > 3)
                    Clipboard.SetText($"{selectedRow.Cells[1]?.Value?.ToString()} {selectedRow.Cells[2]?.Value?.ToString()}");
                else
                    ShowNotification("No row selected. Please select a row to copy.");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (OtherInfoDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = OtherInfoDGV.SelectedRows[0];
                List<string> rowValues = new List<string>();
                foreach (DataGridViewCell cell in selectedRow.Cells.OfType<DataGridViewCell>())
                {
                    List<string> list = rowValues;
                    object? value = cell.Value;
                    list.Add((value != null ? value.ToString() : string.Empty) ?? "N/A");
                }
                string clipboardText = string.Join("\t", rowValues);
                Clipboard.SetText(clipboardText);
            }
            else
                ShowNotification("No row selected. Please select a row to copy.");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            lock (OtherInfoDGV)
            {
                OtherInfoDGV.Rows.Clear();
                ShowNotification("All Cleared");
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (OtherInfoDGV.SelectedCells.Count > 0)
            {
                DataGridViewRow? selectedRow = OtherInfoDGV.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                    OtherInfoDGV.Rows.Remove(selectedRow);
            }
            else
                ShowNotification("Please select a valid row from the DataGridView.");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (OtherInfoDGV.SelectedCells.Count > 0)
            {
                DataGridViewRow? selectedRow = OtherInfoDGV.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                {
                    object? value = selectedRow.Cells[1].Value;
                    string? ipAddress = value != null ? value.ToString() : string.Empty;
                    if (!string.IsNullOrWhiteSpace(ipAddress))
                        Process.Start("CMD.exe", $"/K mode con lines=25 cols=60 & ping {ipAddress} -t");
                    else
                        ShowNotification("Please select a valid IP address from the DataGridView.");
                }
            }
            else
                ShowNotification("Please select a valid row from the DataGridView.");
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string? ip = GetSelectedIP2();
            if (!string.IsNullOrEmpty(ip))
            {
                using (PacketAnalyzer menu = new PacketAnalyzer(_packets[ip]))
                {
                    menu.StartPosition = FormStartPosition.Manual;
                    menu.Location = new Point(Location.X + (Width - menu.Width) / 2, Location.Y + (Height - menu.Height) / 2);
                    menu.ShowDialog(this);
                    Enabled = true;
                }
            }
        }

        private static readonly List<string> ProtectedISPs = new()
        {
            "DigitalOcean, LLC", "Telegram Messenger Network", "Linode, LLC", "Vultr Holdings Corporation",
            "Google LLC", "Microsoft Corporation", "Cloudflare, Inc.", "Akamai Technologies, Inc.", "OVH SAS",
            "Alibaba (US) Technology Co., Ltd.", "LeaseWeb Netherlands B.V.", "Roblox", "Activision Blizzard, Inc.",
            "Electronic Arts Inc.", "Ubisoft Entertainment", "Rockstar Games, Inc.", "Valve Corporation", "Epic Games, Inc.",
            "Nintendo Co., Ltd.", "The Constant Company, LLC", "The Constant Company", "Sony Interactive Entertainment LLC",
            "discord-uslax1", "i3D.net B.V", "Cloudflare London, LLC"
        };

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox3.Text) && !string.IsNullOrEmpty(userInfo.PSNToken))
            {
                guna2TextBox3.Text = userInfo.PSNToken;
            }
            else if (!string.IsNullOrEmpty(guna2TextBox3.Text))
            {
                userInfo.PSNToken = guna2TextBox3.Text;
                Configuration.SaveConfiguration(userInfo);
            }
            using (PlayStationTool form = new PlayStationTool(userInfo.PSNToken))
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(Location.X + (Width - form.Width) / 2, Location.Y + (Height - form.Height) / 2);
                form.ShowDialog(this);
                Enabled = true;
            }
            guna2TextBox1.Hide();
            guna2TextBox2.Show();
            guna2Button3.Show();
            guna2Button4.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Hide();
            guna2TextBox2.Show();
            guna2TextBox3.Show();
            guna2Button3.Show();
            guna2Button4.Hide();

            //psnhideshowect
            guna2TextBox3.Show();
            guna2Button6.Show();

            guna2Button3.Hide();
            guna2TextBox2.Hide  ();

        }
    }
}
