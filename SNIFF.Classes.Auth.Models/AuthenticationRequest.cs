using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

internal class AuthenticationRequest
{
	[JsonProperty("hardwareIdentifier")]
	public string HardwareIdentifier { get; set; }

	[JsonProperty("password")]
	public string Password { get; set; }

	[JsonProperty("username")]
	public string Username { get; set; }

	[JsonProperty("program")]
	public string Program { get; set; }
}
