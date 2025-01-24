using System;
using Newtonsoft.Json;

namespace Xbox_API.Models;

public class SessionRef
{
	[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("scid", NullValueHandling = NullValueHandling.Ignore)]
	public Guid Scid { get; set; }

	[JsonProperty("templateName", NullValueHandling = NullValueHandling.Ignore)]
	public string TemplateName { get; set; }
}
