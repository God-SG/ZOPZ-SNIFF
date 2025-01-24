using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Capabilities
{
	[JsonProperty("connectivity", NullValueHandling = NullValueHandling.Ignore)]
	public bool? Connectivity { get; set; }

	[JsonProperty("userAuthorizationStyle", NullValueHandling = NullValueHandling.Ignore)]
	public bool? UserAuthorizationStyle { get; set; }

	[JsonProperty("searchable", NullValueHandling = NullValueHandling.Ignore)]
	public bool? Searchable { get; set; }

	[JsonProperty("crossPlay", NullValueHandling = NullValueHandling.Ignore)]
	public bool? CrossPlay { get; set; }
}
