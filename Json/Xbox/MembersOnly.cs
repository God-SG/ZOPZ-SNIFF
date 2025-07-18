using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class MembersOnly
    {
        [JsonPropertyName("bumblelionRelayCreator")]
        public string BumblelionRelayCreator { get; set; } = string.Empty;
    }

}
