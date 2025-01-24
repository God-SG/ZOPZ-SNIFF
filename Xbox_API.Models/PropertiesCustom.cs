using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class PropertiesCustom
{
	[JsonProperty("GameMode", NullValueHandling = NullValueHandling.Ignore)]
	public string GameMode { get; set; }

	[JsonProperty("MapName", NullValueHandling = NullValueHandling.Ignore)]
	public string MapName { get; set; }

	[JsonProperty("DayTime", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? DayTime { get; set; }

	[JsonProperty("MATCHTIMEOUT", NullValueHandling = NullValueHandling.Ignore)]
	public string Matchtimeout { get; set; }

	[JsonProperty("SEARCHKEYWORDS", NullValueHandling = NullValueHandling.Ignore)]
	public string Searchkeywords { get; set; }

	[JsonProperty("ModId", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? ModId { get; set; }

	[JsonProperty("CUSTOMSERVERNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string Customservername { get; set; }

	[JsonProperty("ServerPassword", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(FluffyParseStringConverter))]
	public bool? ServerPassword { get; set; }

	[JsonProperty("BuildId", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? BuildId { get; set; }

	[JsonProperty("MINORBUILDID", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Minorbuildid { get; set; }

	[JsonProperty("ClusterId", NullValueHandling = NullValueHandling.Ignore)]
	public string ClusterId { get; set; }

	[JsonProperty("ALLOWDOWNLOADCHARS", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Allowdownloadchars { get; set; }

	[JsonProperty("ALLOWDOWNLOADITEMS", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Allowdownloaditems { get; set; }

	[JsonProperty("OFFICIALSERVICE", NullValueHandling = NullValueHandling.Ignore)]
	public Uri Officialservice { get; set; }

	[JsonProperty("LATENCYCHECKPORT", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Latencycheckport { get; set; }

	[JsonProperty("OFFICIALSERVER", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Officialserver { get; set; }

	[JsonProperty("SESSIONISPVE", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Sessionispve { get; set; }

	[JsonProperty("LEGACY", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(PurpleParseStringConverter))]
	public long? Legacy { get; set; }

	[JsonProperty("hidden_session", NullValueHandling = NullValueHandling.Ignore)]
	public string HiddenSession { get; set; }

	[JsonProperty("Joinability", NullValueHandling = NullValueHandling.Ignore)]
	public string Joinability { get; set; }

	[JsonProperty("hostName", NullValueHandling = NullValueHandling.Ignore)]
	public string HostName { get; set; }

	[JsonProperty("ownerId", NullValueHandling = NullValueHandling.Ignore)]
	public string OwnerId { get; set; }

	[JsonProperty("rakNetGUID", NullValueHandling = NullValueHandling.Ignore)]
	public string RakNetGuid { get; set; }

	[JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
	public string Version { get; set; }

	[JsonProperty("levelId", NullValueHandling = NullValueHandling.Ignore)]
	public string LevelId { get; set; }

	[JsonProperty("worldName", NullValueHandling = NullValueHandling.Ignore)]
	public string WorldName { get; set; }

	[JsonProperty("worldType", NullValueHandling = NullValueHandling.Ignore)]
	public string WorldType { get; set; }

	[JsonProperty("protocol", NullValueHandling = NullValueHandling.Ignore)]
	public long? Protocol { get; set; }

	[JsonProperty("MemberCount", NullValueHandling = NullValueHandling.Ignore)]
	public long? MemberCount { get; set; }

	[JsonProperty("MaxMemberCount", NullValueHandling = NullValueHandling.Ignore)]
	public long? MaxMemberCount { get; set; }

	[JsonProperty("BroadcastSetting", NullValueHandling = NullValueHandling.Ignore)]
	public long? BroadcastSetting { get; set; }

	[JsonProperty("LanGame", NullValueHandling = NullValueHandling.Ignore)]
	public bool? LanGame { get; set; }

	[JsonProperty("OnlineCrossPlatformGame", NullValueHandling = NullValueHandling.Ignore)]
	public bool? OnlineCrossPlatformGame { get; set; }

	[JsonProperty("CrossPlayDisabled", NullValueHandling = NullValueHandling.Ignore)]
	public bool? CrossPlayDisabled { get; set; }

	[JsonProperty("TitleId", NullValueHandling = NullValueHandling.Ignore)]
	public long? TitleId { get; set; }

	[JsonProperty("SupportedConnections", NullValueHandling = NullValueHandling.Ignore)]
	public List<SupportedConnection> SupportedConnections { get; set; }
}
