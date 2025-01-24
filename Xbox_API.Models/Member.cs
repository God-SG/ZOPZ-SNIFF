using System;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class Member
{
	[JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
	public long? Next { get; set; }

	[JsonProperty("joinTime", NullValueHandling = NullValueHandling.Ignore)]
	public DateTimeOffset? JoinTime { get; set; }

	[JsonProperty("constants", NullValueHandling = NullValueHandling.Ignore)]
	public MemberConstants Constants { get; set; }

	[JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
	public MemberProperties Properties { get; set; }

	[JsonProperty("gamertag", NullValueHandling = NullValueHandling.Ignore)]
	public string Gamertag { get; set; }

	[JsonProperty("activeTitleId", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? ActiveTitleId { get; set; }

	[JsonProperty("nat", NullValueHandling = NullValueHandling.Ignore)]
	public string Nat { get; set; }

	[JsonProperty("deviceToken", NullValueHandling = NullValueHandling.Ignore)]
	public string DeviceToken { get; set; }
}
