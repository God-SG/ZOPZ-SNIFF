using System.Net.Http;
using System.Threading.Tasks;

namespace SNIFF.Classes.Auth.Interfaces;

internal interface IRequester
{
	void AddAuthorizationHeader(string authToken);

	Task<T> SendRequestAsync<T>(HttpMethod method, string url, object postData = null) where T : class;
}
