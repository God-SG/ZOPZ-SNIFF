using Newtonsoft.Json;

namespace Xbox_API.Models;

public class MembersInfo
{
	[JsonProperty("first", NullValueHandling = NullValueHandling.Ignore)]
	public long? First { get; set; }

	[JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
	public long? Next { get; set; }

	[JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
	public long? Count { get; set; }

	[JsonProperty("accepted", NullValueHandling = NullValueHandling.Ignore)]
	public long? Accepted { get; set; }

	[JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
	public long? Active { get; set; }
}
