using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class SystemProperties
    {
        [JsonPropertyName("joinRestriction")]
        public string JoinRestriction { get; set; } = string.Empty;

        [JsonPropertyName("readRestriction")]
        public string ReadRestriction { get; set; } = string.Empty;

        [JsonPropertyName("turn")]
        public List<object> Turn { get; set; } = new List<object>();
    }
}
