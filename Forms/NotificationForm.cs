namespace ZOPZ_SNIFF.Menus
{
    public partial class NotificationForm : Form
    {
        public NotificationForm()
        {
            InitializeComponent();
        }

        public void SetMessage(string message)
        {
            lblMsg.Text = message;
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
    }
}
