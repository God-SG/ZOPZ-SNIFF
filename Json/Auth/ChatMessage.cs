using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class ChatMessage
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("sent")]
        public DateTime Sent { get; set; }
        [JsonPropertyName("poster")]
        public string? Poster { get; set; }
    }
}
