using System.Reflection;
using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Sniffer
{
    [Obfuscation(Exclude = true)]
    public class Filter
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("platform")]
        public string? Type { get; set; }
        [JsonPropertyName("type")]
        public string? Platform { get; set; }
        [JsonPropertyName("options")]
        public List<FilterOption> Options { get; set; } = new List<FilterOption>();
    }
}
