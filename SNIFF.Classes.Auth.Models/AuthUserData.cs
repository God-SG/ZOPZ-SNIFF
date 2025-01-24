using System;
using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

public class AuthUserData
{
	[JsonProperty("Username")]
	public string Username { get; set; }

	[JsonProperty("Level")]
	public int Level { get; set; }

	[JsonProperty("Imported")]
	public bool Imported { get; set; }

	[JsonProperty("Created")]
	public DateTimeOffset Created { get; set; }

	[JsonProperty("Expiry")]
	public DateTimeOffset expiry { get; set; }

	[JsonProperty("LastLogin")]
	public DateTimeOffset LastLogin { get; set; }

	[JsonProperty("Programs")]
	public Program[] Programs { get; set; }

	public string GetRankLabel()
	{
		return Level switch
		{
			0 => "Normal", 
			1 => "Reseller", 
			2 => "Admin", 
			_ => "Not Found", 
		};
	}
}
