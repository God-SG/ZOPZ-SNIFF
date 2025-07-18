using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    public partial class Simpletextprompt : Form
    {
        private Func<string, bool>? _verifyFunction;

        public Simpletextprompt()
        {
            InitializeComponent();
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PlaceholderText
        {
            get => textBox1.PlaceholderText;
            set
            {
                if (textBox1.PlaceholderText != value)
                {
                    textBox1.PlaceholderText = value;
                    textBox1.ForeColor = Color.White;
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        textBox1.Text = value;
                    }
                }
            }
        }

        private void textBox1_Enter(object? sender, EventArgs e)
        {
            if (textBox1.Text == PlaceholderText)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.White;
            }
        }

        private void textBox1_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = PlaceholderText;
                textBox1.ForeColor = Color.White;
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public string ShowDialog(Func<string, bool>? verifyFunction = null)
        {
            if (verifyFunction != null)
                _verifyFunction = verifyFunction;
            base.ShowDialog();
            return textBox1.Text;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                if (_verifyFunction == null)
                {
                    Close();
                }
                else
                {
                    if (_verifyFunction(textBox1.Text))
                        Close();
                    else
                        MessageBox.Show("Value Verification Error");
                }
            }
        }

        private void Simpletextprompt_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
    }
}
