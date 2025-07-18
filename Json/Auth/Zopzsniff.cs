using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class Zopzsniff
    {
        [JsonPropertyName("user_count")]
        public int UserCount { get; set; }

        [JsonPropertyName("total_active_users")]
        public int TotalActiveUsers { get; set; }
    }
}
