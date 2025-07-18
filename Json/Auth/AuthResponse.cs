using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class AuthResponse
    {
        [JsonPropertyName("data")]
        public Data? data { get; set; }
        [JsonPropertyName("message")]
        public string? message { get; set; }
        [JsonPropertyName("success")]
        public bool success { get; set; }
    }
}
