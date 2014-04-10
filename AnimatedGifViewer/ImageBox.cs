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
		private const double CENTER = 0.5;
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

		/// <summary>
		/// Horizontal ratio of the x component of the mouse position on the
		/// window to the window's width. i.e. mouse.location.x / window.width.
		/// </summary>
		private double xMouseToWinRatio = 0;

		/// <summary>
		/// Vertical ratio of the y component of the mouse position on the 
		/// window to the window's height. i.e. mouse.location.y / window.height.
		/// </summary>
		private double yMouseToWinRatio = 0;

		/// <summary>
		/// Horizontal ratio of the x component of the mouse position on the picture 
		/// box to the picture box's width. i.e. mouse.location.x / picturebox.width.
		/// </summary>
		private double xMouseToPicRatio = 0;

		/// <summary>
		/// Vertical ratio of the y component of the mouse position on the picture 
		/// box to the picture box's height. i.e. mouse.location.y / picturebox.height.
		/// </summary>
		private double yMouseToPicRatio = 0;

		/// <summary>
		/// Records the previous mouse position that will be used for calculating mouse delta.
		/// </summary>
		private System.Drawing.Point prevMouseLocation = new System.Drawing.Point();

		/// <summary>
		/// Records the current mouse position that will be used for calculating mouse delta.
		/// </summary>
		private System.Drawing.Point currMouseLocation = new System.Drawing.Point();

		/// <summary>
		/// Indicates if the mouse is in panning mode. This requires that the mouse is down
		/// while over the picture box, the picture box is zoomed in to the point where it
		/// is larger than the window, and that the mouse button pressed is the left button.
		/// </summary>
		private bool panningMode = false;
		#endregion

		#region Instance
		/// <summary>
		/// Initializes all the components.
		/// </summary>
		public ImageBox() {
			this.InitializeComponent();
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
			this.PictureBox.Location = new System.Drawing.Point(0, 0);
			this.PictureBox.Name = "PicBox";
			this.PictureBox.Size = new System.Drawing.Size(150, 140);
			this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.PictureBox.TabIndex = 3;
			this.PictureBox.TabStop = false;

			// ImageBoxMenu
			this.ImageBoxMenu.Name = "ImageBoxMenu";
			this.ImageBoxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.ImageBoxMenu.Size = new System.Drawing.Size(180, 70);

			// Window
			this.Window.AutoScroll = false;
			this.Window.BackColor = System.Drawing.SystemColors.Control;
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
			this.Window.MouseEnter += new System.EventHandler(this.ImageBox_MouseEnter);
			this.PictureBox.MouseEnter += new System.EventHandler(this.ImageBox_MouseEnter);
			this.PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			this.PictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
			this.PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
			this.Window.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseWheel);
			this.Window.Resize += new EventHandler(this.Window_Resize);

			// States
			this.fitToWindow = false;
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
		private bool IsImageExceedinWindow() {
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
		private bool IsPictureExceedingWindow() {
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
			// Calculate the ratios of the mouse position to 
			// the dimensions of the picture box and window.
			this.UpdateMouseTargetRatios();

			// Attempt to zoom in.
			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.
			if ((this.PictureBox.Width < (MINMAX * screen.Width)) &&
				(this.PictureBox.Height < (MINMAX * screen.Height))) {
				this.PictureBox.Size = new System.Drawing.Size(Convert.ToInt32(this.PictureBox.Width * ZOOMFACTOR),
					Convert.ToInt32(this.PictureBox.Height * ZOOMFACTOR));
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.AlignPictureBox();
			}
		}

		/// <summary>
		/// Makes the PictureBox dimensions smaller to effect the Zoom.
		/// </summary>
		/// <remarks>Minimum of 5 times smaller.</remarks>
		private void ZoomOut() {
			// Calculate the ratios of the mouse position to 
			// the dimensions of the picture box and window.
			this.UpdateMouseTargetRatios();

			// Attempt to zoom out.
			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.
			if ((this.PictureBox.Width > (screen.Width / MINMAX)) &&
				(this.PictureBox.Height > (screen.Height / MINMAX))) {
				this.PictureBox.Size = new System.Drawing.Size(Convert.ToInt32(this.PictureBox.Width / ZOOMFACTOR),
					Convert.ToInt32(this.PictureBox.Height / ZOOMFACTOR));
				this.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.AlignPictureBox();
			}
		}

		/// <summary>
		/// If the image is larger than the window, scale it down to fit within the window.
		/// </summary>
		private void FitToWindow() {

			ImageBoxDelegate fitToWindow = delegate() {

				// Test if the image goes out of the window's bounds.
				if (this.IsImageExceedinWindow()) {

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

				// Realign the picture box with the window.
				this.UpdateMouseTargetRatios();
				this.AlignPictureBox();
			};

			if (this.InvokeRequired)
				this.Invoke(fitToWindow);
			else
				fitToWindow();
		}

		/// <summary>
		/// Updates the ratios used in the calculation of AlignPictureBox() using the
		/// current position of the mouse over both the picture box and window controls.
		/// </summary>
		private void UpdateMouseTargetRatios() {

			// Calculate the ratios of the mouse position to the dimensions of the picture box and window.
			System.Drawing.Point mouseToWindow = this.Window.PointToClient(Control.MousePosition);
			System.Drawing.Point mouseToPictureBox = this.PictureBox.PointToClient(Control.MousePosition);
			this.xMouseToWinRatio = mouseToWindow.X / (double)this.Window.Size.Width;
			this.yMouseToWinRatio = mouseToWindow.Y / (double)this.Window.Size.Height;
			this.xMouseToPicRatio = mouseToPictureBox.X / (double)this.PictureBox.Size.Width;
			this.yMouseToPicRatio = mouseToPictureBox.Y / (double)this.PictureBox.Size.Height;
		}

		/// <summary>
		/// Aligns the picture box with the window according to where the mouse resides on each control.
		/// The controls will be lined up so that the points (one per control) overlap. The calculations
		/// are performed using ratios of the point locations to the size of the control. Under circumstances
		/// that the picture box is smaller than the window, the picture box will aligned to the center instead.
		/// </summary>
		private void AlignPictureBox() {

			// If the picture box exceeds any of the window's dimensions, use the ratios
			// to align the picture box. Otherwise, center the picture instead.
			this.xMouseToWinRatio = (this.Window.Width < this.PictureBox.Width) ? this.xMouseToWinRatio : CENTER;
			this.yMouseToWinRatio = (this.Window.Height < this.PictureBox.Height) ? this.yMouseToWinRatio : CENTER;
			this.xMouseToPicRatio = (this.Window.Width < this.PictureBox.Width) ? this.xMouseToPicRatio : CENTER;
			this.yMouseToPicRatio = (this.Window.Height < this.PictureBox.Height) ? this.yMouseToPicRatio : CENTER;

			// Calculate the location of the picture box on the window where the points overlap.
			int xPoint = (int)((this.Window.Width * xMouseToWinRatio) - (this.PictureBox.Width * xMouseToPicRatio));
			int yPoint = (int)((this.Window.Height * yMouseToWinRatio) - (this.PictureBox.Height * yMouseToPicRatio));

			// Attempt to align the the points together.
			this.PictureBox.Location = this.RestrictedPictureBoxPosition(xPoint, yPoint);
		}

		/// <summary>
		/// Tests a suggested position for the picture box when it is zoomed in to the
		/// point where the picture box exceeds the window's bounds to see if the position
		/// shifts the picture box too far to the left, right, top, or bottom (i.e. if part
		/// of the background is revealed when deep zoomed). The suggested position is then
		/// corrected to line up with the nearest border. A point with the new position is
		/// then returned.
		/// </summary>
		/// <param name="xPoint">The suggested x position.</param>
		/// <param name="yPoint">The suggested y position.</param>
		/// <returns>The corrected position.</returns>
		private System.Drawing.Point RestrictedPictureBoxPosition(int xPoint, int yPoint) {

			// While the picture box exceeds the bounds of the window, make sure that the picture
			// box always lines up with the side of the window instead of being shifted out of view.
			if (!this.IsPictureWithinWindow()) {
				xPoint = (xPoint < 0) ? xPoint : 0;
				xPoint = ((xPoint + this.PictureBox.Width) > this.Window.Width) ? xPoint : (this.Window.Width - this.PictureBox.Width);
				yPoint = (yPoint < 0) ? yPoint : 0;
				yPoint = ((yPoint + this.PictureBox.Height) > this.Window.Height) ? yPoint : (this.Window.Height - this.PictureBox.Height);
			}

			return new System.Drawing.Point(xPoint, yPoint);
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

			// Ensure that the mouse within the bounds.
			if (this.Window.ClientRectangle.Contains(this.Window.PointToClient(Control.MousePosition))) {

				this.fitToWindow = false;
				if (e.Delta < 0)
					this.ZoomOut();
				else
					this.ZoomIn();

				if (this.IsPictureExceedingWindow()) {
					this.PictureBox.Cursor = this.PanningHandOpen;
				} else {
					this.PictureBox.Cursor = System.Windows.Forms.Cursors.Arrow;
				}
			}
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
		/// Detects when the mouse is entering panning mode and records
		/// the state and changes the mouse cursor to a closed hand.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event arguments.</param>
		private void PictureBox_MouseDown(object sender, MouseEventArgs e) {
			if (this.IsPictureExceedingWindow() && (e.Button == MouseButtons.Left)
				&& this.PictureBox.ClientRectangle.Contains(this.PictureBox.PointToClient(Control.MousePosition))) {
				this.PictureBox.Cursor = this.PanningHandClosed;
				this.panningMode = true;
				this.prevMouseLocation = e.Location;
			}
		}

		/// <summary>
		/// Changes the mouse cursor when the mouse leaves panning mode.
		/// </summary>
		/// <param name="sender">PictureBox</param>
		/// <param name="e">Event arguments.</param>
		private void PictureBox_MouseUp(object sender, MouseEventArgs e) {
			if (this.IsPictureExceedingWindow() && (e.Button == MouseButtons.Left)) {
				this.PictureBox.Cursor = this.PanningHandOpen;
			}
			this.panningMode = false;
		}

		/// <summary>
		/// When the mouse enters panning mode, the image will be shifted in the
		/// same direction that the mouse is moving to achieve the panning effect.
		/// </summary>
		/// <param name="sender">PictureBox</param>
		/// <param name="e">Event arguments.</param>
		private void PictureBox_MouseMove(object sender, MouseEventArgs e) {

			if (this.panningMode) {
				this.currMouseLocation = e.Location;
				System.Drawing.Point location = this.PictureBox.Location;
				int deltaX = this.currMouseLocation.X - prevMouseLocation.X;
				int deltaY = this.currMouseLocation.Y - prevMouseLocation.Y;
				int newX = location.X + deltaX;
				int newY = location.Y + deltaY;

				this.PictureBox.Location = this.RestrictedPictureBoxPosition(newX, newY);
			}
		}
		#endregion

		#region Control Handlers
		/// <summary>
		/// If the image is in "fit to window" state, the
		/// image will be resized on the control's resize event.
		/// </summary>
		/// <param name="sender">Window</param>
		/// <param name="e">Event arguments.</param>
		private void Window_Resize(object sender, EventArgs e) {
			if (this.fitToWindow)
				this.FitToWindow();
			if (this.IsPictureExceedingWindow()) {
				if (this.NearZoom != null)
					this.NearZoom(this, e);
			}

			// Realign the picture box with the window.
			this.UpdateMouseTargetRatios();
			this.AlignPictureBox();
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
