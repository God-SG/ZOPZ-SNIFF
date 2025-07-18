using ZOPZ_SNIFF.Config;
using ZOPZ_SNIFF.Config.Configs;

namespace ZOPZ_SNIFF.UserControls
{
    public partial class FIlterDisplay : UserControl
    {
        private string? _filterName = string.Empty;
        private static readonly Color _disabledColor = Color.FromArgb(30, 30, 30);
        private static readonly Color _enabledColor = Color.FromArgb(25, 25, 25);
        private Sniffer? Sniffer = Configuration.SnifferConfig;


        public FIlterDisplay(string? name, string? platform, string type)
        {
            _filterName = name;
            InitializeComponent();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(platform) && !string.IsNullOrEmpty(_filterName))
            {
                if (Sniffer != null && Sniffer.EnabledFilters != null)
                    if (Sniffer.EnabledFilters.Contains(_filterName))
                        BackColor = _enabledColor;
                    else
                        BackColor = _disabledColor;
                MessageLbl.Text = name;
                UsernameLbl.Text = platform;
                label1.Text = type;
            }
        }

        private void UsernameLbl_Click(object sender, EventArgs e)
        {
            if (Sniffer != null && Sniffer.EnabledFilters != null)
            {
                if (!string.IsNullOrEmpty(_filterName))
                    if (Sniffer.EnabledFilters.Contains(_filterName))
                    {
                        BackColor = _disabledColor;

                        Sniffer.EnabledFilters.Remove(_filterName);
                    }
                    else
                    {
                        Sniffer.EnabledFilters.Add(_filterName);
                        BackColor = _enabledColor;
                    }
                Configuration.SaveConfiguration(Sniffer);
            }
        }
    }
}
