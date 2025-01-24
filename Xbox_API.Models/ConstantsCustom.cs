using Newtonsoft.Json;

namespace Xbox_API.Models;

public class ConstantsCustom
{
	[JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
	public string Type { get; set; }
}
