using Newtonsoft.Json;

namespace Xbox_API.Models;

public class GameLobbyProperties
{
	[JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
	public StickySystem System { get; set; }

	[JsonProperty("custom", NullValueHandling = NullValueHandling.Ignore)]
	public PropertiesCustom Custom { get; set; }
}
