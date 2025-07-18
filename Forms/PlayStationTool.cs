using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZOPZ_SNIFF.Menus;
using static ZOPZ_SNIFF.Menus.Rec;

namespace ZOPZ_SNIFF.Forms
{
    public partial class PlayStationTool : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public string Token = string.Empty;
        public PlayStationTool(string token)
        {
            InitializeComponent();
            Token = token;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            SelectTypeCB.Items.AddRange(new string[]
            {
                "Search by Username",
                "Profile by Username",
                "Profile by AccountId",
                "Titles",
                "Trophies",
                "Games Played",
                "Recent Games"
            });

            SelectTypeCB.SelectedIndex = 0;

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

        private void PlayStationTool_Load(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private async Task<Image> DownloadImageAsync(string url)
        {
            try
            {
                var bytes = await client.GetByteArrayAsync(url);
                using var ms = new System.IO.MemoryStream(bytes);
                return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }
        private async Task LoadDataFromApiAsync(string url)
        {
            dataGridView1.Rows.Clear(); // Clear old data before loading new

            try
            {
                string json = await client.GetStringAsync(url);
                var root = JsonDocument.Parse(json).RootElement;

                var domainResponses = root.GetProperty("domainResponses");
                foreach (var domainResponse in domainResponses.EnumerateArray())
                {
                    if (domainResponse.TryGetProperty("results", out JsonElement results))
                    {
                        foreach (var result in results.EnumerateArray())
                        {
                            var sm = result.GetProperty("socialMetadata");

                            string accountId = sm.GetProperty("accountId").GetString();
                            string onlineId = sm.GetProperty("onlineId").GetString();
                            string country = sm.GetProperty("country").GetString();
                            string language = sm.GetProperty("language").GetString();
                            bool isPsPlus = sm.GetProperty("isPsPlus").GetBoolean();
                            string avatarUrl = sm.GetProperty("avatarUrl").GetString();

                            Image avatar = await DownloadImageAsync(avatarUrl);

                            dataGridView1.Rows.Add(avatar, onlineId, accountId, country, language, isPsPlus);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
        private async Task PerformSearchAsync()
        {
            string input = SearchTB.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Enter a username or account ID.");
                return;
            }

            string selected = SelectTypeCB.SelectedItem?.ToString();
            string url = "";

            if ((selected == "Profile by AccountId" || selected == "Titles" || selected == "Trophies" || selected == "Games Played") && !IsNumeric(input))
            {
                MessageBox.Show("This search requires a numeric Account ID.");
                return;
            }

            if ((selected == "Search by Username" || selected == "Profile by Username") && IsNumeric(input))
            {
                MessageBox.Show("This search requires a username (non-numeric).");
                return;
            }

            switch (selected)
            {
                case "Profile by AccountId":
                    url = $"https://zopzsniff.xyz/profile/account/{input}?NPSSO={Token}";
                    break;
                case "Titles":
                    url = $"https://zopzsniff.xyz/titles/{input}?NPSSO={Token}";
                    break;
                case "Trophies":
                    url = $"https://zopzsniff.xyz/trophies/summary/profile/{input}?NPSSO={Token}";
                    break;
                case "Games Played":
                    url = $"https://zopzsniff.xyz/games/played/{input}?NPSSO={Token}";
                    break;
                case "Search by Username":
                    url = $"https://zopzsniff.xyz/search/{input}?NPSSO={Token}";
                    break;
                case "Profile by Username":
                    url = $"https://zopzsniff.xyz/profile/username/{input}?NPSSO={Token}";
                    break;
                case "Recent Games":
                    url = $"https://zopzsniff.xyz/games/recent?NPSSO={Token}";
                    break;
                default:
                    MessageBox.Show("Invalid search type selected.");
                    return;
            }

            try
            {
                if (selected == "Search by Username")
                {
                    await LoadDataFromApiAsync(url);
                    SearchTB.Clear();
                }
                else
                {
                    dataGridView1.Rows.Clear();

                    var json = await client.GetStringAsync(url);
                    using var doc = JsonDocument.Parse(json);
                    var formatted = ParseElement(doc.RootElement);

                    MessageAlert.Show("Searched Information", formatted);
                }
            }
            catch (Exception ex)
            {
                SearchTB.Text = "Error:\n" + ex.Message;
            }
        }
        private bool IsNumeric(string input)
        {
            return ulong.TryParse(input, out _);
        }

        private string ParseElement(JsonElement element)
        {
            string result = "";

            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                {
                    var value = property.Value;
                    if (value.ValueKind == JsonValueKind.Object || value.ValueKind == JsonValueKind.Array)
                    {
                        result += $"{Capitalize(property.Name)}:\n";
                        result += ParseElement(value);
                    }
                    else
                    {
                        result += $"{Capitalize(property.Name)}: {FormatValue(value)}\n";
                    }
                }
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    result += ParseElement(item);
                    result += "======================================\n";
                }
            }

            return result;
        }

        private string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        private string FormatValue(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "null",
                _ => ""
            };
        }

        private void outputRTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void copyTooClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                List<string> cellValues = new();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    string text = cell.Value?.ToString() ?? "";
                    cellValues.Add(text);
                }

                string rowText = string.Join("\t", cellValues);

                Clipboard.SetText(rowText);
            }
        }

        private void copyEntireRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private async void SearchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent ding sound
                await PerformSearchAsync(); // Call your logic
            }
        }
    }
}