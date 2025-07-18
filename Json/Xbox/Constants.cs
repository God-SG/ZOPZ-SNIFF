using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Constants
    {
        [JsonPropertyName("custom")]
        public CustomConstants Custom { get; set; } = new CustomConstants();

        [JsonPropertyName("system")]
        public SystemConstants System { get; set; } = new SystemConstants();
    }
}
