using Newtonsoft.Json;

namespace Xbox_API.Models;

public class SupportedConnection
{
	[JsonProperty("ConnectionType", NullValueHandling = NullValueHandling.Ignore)]
	public long? ConnectionType { get; set; }

	[JsonProperty("HostIpAddress", NullValueHandling = NullValueHandling.Ignore)]
	public string HostIpAddress { get; set; }

	[JsonProperty("HostPort", NullValueHandling = NullValueHandling.Ignore)]
	public long? HostPort { get; set; }

	[JsonProperty("RakNetGUID", NullValueHandling = NullValueHandling.Ignore)]
	public string RakNetGuid { get; set; }
}
