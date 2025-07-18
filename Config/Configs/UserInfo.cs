using System.Reflection;
using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Config.Configs
{
    public class UserInfo
    {
        [JsonIgnore]
        public static readonly string ConfigurationLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "Settings", "UserInfo.json");
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool AutoLogin { get; set; } = false;
        public bool RememberMe { get; set; } = false;
        public bool ShowDiscordRPC { get; set; } = false;
        public string Adapter { get; set; } = string.Empty;
        public string Recroomtoken { get; set; } = string.Empty;

        public string PSNToken { get; set; } = string.Empty;
        public string XboxToken { get; set; } = string.Empty;
    }
}
