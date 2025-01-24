using DiscordRPC;

namespace SNIFF;

public static class Globals
{
	public static DiscordRpcClient RPCClient;

	private static Timestamps _initialTimestamps;

	private static RichPresence _richPresence;

	static Globals()
	{
		RPCClient = new DiscordRpcClient("1245873641493762190");
		_initialTimestamps = Timestamps.Now;
		_richPresence = new RichPresence
		{
			State = "Status: Idle",
			Details = "Idle",
			Assets = new Assets
			{
				LargeImageKey = "screenshot_2024-09-14_162856-1024x1024-1024x1024",
				LargeImageText = "Version: " + Global.Version.ToString(),
				SmallImageKey = "https://cdn.discordapp.com/emojis/771191366121947176.gif",
				SmallImageText = "ShadowGarden <3"
			},
			Buttons = new Button[2]
			{
				new Button
				{
					Label = "Website",
					Url = "https://partyhax.club/"
				},
				new Button
				{
					Label = "Discord",
					Url = "https://discord.gg/shadowgarden"
				}
			},
			Timestamps = _initialTimestamps
		};
		InitializeRPC();
	}

	public static void InitializeRPC()
	{
		RPCClient.Initialize();
		UpdateRichPresence("Idle", "Status: Idle");
	}

	public static void SetRPC(bool showDiscordRPC)
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

	public static void UpdateRichPresence(string details, string state = null)
	{
		if (state != null)
		{
			_richPresence.State = state;
		}
		_richPresence.Details = details;
		RPCClient.SetPresence(_richPresence);
	}

	public static void ShutdownRPC()
	{
		RPCClient.Dispose();
	}
}
