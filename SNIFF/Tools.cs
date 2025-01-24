using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SNIFF;

public class Tools
{
	public class VersionCheck
	{
		[JsonProperty("version")]
		public Version Version { get; set; }

		[JsonProperty("force_update")]
		public bool ForceUpdate { get; set; }
	}

	public static async Task<bool> CheckForUpdate()
	{
		// auto update
		/*
		VersionCheck version = await Global.Client.GetJsonAsync<VersionCheck>("https://assets.lolzopzsniff.xyz/snifferversion.json");
		if (version.Version > Global.Version)
		{
			if (!version.ForceUpdate && MessageBox.Show("An update for zopz sniff has been found do you want to update", "UPDATE FOUND", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return false;
			}
			byte[] installer = await Global.Client.GetByteArrayAsync("https://lolzopzsniff.xyz/assets/zopzfiles/ZOPZ_SNIFF.exe");
			string text = Path.GetTempFileName() + ".exe";
			File.WriteAllBytes(text, installer);
			Process.Start(text);
			return true;
		}
		*/
		return false;
		
	}
}
