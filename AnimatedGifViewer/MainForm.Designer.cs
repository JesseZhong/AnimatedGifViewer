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
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MakeCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.PropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FullScreenButton = new System.Windows.Forms.Button();
			this.NextButton = new System.Windows.Forms.Button();
			this.PrevButton = new System.Windows.Forms.Button();
			this.ImageBox = new System.Windows.Forms.PictureBox();
			this.MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.BackColor = System.Drawing.SystemColors.Window;
			this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.OpenInMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(674, 24);
			this.MenuStrip.TabIndex = 4;
			this.MenuStrip.Text = "menuStrip1";
			// 
			// FileMenuItem
			// 
			this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.MenuItemSeparator3,
            this.DeleteMenuItem,
            this.MakeCopyMenuItem,
            this.MenuItemSeparator2,
            this.CopyMenuItem,
            this.MenuItemSeparator1,
            this.PropertiesMenuItem,
            this.MenuItemSeparator,
            this.ExitMenuItem});
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileMenuItem.Text = "File";
			// 
			// OpenMenuItem
			// 
			this.OpenMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.folder_open;
			this.OpenMenuItem.Name = "OpenMenuItem";
			this.OpenMenuItem.Size = new System.Drawing.Size(150, 22);
			this.OpenMenuItem.Text = "Open";
			this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
			// 
			// MenuItemSeparator3
			// 
			this.MenuItemSeparator3.Name = "MenuItemSeparator3";
			this.MenuItemSeparator3.Size = new System.Drawing.Size(147, 6);
			// 
			// DeleteMenuItem
			// 
			this.DeleteMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.delete;
			this.DeleteMenuItem.Name = "DeleteMenuItem";
			this.DeleteMenuItem.Size = new System.Drawing.Size(150, 22);
			this.DeleteMenuItem.Text = "Delete";
			this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
			// 
			// MakeCopyMenuItem
			// 
			this.MakeCopyMenuItem.Name = "MakeCopyMenuItem";
			this.MakeCopyMenuItem.Size = new System.Drawing.Size(150, 22);
			this.MakeCopyMenuItem.Text = "Make a Copy...";
			// 
			// MenuItemSeparator2
			// 
			this.MenuItemSeparator2.Name = "MenuItemSeparator2";
			this.MenuItemSeparator2.Size = new System.Drawing.Size(147, 6);
			// 
			// CopyMenuItem
			// 
			this.CopyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyMenuItem.Image")));
			this.CopyMenuItem.Name = "CopyMenuItem";
			this.CopyMenuItem.Size = new System.Drawing.Size(150, 22);
			this.CopyMenuItem.Text = "Copy";
			// 
			// MenuItemSeparator1
			// 
			this.MenuItemSeparator1.Name = "MenuItemSeparator1";
			this.MenuItemSeparator1.Size = new System.Drawing.Size(147, 6);
			// 
			// PropertiesMenuItem
			// 
			this.PropertiesMenuItem.Name = "PropertiesMenuItem";
			this.PropertiesMenuItem.Size = new System.Drawing.Size(150, 22);
			this.PropertiesMenuItem.Text = "Properties";
			// 
			// MenuItemSeparator
			// 
			this.MenuItemSeparator.Name = "MenuItemSeparator";
			this.MenuItemSeparator.Size = new System.Drawing.Size(147, 6);
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Name = "ExitMenuItem";
			this.ExitMenuItem.Size = new System.Drawing.Size(150, 22);
			this.ExitMenuItem.Text = "Exit";
			// 
			// OpenInMenuItem
			// 
			this.OpenInMenuItem.Name = "OpenInMenuItem";
			this.OpenInMenuItem.Size = new System.Drawing.Size(60, 20);
			this.OpenInMenuItem.Text = "Open In";
			// 
			// FullScreenButton
			// 
			this.FullScreenButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.FullScreenButton.BackColor = System.Drawing.SystemColors.Control;
			this.FullScreenButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_FullScreen;
			this.FullScreenButton.FlatAppearance.BorderSize = 0;
			this.FullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.FullScreenButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.FullScreenButton.Location = new System.Drawing.Point(315, 363);
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
			this.NextButton.Location = new System.Drawing.Point(359, 376);
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
			this.PrevButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PrevButton.Location = new System.Drawing.Point(264, 375);
			this.PrevButton.Margin = new System.Windows.Forms.Padding(0);
			this.PrevButton.Name = "PrevButton";
			this.PrevButton.Size = new System.Drawing.Size(51, 25);
			this.PrevButton.TabIndex = 1;
			this.PrevButton.UseVisualStyleBackColor = false;
			this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
			// 
			// ImageBox
			// 
			this.ImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ImageBox.BackColor = System.Drawing.SystemColors.Window;
			this.ImageBox.Location = new System.Drawing.Point(0, 24);
			this.ImageBox.Margin = new System.Windows.Forms.Padding(0);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(676, 339);
			this.ImageBox.TabIndex = 0;
			this.ImageBox.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(674, 410);
			this.Controls.Add(this.FullScreenButton);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.PrevButton);
			this.Controls.Add(this.ImageBox);
			this.Controls.Add(this.MenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStrip;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.TransparencyKey = System.Drawing.SystemColors.Control;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox ImageBox;
		private System.Windows.Forms.Button PrevButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button FullScreenButton;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PropertiesMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenInMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator1;
		private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MakeCopyMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator2;
		private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator3;
	}
}

