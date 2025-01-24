using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using SNIFF.Classes.Auth;
using SNIFF.Classes.Auth.Models;

namespace SNIFF;

internal class Global
{
	public static readonly CacheManager<string, GeolocationResponse> GeoCacheManage;

	public static readonly CacheManager<string, byte[]> FlagCacheManage;

	public static Version Version { get; }

	public static HttpClient Client { get; }

	public static string ZopzIP { get; }

	public static string HardwareIdentifer { get; private set; }

	public static AuthUserData AuthedUser { get; set; }

	public static List<Label> Labels { get; set; }

	static Global()
	{
		Version = new Version(4, 2, 0, 0);
		HardwareIdentifer = SystemGuid.GetHwid();
        //Authenticator = new Authentication(new Requester("https://partyhax.club/"));
        //Authenticator = new Authentication(new Requester("https://lolzopzsniff.xyz/api"));
		GeoCacheManage = new CacheManager<string, GeolocationResponse>("geocache.db");
		FlagCacheManage = new CacheManager<string, byte[]>("flagcache.db");
		Client = new HttpClient((HttpMessageHandler)new HttpClientHandler
		{
			ServerCertificateCustomValidationCallback = (HttpRequestMessage HttpRequestMessage, X509Certificate2 X509Certificate2, X509Chain X509Chain, SslPolicyErrors SslPolicyErrors) => true,
			AllowAutoRedirect = true,
			SslProtocols = SslProtocols.Tls12
		});
	}

	public static bool IsIPv6Enabled()
	{
		string ipv6Address = "2606:4700:4700::1111";
		try
		{
			using Ping ping = new Ping();
			if (ping.Send(ipv6Address).Status == IPStatus.Success)
			{
				return true;
			}
		}
		catch (PingException ex)
		{
			Console.WriteLine("Ping failed: " + ex.Message);
		}
		return false;
	}
}
