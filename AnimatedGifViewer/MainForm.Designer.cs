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
			// SizeButton
			// 
			this.SizeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.SizeButton.BackColor = System.Drawing.SystemColors.Control;
			this.SizeButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Size;
			this.SizeButton.FlatAppearance.BorderSize = 0;
			this.SizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
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
			this.FitSizeButton.BackColor = System.Drawing.SystemColors.Control;
			this.FitSizeButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_FitToWindow;
			this.FitSizeButton.FlatAppearance.BorderSize = 0;
			this.FitSizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
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
			this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
			this.DeleteButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_Delete;
			this.DeleteButton.FlatAppearance.BorderSize = 0;
			this.DeleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
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
			this.RotateClockwiseButton.BackColor = System.Drawing.SystemColors.Control;
			this.RotateClockwiseButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_RotateClockwise;
			this.RotateClockwiseButton.FlatAppearance.BorderSize = 0;
			this.RotateClockwiseButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.RotateClockwiseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RotateClockwiseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.RotateClockwiseButton.Location = new System.Drawing.Point(471, 365);
			this.RotateClockwiseButton.Margin = new System.Windows.Forms.Padding(0);
			this.RotateClockwiseButton.Name = "RotateClockwiseButton";
			this.RotateClockwiseButton.Size = new System.Drawing.Size(25, 25);
			this.RotateClockwiseButton.TabIndex = 6;
			this.RotateClockwiseButton.UseVisualStyleBackColor = false;
			// 
			// RotateCounterButton
			// 
			this.RotateCounterButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.RotateCounterButton.BackColor = System.Drawing.SystemColors.Control;
			this.RotateCounterButton.BackgroundImage = global::AnimatedGifViewer.Properties.Resources.Button_RotateCounter;
			this.RotateCounterButton.FlatAppearance.BorderSize = 0;
			this.RotateCounterButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.RotateCounterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RotateCounterButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.RotateCounterButton.Location = new System.Drawing.Point(437, 365);
			this.RotateCounterButton.Margin = new System.Windows.Forms.Padding(0);
			this.RotateCounterButton.Name = "RotateCounterButton";
			this.RotateCounterButton.Size = new System.Drawing.Size(25, 25);
			this.RotateCounterButton.TabIndex = 5;
			this.RotateCounterButton.UseVisualStyleBackColor = false;
			// 
			// FullScreenButton
			// 
			this.FullScreenButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.FullScreenButton.BackColor = System.Drawing.SystemColors.Control;
			this.FullScreenButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FullScreenButton.BackgroundImage")));
			this.FullScreenButton.FlatAppearance.BorderSize = 0;
			this.FullScreenButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
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
			this.NextButton.BackColor = System.Drawing.SystemColors.Control;
			this.NextButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NextButton.BackgroundImage")));
			this.NextButton.FlatAppearance.BorderSize = 0;
			this.NextButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
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
			this.PrevButton.BackColor = System.Drawing.SystemColors.Control;
			this.PrevButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PrevButton.BackgroundImage")));
			this.PrevButton.FlatAppearance.BorderSize = 0;
			this.PrevButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
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
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(674, 410);
			this.Controls.Add(this.SizeButton);
			this.Controls.Add(this.FitSizeButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.RotateClockwiseButton);
			this.Controls.Add(this.RotateCounterButton);
			this.Controls.Add(this.FullScreenButton);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.PrevButton);
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
		private System.Windows.Forms.ToolStripMenuItem OpenInMenuItem;
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
	}
}

