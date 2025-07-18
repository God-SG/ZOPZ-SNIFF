using ZOPZ_SNIFF.Config;
using ZOPZ_SNIFF.Config.Configs;
using ZOPZ_SNIFF.Forms;
using ZOPZ_SNIFF.Json.Auth;

namespace ZOPZ_SNIFF.Menus
{
    public partial class SplashForm : Form
    {
        private UserInfo userInfo = Configuration.UserInfoConfig!;

        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (!userInfo.AutoLogin)
            {
                Hide();
                new LoginForm().Show();
            }
            else
            {
                if (!string.IsNullOrEmpty(userInfo.Username) && !string.IsNullOrEmpty(userInfo.Password))
                {
                    AuthResponse? response = await Program.api.Login(userInfo.Username, userInfo.Password);
                    if (response != null && response.success)
                    {
                        new MainMenu().Show();
                        Hide();
                        Program.api.LoadFilters();
                        Program.api.LoadStats();
                        Program.api.LoadLableDatas();
                    }
                }
                else
                {
                    new LoginForm().Show();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
