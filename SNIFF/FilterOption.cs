using System.Collections.Generic;
using Newtonsoft.Json;

namespace SNIFF;

internal class FilterOption
{
	[JsonProperty("ports")]
	public List<string> Ports { get; set; }

	[JsonProperty("payloads")]
	public List<string> Payloads { get; set; }

	[JsonProperty("minimum_length")]
	public int MinimumLength { get; set; }

	[JsonProperty("maximum_length")]
	public int MaximumLength { get; set; }
}
