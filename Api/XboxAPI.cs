using System.Net.Http;
using System.Text;
using System.Text.Json;
using ZOPZ_SNIFF.Json.Xbox;

namespace ZOPZ_SNIFF.Api
{
    public class XboxAPI
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _token;

        public XboxAPI(string apikey)
        {
            _token = apikey;
            _client.DefaultRequestHeaders.Add("Authorization", _token);
        }


        public async Task<string> GetSessionInfo(string xuid)
        {
            string json = JsonSerializer.Serialize(new
            {
                global = true,
                owners = new
                {
                    xuids = new string[1]
                    {
                        xuid
                    }
                },
                type = "activity"
            });
            _client.DefaultRequestHeaders.Add("x-xbl-contract-version", "107");
            HttpResponseMessage response = await _client.PostAsync("https://sessiondirectory.xboxlive.com/handles/query?include=relatedInfo&followed=true", new StringContent(json));
            _client.DefaultRequestHeaders.Remove("x-xbl-contract-version");
            string resp = await response.Content.ReadAsStringAsync();
            File.WriteAllText("SessionInfo.json", resp);
            return resp;
        }


        public async Task<string> GetGameLobby(Guid guid)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://sessiondirectory.xboxlive.com/handles/{guid}/session?nocommit=true");
            string resp = await response.Content.ReadAsStringAsync();
            File.WriteAllText("GameLobby.json", resp);
            return resp;
        }


        public async Task<Root?> GetProfile()
        {
            _client.DefaultRequestHeaders.Add("x-xbl-contract-version", "2");
            HttpResponseMessage response = await _client.GetAsync("https://profile.xboxlive.com/users/me/profile/settings?settings=Gamertag");
            _client.DefaultRequestHeaders.Remove("x-xbl-contract-version");
            string raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Root>(raw);
        }

        public async Task<Zopz?> PartyStatusRequest(string? userID)
        {
            string? json = JsonSerializer.Serialize(new
            {
                token = _token,
                xuid = userID
            });
            HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/xbox/party/get", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string raw = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Zopz>(raw);
            }
            return new Zopz();
        }

        public async Task<List<Member>?> GetPartyMembers(string? userId)
        {
            string? json = JsonSerializer.Serialize(new
            {
                token = _token,
                xuid = userId
            });
            HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/xbox/party/get", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string raw = await response.Content.ReadAsStringAsync();
                Zopz? zopz = JsonSerializer.Deserialize<Zopz>(raw);
                return zopz?.Data?.Members?.Values?.ToList();
            }
            return new List<Member>();
        }

        public async Task<Zopz?> Unkickable(string userID)
        {
            string? json = JsonSerializer.Serialize(new
            {
                token = _token,
                xuid = userID
            });
            HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/xbox/party/nokick/", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string raw = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Zopz>(raw);
            }
            return new Zopz();
        }

        public async Task<Zopz?> OpenPartyRequest(string userID)
        {
            string? json = JsonSerializer.Serialize(new
            {
                token = _token,
                xuid = userID
            });
            HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/xbox/party/open/", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string raw = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Zopz>(raw);
            }
            return new Zopz();
        }

        public async Task<Zopz?> ClosePartyRequest(string userID)
        {
            string? json = JsonSerializer.Serialize(new
            {
                token = _token,
                xuid = userID
            });
            HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/xbox/party/close/", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string raw = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Zopz>(raw);
            }
            return new Zopz();
        }

        public async Task<Zopz?> Crashpartyhost(string? userID)
        {
            string? json = JsonSerializer.Serialize(new
            {
                token = _token,
                xuid = userID
            });
            HttpResponseMessage response = await _client.PostAsync("https://zopzsniff.xyz/api/xbox/party/hostcrash/", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string raw = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Zopz>(raw);
            }
            return new Zopz();
        }

        public async Task<string> SendFeedback(string? targetXuid, string? _textReason)
        {
            string? json = JsonSerializer.Serialize(new
            {
                feedbackType = "CommsVoiceMessage",
                textReason = _textReason,
                evidenceId = (string?)null,
                feedbackContext = "User"
            });
            _client.DefaultRequestHeaders.Add("x-xbl-contract-version", "101");
            HttpResponseMessage response = await _client.PostAsync($"https://reputation.xboxlive.com/users/xuid({targetXuid})/feedback", new StringContent(json, Encoding.UTF8, "application/json"));
            _client.DefaultRequestHeaders.Remove("x-xbl-contract-version");
            return response.IsSuccessStatusCode ? "Feedback sent successfully!" : $"Failed to send feedback. Status code: {response.StatusCode}";
        }
    }
}
