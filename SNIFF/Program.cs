using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SNIFF;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		if (IsApplicationAlreadyRunning())
		{
			MessageAlert.Show("ZOPZ SNIFF", "ZOPZ SNIFF Is Already Running");
			Application.Exit();
		}
		else
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new SplashForm());
		}
	}

	private static bool IsApplicationAlreadyRunning()
	{
		return Process.GetProcesses().Count((Process p) => p.ProcessName.Contains(Assembly.GetExecutingAssembly().FullName.Split(',')[0]) && !p.Modules[0].FileName.Contains("vshost")) > 1;
	}
}
