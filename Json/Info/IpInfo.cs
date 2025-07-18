using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Info
{
    public class IpInfo
    {
        [JsonPropertyName("asn")]
        public string Asn { get; set; } = string.Empty;

        [JsonPropertyName("provider")]
        public string Provider { get; set; } = string.Empty;

        [JsonPropertyName("organisation")]
        public string Organisation { get; set; } = string.Empty;

        [JsonPropertyName("continent")]
        public string Continent { get; set; } = string.Empty;

        [JsonPropertyName("continentcode")]
        public string ContinentCode { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("isocode")]
        public string IsoCode { get; set; } = string.Empty;

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("regioncode")]
        public string RegionCode { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; } = string.Empty;

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; } = double.MaxValue;

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; } = double.MaxValue;

        [JsonPropertyName("proxy")]
        public string Proxy { get; set; } = string.Empty;

        [JsonPropertyName("vpn")]
        public string Vpn { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public string? Error { get; set; } = string.Empty;
    }
}
