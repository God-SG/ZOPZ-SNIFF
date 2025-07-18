namespace ZOPZ_SNIFF.Menus
{
    public partial class MessageAlert : Form
    {
        public MessageAlert(string? title, string? value)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            textBox.Text = value;
        }

        public static void Show(string title, string message)
        {
            new MessageAlert(title, message).ShowDialog();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MessageAlert_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
    }
}
