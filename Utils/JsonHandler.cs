using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Utils
{
    public class JsonHandler
    {
        private static readonly JsonSerializerOptions settings = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static bool IsJsonValid(string data) => data.StartsWith("{") && data.EndsWith("}");
        public static string Serialize(object? data) => JsonSerializer.Serialize(data, settings);
        public static T? Deserialize<T>(string data) => JsonSerializer.Deserialize<T>(data);
    }
}
