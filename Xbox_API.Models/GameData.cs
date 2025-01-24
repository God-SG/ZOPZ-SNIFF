using System;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class GameData
{
	[JsonProperty("sessionRef", NullValueHandling = NullValueHandling.Ignore)]
	public SessionRef SessionRef { get; set; }

	[JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
	public string Type { get; set; }

	[JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
	public long? Version { get; set; }

	[JsonProperty("titleId", NullValueHandling = NullValueHandling.Ignore)]
	[JsonConverter(typeof(ParseStringConverter))]
	public long? TitleId { get; set; }

	[JsonProperty("ownerXuid", NullValueHandling = NullValueHandling.Ignore)]
	public string OwnerXuid { get; set; }

	[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
	public Guid? Id { get; set; }

	[JsonProperty("inviteProtocol", NullValueHandling = NullValueHandling.Ignore)]
	public string InviteProtocol { get; set; }

	[JsonProperty("gameTypes", NullValueHandling = NullValueHandling.Ignore)]
	public GameTypes GameTypes { get; set; }

	[JsonProperty("createTime", NullValueHandling = NullValueHandling.Ignore)]
	public DateTimeOffset? CreateTime { get; set; }

	public static GameData FromJson(string json)
	{
		return JsonConvert.DeserializeObject<GameData>(json, Converter.Settings);
	}
}
