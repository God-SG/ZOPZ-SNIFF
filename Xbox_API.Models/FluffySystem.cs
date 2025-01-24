using Newtonsoft.Json;

namespace Xbox_API.Models;

public class FluffySystem
{
	[JsonProperty("xuid", NullValueHandling = NullValueHandling.Ignore)]
	public string Xuid { get; set; }

	[JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
	public long? Index { get; set; }
}
