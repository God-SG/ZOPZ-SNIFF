using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Uwp
{
	[JsonProperty("titleId", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(ParseStringConverter))]
	public long? TitleId { get; set; }

	[JsonProperty("pfn", NullValueHandling = NullValueHandling.Ignore)]
	public string Pfn { get; set; }
}
