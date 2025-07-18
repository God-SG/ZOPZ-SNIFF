using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class MembersInfo
    {
        [JsonPropertyName("accepted")]
        public int Accepted { get; set; }

        [JsonPropertyName("active")]
        public int Active { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("first")]
        public int First { get; set; }

        [JsonPropertyName("next")]
        public int Next { get; set; }
    }

}
