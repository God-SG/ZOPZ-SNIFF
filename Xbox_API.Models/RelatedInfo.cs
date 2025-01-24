using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class RelatedInfo
{
	[JsonProperty("joinRestriction", NullValueHandling = NullValueHandling.Ignore)]
	public string JoinRestriction { get; set; }

	[JsonProperty("closed", NullValueHandling = NullValueHandling.Ignore)]
	public bool? Closed { get; set; }

	[JsonProperty("maxMembersCount", NullValueHandling = NullValueHandling.Ignore)]
	public long? MaxMembersCount { get; set; }

	[JsonProperty("membersCount", NullValueHandling = NullValueHandling.Ignore)]
	public long? MembersCount { get; set; }

	[JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
	public string Visibility { get; set; }

	[JsonProperty("inviteProtocol", NullValueHandling = NullValueHandling.Ignore)]
	public string InviteProtocol { get; set; }

	[JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
	public List<string> Keywords { get; set; }

	[JsonProperty("postedTime", NullValueHandling = NullValueHandling.Ignore)]
	public DateTimeOffset? PostedTime { get; set; }

	[JsonProperty("searchHandleVisibility", NullValueHandling = NullValueHandling.Ignore)]
	public string SearchHandleVisibility { get; set; }
}
