using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Properties
    {
        [JsonPropertyName("custom")]
        public object Custom { get; set; } = new object();

        [JsonPropertyName("system")]
        public SystemProperties System { get; set; } = new SystemProperties();
    }
}
