using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Zopz
    {
        [JsonPropertyName("data")]
        public PartyData? Data { get; set; } = new PartyData();

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("success")]
        public bool Success { get; set; } = false;
    }
}
