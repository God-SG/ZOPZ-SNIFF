using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Subscription
{
	[JsonProperty("changeTypes", NullValueHandling = NullValueHandling.Ignore)]
	public List<string> ChangeTypes { get; set; }

	[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
	public string Id { get; set; }
}
