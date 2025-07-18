using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class CloudComputePackage
    {
        [JsonPropertyName("crossSandbox")]
        public bool CrossSandbox { get; set; } = false;

        [JsonPropertyName("gsiSet")]
        public string GsiSet { get; set; } = string.Empty;

        [JsonPropertyName("titleId")]
        public string TitleId { get; set; } = string.Empty;
    }
}
