using System.Security.Principal;
using System.Text;
using ZOPZ_SNIFF.Json.Auth;
using ZOPZ_SNIFF.Json.Sniffer;
using ZOPZ_SNIFF.Utils;
using Label = ZOPZ_SNIFF.Json.Sniffer.Label;

namespace ZOPZ_SNIFF.Auth
{
    public class ZopzApi
    {
        public CustomRpc? customRpc { get; set; }
        public AuthResponse? data { get; set; }
        public Statistics? statistics { get; set; }
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public List<LabelData> Labels { get; set; } = new List<LabelData>();

        public ZopzApi()
        {
            customRpc = new CustomRpc();
        }

        public async Task<AuthResponse?> Login(string user, string pass)
        {
            string responce = await MakeRequest(RequestType.Post, "api/auth", JsonHandler.Serialize(new
            {
                hardwareIdentifier = WindowsIdentity.GetCurrent().User?.Value ?? "No Hwid Found",
                username = user,
                password = pass,
                program = "zopzsniff"
            }));
            AuthResponse? authResponse = JsonHandler.Deserialize<AuthResponse>(responce);
            data = authResponse;
            return authResponse;
        }

        public async void LoadFilters()
        {
            string responce = await MakeRequest(RequestType.Get, "assets/zopzfiles/onlinefilters.json");
            Filters = JsonHandler.Deserialize<List<Filter>>(responce)!;
        }

        public async void LoadStats()
        {
            string responce = await MakeRequest(RequestType.Get, "api/stats");
            statistics = JsonHandler.Deserialize<Statistics>(responce);
        }

        public async void LoadLableDatas()
        {
            string responce = await MakeRequest(RequestType.Get, "api/label/list");
            Labels = JsonHandler.Deserialize<Label>(responce)?.Data?.ToList()!;
        }

        public async void CreateLableData(LabelData data)
        {
            await MakeRequest(RequestType.Post, "api/label", JsonHandler.Serialize(data));
        }

        private async Task<string> MakeRequest(RequestType type, string path, string? postdata = null)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent? content = postdata != null ? new StringContent(postdata, Encoding.UTF8, "application/json") : null;
                HttpResponseMessage httpResponseMessage = type switch
                {
                    RequestType.Get => await client.GetAsync($"https://zopzsniff.xyz/{path}"),
                    RequestType.Post => await client.PostAsync($"https://zopzsniff.xyz/{path}", content!),
                    _ => throw new Exception("Method isn't listed")
                };
                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
        }

        public string? LookupUsername(string ipAddress) => Labels?.FirstOrDefault(x => x.Value == ipAddress)?.Name;

        private enum RequestType
        {
            Get,
            Post
        }
    }
}
