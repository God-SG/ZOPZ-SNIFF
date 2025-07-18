using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class Programs
    {
        [JsonPropertyName("Name")]
        public string name { get; set; } = string.Empty;
        [JsonPropertyName("Expiry")]
        public string expiry { get; set; } = string.Empty;
    }
}
