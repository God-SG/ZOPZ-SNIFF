using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Result
    {
        [JsonPropertyName("accepted")]
        public int Accepted { get; set; } = 0;

        [JsonPropertyName("clubId")]
        public string ClubId { get; set; } = string.Empty;

        [JsonPropertyName("firstMemberXuid")]
        public string FirstMemberXuid { get; set; } = string.Empty;

        [JsonPropertyName("joinRestriction")]
        public string JoinRestriction { get; set; } = string.Empty;

        [JsonPropertyName("keywords")]
        public List<object> Keywords { get; set; } = new List<object>();

        [JsonPropertyName("readRestriction")]
        public string ReadRestriction { get; set; } = string.Empty;

        [JsonPropertyName("sessionRef")]
        public SessionRef SessionRef { get; set; } = new SessionRef();

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; } = string.Empty;

        [JsonPropertyName("xuid")]
        public string Xuid { get; set; } = string.Empty;
    }
}
