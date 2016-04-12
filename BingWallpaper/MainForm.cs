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
			paperGetter.Start();
		}

		private void tsmiRetry_Click(object sender, EventArgs e)
		{
			paperGetter.Start();
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://blog.wolfogre.com/2016/04/11/280.html");
		}

		private void tsmiQuit_Click(object sender, EventArgs e)
		{
			paperGetter.Abort();
			Application.Exit();
		}
	}
}
