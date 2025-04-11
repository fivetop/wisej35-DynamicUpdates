namespace HybridDynamic
{
	partial class Page1
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
			this.labelStartup = new Wisej.Web.Label();
			this.progressCircle1 = new Wisej.Web.Ext.ProgressCircle.ProgressCircle();
			this.SuspendLayout();
			// 
			// labelStartup
			// 
			this.labelStartup.Font = new System.Drawing.Font("default", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.labelStartup.Location = new System.Drawing.Point(55, 46);
			this.labelStartup.Name = "labelStartup";
			this.labelStartup.Size = new System.Drawing.Size(237, 53);
			this.labelStartup.TabIndex = 1;
			this.labelStartup.Text = "App is OFFLINE";
			// 
			// progressCircle1
			// 
			this.progressCircle1.Location = new System.Drawing.Point(59, 129);
			this.progressCircle1.Name = "progressCircle1";
			this.progressCircle1.Size = new System.Drawing.Size(282, 260);
			this.progressCircle1.Value = 50;
			// 
			// Page1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
			this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
			this.Controls.Add(this.progressCircle1);
			this.Controls.Add(this.labelStartup);
			this.Name = "Page1";
			this.Size = new System.Drawing.Size(1785, 656);
			this.Text = "Page1";
			this.Load += new System.EventHandler(this.Page1_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private Wisej.Web.Label labelStartup;
		private Wisej.Web.Ext.ProgressCircle.ProgressCircle progressCircle1;
	}
}

