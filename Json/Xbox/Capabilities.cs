using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Capabilities
    {
        [JsonPropertyName("allowMembersOnly")]
        public bool AllowMembersOnly { get; set; }

        [JsonPropertyName("autoPopulateServerCandidates")]
        public bool AutoPopulateServerCandidates { get; set; }

        [JsonPropertyName("checkMembersCanBroadcast")]
        public bool CheckMembersCanBroadcast { get; set; }

        [JsonPropertyName("cloudCompute")]
        public bool CloudCompute { get; set; }

        [JsonPropertyName("connectivity")]
        public bool Connectivity { get; set; }

        [JsonPropertyName("crossPlay")]
        public bool CrossPlay { get; set; }

        [JsonPropertyName("multiplayerParty")]
        public bool MultiplayerParty { get; set; }

        [JsonPropertyName("userAuthorizationStyle")]
        public bool UserAuthorizationStyle { get; set; }
    }
}
