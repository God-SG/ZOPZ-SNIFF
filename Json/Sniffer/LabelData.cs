using System.Reflection;
using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Sniffer
{
    [Obfuscation(Exclude = true)]
    public class LabelData
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
