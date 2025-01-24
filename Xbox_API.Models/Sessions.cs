using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Sessions
{
	[JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
	public List<Result> Results { get; set; }

	public static Sessions FromJson(string json)
	{
		return JsonConvert.DeserializeObject<Sessions>(json, Converter.Settings);
	}
}
