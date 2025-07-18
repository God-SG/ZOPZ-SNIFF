using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class SystemMemberConstants
    {
        [JsonPropertyName("index")]
        public int Index { get; set; } = 0;

        [JsonPropertyName("xuid")]
        public string Xuid { get; set; } = string.Empty;
    }
}
