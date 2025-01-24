using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Setting
{
	[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
	public string Id { get; set; }

	[JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
	public string Value { get; set; }
}
