using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Subscription
    {
        [JsonPropertyName("changeTypes")]
        public List<string> ChangeTypes { get; set; } = new List<string>();

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
