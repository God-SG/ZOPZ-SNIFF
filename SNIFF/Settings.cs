using System.Collections.Generic;
using System.IO;

namespace SNIFF;

internal class Settings
{
	public Dictionary<string, string> settings;

	public Settings(string filepath)
	{
		settings = new Dictionary<string, string>();
		readSettings(filepath);
	}

	private void readSettings(string filepath)
	{
		if (!File.Exists(filepath))
		{
			return;
		}
		StreamReader streamReader = new StreamReader(filepath);
		string str = "";
		while ((str = streamReader.ReadLine()) != null)
		{
			string[] strArrays = str.Split(' ');
			if (strArrays.Length > 1)
			{
				settings.Add(strArrays[0], strArrays[1]);
			}
		}
		streamReader.Close();
	}

	public void writeSettings(string fileLocation)
	{
		if (!File.Exists(fileLocation))
		{
			File.Create(fileLocation);
		}
		StreamWriter streamWriter = new StreamWriter(fileLocation);
		foreach (string key in settings.Keys)
		{
			streamWriter.WriteLine(key + " " + settings[key]);
		}
		streamWriter.Close();
	}
}
