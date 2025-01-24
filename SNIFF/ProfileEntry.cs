using System;
using Newtonsoft.Json;

namespace SNIFF;

public class ProfileEntry
{
	[JsonProperty("accountId")]
	public long AccountId { get; set; }

	[JsonProperty("username")]
	public string Username { get; set; }

	[JsonProperty("displayName")]
	public string DisplayName { get; set; }

	[JsonProperty("profileImage")]
	public string ProfileImage { get; set; }

	[JsonProperty("isJunior")]
	public bool? IsJunior { get; set; }

	[JsonProperty("platforms")]
	public long Platforms { get; set; }

	[JsonProperty("personalPronouns")]
	public long PersonalPronouns { get; set; }

	[JsonProperty("identityFlags")]
	public long IdentityFlags { get; set; }

	[JsonProperty("createdAt")]
	public DateTimeOffset CreatedAt { get; set; }

	[JsonProperty("isMetaPlatformBlocked")]
	public bool IsMetaPlatformBlocked { get; set; }

	[JsonProperty("bannerImage", NullValueHandling = NullValueHandling.Ignore)]
	public string BannerImage { get; set; }
}
