// ImageBoxMenu.cs
// Authored by Jesse Z. Zhong
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace AnimatedGifViewer {
	public class ImageBoxMenu : System.Windows.Forms.ContextMenuStrip {

		#region Members
		public System.Windows.Forms.ToolStripMenuItem OpenWithMenuItem;			// Open With
		private System.Windows.Forms.ToolStripSeparator Separator0;				// ---------------
		public System.Windows.Forms.ToolStripMenuItem SetAsDesktopMenuItem;		// Set as Desktop Background
		private System.Windows.Forms.ToolStripSeparator Separator1;				// ---------------
		public System.Windows.Forms.ToolStripMenuItem OpenLocationMenuItem;		// Open File Location
		private System.Windows.Forms.ToolStripSeparator Separator2;				// ---------------
		public System.Windows.Forms.ToolStripMenuItem RotateClockwiseMenuItem;	// Rotate Clockwise
		public System.Windows.Forms.ToolStripMenuItem RotateCounterCMenuItem;	// Rotate Counterclockwise
		private System.Windows.Forms.ToolStripSeparator Separator3;				// ---------------
		public System.Windows.Forms.ToolStripMenuItem CopyMenuItem;				// Copy
		public System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;			// Delete
		private System.Windows.Forms.ToolStripSeparator Separator4;				// ---------------
		public System.Windows.Forms.ToolStripMenuItem PropertiesMenuItem;		// Properties
		#endregion

		#region Instance
		/// <summary>
		/// Initializes all the components.
		/// </summary>
		public ImageBoxMenu() : base() {
			this.InitializeComponent();
		}

		/// <summary>
		/// Initializes the components of the right click menu.
		/// </summary>
		private void InitializeComponent() {
			this.OpenWithMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SetAsDesktopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenLocationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RotateClockwiseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RotateCounterCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Separator0 = new System.Windows.Forms.ToolStripSeparator();
			this.Separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.Separator2 = new System.Windows.Forms.ToolStripSeparator();
			this.Separator3 = new System.Windows.Forms.ToolStripSeparator();
			this.Separator4 = new System.Windows.Forms.ToolStripSeparator();
			this.SuspendLayout();

			// ClickMenu
			this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.OpenWithMenuItem,
				this.Separator0,
				this.OpenLocationMenuItem,
				this.Separator1,
				this.SetAsDesktopMenuItem,
				this.Separator2,
				this.RotateClockwiseMenuItem,
				this.RotateCounterCMenuItem,
				this.Separator3,
				this.CopyMenuItem,
				this.DeleteMenuItem,
				this.Separator4,
				this.PropertiesMenuItem
			});

			// OpenWithMenuItem
			this.OpenWithMenuItem.Name = "OpenWithMenuItem";
			this.OpenWithMenuItem.Size = new System.Drawing.Size(156, 26);
			this.OpenWithMenuItem.Text = "Open With";

			// Separator0
			this.Separator0.Name = "Separator0";
			this.Separator0.Size = new System.Drawing.Size(153, 6);

			// SetAsDesktopMenuItem
			this.SetAsDesktopMenuItem.Name = "SetAsDesktopMenuItem";
			this.SetAsDesktopMenuItem.Size = new System.Drawing.Size(156, 26);
			this.SetAsDesktopMenuItem.Text = "Set as Desktop Background";

			// Separator1
			this.Separator1.Name = "Separator1";
			this.Separator1.Size = new System.Drawing.Size(153, 6);

			// OpenLocationMenuItem
			this.OpenLocationMenuItem.Name = "OpenLocationMenuItem";
			this.OpenLocationMenuItem.Size = new System.Drawing.Size(156, 26);
			this.OpenLocationMenuItem.Text = "Open File Location";

			// Separator2
			this.Separator2.Name = "Separator2";
			this.Separator2.Size = new System.Drawing.Size(153, 6);

			// RotateClockwiseMenuItem
			this.RotateClockwiseMenuItem.Name = "RotateClockwiseMenuItem";
			this.RotateClockwiseMenuItem.Size = new System.Drawing.Size(156, 26);
			this.RotateClockwiseMenuItem.Text = "Rotate Clockwise";

			// RotateCounterCMenuItem
			this.RotateCounterCMenuItem.Name = "RotateCounterCMenuItem";
			this.RotateCounterCMenuItem.Size = new System.Drawing.Size(156, 26);
			this.RotateCounterCMenuItem.Text = "Rotate Counterclockwise";

			// Separator3
			this.Separator3.Name = "Separator3";
			this.Separator3.Size = new System.Drawing.Size(153, 6);

			// CopyMenuItem
			this.CopyMenuItem.Name = "CopyMenuItem";
			this.CopyMenuItem.Size = new System.Drawing.Size(156, 26);
			this.CopyMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.Menu_Copy;
			this.CopyMenuItem.Text = "Copy";

			// DeleteMenuItem
			this.DeleteMenuItem.Name = "DeleteMenuItem";
			this.DeleteMenuItem.Size = new System.Drawing.Size(156, 26);
			this.DeleteMenuItem.Image = global::AnimatedGifViewer.Properties.Resources.Menu_Delete;
			this.DeleteMenuItem.Text = "Delete";

			// Separator4
			this.Separator4.Name = "Separator4";
			this.Separator4.Size = new System.Drawing.Size(153, 6);

			// DeleteMenuItem
			this.PropertiesMenuItem.Name = "PropertiesMenuItem";
			this.PropertiesMenuItem.Size = new System.Drawing.Size(156, 26);
			this.PropertiesMenuItem.Text = "Properties";

			// ImageBoxMenu
			this.Name = "ImageBoxMenu";
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
		}
		#endregion
	}
}
