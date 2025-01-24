using Newtonsoft.Json;

namespace Xbox_API.Models;

public static class Serialize
{
	public static string ToJson(this GameLobby self)
	{
		return JsonConvert.SerializeObject(self, Converter.Settings);
	}
}
