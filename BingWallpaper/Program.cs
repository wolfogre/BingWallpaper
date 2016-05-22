using CountlySDK;
using System;
using System.IO;
using System.Windows.Forms;

namespace BingWallpaper
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Countly.StartSession("http://countly.wolfogre.com", "bdb2627a3e46353c93c60c566f045cc4d0c99005", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
			Application.Run(new MainForm());
			Countly.EndSession();
			if (Directory.Exists("countly"))
			{
				Directory.Delete("countly", true);
			}
		}
	}
}
