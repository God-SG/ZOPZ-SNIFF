using System;
using Newtonsoft.Json;

namespace Xbox_API.Models;

internal class ParseStringConverter : JsonConverter
{
	public static readonly ParseStringConverter Singleton = new ParseStringConverter();

	public override bool CanConvert(Type t)
	{
		if (!(t == typeof(long)))
		{
			return t == typeof(long?);
		}
		return true;
	}

	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}
		if (long.TryParse(serializer.Deserialize<string>(reader), out var l))
		{
			return l;
		}
		throw new Exception("Cannot unmarshal type long");
	}

	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	{
		if (untypedValue == null)
		{
			serializer.Serialize(writer, null);
		}
		else
		{
			serializer.Serialize(writer, ((long)untypedValue).ToString());
		}
	}
}
