using Microsoft.Win32;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Text;
using ZOPZ_SNIFF.Api;
using ZOPZ_SNIFF.Json.Rec;
using ZOPZ_SNIFF.Utils;

namespace ZOPZ_SNIFF.Menus
{
    public partial class Rec : Form
    {
        private const string RegistryPath = @"Software\Against Gravity\Rec Room";
        private const string ValuePrefix = "RECENT_PLAYERS_PREF_";
        private const string ApiUrl = "https://zopzsniff.xyz/account/bulk?";
        private NotificationForm? notification;
        public string Token = string.Empty;
        private readonly HttpClient _client = new HttpClient();
        private DateTime _lastChangeTime = DateTime.MinValue;
        public List<AccountInfo>? friends { get; set; } = new List<AccountInfo>();

        public Rec(string token)
        {
            InitializeComponent();
            Token = token;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        private void BindUserProfilesToGrid(List<UserProfile> profiles)
        {
            dataGridView1.DataSource = profiles;

            // Rename headers (make sure columns exist)
            if (dataGridView1.Columns["UserId"] != null)
                dataGridView1.Columns["UserId"].HeaderText = "User ID";

            if (dataGridView1.Columns["DisplayName"] != null)
                dataGridView1.Columns["DisplayName"].HeaderText = "Display Name";

            if (dataGridView1.Columns["Username"] != null)
                dataGridView1.Columns["Username"].HeaderText = "Username";

            if (dataGridView1.Columns["ProfileImageUrl"] != null)
                dataGridView1.Columns["ProfileImageUrl"].HeaderText = "";
        }
        private List<long> GetRecentPlayerIds()
        {
            var ids = new HashSet<long>();
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    if (key == null) return ids.ToList();

                    foreach (var valueName in key.GetValueNames())
                    {
                        if (!valueName.StartsWith(ValuePrefix)) continue;

                        var rawData = key.GetValue(valueName) as byte[];
                        if (rawData == null) continue;

                        try
                        {
                            var decoded = Encoding.UTF8.GetString(rawData).Trim('\0', ' ');
                            if (decoded.StartsWith("[") && decoded.EndsWith("]"))
                            {
                                var list = JsonConvert.DeserializeObject<List<long>>(decoded);
                                foreach (var id in list)
                                    ids.Add(id);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to parse: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registry read error: {ex.Message}");
            }

            return ids.ToList();
        }
        public class RecNetUser
        {
            [JsonProperty("accountId")]
            public long accountId { get; set; }
            [JsonProperty("displayName")]
            public string displayName { get; set; }
            [JsonProperty("username")]

            public string username { get; set; }
            [JsonProperty("profileImage")]
            public string profileImage { get; set; }
        }

        public class UserProfile
        {
            public Image ProfileImageUrl { get; set; }
            public long UserId { get; set; }
            [JsonProperty("username")]
            public string DisplayName { get; set; }
            public string Username { get; set; }
        }
        private async Task<List<UserProfile>> FetchProfilesAsync(List<long> ids)
        {
            var results = new List<UserProfile>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = ApiUrl + "id=" + string.Join("&id=", ids);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("RecRoomRegistryViewer/1.0");

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var users = JsonConvert.DeserializeObject<List<RecNetUser>>(json);

                        foreach (var user in users)
                        {
                            var imageUrl = $"https://img.rec.net/{user.profileImage}?width=50&cropSquare=true";
                            Image profileImage = null;

                            try
                            {
                                var imageStream = await client.GetStreamAsync(imageUrl);
                                profileImage = Image.FromStream(imageStream);
                            }
                            catch
                            {
                                // Handle failed image download if necessary (e.g., assign null or default image)
                                profileImage = null;
                            }

                            results.Add(new UserProfile
                            {
                                UserId = user.accountId,
                                DisplayName = user.displayName,
                                Username = user.username,
                                ProfileImageUrl = profileImage
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show($"API error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"HTTP error: {ex.Message}");
                }
            }

            return results;
        }

        private void ShowNotification(string message)
        {
            if (notification == null || notification.IsDisposed)
            {
                notification = new NotificationForm();
                notification.SetMessage(message);
                notification.StartPosition = FormStartPosition.Manual;
                notification.Owner = this;
                notification.TopMost = true;
            }
            else
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

        private async void rec_Load(object sender, EventArgs e)
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
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e) => Hide();

        private void FormatDataGridViewColumns()
        {
            if (dataGridView1 != null)
            {
                if (dataGridView1.Columns["Username"] is DataGridViewColumn UserName)
                {
                    UserName.HeaderText = "Username";
                    UserName.Width = 60;
                    UserName.ReadOnly = true;
                }
                if (dataGridView1.Columns["DisplayName"] is DataGridViewColumn DisplayName)
                {
                    DisplayName.HeaderText = "Display Name";
                    DisplayName.Width = 50;
                    DisplayName.ReadOnly = true;
                }
                if (dataGridView1.Columns["AccountId"] is DataGridViewColumn AccountId)
                {
                    AccountId.HeaderText = "Account ID";
                    AccountId.Width = 50;
                }
                if (dataGridView1.Columns["IsJunior"] is DataGridViewColumn IsJunior)
                {
                    IsJunior.HeaderText = "Is Junior";
                    IsJunior.Width = 40;
                }
                if (dataGridView1.Columns["Platforms"] is DataGridViewColumn Platforms)
                {
                    Platforms.HeaderText = "Platforms";
                    Platforms.Width = 40;
                }
                if (dataGridView1.Columns["CreatedAt"] is DataGridViewColumn CreatedAt)
                {
                    CreatedAt.HeaderText = "Created At";
                    CreatedAt.Width = 180;
                }
            }
        }

        private void PopulateDataGridView(List<AccountInfo> jsonArray)
        {
            if (jsonArray.Any())
            {
                dataGridView1.DataSource = jsonArray.Select(accountEntry => new
                {
                    accountEntry.username,
                    accountEntry.displayName,
                    accountEntry.accountId,
                    accountEntry.isJunior,
                    accountEntry.platforms,
                    accountEntry.createdAt
                }).ToList();
                FormatDataGridViewColumns();
            }
        }

        private void FilterRows(string filterText)
        {
            if (friends == null) return;
            List<AccountInfo> filteredEntries = new(friends.Where(i => i.username.Contains(filterText) || i.displayName.Contains(filterText)));
            Invoke(() => PopulateDataGridView(filteredEntries));
        }

        private async void guna2TextBox1_TextChanged(object? sender, EventArgs? e)
        {
            _lastChangeTime = DateTime.Now;
            await Task.Delay(200);
            if (!(_lastChangeTime.AddMilliseconds(100.0) > DateTime.Now))
            {
                FilterRows(guna2TextBox1.Text);
            }
        }

        private async void clearSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Relationships>? friendEntries = await RecRoom.GetAccountData(_client);
            if (friendEntries != null)
            {
                long[]? ids = friendEntries.Where(x => x.RelationshipType != 0L).Select(i => i.OtherPlayerID)?.ToArray();
                if (ids != null)
                {
                    friends = await RecRoom.GetBulkAccountInfo(_client, ids);
                    guna2TextBox1_TextChanged(null, null);
                }
            }
        }


        private async void massReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModerationBlockDetails? info = await RecRoom.GetModeratioDetails(_client);
            if (info != null && info.IsBan)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine($"Username: {info.AssociatedAccountUsername}");
                builder.AppendLine($"Banned: {info.IsBan}");
                builder.AppendLine($"Ban Time: {info.Duration}");
                builder.AppendLine($"Device Ban: {info.IsDeviceBan}");
                builder.AppendLine($"Host Kicked: {info.IsHostKick}"); // IDK what this even is zopz ngl PC
                builder.AppendLine($"Voice AutoBanned: {info.IsVoiceModAutoban}");
                builder.AppendLine($"Warned: {info.IsWarning}");
                builder.AppendLine($"Message: {info.Message}");
                new FormHandler(this, Location, Width, Height).ShowMessage("Moderation Block Details", builder.ToString());
            }
            else
                new FormHandler(this, Location, Width, Height).ShowMessage("Moderation Block Details", "User is not banned");
        }

        private async void getYoureAccountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyInfo? info = await RecRoom.FetchAndDisplayUserProfile(_client);
            if (info != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine($"Account ID: {info.accountId}");
                builder.AppendLine($"Username: {info.username}");
                builder.AppendLine($"Display Name: {info.displayName}");
                builder.AppendLine($"Birthday: {info.birthday}");
                builder.AppendLine($"Email: {info.email}");
                builder.AppendLine($"Platforms: {info.platforms}");
                builder.AppendLine($"Profile Image: {info.profileImage}");
                builder.AppendLine($"Is Junior: {info.isJunior}");
                builder.AppendLine($"Personal Pronouns: {info.personalPronouns}");
                builder.AppendLine($"Identity Flags: {info.identityFlags}");
                builder.AppendLine($"Created At: {info.createdAt}");
                builder.AppendLine($"Is Meta Platform Blocked: {info.isMetaPlatformBlocked}");
                builder.AppendLine($"Available Username Changes: {info.availableUsernameChanges}");
                builder.AppendLine($"Is Fake Junior Birthday: {info.isFakeJuniorBirthday}");
                builder.AppendLine($"Phone Number: {info.phone}");
                new FormHandler(this, Location, Width, Height).ShowMessage("Account Information", builder.ToString());
            }
        }

        private async void childrenAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ChildAccount>? accounts = await RecRoom.GetChildrenAccountInfo(_client);
            StringBuilder builder = new StringBuilder();
            if (accounts?.Count > 0)
            {
                foreach (ChildAccount account in accounts)
                {
                    builder.AppendLine($"ID: {account.accountId}");
                    builder.AppendLine($"Username: {account.username}");
                    builder.AppendLine($"DisplayName: {account.displayName}");
                    builder.AppendLine($"Image: {account.profileImage}");
                    builder.AppendLine($"Junior: {account.isJunior}");
                    builder.AppendLine($"Platforms: {account.platforms}");
                    builder.AppendLine($"Personal Pronouns: {account.personalPronouns}");
                    builder.AppendLine($"Identity Flags: {account.identityFlags}");
                    builder.AppendLine($"Created At: {account.createdAt}");
                    builder.AppendLine($"Is Meta Platform Blocked: {account.isMetaPlatformBlocked}");
                    builder.AppendLine("===============================================");
                }
                new FormHandler(this, Location, Width, Height).ShowMessage("Children Account Information", builder.ToString());
            }
            else
                new FormHandler(this, Location, Width, Height).ShowMessage("Children Account Information", "No children account information found.");
        }

        private async void massReportProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? idInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(idInput, out ulong id))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.ReportUser(token, id, 104, "this person has a Inappropriate name, profile picture, or bio"));
                }
                await Task.WhenAll(reportTasks);
                MessageBox.Show("All Reports sent out");
            }
            else
                MessageBox.Show("Invalid ID entered. Please enter a valid number.");
        }

        private async void massReportDisruptiveTrollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? idInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(idInput, out ulong id))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.ReportUser(token, id, 103, "this person has been Disruptively trolling"));
                }
                await Task.WhenAll(reportTasks);
                MessageBox.Show("All Reports sent out");
            }
            else
                MessageBox.Show("Invalid ID entered. Please enter a valid number.");
        }

        private async void massReportSexualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? idInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(idInput, out ulong id))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.ReportUser(token, id, 102, "This person is seuxally harassing people over voice chat"));
                }
                await Task.WhenAll(reportTasks);
                MessageBox.Show("All Reports sent out");
            }
            else
                MessageBox.Show("Invalid ID entered. Please enter a valid number.");
        }

        private async void massReportTrollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? idInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(idInput, out ulong id))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.ReportUser(token, id, 101, "Hes mass trolling on alt accounts"));
                }
                await Task.WhenAll(reportTasks);
                MessageBox.Show("All Reports sent out");
            }
            else
                MessageBox.Show("Invalid ID entered. Please enter a valid number.");
        }

        private async void massReportUnder13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? idInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(idInput, out ulong id))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.ReportUser(token, id, 100, "This player is under 13, this person admitted too it over voice chat"));
                }
                await Task.WhenAll(reportTasks);
                MessageBox.Show("All Reports sent out");
            }
            else
                MessageBox.Show("Invalid ID entered. Please enter a valid number.");
        }

        private async void massReportBanEvasionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? idInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(idInput, out ulong id))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.ReportUser(token, id, -1, "this person is Ban evading on this account"));
                }
                await Task.WhenAll(reportTasks);
                MessageBox.Show("All Reports sent out");
            }
            else
                MessageBox.Show("Invalid ID entered. Please enter a valid number.");
        }

        private async void massReportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tokens = await RecRoom.GetTokens();
            List<Task> reportTasks = new List<Task>();
            string? id = new FormHandler(this, Location, Width, Height).ShowTextPromp("Image ID");
            Enabled = true;
            foreach (string token in tokens)
            {
                reportTasks.Add(RecRoom.ReportUserImage(token, id, " "));
            }
            await Task.WhenAll(reportTasks);
            MessageBox.Show("All Reports sent out");
        }

        private async void lookupUsernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? usernameInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Username");
            if (!string.IsNullOrEmpty(usernameInput))
            {
                SearchInfo? info = await RecRoom.GetAccountInfoFromUsername(_client, usernameInput);
                if (info != null)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Account ID: {info.accountId}");
                    builder.AppendLine($"Username: {info.username}");
                    builder.AppendLine($"Display Name: {info.displayName}");
                    builder.AppendLine($"Profile Image: {info.profileImage}");
                    builder.AppendLine($"Is Junior: {info.isJunior}");
                    builder.AppendLine($"Personal Pronouns: {info.personalPronouns}");
                    builder.AppendLine($"Identity Flags: {info.identityFlags}");
                    builder.AppendLine($"Created At: {info.createdAt}");
                    builder.AppendLine($"Is Meta Platform Blocked: {info.isMetaPlatformBlocked}");
                    new FormHandler(this, Location, Width, Height).ShowMessage("Account Information", builder.ToString());
                }
            }
        }

        private async void lookupUseridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || dataGridView1.Columns.Count == 0)
            {
                ShowNotification("DataGridView is empty.");
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                ShowNotification("No row is selected.");
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow.Cells.Count <= 2)
            {
                ShowNotification("Selected row does not have the required cell.");
                return;
            }
            string? username = selectedRow.Cells[2].Value?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                ShowNotification("Username input is invalid.");
                return;
            }
            List<AccountInfo>? accountinfo = await RecRoom.GetBulkAccountInfo(_client, new long[] { long.Parse(username) });
            if (accountinfo == null) return;
            if (accountinfo.Count != 0)
            {
                AccountInfo info = accountinfo[0];
                StringBuilder builder = new StringBuilder();
                builder.AppendLine($"Account ID: {info.accountId}");
                builder.AppendLine($"Username: {info.username}");
                builder.AppendLine($"Display Name: {info.displayName}");
                builder.AppendLine($"Profile Image: {info.profileImage}");
                builder.AppendLine($"Is Junior: {info.isJunior}");
                builder.AppendLine($"Personal Pronouns: {info.personalPronouns}");
                builder.AppendLine($"Identity Flags: {info.identityFlags}");
                builder.AppendLine($"Created At: {info.createdAt}");
                builder.AppendLine($"Is Meta Platform Blocked: {info.isMetaPlatformBlocked}");
                new FormHandler(this, Location, Width, Height).ShowMessage("Account Information", builder.ToString());
            }
        }

        private async void getRoomDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || dataGridView1.Columns.Count == 0) return;
            string? usernameInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Username");
            if (!string.IsNullOrEmpty(usernameInput))
            {
                int? accountId = await RecRoom.GetAccountId(_client, usernameInput);
                if (accountId != 0)
                {
                    PlayerInstance? instance = await RecRoom.GetPlayerInstanceData(_client, accountId);
                    if (instance != null && !string.IsNullOrEmpty(instance.roomInstance?.name))
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine($"Room Instance ID: {instance.roomInstance?.roomInstanceId}");
                        builder.AppendLine($"Room ID: {instance.roomInstance?.roomId}");
                        builder.AppendLine($"SubRoom ID: {instance.roomInstance?.subRoomId}");
                        builder.AppendLine($"Location: {instance.roomInstance?.location}");
                        builder.AppendLine($"Room Name: {instance.roomInstance?.name}");
                        builder.AppendLine($"Room Max Capacity: {instance.roomInstance?.maxCapacity}");
                        builder.AppendLine($"Room Is Full: {instance.roomInstance?.isFull}");
                        builder.AppendLine($"Room Is Private: {instance.roomInstance?.isPrivate}");
                        builder.AppendLine($"Room Is In Progress: {instance.roomInstance?.isInProgress}");
                        builder.AppendLine($"Matchmaking Policy: {instance.roomInstance?.matchmakingPolicy}");
                        builder.AppendLine($"Voice Server ID: {instance.roomInstance?.voiceServerId}");
                        builder.AppendLine($"Voice Auth ID: {instance.roomInstance?.voiceAuthId}");
                        new FormHandler(this, Location, Width, Height).ShowMessage("Player Room Data", builder.ToString());
                    }
                    else
                        MessageBox.Show("Player is not currently in a room.");
                }
            }
        }

        private async void massSubBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? userInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Account ID");
            if (ulong.TryParse(userInput, out ulong userId))
            {
                Enabled = true;
                string[] tokens = await RecRoom.GetTokens();
                List<Task> reportTasks = new List<Task>();
                foreach (string token in tokens)
                {
                    reportTasks.Add(RecRoom.SubscribeToUser(token, userId));
                }
                await Task.WhenAll(reportTasks);
                ShowNotification("Sent all subscriptions");
            }
            else
                ShowNotification("Invalid user ID. Please enter a valid number.");
        }

        private async void massAddFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tokens = await RecRoom.GetTokens();
            string? userInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Enter Username");
            if (!string.IsNullOrEmpty(userInput))
            {
                SearchInfo? account = await RecRoom.GetAccountInfoFromUsername(_client, userInput);
                if (account != null && !string.IsNullOrEmpty(account.username))
                    ShowNotification("Invalid username. Could not find account.");
                else if (account != null)
                {
                    Enabled = true;
                    List<Task> reportTasks = new List<Task>();
                    foreach (string token in tokens)
                    {
                        reportTasks.Add(RecRoom.AddRelationship(token, account.accountId.ToString()));
                    }
                    await Task.WhenAll(reportTasks);
                    ShowNotification("Sent all mass add requests.");
                }
            }
        }

        private void copyTooClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells.Count > 0)
                {
                    object? cellValue = selectedRow.Cells[0].Value;
                    if (cellValue != null)
                    {
                        string? cellValueStr = cellValue.ToString();
                        string combinedCellValue = cellValueStr ?? "";
                        Thread.Sleep(100);
                        Clipboard.SetText(combinedCellValue);
                    }
                }
            }
        }

        private void copyEntireRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object? dataSource = dataGridView1.DataSource;
            if (dataSource == null) return;
            switch (dataSource)
            {
                case BindingSource bindingSource:
                    bindingSource.Clear();
                    break;
                case DataTable dataTable:
                    dataTable.Clear();
                    break;
                case IList<object> list:
                    list.Clear();
                    dataGridView1.DataSource = new List<object>();
                    break;
                default:
                    dataGridView1.DataSource = null;
                    break;
            }
        }

        private async void TakeBioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? usernameInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Username");
            if (!string.IsNullOrEmpty(usernameInput))
            {
                SearchInfo? account = await RecRoom.GetAccountInfoFromUsername(_client, usernameInput);
                if (account == null) return;
                if (account.accountId == 0)
                {
                    ShowNotification("Account ID not found for the provided username.");
                }
                else
                {
                    PlayerBio? friendBio = await RecRoom.GetPlayerBio(_client, account.accountId.ToString());
                    if (friendBio == null) return;
                    if (!string.IsNullOrEmpty(friendBio.bio))
                    {
                        MessageBox.Show("Bio data is empty or not found.");
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to update your bio to: '" + friendBio.bio + "'?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            await RecRoom.UpdateMyBio(_client, friendBio.bio);
                            ShowNotification("Your bio has been updated with your friend's bio.");
                        }
                        else
                            ShowNotification("Bio update canceled.");
                    }
                }
            }
            else
                MessageBox.Show("Username cannot be empty.");
        }

        private async void checkPlayersBioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || dataGridView1.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView is empty.");
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("No row is selected.");
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow.Cells.Count <= 2)
            {
                MessageBox.Show("Selected row does not have the required cell.");
                return;
            }
            string? accountId = selectedRow.Cells[2].Value?.ToString();
            if (string.IsNullOrEmpty(accountId))
            {
                MessageBox.Show("Account ID is empty.");
                return;
            }
            PlayerBio? bio = await RecRoom.GetPlayerBio(_client, accountId);
            if (bio != null && !string.IsNullOrEmpty(bio.bio))
                new FormHandler(this, Location, Width, Height).ShowMessage("Friend's Bio", bio.bio);
            else
                MessageBox.Show("Bio data is empty or not found.");
        }

        private async void massCheerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string? usernameInput = new FormHandler(this, Location, Width, Height).ShowTextPromp("Enter Username");
            if (!string.IsNullOrEmpty(usernameInput))
            {
                ShowNotification("You entered: " + usernameInput);
                string[] tokens = await RecRoom.GetTokens();
                int? accountId = await RecRoom.GetAccountId(_client, usernameInput);
                if (accountId > 0)
                {
                    List<Task> cheerTasks = new List<Task>();
                    foreach (string token in tokens)
                    {
                        cheerTasks.Add(RecRoom.SendCheer(token, token, accountId));
                    }
                    await Task.WhenAll(cheerTasks);
                    ShowNotification("All Cheers sent out successfully!");
                }
                else
                    ShowNotification("Username not found. Please check the username and try again.");
            }
            else
                ShowNotification("Invalid Username entered. Please enter a valid username.");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (notification != null)
                notification.Hide();
            timer1.Stop();
        }

        private async void getResentPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userIds = GetRecentPlayerIds();
            if (userIds.Count == 0)
            {
                MessageBox.Show("No user IDs found in registry.");
                return;
            }

            var profiles = await FetchProfilesAsync(userIds);
            BindUserProfilesToGrid(profiles);
        }
    }
}