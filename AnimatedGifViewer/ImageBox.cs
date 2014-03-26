using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AnimatedGifViewer {
	public class ImageBox : System.Windows.Forms.UserControl {

		#region Members
		private System.Windows.Forms.PictureBox PictureBox;
		private System.Windows.Forms.Panel OuterPanel;
		private System.ComponentModel.Container components = null;
		private bool fitToWindow;
		#endregion

		#region Constants
		private const double ZOOMFACTOR = 1.25;
		private const int MINMAX = 5;
		#endregion

		#region Instance
		/// <summary>
		/// Initializes all the components.
		/// </summary>
		public ImageBox() {
			this.InitializeComponent();

			this.fitToWindow = false;
		}

		/// <summary>
		/// Initializes the components of the image box.
		/// </summary>
		private void InitializeComponent() {
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.OuterPanel = new System.Windows.Forms.Panel();
			this.OuterPanel.SuspendLayout();
			this.SuspendLayout();
			
			// PictureBox
			this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.PictureBox.Location = new System.Drawing.Point(0, 0);
			this.PictureBox.Name = "PicBox";
			this.PictureBox.Size = new System.Drawing.Size(150, 140);
			this.PictureBox.TabIndex = 3;
			this.PictureBox.TabStop = false;

			// OuterPanel
			this.OuterPanel.AutoScroll = false;
			this.OuterPanel.BackColor = System.Drawing.Color.White;
			this.OuterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OuterPanel.Controls.Add(this.PictureBox);
			this.OuterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OuterPanel.Location = new System.Drawing.Point(0, 0);
			this.OuterPanel.Name = "OuterPanel";
			this.OuterPanel.Size = new System.Drawing.Size(this.Width, this.Height);
			this.OuterPanel.TabIndex = 4;
			
			// PictureBox
			this.Controls.Add(this.OuterPanel);
			this.Name = "PictureBox";
			this.Size = new System.Drawing.Size(210, 190);
			this.OuterPanel.ResumeLayout(false);
			this.ResumeLayout(false);

			// Event handlers
			this.OuterPanel.MouseEnter += new EventHandler(this.ImageBox_MouseEnter);
			this.PictureBox.MouseEnter += new EventHandler(this.ImageBox_MouseEnter);
			this.OuterPanel.MouseWheel += new MouseEventHandler(this.ImageBox_MouseWheel);
			this.OuterPanel.Resize += new EventHandler(this.OuterPanel_Resize);
		}

		/// <summary>
		/// Creates a simple red cross as a bitmap and displays it in the picture box.
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
				this.fitToWindow = true;
				this.FitToWindow();
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

		#region Zooming Methods
		/// <summary>
		/// Checks if the picture box goes out of bounds of the outer panel.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool CompareControlSize() {
			if ((this.OuterPanel.Width < this.PictureBox.Image.Width) ||
				(this.OuterPanel.Height < this.PictureBox.Image.Height))
				return true;
			return false;
		}

		/// <summary>
		/// Makes the PictureBox dimensions larger to effect the Zoom.
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
		/// Makes the PictureBox dimensions smaller to effect the Zoom.
		/// </summary>
		/// <remarks>Minimum of 5 times smaller.</remarks>
		private void ZoomOut() {
			if ((this.PictureBox.Width > (this.OuterPanel.Width / MINMAX)) &&
				(this.PictureBox.Height > (this.OuterPanel.Height / MINMAX))) {
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Width / ZOOMFACTOR);
				this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Height / ZOOMFACTOR);
			}
		}

		/// <summary>
		/// If the image is larger than the window, scale it down to fit within the window.
		/// </summary>
		private void FitToWindow() {
			if (this.CompareControlSize()) {
				double widthRatio = this.PictureBox.Image.Width / (double)this.OuterPanel.Width;
				double heightRatio = this.PictureBox.Image.Height / (double)this.OuterPanel.Height;

				if (widthRatio > heightRatio) {
					this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Image.Width / widthRatio);
					this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Image.Height / widthRatio);
				} else {
					this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Image.Width / heightRatio);
					this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Image.Height / heightRatio);
				}

				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			} else {
				this.PictureBox.Width = this.PictureBox.Image.Width;
				this.PictureBox.Height = this.PictureBox.Image.Height;
			}
		}
		#endregion

		#region Mouse Handlers
		/// <summary>
		/// Zoom in or out when the mouse wheel is moved.
		/// If the image is larger than the outer panel,
		/// panning of the image is enabled for the mouse.
		/// </summary>
		/// <param name="sender">ImageBox Controls</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBox_MouseWheel(object sender, MouseEventArgs e) {
			this.fitToWindow = false;
			if (e.Delta < 0)
				this.ZoomOut();
			else
				this.ZoomIn();

			if (this.CompareControlSize()) {
				this.PictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			} else
				this.PictureBox.Cursor = System.Windows.Forms.Cursors.Arrow;
		}

		/// <summary>
		/// Bring the image box into focus when the mouse enters its area.
		/// </summary>
		/// <param name="sender">ImageBox Controls</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBox_MouseEnter(object sender, EventArgs e) {
			if (this.PictureBox.Focused == false)
				this.PictureBox.Focus();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PictureBox_Clicked(object sender, MouseEventArgs e) {
			//this.PictureBox.Cursor = System.Windows.Forms.Cursors.
		}
		#endregion

		#region Control Handler
		/// <summary>
		/// If the image is in "fit to window" state, the
		/// image will be resized on the control's resize event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OuterPanel_Resize(object sender, EventArgs e) {
			if (this.fitToWindow)
				this.FitToWindow();
		}
		#endregion
	}
}
