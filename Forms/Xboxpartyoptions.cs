using ZOPZ_SNIFF.Api;
using ZOPZ_SNIFF.Json.Xbox;
using static ZOPZ_SNIFF.Menus.Rec;
namespace ZOPZ_SNIFF.Menus
{
    public partial class Xboxpartyoptions : Form
    {
        public string UserID { get; set; } = string.Empty;
        public string? GamerTag { get; set; }
        public string? imageUrl { get; set; } 
        public string? Email { get; set; }
        private string? _token { get; set; }
        private XboxAPI xbox { get; set; }

        public Xboxpartyoptions(string authorizationToken)
        {
            InitializeComponent();
            _token = authorizationToken;
            xbox = new XboxAPI(_token);
            GetUser();
        }

        public async void GetUser()
        {
            Root? profile = await xbox.GetProfile();
            UserID = profile?.ProfileUsers?.First()?.HostId ?? string.Empty;
            GamerTag = profile?.ProfileUsers?.First().Settings.First().Value;
        }

        private void xboxpartyoptions_Load(object sender, EventArgs e)
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

        private async void partyStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zopz? responseBody = await xbox.PartyStatusRequest(UserID);
            if (responseBody == null || responseBody.Data == null || responseBody.Data?.Results == null)
            {
                MessageAlert.Show("Alert", "Empty response received.");
                return;
            }
            Result? results = responseBody.Data?.Results?.FirstOrDefault();
            if (results != null)
            {
                string status = results.Status ?? "Unknown";
                string visibility = results.Visibility ?? "Unknown";
                string accepted = results.Accepted.ToString() ?? "0";
                string readRestriction = results.ReadRestriction ?? "None";
                string startTime = results.StartTime.ToString() ?? "Unknown";
                MessageAlert.Show("Alert", $"Total Members: {accepted}\nRead Restriction: {readRestriction}");
            }
            else
                MessageAlert.Show("Alert", "No results found.");
        }

        private void UpdateDataGridView(List<Member>? members)
        {
            if (members != null)
            {
                dataGridView1.Rows.Clear();
                bool isfirst = true;
                foreach (Member member in members)
                {
                    dataGridView1.Rows.Add(new object[]
                    {
                        member.Gamertag, isfirst ? "Host" : "Member", member.Constants.System.Xuid
                    });
                    isfirst = false;
                }
            }
        }


        private async void grabMyPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Member>? partyMembers = await xbox.GetPartyMembers(UserID);
            UpdateDataGridView(partyMembers);
        }

        private void copyEntireRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }



        private async void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zopz? result = await xbox.Unkickable(UserID);
            if (result != null)
                MessageAlert.Show("Alert", result.Message ?? "");
        }



        private async void openPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zopz? result = await xbox.OpenPartyRequest(UserID);
            if (result != null)
                MessageAlert.Show("Alert", result.Message);
        }



        private async void closePartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zopz? result = await xbox.ClosePartyRequest(UserID);
            if (result != null)
                MessageAlert.Show("Alert", result.Message);
        }

        private async void PartyStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zopz? responseBody = await xbox.PartyStatusRequest(UserID);
            if (responseBody != null && responseBody.Data != null)
            {
                Result? result = responseBody.Data?.Results?.First();
                if (result != null)
                {
                    string status = result.Status ?? "Unknown";
                    string visibility = result.Visibility ?? "Unknown";
                    string accepted = result.Accepted.ToString() ?? "0";
                    string readRestriction = result.ReadRestriction ?? "None";
                    string startTime = result.StartTime.ToString() ?? "Unknown";
                    MessageAlert.Show("Alert", $"Total Members: {accepted}\nRead Restriction: {readRestriction}");
                }
                else
                    MessageAlert.Show("Alert", "No results found");
            }
        }



        private async void crashPartyHostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zopz? result = await xbox.Crashpartyhost(UserID);
            if (result != null)
                MessageAlert.Show("Alert", result.Message);
        }



        private async void massReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string? targetXuid = selectedRow.Cells[2].Value?.ToString();
                string feedbackText = "he was harassing me in the voice chat";
                if (string.IsNullOrEmpty(feedbackText))
                {
                    MessageAlert.Show("Alert", "Please enter feedback before reporting.");
                    return;
                }
                try
                {
                    string? response = await xbox.SendFeedback(targetXuid, feedbackText);
                    MessageAlert.Show("Alert", response);
                }
                catch (Exception ex)
                {
                    MessageAlert.Show("Alert", "Error: " + ex.Message);
                }

            }
            else
                MessageAlert.Show("Alert", "No user selected. Please select a user to report.");
        }
    }
}
