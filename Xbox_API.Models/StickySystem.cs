using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class StickySystem
{
	[JsonProperty("joinRestriction", NullValueHandling = NullValueHandling.Ignore)]
	public string JoinRestriction { get; set; }

	[JsonProperty("readRestriction", NullValueHandling = NullValueHandling.Ignore)]
	public string ReadRestriction { get; set; }

	[JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
	public List<string> Keywords { get; set; }

	[JsonProperty("turn", NullValueHandling = NullValueHandling.Ignore)]
	public List<object> Turn { get; set; }

	[JsonProperty("searchHandleVisibility", NullValueHandling = NullValueHandling.Ignore)]
	public string SearchHandleVisibility { get; set; }

	[JsonProperty("matchmaking", NullValueHandling = NullValueHandling.Ignore)]
	public Servers Matchmaking { get; set; }
}
