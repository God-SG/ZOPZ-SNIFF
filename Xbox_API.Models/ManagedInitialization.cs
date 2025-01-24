using Newtonsoft.Json;

namespace Xbox_API.Models;

public class ManagedInitialization
{
	[JsonProperty("membersNeededToStart", NullValueHandling = NullValueHandling.Ignore)]
	public long? MembersNeededToStart { get; set; }

	[JsonProperty("joinTimeout", NullValueHandling = NullValueHandling.Ignore)]
	public long? JoinTimeout { get; set; }

	[JsonProperty("measurementTimeout", NullValueHandling = NullValueHandling.Ignore)]
	public long? MeasurementTimeout { get; set; }

	[JsonProperty("autoEvaluate", NullValueHandling = NullValueHandling.Ignore)]
	public bool? AutoEvaluate { get; set; }
}
