using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class Statistics
    {
        [JsonPropertyName("total_user_count")]
        public int TotalUserCount { get; set; }

        [JsonPropertyName("banned_users")]
        public int BannedUsers { get; set; }

        [JsonPropertyName("program_statistics")]
        public ProgramStatistics? ProgramStatistics { get; set; }
    }
}
