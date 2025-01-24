using Newtonsoft.Json;

namespace Xbox_API.Models;

public class GameLobbyConstants
{
	[JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
	public PurpleSystem System { get; set; }

	[JsonProperty("custom", NullValueHandling = NullValueHandling.Ignore)]
	public ConstantsCustom Custom { get; set; }
}
