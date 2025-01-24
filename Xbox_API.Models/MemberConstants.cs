using Newtonsoft.Json;

namespace Xbox_API.Models;

public class MemberConstants
{
	[JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
	public FluffySystem System { get; set; }

	[JsonProperty("custom", NullValueHandling = NullValueHandling.Ignore)]
	public Servers Custom { get; set; }
}
