using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class ProfileUser
{
	[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
	public string Id { get; set; }

	[JsonProperty("hostId", NullValueHandling = NullValueHandling.Ignore)]
	public string HostId { get; set; }

	[JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
	public List<Setting> Settings { get; set; }

	[JsonProperty("isSponsoredUser", NullValueHandling = NullValueHandling.Ignore)]
	public bool? IsSponsoredUser { get; set; }
}
