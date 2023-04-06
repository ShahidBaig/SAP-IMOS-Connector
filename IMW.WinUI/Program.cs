using Microsoft.Extensions.Configuration;
using IMW.Common;

namespace IMW.WinUI
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.

			var builder = new ConfigurationBuilder();

			builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			AppConfiguration.Configuration = builder.Build();

			ApplicationConfiguration.Initialize();
            Application.Run(new frmMainScreen());
        }
    }
}