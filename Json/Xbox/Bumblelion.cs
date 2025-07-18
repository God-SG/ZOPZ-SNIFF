using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Bumblelion
    {
        [JsonPropertyName("audioEnabled")]
        public bool AudioEnabled { get; set; } = false;

        [JsonPropertyName("bumblelionConnectionState")]
        public int BumblelionConnectionState { get; set; } = 0;

        [JsonPropertyName("entityId")]
        public string EntityId { get; set; } = string.Empty;
    }
}
