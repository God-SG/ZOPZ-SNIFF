using Newtonsoft.Json;

namespace Xbox_API.Models;

public class GameTypes
{
	[JsonProperty("uwp-desktop", NullValueHandling = NullValueHandling.Ignore)]
	public Uwp UwpDesktop { get; set; }

	[JsonProperty("era", NullValueHandling = NullValueHandling.Ignore)]
	public Era Era { get; set; }

	[JsonProperty("uwp-xboxone", NullValueHandling = NullValueHandling.Ignore)]
	public Uwp UwpXboxone { get; set; }

	[JsonProperty("scarlett", NullValueHandling = NullValueHandling.Ignore)]
	public Era Scarlett { get; set; }
}
