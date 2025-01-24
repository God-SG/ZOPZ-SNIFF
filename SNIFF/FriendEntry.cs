using Newtonsoft.Json;

namespace SNIFF;

public class FriendEntry
{
	[JsonProperty("Id")]
	public long Id { get; set; }

	[JsonProperty("PlayerID")]
	public long PlayerId { get; set; }

	[JsonProperty("OtherPlayerID")]
	public long OtherPlayerId { get; set; }

	[JsonProperty("RelationshipType")]
	public long RelationshipType { get; set; }

	[JsonProperty("Favorited")]
	public long Favorited { get; set; }

	[JsonProperty("Muted")]
	public long Muted { get; set; }

	[JsonProperty("Ignored")]
	public long Ignored { get; set; }
}
