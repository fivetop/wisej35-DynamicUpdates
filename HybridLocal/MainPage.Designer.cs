using System;

namespace HybridLocal
{
	partial class MainPage
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Wisej Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.progressBar1 = new Wisej.Web.ProgressBar();
			this.labelVersion = new Wisej.Web.Label();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = Wisej.Web.AnchorStyles.None;
			this.progressBar1.Location = new System.Drawing.Point(656, 304);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(472, 49);
			this.progressBar1.TabIndex = 0;
			this.progressBar1.Text = "Checking for Updates...";
			this.progressBar1.Value = 5;
			// 
			// labelVersion
			// 
			this.labelVersion.Anchor = Wisej.Web.AnchorStyles.None;
			this.labelVersion.Location = new System.Drawing.Point(656, 376);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(472, 18);
			this.labelVersion.TabIndex = 1;
			this.labelVersion.Text = "Current Version: ";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
			this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.progressBar1);
			this.Name = "MainPage";
			this.Size = new System.Drawing.Size(1785, 656);
			this.Text = "Page1";
			this.Load += new System.EventHandler(this.Page1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Wisej.Web.ProgressBar progressBar1;
		private Wisej.Web.Label labelVersion;
	}
}

