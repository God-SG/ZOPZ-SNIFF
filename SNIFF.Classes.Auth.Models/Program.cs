using System;
using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

public class Program
{
	[JsonProperty("Name")]
	public string Name { get; set; }

	[JsonProperty("Expiry")]
	public DateTimeOffset Expiry { get; set; }
}
