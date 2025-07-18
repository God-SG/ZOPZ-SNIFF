using System.Reflection;
using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Sniffer
{
    [Obfuscation(Exclude = true)]
    public class FilterOption
    {
        [JsonPropertyName("ports")]
        public List<string> Ports { get; set; } = new List<string>();

        [JsonPropertyName("payloads")]
        public List<string>? Payloads { get; set; } = new List<string>();

        [JsonPropertyName("minimum_length")]
        public int MinimumLength { get; set; } = 0;
        [JsonPropertyName("maximum_length")]
        public int MaximumLength { get; set; } = 0;
        [JsonPropertyName("ip_regex")]
        public string? IpRegex { get; set; } = string.Empty;
        [JsonPropertyName("isp")]
        public string? Isp { get; set; } = string.Empty;
    }
}
