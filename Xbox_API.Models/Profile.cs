using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Profile
{
	[JsonProperty("profileUsers", NullValueHandling = NullValueHandling.Ignore)]
	public List<ProfileUser> ProfileUsers { get; set; }

	public static Profile FromJson(string json)
	{
		return JsonConvert.DeserializeObject<Profile>(json, Converter.Settings);
	}
}
