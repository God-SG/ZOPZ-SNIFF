using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SNIFF.Classes.Auth.Models;

namespace SNIFF;

internal class IPLocator
{
	private static readonly HttpClient Client = new HttpClient();

	private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();

	public static string LookupUsernameAsync(string IPAddress)
	{
		try
		{
			return (from x in Global.Labels
				where x.Value == IPAddress
				select x.Name).FirstOrDefault();
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred: " + ex.Message);
			return null;
		}
	}

	public async Task<string[]> IPLocationAsync(string IP)
	{
		try
		{
			if (Global.GeoCacheManage.TryGetCachedResponse(IP, out var val))
			{
				return new string[6]
				{
					val.City,
					val.Region,
					val.country_name,
					val.Isp,
					val.Hosting.ToString(),
					val.Timezone
				};
			}
			GeolocationResponse geoLocation = JsonConvert.DeserializeObject<GeolocationResponse>(await new HttpClient().GetStringAsync("https://json.geoiplookup.io/" + IP));
			Global.GeoCacheManage.CacheResponse(IP, geoLocation);
			Global.GeoCacheManage.SaveCacheToFile();
			if (geoLocation != null)
			{
				return new string[6]
				{
					geoLocation.City,
					geoLocation.Region,
					geoLocation.country_name,
					geoLocation.Isp,
					geoLocation.Hosting.ToString(),
					geoLocation.Timezone
				};
			}
			return new string[8] { "", "", "", "", "", "", "", "" };
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred: " + ex.Message);
			return new string[8] { "", "", "", "", "", "", "", "" };
		}
	}

	private static void CacheResult(string key, string result)
	{
		Cache[key] = result;
	}
}
