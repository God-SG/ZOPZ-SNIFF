using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using ZOPZ_SNIFF.Json.Rec;

namespace ZOPZ_SNIFF.Api
{
    public class RecRoom
    {
        public static async Task<SearchInfo?> GetAccountInfoFromUsername(HttpClient _httpClient, string username)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://zopzsniff.xyz/accounts/{username}");
            if (!response.IsSuccessStatusCode)
                return new SearchInfo();
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SearchInfo>(raw);
        }

        public static async Task<string[]> GetTokens()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://zopzsniff.xyz/assets/zopzfiles/tokens.txt");
                if (!response.IsSuccessStatusCode)
                    return new string[0];
                string raw = await response.Content.ReadAsStringAsync();
                return raw.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static async Task<string> ReportUser(string _token, ulong playerId, int category, string details)
        {
            string json = JsonSerializer.Serialize(new
            {
                ReportCategory = category,
                PlayerIdReported = playerId.ToString(),
                RoomId = "0",
                Details = details
            });
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
                HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/accounts/apis/api/PlayerReporting/v1/internal/create", new StringContent(json, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode ? $"Report sent successfully." : $"Failed to send report. Status code: {response.StatusCode}.";
            }
        }

        public static async Task<string> ReportUserImage(string _token, string? imageId, string reportDetails)
        {
            string json = JsonSerializer.Serialize(new
            {
                ReportCategory = 1,
                ReportDetails = reportDetails
            });
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
                HttpResponseMessage response = await _client.PostAsync($"https://zopzsniff.xyz/api/images/v2/{imageId}/report", new StringContent(json, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode ? $"Report sent successfully." : $"Failed to send report. Status code: {response.StatusCode}.";
            }
        }

        public static async Task<string> SubscribeToUser(string _token, ulong userId)
        {
            string json = JsonSerializer.Serialize(new
            {
                userId
            });
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
                HttpResponseMessage response = await _client.PostAsync($"https://zopzsniff.xyz/subscription/{userId}", new StringContent(json, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode ? $"Successfully subscribed to user ID: {userId}." : $"Failed to subscribe to user ID: {userId}. Status Code: {response.StatusCode}.";
            }
        }

        public static async Task<string> SendCheer(string _token, string? token, int? playerId)
        {
            string json = JsonSerializer.Serialize(new
            {
                CheerCategory = 1,
                PlayerIdTo = playerId
            });
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
                HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/PlayerCheer/v1/create", new StringContent(json, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode ? $"Successfully sent cheer for Player ID: {playerId}" : $"Failed to cheer for Player ID: {playerId}. Status Code: {response.StatusCode}";
            }
        }

        public static async Task<int?> GetAccountId(HttpClient _httpClient, string username)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://zopzsniff.xyz/accountId/{username}");
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccountInfo>(raw)?.accountId;
        }

        public static async Task<List<Relationships>?> GetAccountData(HttpClient _httpClient)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://zopzsniff.xyz/api/relationships/v2/get");
            if (!response.IsSuccessStatusCode)
                return new List<Relationships>();
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Relationships[]>(raw)?.ToList();
        }

        public static async Task<List<AccountInfo>?> GetBulkAccountInfo(HttpClient _httpClient, long[] ids)
        {
            string queryString = string.Join("&", ids.Select(id => $"id={id}"));
            HttpResponseMessage response = await _httpClient.GetAsync($"https://zopzsniff.xyz/account/bulk?{queryString}");
            if (!response.IsSuccessStatusCode)
                return new List<AccountInfo>();
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccountInfo[]>(jsonString)?.ToList();
        }

        public static async Task<ModerationBlockDetails?> GetModeratioDetails(HttpClient _httpClient)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://zopzsniff.xyz/api/PlayerReporting/v1/moderationBlockDetails");
            if (!response.IsSuccessStatusCode)
                return new ModerationBlockDetails();
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ModerationBlockDetails>(raw);
        }

        public static async Task<MyInfo?> FetchAndDisplayUserProfile(HttpClient _httpClient)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://zopzsniff.xyz/account/me");
            if (!response.IsSuccessStatusCode)
                return new MyInfo();
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MyInfo>(raw);
        }

        public static async Task<PlayerBio?> GetPlayerBio(HttpClient _httpClient, string accountId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://zopzsniff.xyz/accounts/account/bio/{accountId}/bio");
            if (!response.IsSuccessStatusCode)
                return new PlayerBio();
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PlayerBio>(raw);
        }

        public static async Task<string> UpdateMyBio(HttpClient _httpClient, string newBio)
        {
            FormUrlEncodedContent postParams = new FormUrlEncodedContent(new Dictionary<string, string> { { "bio", newBio } });
            HttpResponseMessage response = await _httpClient.PostAsync("https://zopzsniff.xyz/account/me/bio", postParams);
            return response.IsSuccessStatusCode ? "Bio updated successfully." : $"Failed to update bio with status code: {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}";
        }

        public static async Task<string> AddRelationship(string _token, string accountId)
        {
            string json = JsonSerializer.Serialize(new
            {
                PlayerID = accountId,
                RelationshipType = 1,
                Favorited = 0,
                Muted = 0,
                Ignored = 0
            });
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
                HttpResponseMessage response = await _client.PostAsync($"https://zopzsniff.xyz/api/relationships/v3/{accountId}", new StringContent(json, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode ? $"Added Relationship successfully." : $"Failed to add relationship. Error code: {response.StatusCode}";
            }
        }

        public static async Task<PlayerInstance?> GetPlayerInstanceData(HttpClient _httpClient, int? accountId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://zopzsniff.xyz/instance/{accountId}");
            string raw = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? JsonSerializer.Deserialize<PlayerInstance>(raw) : new PlayerInstance();
        }

        public static async Task<List<ChildAccount>?> GetChildrenAccountInfo(HttpClient _httpClient)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://zopzsniff.xyz/account/me/children");
            string raw = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<ChildAccount>>(raw) : new List<ChildAccount>();
        }

    }
}
