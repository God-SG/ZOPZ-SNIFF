using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SNIFF.Classes.Auth.Models;

public class ChatMessage
{
	[JsonProperty("message")]
	[JsonPropertyName("message")]
	public string Message { get; set; }

	[JsonProperty("sent")]
	[JsonPropertyName("sent")]
	public DateTime Sent { get; set; }

	[JsonProperty("poster")]
	[JsonPropertyName("poster")]
	public string Poster { get; set; }
}
