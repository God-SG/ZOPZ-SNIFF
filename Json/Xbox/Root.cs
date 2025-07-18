using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class Root
    {
        [JsonPropertyName("profileUsers")]
        public List<ProfileUser> ProfileUsers { get; set; } = new List<ProfileUser>();
    }
}
