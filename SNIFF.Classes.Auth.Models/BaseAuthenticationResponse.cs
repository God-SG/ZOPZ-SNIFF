using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

public class BaseAuthenticationResponse<T>
{
	[JsonProperty("success")]
	public bool Success { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }

	[JsonProperty("data")]
	public T Data { get; set; }
}
