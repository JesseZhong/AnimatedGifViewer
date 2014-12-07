namespace ClockworkControls
{
	partial class SystemBrowser
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ListView = new System.Windows.Forms.ListView();
			this.TextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ListView
			// 
			this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListView.Location = new System.Drawing.Point(0, 25);
			this.ListView.Margin = new System.Windows.Forms.Padding(0);
			this.ListView.Name = "ListView";
			this.ListView.Size = new System.Drawing.Size(361, 392);
			this.ListView.TabIndex = 0;
			this.ListView.UseCompatibleStateImageBehavior = false;
			// 
			// TextBox
			// 
			this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBox.Location = new System.Drawing.Point(0, 0);
			this.TextBox.Margin = new System.Windows.Forms.Padding(0);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(361, 20);
			this.TextBox.TabIndex = 1;
			// 
			// SystemBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TextBox);
			this.Controls.Add(this.ListView);
			this.Name = "SystemBrowser";
			this.Size = new System.Drawing.Size(361, 417);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView ListView;
		private System.Windows.Forms.TextBox TextBox;
	}
}
