using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace AnimatedGifViewer {
	public class ImageBox : System.Windows.Forms.UserControl {

		private System.Windows.Forms.PictureBox PictureBox;
		private System.Windows.Forms.Panel OuterPanel;
		private System.ComponentModel.Container components = null;

		private double ZOOMFACTOR = 1.25;	// = 25% smaller or larger
		private int MINMAX = 5;				// 5 times bigger or smaller than the ctrl

		#region Work
		/// <summary>
		/// Initializes all the components.
		/// </summary>
		public ImageBox() {
			this.InitializeComponent();
			this.InitCtrl();
		}

		/// <summary>
		/// Initializes the components of the image box.
		/// </summary>
		private void InitializeComponent() {
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.OuterPanel = new System.Windows.Forms.Panel();
			this.OuterPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// PicBox
			// 
			this.PictureBox.Location = new System.Drawing.Point(0, 0);
			this.PictureBox.Name = "PicBox";
			this.PictureBox.Size = new System.Drawing.Size(150, 140);
			this.PictureBox.TabIndex = 3;
			this.PictureBox.TabStop = false;
			// 
			// OuterPanel
			// 
			this.OuterPanel.AutoScroll = false;
			this.OuterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OuterPanel.Controls.Add(this.PictureBox);
			this.OuterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OuterPanel.Location = new System.Drawing.Point(0, 0);
			this.OuterPanel.Name = "OuterPanel";
			this.OuterPanel.Size = new System.Drawing.Size(210, 190);
			this.OuterPanel.TabIndex = 4;
			// 
			// PictureBox
			// 
			this.Controls.Add(this.OuterPanel);
			this.Name = "PictureBox";
			this.Size = new System.Drawing.Size(210, 190);
			this.OuterPanel.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Dispose of the components used within the image box.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Sets or gets the image that will be displayed.
		/// </summary>
		[Browsable(false)]
		public Image Image {
			get { return this.PictureBox.Image; }
			set { 
				this.PictureBox.Image = value;
				this.PictureBox.Size = new System.Drawing.Size(value.Width, value.Height);
			}
		}

		/// <summary>
		/// Sets or gets the size mode for the image box.
		/// </summary>
		[Browsable(false)]
		public PictureBoxSizeMode SizeMode {
			get { return this.PictureBox.SizeMode; }
			set { this.PictureBox.SizeMode = value; }
		}

		/// <summary>
		/// Set or gets the border style for the image box.
		/// </summary>
		[Browsable(false)]
		public BorderStyle Border {
			get { return OuterPanel.BorderStyle; }
			set { OuterPanel.BorderStyle = value; }
		}
		#endregion

		#region Other Methods

		/// <summary>
		/// Special settings for the picturebox ctrl
		/// </summary>
		private void InitCtrl() {
			PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			PictureBox.Location = new Point(0, 0);
			OuterPanel.Dock = DockStyle.Fill;
			//OuterPanel.Cursor = System.Windows.Forms.Cursors.Hand;
			OuterPanel.AutoScroll = false;
			OuterPanel.MouseEnter += new EventHandler(ImageBox_MouseEnter);
			PictureBox.MouseEnter += new EventHandler(ImageBox_MouseEnter);
			this.MouseEnter += new EventHandler(ImageBox_MouseEnter);
			OuterPanel.MouseWheel += new MouseEventHandler(ImageBox_MouseWheel);
		}

		/// <summary>
		/// Creates a simple red cross as a bitmap and display it in the picture box.
		/// </summary>
		private void RedCross() {
			Bitmap bmp = new Bitmap(OuterPanel.Width, OuterPanel.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
			Graphics gr;
			gr = Graphics.FromImage(bmp);
			Pen pencil = new Pen(Color.Red, 5);
			gr.DrawLine(pencil, 0, 0, OuterPanel.Width, OuterPanel.Height);
			gr.DrawLine(pencil, 0, OuterPanel.Height, OuterPanel.Width, 0);
			PictureBox.Image = bmp;
			gr.Dispose();
		}

		#endregion

		#region Zooming Methods

		/// <summary>
		/// Make the PictureBox dimensions larger to effect the Zoom.
		/// </summary>
		/// <remarks>Maximum of 5 times larger.</remarks>
		private void ZoomIn() {
			if ((this.PictureBox.Width < (MINMAX * this.OuterPanel.Width)) &&
				(this.PictureBox.Height < (MINMAX * this.OuterPanel.Height))) {
				this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Width * ZOOMFACTOR);
				this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Height * ZOOMFACTOR);
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			}
		}

		/// <summary>
		/// Make the PictureBox dimensions smaller to effect the Zoom.
		/// </summary>
		/// <remarks>Minimum of 5 times smaller.</remarks>
		private void ZoomOut() {
			if ((this.PictureBox.Width > (this.OuterPanel.Width / MINMAX)) &&
				(this.PictureBox.Height > (this.OuterPanel.Height / MINMAX))) {
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.PictureBox.Width = Convert.ToInt32(PictureBox.Width / ZOOMFACTOR);
				this.PictureBox.Height = Convert.ToInt32(PictureBox.Height / ZOOMFACTOR);
			}
		}

		#endregion

		#region Mouse Handlers
		/// <summary>
		/// Zoom in or out when the mouse wheel is moved.
		/// </summary>
		/// <param name="sender">ImageBox</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBox_MouseWheel(object sender, MouseEventArgs e) {
			if (e.Delta < 0)
				this.ZoomOut();
			else
				this.ZoomIn();
		}

		/// <summary>
		/// Bring the image box into focus when the mouse enters its area.
		/// </summary>
		/// <param name="sender">ImageBox</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBox_MouseEnter(object sender, EventArgs e) {
			if (this.Focused == false)
				this.Focus();
		}
		#endregion
	}
}
