namespace AnimatedGifViewer {
	partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.FullScreenButton = new System.Windows.Forms.Button();
			this.NextButton = new System.Windows.Forms.Button();
			this.PrevButton = new System.Windows.Forms.Button();
			this.PictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// FullScreenButton
			// 
			this.FullScreenButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.FullScreenButton.BackColor = System.Drawing.SystemColors.Control;
			this.FullScreenButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_FullScreen;
			this.FullScreenButton.FlatAppearance.BorderSize = 0;
			this.FullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.FullScreenButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.FullScreenButton.Location = new System.Drawing.Point(184, 258);
			this.FullScreenButton.Margin = new System.Windows.Forms.Padding(0);
			this.FullScreenButton.Name = "FullScreenButton";
			this.FullScreenButton.Size = new System.Drawing.Size(44, 48);
			this.FullScreenButton.TabIndex = 3;
			this.FullScreenButton.UseVisualStyleBackColor = false;
			this.FullScreenButton.Click += new System.EventHandler(this.FullScreenButton_Click);
			// 
			// NextButton
			// 
			this.NextButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.NextButton.BackColor = System.Drawing.SystemColors.Control;
			this.NextButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Next;
			this.NextButton.FlatAppearance.BorderSize = 0;
			this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.NextButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.NextButton.Location = new System.Drawing.Point(228, 271);
			this.NextButton.Margin = new System.Windows.Forms.Padding(0);
			this.NextButton.Name = "NextButton";
			this.NextButton.Size = new System.Drawing.Size(51, 25);
			this.NextButton.TabIndex = 2;
			this.NextButton.UseVisualStyleBackColor = false;
			this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
			// 
			// PrevButton
			// 
			this.PrevButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.PrevButton.BackColor = System.Drawing.SystemColors.Control;
			this.PrevButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Previous;
			this.PrevButton.FlatAppearance.BorderSize = 0;
			this.PrevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PrevButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.PrevButton.Location = new System.Drawing.Point(133, 270);
			this.PrevButton.Margin = new System.Windows.Forms.Padding(0);
			this.PrevButton.Name = "PrevButton";
			this.PrevButton.Size = new System.Drawing.Size(51, 25);
			this.PrevButton.TabIndex = 1;
			this.PrevButton.UseVisualStyleBackColor = false;
			this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
			// 
			// PictureBox
			// 
			this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PictureBox.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox.Location = new System.Drawing.Point(0, -1);
			this.PictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(415, 258);
			this.PictureBox.TabIndex = 0;
			this.PictureBox.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(413, 305);
			this.Controls.Add(this.FullScreenButton);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.PrevButton);
			this.Controls.Add(this.PictureBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox PictureBox;
		private System.Windows.Forms.Button PrevButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button FullScreenButton;
	}
}

