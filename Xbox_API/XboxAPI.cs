using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xbox_API.Models;

namespace Xbox_API;

public class XboxAPI
{
	private static readonly Random _rnd = new Random();

	private readonly HttpClient _client = new HttpClient();

	private readonly string _apikey;

	public XboxAPI(string apikey)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		_apikey = apikey;
		((HttpHeaders)_client.DefaultRequestHeaders).Add("Authorization", apikey);
		((HttpHeaders)_client.DefaultRequestHeaders).Add("x-xbl-contract-version", "107");
		((HttpHeaders)_client.DefaultRequestHeaders).Add("Accept-Language", "json");
	}

	public async Task<GameData> GetGameDataAsync(Guid name, Guid scid, string templateName)
	{
		string json = JsonConvert.SerializeObject(new
		{
			sessionRef = new { name, scid, templateName },
			type = "activity",
			version = 1
		});
		return GameData.FromJson(await (await _client.PostAsync("https://sessiondirectory.xboxlive.com/handles", (HttpContent)new StringContent(json))).Content.ReadAsStringAsync());
	}

	public async Task<Sessions> GetSessionInfo(string xuid)
	{
		string json = JsonConvert.SerializeObject(new
		{
			global = true,
			owners = new
			{
				xuids = new string[1] { xuid }
			},
			type = "activity"
		});
		return Sessions.FromJson(await (await _client.PostAsync("https://sessiondirectory.xboxlive.com/handles/query?include=relatedInfo&followed=true", (HttpContent)new StringContent(json))).Content.ReadAsStringAsync());
	}

	public async Task<GameLobby> GetGameLobbyAsync(Guid guid)
	{
		return GameLobby.FromJson(await (await _client.GetAsync($"https://sessiondirectory.xboxlive.com/handles/{guid}/session?nocommit=true")).Content.ReadAsStringAsync());
	}

	public async Task<Profile> GetProfileAsync()
	{
		((HttpHeaders)_client.DefaultRequestHeaders).Remove("x-xbl-contract-version");
		((HttpHeaders)_client.DefaultRequestHeaders).Add("x-xbl-contract-version", "2");
		HttpResponseMessage obj = await _client.GetAsync("https://profile.xboxlive.com/users/me/profile/settings?settings=Gamertag");
		obj.EnsureSuccessStatusCode();
		((HttpHeaders)_client.DefaultRequestHeaders).Remove("x-xbl-contract-version");
		((HttpHeaders)_client.DefaultRequestHeaders).Add("x-xbl-contract-version", "107");
		return Profile.FromJson(await obj.Content.ReadAsStringAsync());
	}

	public async Task<bool> SpoofIP(Guid handle, string ip, ushort port)
	{
		string json = JsonConvert.SerializeObject(new
		{
			members = new
			{
				me = new
				{
					properties = new
					{
						system = new
						{
							secureDeviceAddress = GetSecureDeviceAddress(ip, port)
						}
					}
				}
			}
		});
		return (await _client.PutAsync($"https://sessiondirectory.xboxlive.com/handles/{handle}/session", (HttpContent)new StringContent(json))).IsSuccessStatusCode;
	}

	public async Task<bool> SessionKill(Guid handle)
	{
		string json = JsonConvert.SerializeObject(new
		{
			members = new
			{
				me = new
				{
					properties = new
					{
						system = new
						{
							active = false
						}
					}
				}
			}
		});
		return (await _client.PutAsync($"https://sessiondirectory.xboxlive.com/handles/{handle}/session", (HttpContent)new StringContent(json))).IsSuccessStatusCode;
	}

	private static string GetSecureDeviceAddress(string ipAddress, ushort port)
	{
		byte[] secureDeviceAddressBuffer = new byte[39];
		_rnd.NextBytes(secureDeviceAddressBuffer);
		Array.Copy(new byte[2] { 1, 0 }, 0, secureDeviceAddressBuffer, 0, 2);
		Array.Copy(new byte[4] { 32, 1, 0, 0 }, 0, secureDeviceAddressBuffer, 19, 4);
		int[] portIndexes = PortBytes(port);
		int[] ipAddressArray = (from n in ipAddress.Split('.')
			select Convert.ToInt32(n)).ToArray();
		for (int i = 0; i < ipAddressArray.Length; i++)
		{
			if (i < 2)
			{
				secureDeviceAddressBuffer[30 - i] = Convert.ToByte(portIndexes[i]);
			}
			secureDeviceAddressBuffer[31 + i] = Convert.ToByte(255 - ipAddressArray[i]);
		}
		return Convert.ToBase64String(secureDeviceAddressBuffer);
	}

	private static int[] PortBytes(int port)
	{
		int offset = port / 256;
		int n1 = 255 - (port - offset * 256);
		int n2 = 255 - offset;
		return new int[2] { n1, n2 };
	}
}
