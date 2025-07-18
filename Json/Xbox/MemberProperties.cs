using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class MemberProperties
    {
        [JsonPropertyName("custom")]
        public CustomMemberProperties Custom { get; set; } = new CustomMemberProperties();

        [JsonPropertyName("system")]
        public SystemMemberProperties System { get; set; } = new SystemMemberProperties();
    }
}
