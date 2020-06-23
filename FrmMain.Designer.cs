namespace Project.Core.Protector
{
	partial class FrmMain
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.OpInfo = new System.Windows.Forms.ListView();
			this.Date = new System.Windows.Forms.ColumnHeader();
			this.TargetIp = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// OpInfo
			// 
			this.OpInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Date,
            this.TargetIp});
			this.OpInfo.HideSelection = false;
			this.OpInfo.Location = new System.Drawing.Point(5, 5);
			this.OpInfo.Name = "OpInfo";
			this.OpInfo.Size = new System.Drawing.Size(792, 441);
			this.OpInfo.TabIndex = 0;
			this.OpInfo.UseCompatibleStateImageBehavior = false;
			this.OpInfo.View = System.Windows.Forms.View.Details;
			// 
			// Date
			// 
			this.Date.Name = "Date";
			this.Date.Text = "日期";
			this.Date.Width = 180;
			// 
			// TargetIp
			// 
			this.TargetIp.Name = "TargetIp";
			this.TargetIp.Text = "目标";
			this.TargetIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TargetIp.Width = 240;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.OpInfo);
			this.Name = "FrmMain";
			this.Text = "Main";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView OpInfo;
		private System.Windows.Forms.ColumnHeader Date;
		private System.Windows.Forms.ColumnHeader TargetIp;
	}
}

