using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PcapDotNet.Packets.IpV4;

namespace SNIFF;

public static class Extensions
{
	public static int GetPacketDestinationPort(this IpV4Datagram packet)
	{
		return packet.Tcp?.DestinationPort ?? packet.Udp?.DestinationPort ?? packet.Udp?.SourcePort ?? packet.Tcp?.SourcePort ?? 0;
	}

	public static bool Contains<T>(this IEnumerable<T> list, IEnumerable<T> sublist)
	{
		T[] listArray = list.ToArray();
		T[] sublistArray = sublist.ToArray();
		if (sublistArray.Length == 0)
		{
			return true;
		}
		if (listArray.Length < sublistArray.Length)
		{
			return false;
		}
		for (int i = 0; i <= listArray.Length - sublistArray.Length; i++)
		{
			if (listArray.Skip(i).Take(sublistArray.Length).SequenceEqual(sublistArray))
			{
				return true;
			}
		}
		return false;
	}

	public static async Task<T> GetJsonAsync<T>(this HttpClient client, string url) where T : class
	{
		return JsonConvert.DeserializeObject<T>(await client.GetStringAsync(url));
	}
}
