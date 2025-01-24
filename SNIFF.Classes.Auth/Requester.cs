using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SNIFF.Classes.Auth.Interfaces;

namespace SNIFF.Classes.Auth;

internal class Requester : IRequester
{
	private readonly HttpClient _client;

	private readonly string _baseUrl;

	public Requester(string baseUrl)
	{
		
		_client = new HttpClient((HttpMessageHandler)new HttpClientHandler());
		if (!baseUrl.EndsWith("/"))
		{
			baseUrl += "/";
		}
		_baseUrl = baseUrl;
	}

	public async Task<T> SendRequestAsync<T>(HttpMethod method, string url, object postData = null) where T : class
	{
		HttpRequestMessage request = new HttpRequestMessage(method, _baseUrl + url);
		string jsonBody = JsonConvert.SerializeObject(postData);
		if (postData != null)
		{
			//request.Content = (HttpContent)new StringContent(jsonBody);
		}
		HttpResponseMessage response = await _client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			//return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
		}
		if (response.StatusCode < HttpStatusCode.InternalServerError)
		{
			//return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
		}
		throw new Exception("Could not get response");
	}

	public void AddAuthorizationHeader(string value)
	{
		((HttpHeaders)_client.DefaultRequestHeaders).Remove("Authorization");
		((HttpHeaders)_client.DefaultRequestHeaders).TryAddWithoutValidation("Authorization", value);
	}
}
