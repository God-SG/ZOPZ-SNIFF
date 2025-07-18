using System.Diagnostics;
using System.Reflection;
using ZOPZ_SNIFF.Auth;
using ZOPZ_SNIFF.Config;
using ZOPZ_SNIFF.Forms;
using ZOPZ_SNIFF.Menus;
using ZOPZ_SNIFF.Utils;

namespace ZOPZ_SNIFF
{
    public class Program
    {
        public static ZopzApi api = new ZopzApi();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (IsApplicationAlreadyRunning())
            {
                MessageAlert.Show("Alert", "ZOPZ SNIFF Is Already Running");
                Application.Exit();
            }
            else
            {
                FileHandler.CheckDirectories();
                ApplicationConfiguration.Initialize();
                Configuration.LoadConfigurations();
                Application.Run(new SplashForm());
            }
        }


        private static bool IsApplicationAlreadyRunning()
        {
            string? currentProcessName = Assembly.GetExecutingAssembly().GetName().Name;
            return Process.GetProcesses().Count(p => p.ProcessName.Equals(currentProcessName, StringComparison.OrdinalIgnoreCase) && !p.MainModule!.FileName.Contains("vshost")) > 1;
        }
    }
}