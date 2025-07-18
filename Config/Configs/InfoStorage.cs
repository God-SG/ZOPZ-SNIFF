using System.Reflection;
using ZOPZ_SNIFF.Json.Info;

namespace ZOPZ_SNIFF.Config.Configs
{
    public class InfoStorage
    {
        public static readonly string ConfigurationLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "Settings", "InfoStorage.json");
        public Dictionary<string, IpInfo> IpInfos { get; set; } = new Dictionary<string, IpInfo>();
    }
}
