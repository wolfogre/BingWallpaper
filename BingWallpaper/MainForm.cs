using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace BingWallpaper
{
	public partial class MainForm : Form
	{
		Thread backgroundThread;

		const uint SPI_GETDESKWALLPAPER = 0x0073;
		const uint SPI_SETDESKWALLPAPER = 0x0014;

		[DllImport("user32.dll")]
		static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			backgroundThread = new Thread(new ThreadStart(BackgroundWork));
			backgroundThread.Start();
		}

		private void tsmiRetry_Click(object sender, EventArgs e)
		{
			if (backgroundThread != null && backgroundThread.IsAlive)
				backgroundThread.Abort();
			backgroundThread = new Thread(new ThreadStart(BackgroundWork));
			backgroundThread.Start();
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://blog.wolfogre.com");
		}

		private void tsmiQuit_Click(object sender, EventArgs e)
		{
			if (backgroundThread != null && backgroundThread.IsAlive)
				backgroundThread.Abort();
			Application.Exit();
		}

		private void BackgroundWork()
		{
			tsmiRetry.Enabled = false;

			notifyIcon.Text = "Bing壁纸:准备获取壁纸...";
			Thread.Sleep(10 * 1000);

			notifyIcon.Text = "Bing壁纸:正在获取壁纸...";
			string bingUrl = "https://www.bing.com";
			string wallpaperPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BingWallpaper\\";
			if (!Directory.Exists(wallpaperPath))
				Directory.CreateDirectory(wallpaperPath);

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(bingUrl);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				MatchCollection matchs = Regex.Matches(reader.ReadToEnd(), "g_img={url:'.*?'");

				if (matchs.Count != 1)
					throw new Exception("提取壁纸URL出错！");

				response.Close();

				string wallpaperUrl = bingUrl + matchs[0].Value.Substring("g_img={url:'".Length, matchs[0].Value.Length - "g_img={url:'".Length - "'".Length);
				string wallpaperName = wallpaperUrl.Substring(wallpaperUrl.LastIndexOf("/") + 1);

				if (File.Exists(wallpaperPath + wallpaperName))
				{
					notifyIcon.ShowBalloonTip(10, "Bing壁纸", "已经是最新壁纸", ToolTipIcon.Info);
					notifyIcon.Text = "Bing壁纸:准备退出...";
					Thread.Sleep(10 * 1000);
					Application.Exit();
					return;
				}

				request = (HttpWebRequest)WebRequest.Create(wallpaperUrl);
				response = (HttpWebResponse)request.GetResponse();

				FileStream fStream = new FileStream(wallpaperPath + wallpaperName, FileMode.Create);
				response.GetResponseStream().CopyTo(fStream);
				response.Close();
				fStream.Close();

				SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, new StringBuilder(wallpaperPath + wallpaperName), 1);
			}
			catch (Exception ex)
			{
				notifyIcon.Text = "Bing壁纸";
				notifyIcon.ShowBalloonTip(10, "Bing壁纸", "获取壁纸失败:" + ex.Message, ToolTipIcon.Error);
				tsmiRetry.Enabled = true;
				return;
			}

			notifyIcon.ShowBalloonTip(10, "Bing壁纸", "成功更换壁纸", ToolTipIcon.Info);
			notifyIcon.Text = "Bing壁纸:准备退出...";
			Thread.Sleep(10 * 1000);
			Application.Exit();
		}
		
	}
}
