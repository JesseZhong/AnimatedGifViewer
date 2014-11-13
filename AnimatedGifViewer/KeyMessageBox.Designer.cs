namespace AnimatedGifViewer {
	partial class KeyMessageBox {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.Message = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Message
			// 
			this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Message.BackColor = System.Drawing.SystemColors.ControlLight;
			this.Message.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Message.Enabled = false;
			this.Message.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Message.Location = new System.Drawing.Point(12, 12);
			this.Message.Name = "Message";
			this.Message.ReadOnly = true;
			this.Message.Size = new System.Drawing.Size(199, 13);
			this.Message.TabIndex = 0;
			this.Message.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// KeyMessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(223, 59);
			this.Controls.Add(this.Message);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "KeyMessageBox";
			this.Text = "KeyMessageBox";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Message;

	}
}