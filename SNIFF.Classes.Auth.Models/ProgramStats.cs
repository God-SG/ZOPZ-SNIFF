using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

public class ProgramStats
{
	[JsonProperty("user_count")]
	public long UserCount { get; set; }

	[JsonProperty("total_active_users")]
	public long TotalActiveUsers { get; set; }
}
