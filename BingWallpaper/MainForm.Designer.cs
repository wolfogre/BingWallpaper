namespace BingWallpaper
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiRetry = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Bing壁纸";
			this.notifyIcon.Visible = true;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRetry,
            this.tsmiAbout,
            this.tsmiQuit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(153, 92);
			// 
			// tsmiRetry
			// 
			this.tsmiRetry.Image = global::BingWallpaper.Properties.Resources.ic_loop_black_18dp;
			this.tsmiRetry.Name = "tsmiRetry";
			this.tsmiRetry.Size = new System.Drawing.Size(152, 22);
			this.tsmiRetry.Text = "重试";
			// 
			// tsmiAbout
			// 
			this.tsmiAbout.Image = global::BingWallpaper.Properties.Resources.ic_info_outline_black_18dp;
			this.tsmiAbout.Name = "tsmiAbout";
			this.tsmiAbout.Size = new System.Drawing.Size(152, 22);
			this.tsmiAbout.Text = "关于";
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Image = global::BingWallpaper.Properties.Resources.ic_power_settings_new_black_18dp;
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.Size = new System.Drawing.Size(152, 22);
			this.tsmiQuit.Text = "退出";
			this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "Bing壁纸";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem tsmiRetry;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
	}
}

