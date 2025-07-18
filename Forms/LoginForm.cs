using ZOPZ_SNIFF.Config;
using ZOPZ_SNIFF.Config.Configs;
using ZOPZ_SNIFF.Forms;
using ZOPZ_SNIFF.Json.Auth;

namespace ZOPZ_SNIFF.Menus
{
    public partial class LoginForm : Form
    {
        private static UserInfo? userInfo = Configuration.UserInfoConfig;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            if (LoginBTN.Text == "Login")
            {
                LoginBTN.Text = "Signing you in...";
                AuthResponse? response = await Program.api.Login(guna2TextBox1.Text, guna2TextBox2.Text);

                if (response != null && response.success)
                {
                    if (userInfo != null)
                    {
                        userInfo.Username = guna2TextBox1.Text;
                        userInfo.Password = guna2TextBox2.Text;
                        userInfo.AutoLogin = true;
                        Configuration.SaveConfiguration(userInfo);
                        MessageAlert.Show("Alert", "Welcome back " + userInfo.Username);
                    }
                    Hide();
                    new MainMenu().Show();
                    Program.api.LoadFilters();
                    Program.api.LoadStats();
                    Program.api.LoadLableDatas();
                }
                else
                {
                    LoginBTN.Text = "Login";
                    string errorMsg = response?.message ?? "Login failed. Please try again.";
                    MessageAlert.Show("Alert", errorMsg);

                    guna2TextBox2.Text = string.Empty;
                    guna2TextBox2.Focus();
                }
            }

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (userInfo != null && userInfo.RememberMe)
            {
                if (!string.IsNullOrEmpty(userInfo.Username))
                    guna2TextBox1.Text = userInfo.Username;
                if (!string.IsNullOrEmpty(userInfo.Password))
                    guna2TextBox2.Text = userInfo.Password;
                Remebermecheck.Checked = true;
            }
        }

        private void Remebermecheck_CheckedChanged(object sender, EventArgs e)
        {
            if (userInfo != null)
            {
                userInfo.RememberMe = Remebermecheck.Checked;
                Configuration.SaveConfiguration(userInfo);
            }
        }
    }
}
