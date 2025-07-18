using ZOPZ_SNIFF.Api;
using ZOPZ_SNIFF.Json.Auth;
using ZOPZ_SNIFF.Json.Info;
using ZOPZ_SNIFF.Json.Sniffer;

namespace ZOPZ_SNIFF.Menus
{
    public partial class Geo : Form
    {
        public Geo() => InitializeComponent();

        private void guna2ControlBox1_Click(object sender, EventArgs e) => Close();

        private async void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                string ipAddress = guna2TextBox1.Text;
                IpInfo? result = await Info.GetAddressInfo(ipAddress);
                if (result != null)
                {
                    guna2DataGridView1.Rows.Clear();
                    if (!string.IsNullOrEmpty(result.Error))
                    {
                        MessageBox.Show("Failed to retrieve data.");
                    }
                    else
                    {
                        guna2DataGridView1.Rows.Add($"IP Address: {result.Address}");
                        guna2DataGridView1.Rows.Add($"Continent: {result.ContinentCode}");
                        guna2DataGridView1.Rows.Add($"Continent Code: {result.ContinentCode}");
                        guna2DataGridView1.Rows.Add($"Country: {result.Country}");
                        guna2DataGridView1.Rows.Add($"Region: {result.Region}");
                        guna2DataGridView1.Rows.Add($"Region Name: {result.Region}");
                        guna2DataGridView1.Rows.Add($"City: {result.City}");
                        guna2DataGridView1.Rows.Add($"Zip: {result.Postcode}");
                        guna2DataGridView1.Rows.Add($"Latitude: {result.Latitude}");
                        guna2DataGridView1.Rows.Add($"Longitude: {result.Longitude}");
                        guna2DataGridView1.Rows.Add($"ISP: {result.Provider}");
                        guna2DataGridView1.Rows.Add($"Organization: {result.Organisation}");
                        guna2DataGridView1.Rows.Add($"AS: {result.Asn}");
                    }
                }
            }
        }

        private void guna2TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                IEnumerable<LabelData>? results = Program.api.Labels?.Where(x => x.Name == guna2TextBox2.Text);
                guna2DataGridView2.Rows.Clear();
                if (results != null && results.Any())
                {
                    foreach (LabelData result in results)
                    {
                        guna2DataGridView2.Rows.Add(new object[]
                        {
                            $"IP Address: {result.Value}"
                        });
                    }
                }
                else
                    MessageBox.Show("No Users");
            }
        }

        private async void guna2TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                string ipAddress = guna2TextBox3.Text;
                IpInfo? result = await Info.GetAddressInfo(ipAddress);
                if (result != null)
                {
                    guna2DataGridView2.Rows.Clear();
                    guna2DataGridView3.Rows.Clear();
                    IOrderedEnumerable<LabelData>? userResults = Program.api.Labels?.Where(x => x.Name == ipAddress).OrderBy(x => x.Name);
                    if (!string.IsNullOrEmpty(result.Error))
                    {
                        if (userResults != null && userResults.Any())
                        {
                            foreach (LabelData user in userResults)
                                guna2DataGridView2.Rows.Add($"Username: {user.Name}");
                        }
                    }
                    else
                    {
                        if (userResults != null && userResults.Any())
                        {
                            foreach (LabelData user in userResults)
                                guna2DataGridView3.Rows.Add($"Username: {user.Name}");
                        }
                        guna2DataGridView3.Rows.Add($"IP Address: {result.Address}");
                        guna2DataGridView3.Rows.Add($"Continent: {result.ContinentCode}");
                        guna2DataGridView3.Rows.Add($"Continent Code: {result.ContinentCode}");
                        guna2DataGridView3.Rows.Add($"Country: {result.Country}");
                        guna2DataGridView3.Rows.Add($"Region: {result.Region}");
                        guna2DataGridView3.Rows.Add($"Region Name: {result.Region}");
                        guna2DataGridView3.Rows.Add($"City: {result.City}");
                        guna2DataGridView3.Rows.Add($"Zip: {result.Postcode}");
                        guna2DataGridView3.Rows.Add($"Latitude: {result.Latitude}");
                        guna2DataGridView3.Rows.Add($"Longitude: {result.Longitude}");
                        guna2DataGridView3.Rows.Add($"ISP: {result.Provider}");
                        guna2DataGridView3.Rows.Add($"Organization: {result.Organisation}");
                        guna2DataGridView3.Rows.Add($"AS: {result.Asn}");
                    }
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void Geo_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
    }
}
