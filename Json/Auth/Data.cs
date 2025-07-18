using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class Data
    {
        [JsonPropertyName("Created")]
        public string? created { get; set; }
        [JsonPropertyName("Imported")]
        public bool imported { get; set; }
        [JsonPropertyName("LastLogin")]
        public string? lastLogin { get; set; }
        [JsonPropertyName("Level")]
        public int level { get; set; }
        [JsonPropertyName("Programs")]
        public Programs[]? programs { get; set; }
        [JsonPropertyName("Username")]
        public string username { get; set; } = string.Empty;
    }
}
