using System;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class TentacledSystem
{
	[JsonProperty("secureDeviceAddress", NullValueHandling = NullValueHandling.Ignore)]
	public string SecureDeviceAddress { get; set; }

	[JsonProperty("subscription", NullValueHandling = NullValueHandling.Ignore)]
	public Subscription Subscription { get; set; }

	[JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
	public bool? Active { get; set; }

	[JsonProperty("connection", NullValueHandling = NullValueHandling.Ignore)]
	public Guid? Connection { get; set; }
}
