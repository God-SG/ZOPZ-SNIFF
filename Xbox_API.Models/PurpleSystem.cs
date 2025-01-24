using Newtonsoft.Json;

namespace Xbox_API.Models;

public class PurpleSystem
{
	[JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
	public long? Version { get; set; }

	[JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
	public string Visibility { get; set; }

	[JsonProperty("inviteProtocol", NullValueHandling = NullValueHandling.Ignore)]
	public string InviteProtocol { get; set; }

	[JsonProperty("capabilities", NullValueHandling = NullValueHandling.Ignore)]
	public Capabilities Capabilities { get; set; }

	[JsonProperty("maxMembersCount", NullValueHandling = NullValueHandling.Ignore)]
	public long? MaxMembersCount { get; set; }

	[JsonProperty("managedInitialization", NullValueHandling = NullValueHandling.Ignore)]
	public ManagedInitialization ManagedInitialization { get; set; }

	[JsonProperty("timeouts", NullValueHandling = NullValueHandling.Ignore)]
	public Timeouts Timeouts { get; set; }
}
