using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using ZOPZ_SNIFF.Json.Info;
using ZOPZ_SNIFF.Utils;

namespace ZOPZ_SNIFF.Api
{
    public class Info
    {
        public static async Task<IpInfo?> GetAddressInfo(string ip_address)
        {
            if (!IPAddress.TryParse(ip_address, out IPAddress? parsedIp))
            {
                throw new ArgumentException($"The IP address '{ip_address}' is invalid.");
            }
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _httpClient.GetAsync($"https://cyberwarden.cc/info?address={ip_address}");
                string raw = await response.Content.ReadAsStringAsync();
                return JsonHandler.IsJsonValid(raw) ? JsonSerializer.Deserialize<IpInfo>(raw) : new IpInfo();
            }
        }
    }
}
