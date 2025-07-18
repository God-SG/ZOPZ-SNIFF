using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class ProfileUser
    {
        [JsonPropertyName("hostId")]
        public string HostId { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("isSponsoredUser")]
        public bool IsSponsoredUser { get; set; } = false;

        [JsonPropertyName("settings")]
        public List<Setting> Settings { get; set; } = new List<Setting>();
    }
}
