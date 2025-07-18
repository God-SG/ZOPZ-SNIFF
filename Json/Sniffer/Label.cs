using System.Reflection;
using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Sniffer
{
    [Obfuscation(Exclude = true)]
    public class Label
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public LabelData[]? Data { get; set; }
    }
}
