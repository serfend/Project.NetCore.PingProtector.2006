using NETworkManager.Settings;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Project.Core.Protector
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (!AutostartManager.IsEnabled) AutostartManager.EnableAsync();
			var main = new Main();
			while (true)
			{
				Thread.Sleep(1000);
			};
		}
	}
}