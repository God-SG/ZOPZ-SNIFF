using Newtonsoft.Json;

namespace Xbox_API.Models;

public class MemberProperties
{
	[JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
	public TentacledSystem System { get; set; }

	[JsonProperty("custom", NullValueHandling = NullValueHandling.Ignore)]
	public Servers Custom { get; set; }
}
