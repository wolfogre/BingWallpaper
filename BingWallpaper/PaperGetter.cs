using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Diagnostics;

namespace BingWallpaper
{
	class PaperGetter
	{

		Thread backgroundThread;
		NotifyIcon notifyIcon;
		string bingUrl = "https://www.bing.com";
		string wallpaperPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\BingWallpaper\";

		const uint SPI_GETDESKWALLPAPER = 0x0073;
		const uint SPI_SETDESKWALLPAPER = 0x0014;
		const uint MAX_PATH = 1000;

		[DllImport("user32.dll")]
		static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

		public PaperGetter(NotifyIcon notifyIcon)
		{
			this.notifyIcon = notifyIcon;
		}

		public void Start()
		{
			if (backgroundThread != null && backgroundThread.IsAlive)
				backgroundThread.Abort();
			backgroundThread = new Thread(new ThreadStart(BackgroundWork));
			backgroundThread.Start();
		}

		public void Abort()
		{
			if (backgroundThread != null && backgroundThread.IsAlive)
				backgroundThread.Abort();
		}

		private void BackgroundWork()
		{
			Debug.Assert(notifyIcon.ContextMenuStrip.Items.Find("tsmiRetry", false).Length == 1);
			notifyIcon.ContextMenuStrip.Items.Find("tsmiRetry", false)[0].Enabled = false;

			notifyIcon.Text = "准备获取壁纸...";
			Thread.Sleep(10 * 1000);

			notifyIcon.Text = "正在获取壁纸...";
			
			if (!Directory.Exists(wallpaperPath))
				Directory.CreateDirectory(wallpaperPath);

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(bingUrl);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				MatchCollection matchs = Regex.Matches(reader.ReadToEnd(), "g_img={url:'.*?'");
				response.Close();

				if (matchs.Count != 1)
					throw new Exception("提取壁纸URL出错！程序可能永久失效！");

				string wallpaperUrl = bingUrl + matchs[0].Value.Substring("g_img={url:'".Length, matchs[0].Value.Length - "g_img={url:'".Length - "'".Length);
				string wallpaperName = wallpaperUrl.Substring(wallpaperUrl.LastIndexOf("/") + 1);
				string newWallpaper = wallpaperPath + wallpaperName;

				if (!File.Exists(newWallpaper))
				{
					request = (HttpWebRequest)WebRequest.Create(wallpaperUrl);
					response = (HttpWebResponse)request.GetResponse();
					FileStream fStream = new FileStream(newWallpaper, FileMode.Create);
					response.GetResponseStream().CopyTo(fStream);
					response.Close();
					fStream.Close();
				}

				StringBuilder nowWallpaper = new StringBuilder((int)MAX_PATH);
				SystemParametersInfo(SPI_GETDESKWALLPAPER, MAX_PATH, nowWallpaper, 0);

				if (nowWallpaper.ToString() == newWallpaper)
				{
					notifyIcon.ShowBalloonTip(10, "Bing壁纸", "已经是最新壁纸", ToolTipIcon.Info);
					notifyIcon.Text = "准备退出...";
					Thread.Sleep(15 * 1000);
					Application.Exit();
					return;
				}

				SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, new StringBuilder(newWallpaper), 0);
			}
			catch (Exception ex)
			{
				notifyIcon.Text = "Bing壁纸";
				notifyIcon.ShowBalloonTip(10, "Bing壁纸", "获取壁纸失败:" + ex.Message, ToolTipIcon.Error);
				notifyIcon.ContextMenuStrip.Items.Find("tsmiRetry", false)[0].Enabled = true;
				return;
			}

			notifyIcon.ShowBalloonTip(10, "Bing壁纸", "成功更换壁纸", ToolTipIcon.Info);
			notifyIcon.Text = "准备退出...";
			Thread.Sleep(15 * 1000);
			Application.Exit();
		}
	}
}
