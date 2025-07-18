using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class SessionRef
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("scid")]
        public string Scid { get; set; } = string.Empty;

        [JsonPropertyName("templateName")]
        public string TemplateName { get; set; } = string.Empty;
    }
}
