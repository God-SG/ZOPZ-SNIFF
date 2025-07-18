using System.Data;
using System.Text.RegularExpressions;
using ZOPZ_SNIFF.Auth;
using ZOPZ_SNIFF.Config;
using ZOPZ_SNIFF.Config.Configs;
using ZOPZ_SNIFF.Json.Auth;
using ZOPZ_SNIFF.Json.Sniffer;
using ZOPZ_SNIFF.UserControls;

namespace ZOPZ_SNIFF.Menus
{
    public partial class SettingsMenu : Form
    {
        private UserInfo userInfo = Configuration.UserInfoConfig!;
        private DateTime _lastChangeTime = DateTime.MinValue;
        private ChatClient chatClient { get; set; }
        public SettingsMenu()
        {
            InitializeComponent();
            Program.api?.LoadLableDatas();
            chatClient = new ChatClient(Program.api?.data?.message!);
            /* chatClient.OnConnected += () => //This isnt being called by Socket.io idk why
             {
                 Invoke(() => ChatClient_OnConnected);
             };*/
            chatClient.OnDisconnected += () =>
            {
                Invoke(() => ChatClient_OnDisconnected);
            };
            chatClient.OnMessageReceived += (m) =>
            {
                Invoke(() => ChatClient_OnMessageReceived(m));
            };
            chatClient.OnMessagesReceived += (ms) =>
            {
                Invoke(() => ChatClient_OnMessagesReceived(ms));
            };
        }


        private void ChatClient_OnDisconnected()
        {
            MessageAlert.Show("Alert", "Disconnected");
        }

        private void ChatClient_OnMessagesReceived(IEnumerable<ChatMessage> obj)
        {
            foreach (ChatMessage msg in obj.Reverse())
            {
                AddMessage(msg);
            }
        }

        private void ChatClient_OnMessageReceived(ChatMessage msg)
        {
            AddMessage(msg);
        }

        private void AddMessage(ChatMessage msg)
        {
            chatPanel.Controls.Add(new UserSentmessage(msg.Poster, msg.Message));
        }

        private async void SettingsMenu_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            label12.Text = "Username: " + userInfo.Username;
            guna2CheckBox2.Checked = userInfo.ShowDiscordRPC;
            guna2CheckBox1.Checked = userInfo.AutoLogin;
            label15.Text = $"Last Login: {DateTime.Now:MMM/dd/20yy}";
            label13.Text = $"App Version: 5.0.0.0";
            label9.Text = $"Total Clients: {Program.api.statistics?.TotalUserCount}";
            label11.Text = $"Total With Plan: {Program.api.statistics?.ProgramStatistics?.Zopzsniff?.UserCount}";
            LoadGameFilters();
            Loadlogs();
            await chatClient.Connect();
            await chatClient.JoinRoom("zopzsniff");
        }

        private void Loadlogs()
        {
            if (Program.api.Labels != null)
            {
                DataTable dataTable = CreateDataTableFromLabels(Program.api.Labels);
                PopulateDataGridView(dataTable);
                guna2TextBox1_TextChanged(null, null);
            }
        }

        private void PopulateDataGridView(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Label";
            dataGridView1.Columns[2].HeaderText = "IP Address";
        }

        private DataTable CreateDataTableFromLabels(List<LabelData> labels)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Label");
            dataTable.Columns.Add("IP Address");
            for (int i = 0; i < labels.Count; i++)
            {
                dataTable.Rows.Add([i + 1, labels[i].Name, labels[i].Value]);
            }
            return dataTable;
        }

        private void LoadGameFilters()
        {
            IOrderedEnumerable<Filter>? filters = Program.api?.Filters?.OrderByDescending(x => x.Name);
            if (filters != null)
            {
                foreach (Filter? filter in filters)
                {
                    FIlterDisplay filterDisplay = new FIlterDisplay(filter.Name, filter.Type, filter.Platform);
                    FilterDisplayPanel.Controls.Add(filterDisplay);
                }
            }
        }

        private async void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            try
            {
                await chatClient.Disconnect();
            }
            finally
            {
                Hide();
            }
        }

        private static bool ValidateIP(string value)
        {
            value = value.Trim();
            return new Regex("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$").IsMatch(value);
        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                string ipAddress = host.Text;
                if (ValidateIP(ipAddress))
                {
                    LabelData label = new LabelData()
                    {
                        Name = guna2TextBox1.Text,
                        Value = ipAddress
                    };
                    Program.api.CreateLableData(label);
                    Program.api.Labels?.Add(label);
                    guna2TextBox1_TextChanged(null, null);
                }
                else
                    MessageAlert.Show("Alert", "Invalid IP address.");
            }
        }

        private void FilterRows(string filterText)
        {
            List<LabelData> filteredEntries = new List<LabelData>();
            if (Program.api.Labels is not null)
            {
                foreach (LabelData label in Program.api.Labels)
                {
                    if (label is null || label.Name is null) continue;
                    if (label.Name.ToLower().Contains(filterText))
                    {
                        filteredEntries.Add(label);
                    }
                }
            }
            DataTable filteredDataTable = CreateDataTableFromLabels(filteredEntries);
            PopulateDataGridView(filteredDataTable);
        }

        private async void guna2TextBox1_TextChanged(object? sender, EventArgs? e)
        {
            _lastChangeTime = DateTime.Now;
            await Task.Delay(200);
            if (!(_lastChangeTime.AddMilliseconds(150.0) > DateTime.Now))
            {
                FilterRows(guna2TextBox1.Text.ToLower());
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (userInfo != null)
            {
                userInfo.AutoLogin = guna2CheckBox1.Checked;
                Configuration.SaveConfiguration(userInfo);
            }
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (userInfo != null)
            {
                userInfo.ShowDiscordRPC = guna2CheckBox2.Checked;
                Configuration.SaveConfiguration(userInfo);
            }
        }

        private void guna2TabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            LoadGameFilters();
        }

        private void Messageboxtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (Messageboxtb.Text.Length > 119)
                    MessageAlert.Show("Alert", "Your message exceeds the 119 character limit. Please shorten it");
                else
                {
                    chatClient.SendMessage(Messageboxtb.Text);
                    Messageboxtb.Text = string.Empty;
                }
            }
        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void copyTooClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                if (selectedRow.Cells.Count >= 3)
                {
                    string cell1 = selectedRow.Cells[1]?.Value?.ToString() ?? "";
                    string cell2 = selectedRow.Cells[2]?.Value?.ToString() ?? "";
                    Clipboard.SetText($"{cell1} {cell2}");
                }
                else
                {
                    MessageAlert.Show("Alert", "Selected row does not contain enough cells to copy.");
                }
            }
            else
            {
                MessageAlert.Show("Alert", "No row selected. Please select a row to copy.");
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                if (selectedRow.Cells.Count >= 3)
                {
                    string cell1 = selectedRow.Cells[1]?.Value?.ToString() ?? "";
                    string cell2 = selectedRow.Cells[2]?.Value?.ToString() ?? "";
                    Clipboard.SetText($"{cell1} {cell2}");
                }
                else
                {
                    MessageAlert.Show("Alert", "Selected row does not contain enough cells to copy.");
                }
            }
            else
            {
                MessageAlert.Show("Alert", "No row selected. Please select a row to copy.");
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            MessageAlert.Show("Alert", "Rows Have Been Cleared");
        }

        private void clearSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                DataGridViewRow? selectedRow = dataGridView1.SelectedCells[0].OwningRow;
                if (selectedRow != null)
                {
                    dataGridView1.Rows.Remove(selectedRow);
                    dataGridView1.Refresh();
                }
            }
            else
                MessageAlert.Show("Alert", "Please select a valid row from the DataGridView.");
        }
    }
}
