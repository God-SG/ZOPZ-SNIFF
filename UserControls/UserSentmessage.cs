namespace ZOPZ_SNIFF.Menus
{
    public partial class UserSentmessage : UserControl
    {
        public UserSentmessage(string? poster, string? message)
        {
            InitializeComponent();
            UsernameLbl.Text = poster;
            MessageLbl.Text = message;
        }
    }
}
