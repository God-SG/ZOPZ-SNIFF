using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Timeouts
{
	[JsonProperty("reserved", NullValueHandling = NullValueHandling.Ignore)]
	public long? Reserved { get; set; }

	[JsonProperty("inactive", NullValueHandling = NullValueHandling.Ignore)]
	public long? Inactive { get; set; }

	[JsonProperty("ready", NullValueHandling = NullValueHandling.Ignore)]
	public long? Ready { get; set; }

	[JsonProperty("sessionEmpty", NullValueHandling = NullValueHandling.Ignore)]
	public long? SessionEmpty { get; set; }
}
