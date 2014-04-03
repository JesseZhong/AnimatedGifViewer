// ImageBox.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
#endregion

namespace AnimatedGifViewer {
	public class ImageBox : System.Windows.Forms.UserControl {

		#region Constants
		private const double ZOOMFACTOR = 1.25;
		private const int MINMAX = 10;
		#endregion

		#region Members
		private System.Windows.Forms.PictureBox PictureBox;
		private System.Windows.Forms.Panel Window;
		public ImageBoxMenu ImageBoxMenu;
		private bool fitToWindow;
		delegate void ImageBoxDelegate();

		private System.Windows.Forms.Cursor PanningHandOpen;
		private System.Windows.Forms.Cursor PanningHandClosed;
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
			this.Window = new System.Windows.Forms.Panel();
			this.ImageBoxMenu = new ImageBoxMenu();
			this.Window.SuspendLayout();
			this.SuspendLayout();
			
			// PictureBox
			this.PictureBox.BorderStyle = BorderStyle.None;
			this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.PictureBox.Location = new System.Drawing.Point(0, 0);
			this.PictureBox.Name = "PicBox";
			this.PictureBox.Size = new System.Drawing.Size(150, 140);
			this.PictureBox.TabIndex = 3;
			this.PictureBox.TabStop = false;

			// ImageBoxMenu
			this.ImageBoxMenu.Name = "ImageBoxMenu";
			this.ImageBoxMenu.Size = new System.Drawing.Size(180, 70);

			// Window
			this.Window.AutoScroll = false;
			this.Window.BackColor = System.Drawing.Color.White;
			this.Window.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Window.Controls.Add(this.PictureBox);
			this.Window.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Window.Location = new System.Drawing.Point(0, 0);
			this.Window.Name = "Window";
			this.Window.Size = new System.Drawing.Size(this.Width, this.Height);
			this.Window.TabIndex = 4;
			
			// ImageBox
			this.Controls.Add(this.Window);
			this.Name = "ImageBox";
			this.Size = new System.Drawing.Size(210, 190);
			this.ContextMenuStrip = this.ImageBoxMenu;
			this.Window.ResumeLayout(false);
			this.ResumeLayout(false);

			// Cursors
			this.PanningHandOpen = this.LoadCursor(global::AnimatedGifViewer.Properties.Resources.Cursor_PanningHand1);
			this.PanningHandClosed = this.LoadCursor(global::AnimatedGifViewer.Properties.Resources.Cursor_PanningHand2);

			// Event handlers
			this.Window.MouseEnter += new EventHandler(this.ImageBox_MouseEnter);
			this.PictureBox.MouseEnter += new EventHandler(this.ImageBox_MouseEnter);
			this.Window.MouseWheel += new MouseEventHandler(this.ImageBox_MouseWheel);
			this.Window.Resize += new EventHandler(this.OuterPanel_Resize);
		}

		/// <summary>
		/// Creates a simple red cross as a bitmap and displays it in the picture box.
		/// </summary>
		private void RedCross() {
			Bitmap bmp = new Bitmap(Window.Width, Window.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
			Graphics gr;
			gr = Graphics.FromImage(bmp);
			Pen pencil = new Pen(Color.Red, 5);
			gr.DrawLine(pencil, 0, 0, Window.Width, Window.Height);
			gr.DrawLine(pencil, 0, Window.Height, Window.Width, 0);
			PictureBox.Image = bmp;
			gr.Dispose();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
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
					this.fitToWindow = true;
					if (value != null)
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
			get { return Window.BorderStyle; }
			set { Window.BorderStyle = value; }
		}
		#endregion

		#region Zooming Methods
		/// <summary>
		/// Checks if the image size exceeds the size of the window.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool IsImageLargerThanWindow() {
			if ((this.Window != null) &&
				(this.PictureBox != null) &&
				(this.PictureBox.Image != null))
					if ((this.Window.Width < this.PictureBox.Image.Width) ||
						(this.Window.Height < this.PictureBox.Image.Height))
						return true;
			return false;
		}

		/// <summary>
		/// Checks if the picture box goes out of size of the window.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool IsPictureLargerThanWindow() {
			if ((this.Window != null) &&
				(this.PictureBox != null))
				if ((this.Window.Width < this.PictureBox.Width) ||
					(this.Window.Height < this.PictureBox.Height))
						return true;
			return false;
		}

		/// <summary>
		/// Checks if the picture box falls within the size of the window.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool IsPictureWithinWindow() {
			if ((this.Window != null) &&
				(this.PictureBox != null))
				if ((this.Window.Width >= this.PictureBox.Width) ||
					(this.Window.Height >= this.PictureBox.Height))
					return true;
			return false;
		}

		/// <summary>
		/// Makes the PictureBox dimensions larger to effect the Zoom.
		/// </summary>
		/// <remarks>Maximum of 5 times larger.</remarks>
		private void ZoomIn() {
			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.
			if ((this.PictureBox.Width < (MINMAX * screen.Width)) &&
				(this.PictureBox.Height < (MINMAX * screen.Height))) {
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
			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.
			if ((this.PictureBox.Width > (screen.Width / MINMAX)) &&
				(this.PictureBox.Height > (screen.Height / MINMAX))) {
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Width / ZOOMFACTOR);
				this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Height / ZOOMFACTOR);
			}
		}

		/// <summary>
		/// If the image is larger than the window, scale it down to fit within the window.
		/// </summary>
		private void FitToWindow() {

			ImageBoxDelegate fitToWindow = delegate() {

				// Test if the image goes out of the window's bounds.
				if (this.IsImageLargerThanWindow()) {

					// Calculate image to window ratio for width and height.
					double widthRatio = this.PictureBox.Image.Width / (double)this.Window.Width;
					double heightRatio = this.PictureBox.Image.Height / (double)this.Window.Height;

					// Compare which ratio is larger, and size down the picture box accordingly.
					if (widthRatio > heightRatio) {
						this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Image.Width / widthRatio);
						this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Image.Height / widthRatio);
					} else {
						this.PictureBox.Width = Convert.ToInt32(this.PictureBox.Image.Width / heightRatio);
						this.PictureBox.Height = Convert.ToInt32(this.PictureBox.Image.Height / heightRatio);
					}
				} else {
					// Resize the picture box to the image's 
					// dimensions if the dimensions fit inside the window.
					this.PictureBox.Width = this.PictureBox.Image.Width;
					this.PictureBox.Height = this.PictureBox.Image.Height;
				}

				// Make sure the image fills the picture box.
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

				// Center the image in the window.
				this.CenterImage();
			};

			if (this.InvokeRequired)
				this.Invoke(fitToWindow);
			else
				fitToWindow();
		}

		/// <summary>
		/// Centers the image box within the window.
		/// </summary>
		private void CenterImage() {
			this.PictureBox.Location = new System.Drawing.Point(
				(this.Window.Width - this.PictureBox.Width) / 2, 
				(this.Window.Height - this.PictureBox.Height) / 2);
		}
		#endregion

		#region Mouse Handlers
		/// <summary>
		/// Zoom in or out when the mouse wheel is moved.
		/// If the image is larger than the window,
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

			if (this.IsPictureLargerThanWindow()) {
				this.PictureBox.Cursor = this.PanningHandOpen;
			} else {
				this.PictureBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			}

			// Center the picture box if 
			// it is smaller than the window.
			if (this.IsPictureWithinWindow())
				this.CenterImage();
		}

		/// <summary>
		/// Bring the image box into focus when the mouse enters its area.
		/// </summary>
		/// <param name="sender">ImageBox Controls</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBox_MouseEnter(object sender, EventArgs e) {
			if (this.IsActive(this.ParentForm.Handle) && (this.PictureBox.Focused == false))
				this.PictureBox.Focus();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PictureBox_MouseClick(object sender, MouseEventArgs e) {
			//this.PictureBox.Cursor = System.Windows.Forms.Cursors.
		}
		#endregion

		#region Control Handlers
		/// <summary>
		/// If the image is in "fit to window" state, the
		/// image will be resized on the control's resize event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OuterPanel_Resize(object sender, EventArgs e) {
			if (this.fitToWindow)
				this.FitToWindow();
			if (this.IsPictureLargerThanWindow()) {
				if (this.NearZoom != null)
					this.NearZoom(this, e);
			}

			// Center the picture box if 
			// it is smaller than the window.
			if (this.IsPictureWithinWindow())
				this.CenterImage();
		}
		#endregion

		#region Control Events
		/// <summary>
		/// This event is raised when the image extends 
		/// out of the windows bounds due to zooming.
		/// </summary>
		public event EventHandler NearZoom;
		#endregion

		#region Win32
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		/// <summary>
		/// Takes the window handle and determines if the window is currently active.
		/// </summary>
		/// <param name="handle">Window handle.</param>
		/// <returns>True means the window is currently active/on top. False is otherwise.</returns>
		private bool IsActive(IntPtr handle) {
			IntPtr activeHandle = GetForegroundWindow();
			return (activeHandle == handle);
		}

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

		/// <summary>
		/// Loads a colored cursor file into memory and creates a cursor out of it.
		/// </summary>
		/// <param name="resource">Local cursor resource file. Expected: *.cur</param>
		/// <returns>The newly created cursor.</returns>
		private System.Windows.Forms.Cursor LoadCursor(byte[] resource) {
			const int IMAGE_CURSOR = 2; 
			const uint LR_LOADFROMFILE = 0x00000010;

			string file = Path.GetTempFileName();
			File.WriteAllBytes(file, resource);
			System.Windows.Forms.Cursor cursor = new System.Windows.Forms.Cursor(LoadImage(IntPtr.Zero, 
				file, IMAGE_CURSOR, 0, 0, LR_LOADFROMFILE));
			File.Delete(file);
			return cursor;
		}
		#endregion
	}
}
