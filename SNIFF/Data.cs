using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNIFF;

public class Data
{
	public static async Task<string> Download(string url)
	{
		HttpClient client = new HttpClient();
		try
		{
			return await client.GetStringAsync(url);
		}
		finally
		{
			((IDisposable)client)?.Dispose();
		}
	}
}
