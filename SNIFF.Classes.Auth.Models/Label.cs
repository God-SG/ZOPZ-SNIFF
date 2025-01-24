using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

internal class Label
{
	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("value")]
	public string Value { get; set; }
}
