using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Auth
{
    public class ProgramStatistics
    {
        [JsonPropertyName("zopzsniff")]
        public Zopzsniff? Zopzsniff { get; set; }
    }
}
