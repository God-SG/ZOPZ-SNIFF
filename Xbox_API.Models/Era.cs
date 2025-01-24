using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Era
{
	[JsonProperty("titleId", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(ParseStringConverter))]
	public long? TitleId { get; set; }
}
