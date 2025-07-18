using DiscordRPC;
using ZOPZ_SNIFF.Json.Auth;
using Button = DiscordRPC.Button;

namespace ZOPZ_SNIFF.Auth
{
    public class CustomRpc
    {
        public CustomRpc()
        {
            InitializeRPC();
        }

        public void InitializeRPC()
        {
            RPCClient.Initialize();
            UpdateRichPresence("", "Status: Idle");
        }

        public void SetRPC(bool showDiscordRPC)
        {
            if (showDiscordRPC)
            {
                _richPresence.Timestamps = _initialTimestamps;
                RPCClient.SetPresence(_richPresence);
            }
            else
            {
                RPCClient.ClearPresence();
            }
        }

        public void UpdateRichPresence(string details, string? state = null)
        {
            if (state != null)
            {
                _richPresence.State = state;
            }
            _richPresence.Details = details;
            RPCClient.SetPresence(_richPresence);
        }

        public void ShutdownRPC() => RPCClient.Dispose();

        public DiscordRpcClient RPCClient = new DiscordRpcClient("1354869008813527161");
        private static Timestamps _initialTimestamps = Timestamps.Now;

        private RichPresence _richPresence = new RichPresence()
        {
            State = "Status: Idle",
            Details = "Idle",
            Assets = new Assets()
            {
                LargeImageKey = "screenshot_2024-09-14_162856-1024x1024-1024x1024",
                LargeImageText = $"Version: 5.0.0.0",
                SmallImageKey = "https://cdn.discordapp.com/emojis/771191366121947176.gif",
                SmallImageText = "ZOPZ SNIFF"
            },
            Buttons = new Button[]
            {
                new Button()
                {
                    Label = "Website",
                    Url = "https://zopzsniff.xyz/"
                },
                new Button()
                {
                    Label = "Discord",
                    Url = "https://discord.zopzsniff.xyz/"
                }
            },
            Timestamps = _initialTimestamps
        };
    }
}
