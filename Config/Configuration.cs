using System.Reflection;
using ZOPZ_SNIFF.Config.Configs;
using ZOPZ_SNIFF.Utils;

namespace ZOPZ_SNIFF.Config
{
    public class Configuration
    {
        private static InfoStorage? _infostorage;
        public static InfoStorage? InfoStorage
        {
            get => _infostorage;
            set => _infostorage = value;
        }

        private static UserInfo? _userinfo;
        public static UserInfo? UserInfoConfig
        {
            get => _userinfo;
            set => _userinfo = value;
        }

        private static Sniffer? _sniffer;
        public static Sniffer? SnifferConfig
        {
            get => _sniffer;
            set => _sniffer = value;
        }

        public static void LoadConfigurations()
        {
            LoadConfiguration(ref _userinfo, () => new UserInfo());
            LoadConfiguration(ref _sniffer, () => new Sniffer());
            LoadConfiguration(ref _infostorage, () => new InfoStorage());
        }

        public static void SaveConfigurations()
        {
            SaveConfiguration(_userinfo);
            SaveConfiguration(_sniffer);
            SaveConfiguration(_infostorage);
        }

        public static void LoadConfiguration<T>(ref T? config, Func<T> createDefault) where T : class, new()
        {
            string? configLocation = typeof(T).GetField("ConfigurationLocation", BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as string;
            if (string.IsNullOrEmpty(configLocation) || !File.Exists(configLocation))
            {
                config = createDefault();
                SaveConfiguration(config);
                return;
            }
            string json = File.ReadAllText(configLocation);
            config = JsonHandler.Deserialize<T>(json);
        }

        public static void SaveConfiguration<T>(T? config) where T : class, new()
        {
            string? configLocation = typeof(T).GetField("ConfigurationLocation", BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as string;
            if (configLocation != null)
            {
                File.WriteAllText(configLocation, JsonHandler.Serialize(config));
            }
        }
    }
}
