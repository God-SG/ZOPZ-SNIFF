using System;
using Newtonsoft.Json;

namespace Xbox_API.Models;

internal class FluffyParseStringConverter : JsonConverter
{
	public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();

	public override bool CanConvert(Type t)
	{
		if (!(t == typeof(bool)))
		{
			return t == typeof(bool?);
		}
		return true;
	}

	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}
		if (bool.TryParse(serializer.Deserialize<string>(reader), out var b))
		{
			return b;
		}
		throw new Exception("Cannot unmarshal type bool");
	}

	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	{
		if (untypedValue == null)
		{
			serializer.Serialize(writer, null);
			return;
		}
		string boolString = (((bool)untypedValue) ? "true" : "false");
		serializer.Serialize(writer, boolString);
	}
}
