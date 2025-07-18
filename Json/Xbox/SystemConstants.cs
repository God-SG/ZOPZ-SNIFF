using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class SystemConstants
    {
        [JsonPropertyName("capabilities")]
        public Capabilities Capabilities { get; set; } = new Capabilities();

        [JsonPropertyName("cloudComputePackage")]
        public CloudComputePackage CloudComputePackage { get; set; } = new CloudComputePackage();

        [JsonPropertyName("inactiveRemovalTimeout")]
        public int InactiveRemovalTimeout { get; set; } = 0;

        [JsonPropertyName("inviteProtocol")]
        public string InviteProtocol { get; set; } = string.Empty;

        [JsonPropertyName("maxMembersCount")]
        public int MaxMembersCount { get; set; } = 0;

        [JsonPropertyName("readyRemovalTimeout")]
        public int ReadyRemovalTimeout { get; set; } = 0;

        [JsonPropertyName("reservedRemovalTimeout")]
        public int ReservedRemovalTimeout { get; set; } = 0;

        [JsonPropertyName("sessionEmptyTimeout")]
        public int SessionEmptyTimeout { get; set; } = 0;

        [JsonPropertyName("version")]
        public int Version { get; set; } = 0;

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; } = string.Empty ;
    }
}
