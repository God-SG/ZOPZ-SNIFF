using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class SystemMemberProperties
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; } = false;

        [JsonPropertyName("connection")]
        public string Connection { get; set; } = string.Empty;

        [JsonPropertyName("subscription")]
        public Subscription Subscription { get; set; } = new Subscription();
    }

}
