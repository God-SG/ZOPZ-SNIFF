using System.Collections.Generic;

namespace SNIFF;

public class SettingsModel
{
	public string Username { get; set; }

	public string Password { get; set; }

	public string Adapter { get; set; }

	public bool RememberMe { get; set; }

	public string Token { get; set; }

	public string Recroomtoken { get; set; }

	public bool ShowDiscordRPC { get; set; } = true;

	public bool AutoLogin { get; set; }

	public bool AppTopMost { get; set; }

	public bool FilteredGames { get; set; } = true;

	public bool Playstation { get; set; } = true;

	public bool Xbox { get; set; } = true;

	public bool XBLTool { get; set; } = true;

	public bool Otherinfo { get; set; } = true;

	public bool Pctab { get; set; } = true;

	public bool PFmode { get; set; }

	public string Yourepsn { get; set; }

	public List<string> EnabledFilters { get; set; }

	public List<string> EnabledOnlineFilters { get; set; }
}
