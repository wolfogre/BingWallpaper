using CountlySDK;
using System;
using System.Windows.Forms;

namespace BingWallpaper
{
	public partial class MainForm : Form
	{
		PaperGetter paperGetter;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			paperGetter = new PaperGetter(notifyIcon);
			Countly.RecordEvent("Start");
			paperGetter.Start();
		}

		private void tsmiRetry_Click(object sender, EventArgs e)
		{
			Countly.RecordEvent("Retry");
			paperGetter.Start();
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			Countly.RecordEvent("About");
			System.Diagnostics.Process.Start("http://blog.wolfogre.com/2016/04/11/280.html");
		}

		private void tsmiQuit_Click(object sender, EventArgs e)
		{
			Countly.RecordEvent("Quit");
			paperGetter.Abort();
			Application.Exit();
		}
	}
}
