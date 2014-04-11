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
			this.OpenWithMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SizeButton = new System.Windows.Forms.Button();
			this.FitSizeButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.RotateClockwiseButton = new System.Windows.Forms.Button();
			this.RotateCounterButton = new System.Windows.Forms.Button();
			this.FullScreenButton = new System.Windows.Forms.Button();
			this.NextButton = new System.Windows.Forms.Button();
			this.PrevButton = new System.Windows.Forms.Button();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.BackColor = System.Drawing.SystemColors.ControlDark;
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.OpenWithMenuItem,
            this.AboutMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
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
			this.FileMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(36, 20);
			this.FileMenuItem.Text = "File";
			// 
			// OpenMenuItem
			// 
			this.OpenMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.folder_open;
			this.OpenMenuItem.Name = "OpenMenuItem";
			this.OpenMenuItem.Size = new System.Drawing.Size(141, 22);
			this.OpenMenuItem.Text = "Open";
			this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
			// 
			// MenuItemSeparator3
			// 
			this.MenuItemSeparator3.Name = "MenuItemSeparator3";
			this.MenuItemSeparator3.Size = new System.Drawing.Size(138, 6);
			// 
			// DeleteMenuItem
			// 
			this.DeleteMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.Menu_Delete;
			this.DeleteMenuItem.Name = "DeleteMenuItem";
			this.DeleteMenuItem.Size = new System.Drawing.Size(141, 22);
			this.DeleteMenuItem.Text = "Delete";
			this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
			// 
			// MakeCopyMenuItem
			// 
			this.MakeCopyMenuItem.Name = "MakeCopyMenuItem";
			this.MakeCopyMenuItem.Size = new System.Drawing.Size(141, 22);
			this.MakeCopyMenuItem.Text = "Make a Copy...";
			this.MakeCopyMenuItem.Click += new System.EventHandler(this.MakeCopyMenuItem_Click);
			// 
			// MenuItemSeparator2
			// 
			this.MenuItemSeparator2.Name = "MenuItemSeparator2";
			this.MenuItemSeparator2.Size = new System.Drawing.Size(138, 6);
			// 
			// CopyMenuItem
			// 
			this.CopyMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.Menu_Copy;
			this.CopyMenuItem.Name = "CopyMenuItem";
			this.CopyMenuItem.Size = new System.Drawing.Size(141, 22);
			this.CopyMenuItem.Text = "Copy";
			this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
			// 
			// MenuItemSeparator1
			// 
			this.MenuItemSeparator1.Name = "MenuItemSeparator1";
			this.MenuItemSeparator1.Size = new System.Drawing.Size(138, 6);
			// 
			// PropertiesMenuItem
			// 
			this.PropertiesMenuItem.Name = "PropertiesMenuItem";
			this.PropertiesMenuItem.Size = new System.Drawing.Size(141, 22);
			this.PropertiesMenuItem.Text = "Properties";
			this.PropertiesMenuItem.Click += new System.EventHandler(this.PropertiesMenuItem_Click);
			// 
			// MenuItemSeparator
			// 
			this.MenuItemSeparator.Name = "MenuItemSeparator";
			this.MenuItemSeparator.Size = new System.Drawing.Size(138, 6);
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Name = "ExitMenuItem";
			this.ExitMenuItem.Size = new System.Drawing.Size(141, 22);
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// OpenWithMenuItem
			// 
			this.OpenWithMenuItem.Name = "OpenWithMenuItem";
			this.OpenWithMenuItem.Size = new System.Drawing.Size(69, 20);
			this.OpenWithMenuItem.Text = "Open With";
			// 
			// AboutMenuItem
			// 
			this.AboutMenuItem.Name = "AboutMenuItem";
			this.AboutMenuItem.Size = new System.Drawing.Size(47, 20);
			this.AboutMenuItem.Text = "About";
			this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
			// 
			// SizeButton
			// 
			this.SizeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.SizeButton.BackColor = System.Drawing.Color.Transparent;
			this.SizeButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Size;
			this.SizeButton.FlatAppearance.BorderSize = 0;
			this.SizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SizeButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.SizeButton.Location = new System.Drawing.Point(151, 365);
			this.SizeButton.Margin = new System.Windows.Forms.Padding(0);
			this.SizeButton.Name = "SizeButton";
			this.SizeButton.Size = new System.Drawing.Size(37, 25);
			this.SizeButton.TabIndex = 9;
			this.SizeButton.UseVisualStyleBackColor = false;
			// 
			// FitSizeButton
			// 
			this.FitSizeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.FitSizeButton.BackColor = System.Drawing.Color.Transparent;
			this.FitSizeButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_FitToWindow;
			this.FitSizeButton.FlatAppearance.BorderSize = 0;
			this.FitSizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.FitSizeButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.FitSizeButton.Location = new System.Drawing.Point(212, 365);
			this.FitSizeButton.Margin = new System.Windows.Forms.Padding(0);
			this.FitSizeButton.Name = "FitSizeButton";
			this.FitSizeButton.Size = new System.Drawing.Size(27, 25);
			this.FitSizeButton.TabIndex = 8;
			this.FitSizeButton.UseVisualStyleBackColor = false;
			// 
			// DeleteButton
			// 
			this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.DeleteButton.BackColor = System.Drawing.Color.Transparent;
			this.DeleteButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Delete;
			this.DeleteButton.FlatAppearance.BorderSize = 0;
			this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.DeleteButton.Location = new System.Drawing.Point(512, 365);
			this.DeleteButton.Margin = new System.Windows.Forms.Padding(0);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(25, 25);
			this.DeleteButton.TabIndex = 7;
			this.DeleteButton.UseVisualStyleBackColor = false;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// RotateClockwiseButton
			// 
			this.RotateClockwiseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.RotateClockwiseButton.BackColor = System.Drawing.Color.Transparent;
			this.RotateClockwiseButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_RotateClockwise;
			this.RotateClockwiseButton.FlatAppearance.BorderSize = 0;
			this.RotateClockwiseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RotateClockwiseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.RotateClockwiseButton.Location = new System.Drawing.Point(471, 365);
			this.RotateClockwiseButton.Margin = new System.Windows.Forms.Padding(0);
			this.RotateClockwiseButton.Name = "RotateClockwiseButton";
			this.RotateClockwiseButton.Size = new System.Drawing.Size(25, 25);
			this.RotateClockwiseButton.TabIndex = 6;
			this.RotateClockwiseButton.UseVisualStyleBackColor = false;
			this.RotateClockwiseButton.Click += new System.EventHandler(this.RotateClockwiseButton_Click);
			// 
			// RotateCounterButton
			// 
			this.RotateCounterButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.RotateCounterButton.BackColor = System.Drawing.Color.Transparent;
			this.RotateCounterButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_RotateCounter;
			this.RotateCounterButton.FlatAppearance.BorderSize = 0;
			this.RotateCounterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RotateCounterButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.RotateCounterButton.Location = new System.Drawing.Point(437, 365);
			this.RotateCounterButton.Margin = new System.Windows.Forms.Padding(0);
			this.RotateCounterButton.Name = "RotateCounterButton";
			this.RotateCounterButton.Size = new System.Drawing.Size(25, 25);
			this.RotateCounterButton.TabIndex = 5;
			this.RotateCounterButton.UseVisualStyleBackColor = false;
			this.RotateCounterButton.Click += new System.EventHandler(this.RotateCounterButton_Click);
			// 
			// FullScreenButton
			// 
			this.FullScreenButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.FullScreenButton.BackColor = System.Drawing.Color.Transparent;
			this.FullScreenButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_FullScreen;
			this.FullScreenButton.FlatAppearance.BorderSize = 0;
			this.FullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.FullScreenButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FullScreenButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.FullScreenButton.Location = new System.Drawing.Point(320, 353);
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
			this.NextButton.BackColor = System.Drawing.Color.Transparent;
			this.NextButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Next;
			this.NextButton.FlatAppearance.BorderSize = 0;
			this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.NextButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.NextButton.Location = new System.Drawing.Point(364, 365);
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
			this.PrevButton.BackColor = System.Drawing.Color.Transparent;
			this.PrevButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Previous;
			this.PrevButton.FlatAppearance.BorderSize = 0;
			this.PrevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PrevButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PrevButton.Location = new System.Drawing.Point(269, 365);
			this.PrevButton.Margin = new System.Windows.Forms.Padding(0);
			this.PrevButton.Name = "PrevButton";
			this.PrevButton.Size = new System.Drawing.Size(51, 25);
			this.PrevButton.TabIndex = 1;
			this.PrevButton.UseVisualStyleBackColor = false;
			this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(674, 410);
			this.Controls.Add(this.MenuStrip);
			this.Controls.Add(this.SizeButton);
			this.Controls.Add(this.FitSizeButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.RotateClockwiseButton);
			this.Controls.Add(this.RotateCounterButton);
			this.Controls.Add(this.FullScreenButton);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.PrevButton);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStrip;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PrevButton;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button FullScreenButton;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PropertiesMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenWithMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator1;
		private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MakeCopyMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator2;
		private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
		private System.Windows.Forms.ToolStripSeparator MenuItemSeparator3;
		private System.Windows.Forms.Button RotateCounterButton;
		private System.Windows.Forms.Button RotateClockwiseButton;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button FitSizeButton;
		private System.Windows.Forms.Button SizeButton;
		private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
	}
}

