using System.Collections.Generic;
using Newtonsoft.Json;

namespace SNIFF;

internal class Filter
{
	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("description")]
	public string Description { get; set; }

	[JsonProperty("platform")]
	public string Platform { get; set; }

	[JsonProperty("options")]
	public List<FilterOption> Options { get; set; }
}
