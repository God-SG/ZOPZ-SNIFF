using System;
using System.IO;
using Newtonsoft.Json;

namespace SNIFF;

public static class SettingsManager
{
	private static string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF");

	private static string jsonpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "settings.json");

	private static SettingsModel settings = null;

	public static void Save(this SettingsModel model)
	{
		if (!Directory.Exists(datapath))
		{
			Directory.CreateDirectory(datapath);
		}
		settings = model;
		string contents = JsonConvert.SerializeObject(model, Formatting.Indented);
		File.WriteAllText(jsonpath, contents);
	}

	public static SettingsModel Load()
	{
		if (settings != null)
		{
			return settings;
		}
		if (!Directory.Exists(datapath))
		{
			Directory.CreateDirectory(datapath);
		}
		if (!File.Exists(jsonpath))
		{
			new SettingsModel().Save();
		}
		return settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(jsonpath));
	}
}
