using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class MemberConstants
    {
        [JsonPropertyName("custom")]
        public CustomMemberConstants Custom { get; set; } = new CustomMemberConstants();

        [JsonPropertyName("system")]
        public SystemMemberConstants System { get; set; } = new SystemMemberConstants();
    }

}
