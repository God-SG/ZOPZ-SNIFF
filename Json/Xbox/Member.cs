using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Member
    {
        [JsonPropertyName("activeTitleId")]
        public string ActiveTitleId { get; set; } = string.Empty;

        [JsonPropertyName("constants")]
        public MemberConstants Constants { get; set; } = new MemberConstants();

        [JsonPropertyName("gamertag")]
        public string Gamertag { get; set; } = string.Empty;

        [JsonPropertyName("joinTime")]
        public DateTime JoinTime { get; set; } = DateTime.Now;

        [JsonPropertyName("next")]
        public int Next { get; set; } = 0;

        [JsonPropertyName("properties")]
        public MemberProperties Properties { get; set; } = new MemberProperties();
    }
}
