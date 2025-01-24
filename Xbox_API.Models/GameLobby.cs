using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class GameLobby
{
	[JsonProperty("membersInfo", NullValueHandling = NullValueHandling.Ignore)]
	public MembersInfo MembersInfo { get; set; }

	[JsonProperty("constants", NullValueHandling = NullValueHandling.Ignore)]
	public GameLobbyConstants Constants { get; set; }

	[JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
	public GameLobbyProperties Properties { get; set; }

	[JsonProperty("servers", NullValueHandling = NullValueHandling.Ignore)]
	public Servers Servers { get; set; }

	[JsonProperty("searchHandle", NullValueHandling = NullValueHandling.Ignore)]
	public Guid? SearchHandle { get; set; }

	[JsonProperty("members", NullValueHandling = NullValueHandling.Ignore)]
	public Dictionary<string, Member> Members { get; set; }

	[JsonProperty("correlationId", NullValueHandling = NullValueHandling.Ignore)]
	public Guid? CorrelationId { get; set; }

	[JsonProperty("contractVersion", NullValueHandling = NullValueHandling.Ignore)]
	public long? ContractVersion { get; set; }

	[JsonProperty("branch", NullValueHandling = NullValueHandling.Ignore)]
	public Guid? Branch { get; set; }

	[JsonProperty("changeNumber", NullValueHandling = NullValueHandling.Ignore)]
	public long? ChangeNumber { get; set; }

	[JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
	public DateTimeOffset? StartTime { get; set; }

	public static GameLobby FromJson(string json)
	{
		return JsonConvert.DeserializeObject<GameLobby>(json, Converter.Settings);
	}
}
