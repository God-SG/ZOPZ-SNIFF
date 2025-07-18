using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class PartyData
    {
        [JsonPropertyName("branch")]
        public string Branch { get; set; } = string.Empty;

        [JsonPropertyName("changeNumber")]
        public int ChangeNumber { get; set; } = 0;

        [JsonPropertyName("constants")]
        public Constants Constants { get; set; } = new Constants();

        [JsonPropertyName("contractVersion")]
        public int ContractVersion { get; set; } = 0;

        [JsonPropertyName("correlationId")]
        public string CorrelationId { get; set; } = string.Empty;

        [JsonPropertyName("members")]
        public Dictionary<string, Member>? Members { get; set; } = new Dictionary<string, Member>();

        [JsonPropertyName("membersInfo")]
        public MembersInfo MembersInfo { get; set; } = new MembersInfo();

        [JsonPropertyName("membersOnly")]
        public MembersOnly MembersOnly { get; set; } = new MembersOnly();

        [JsonPropertyName("properties")]
        public Properties Properties { get; set; } = new Properties();

        [JsonPropertyName("results")]
        public List<Result>? Results { get; set; } = new List<Result>();

        [JsonPropertyName("servers")]
        public object Servers { get; set; } = new object(); // Leaving ass is for now untill neeeded pc ;)

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; } = DateTime.Now;
    }
}
