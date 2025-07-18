using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class CustomConstants
    {
        [JsonPropertyName("bumblelion")]
        public bool Bumblelion { get; set; }

        [JsonPropertyName("requiredVersion")]
        public int RequiredVersion { get; set; }

        [JsonPropertyName("requiredVersionWebRtc")]
        public int RequiredVersionWebRtc { get; set; }

        [JsonPropertyName("xrnxbl")]
        public bool Xrnxbl { get; set; }
    }
}
