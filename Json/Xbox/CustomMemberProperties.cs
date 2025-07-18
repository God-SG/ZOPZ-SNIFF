using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class CustomMemberProperties
    {
        [JsonPropertyName("bumblelion")]
        public Bumblelion Bumblelion { get; set; } = new Bumblelion();

        [JsonPropertyName("deviceId")]
        public string DeviceId { get; set; } = string.Empty;

        [JsonPropertyName("isBroadcasting")]
        public bool IsBroadcasting { get; set; } = false;

        [JsonPropertyName("simpleConnectionState")]
        public int SimpleConnectionState { get; set; } = 0;

        [JsonPropertyName("voicesessionid")]
        public string VoiceSessionId { get; set; } = string.Empty;
    }
}
