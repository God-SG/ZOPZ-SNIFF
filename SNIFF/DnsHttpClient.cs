using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DnsClient;
using DnsClient.Protocol;

namespace SNIFF;

public class DnsHttpClient : HttpClient
{
	public DnsHttpClient(HttpClientHandler handler)
		: base((HttpMessageHandler)(object)handler)
	{
	}

	public async Task<string> GetStringAsync(string url)
	{
		UriBuilder builder = new UriBuilder(url);
		string host = builder.Host;
		IDnsQueryResponse query = new LookupClient(IPAddress.Parse(Global.IsIPv6Enabled() ? "2606:4700:4700::1111" : "1.1.1.1")).Query(new DnsQuestion(builder.Host, (!Global.IsIPv6Enabled()) ? QueryType.A : QueryType.AAAA));
		int randomIndex = new Random().Next(0, query.Answers.Count);
		string ip = ((query.Answers[randomIndex] is AaaaRecord aaaaRecord) ? aaaaRecord.Address.ToString() : (query.Answers[randomIndex] as ARecord)?.Address.ToString());
		builder.Host = ip;
		if (url.StartsWith("https"))
		{
			builder.Port = 443;
		}
		HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
		if (url.StartsWith("https"))
		{
			((HttpHeaders)newRequest.Headers).TryAddWithoutValidation("Host", host);
		}
		else
		{
			((HttpHeaders)newRequest.Headers).TryAddWithoutValidation("Host", host);
		}
		return await (await ((HttpClient)this).SendAsync(newRequest)).Content.ReadAsStringAsync();
	}

	public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
	{
		string host = message.RequestUri.Host;
		IDnsQueryResponse query = new LookupClient(IPAddress.Parse(Global.IsIPv6Enabled() ? "2606:4700:4700::1111" : "1.1.1.1")).Query(new DnsQuestion(host, (!Global.IsIPv6Enabled()) ? QueryType.A : QueryType.AAAA));
		int randomIndex = new Random().Next(0, query.Answers.Count);
		string ip = ((query.Answers[randomIndex] is AaaaRecord aaaaRecord) ? aaaaRecord.Address.ToString() : (query.Answers[randomIndex] as ARecord)?.Address.ToString());
		UriBuilder newUri = new UriBuilder(message.RequestUri);
		newUri.Host = ip;
		message.RequestUri = newUri.Uri;
		((HttpHeaders)message.Headers).TryAddWithoutValidation("Host", host);
		return await ((HttpClient)this).SendAsync(message);
	}
}
