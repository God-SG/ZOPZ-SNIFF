using System.Reflection;

namespace ZOPZ_SNIFF.Config.Configs
{
    public class Sniffer
    {
        public static readonly string ConfigurationLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "Settings", "Sniffer.json");
        public List<string>? EnabledFilters { get; set; } = new List<string>();
    }
}
