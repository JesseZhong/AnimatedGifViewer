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
		/// <summary>
		/// 0.5 is the center of 1. Self explanatory.
		/// </summary>
		private const double CENTER = 0.5;

		/// <summary>
		/// Indicates the rate at which an image can be magnified.
		/// </summary>
		private const double MOUSE_ZOOM_FACTOR = 1.10;

		/// <summary>
		/// Indicates the rate at which a slider will magnify an image.
		/// </summary>
		public const double SLIDER_ZOOM_FACTOR = 1.02;

		/// <summary>
		/// Indicates how many times an image can be magnified.
		/// </summary>
		public const int ZOOM_MIN_MAX = 10;

		/// <summary>
		/// Magnification uses a compound rate ("interest") approach. Because of this, a special function needs
		/// to be used to calculate the maximum number of magnification levels that will be displayed in the slider.
		/// This equation calculates the levels at which an image can be magnified, either enlarged or shrunken.
		/// The interior of the function (floor(log(zoomMinMax) / log(zoomFactor))) captures the maximum number
		/// of full magnifications of either enlarging or shrinking.
		/// The 1 is to account for a partial step / level to full magnification.
		/// </summary>
		public static readonly int LEVELS_OF_COMPOUND_MAGNIFICATION = 
			Convert.ToInt32(Math.Log(ZOOM_MIN_MAX) / Math.Log(SLIDER_ZOOM_FACTOR)) + 1;
		#endregion

		#region Members
		private AnimPictureBox mPictureBox;
		private System.Windows.Forms.Panel mWindow;
		private bool mFitToWindow;
		delegate void ImageBoxDelegate();

		private System.Windows.Forms.Cursor mPanningHandOpen;
		private System.Windows.Forms.Cursor mPanningHandClosed;

		/// <summary>
		/// Horizontal ratio of the x component of the mouse position on the
		/// window to the window's width. i.e. mouse.location.x / window.width.
		/// </summary>
		private double mXMouseToWinRatio = 0;

		/// <summary>
		/// Vertical ratio of the y component of the mouse position on the 
		/// window to the window's height. i.e. mouse.location.y / window.height.
		/// </summary>
		private double mYMouseToWinRatio = 0;

		/// <summary>
		/// Horizontal ratio of the x component of the mouse position on the picture 
		/// box to the picture box's width. i.e. mouse.location.x / picturebox.width.
		/// </summary>
		private double mXMouseToPicRatio = 0;

		/// <summary>
		/// Vertical ratio of the y component of the mouse position on the picture 
		/// box to the picture box's height. i.e. mouse.location.y / picturebox.height.
		/// </summary>
		private double mYMouseToPicRatio = 0;

		/// <summary>
		/// Records the previous mouse position that will be used for calculating mouse delta.
		/// </summary>
		private System.Drawing.Point mPrevMouseLocation = new System.Drawing.Point();

		/// <summary>
		/// Records the current mouse position that will be used for calculating mouse delta.
		/// </summary>
		private System.Drawing.Point mCurrMouseLocation = new System.Drawing.Point();

		/// <summary>
		/// Indicates if the mouse is in panning mode. This requires that the mouse is down
		/// while over the picture box, the picture box is zoomed in to the point where it
		/// is larger than the window, and that the mouse button pressed is the left button.
		/// </summary>
		private bool mPanningMode = false;
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
			this.mPictureBox = new AnimPictureBox();
			this.mWindow = new System.Windows.Forms.Panel();
			this.mWindow.SuspendLayout();
			this.SuspendLayout();
			
			// PictureBox
			this.mPictureBox.BorderStyle = BorderStyle.None;
			this.mPictureBox.Location = new System.Drawing.Point(0, 0);
			this.mPictureBox.Name = "PicBox";
			this.mPictureBox.Size = new System.Drawing.Size(150, 140);
			this.mPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.mPictureBox.TabIndex = 3;
			this.mPictureBox.TabStop = false;

			// Window
			this.mWindow.AutoScroll = false;
			this.mWindow.BackColor = System.Drawing.SystemColors.Control;
			this.mWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.mWindow.Controls.Add(this.mPictureBox);
			this.mWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mWindow.Location = new System.Drawing.Point(0, 0);
			this.mWindow.Name = "Window";
			this.mWindow.Size = new System.Drawing.Size(this.Width, this.Height);
			this.mWindow.TabIndex = 4;
			
			// ImageBox
			this.Controls.Add(this.mWindow);
			this.Name = "ImageBox";
			this.Size = new System.Drawing.Size(210, 190);
			this.mWindow.ResumeLayout(false);
			this.ResumeLayout(false);

			// Cursors
			this.mPanningHandOpen = this.LoadCursor(global::AnimatedGifViewer.Properties.Resources.Cursor_PanningHand1);
			this.mPanningHandClosed = this.LoadCursor(global::AnimatedGifViewer.Properties.Resources.Cursor_PanningHand2);

			// Event handlers
			this.mWindow.MouseEnter += new System.EventHandler(this.ImageBox_MouseEnter);
			this.mPictureBox.MouseEnter += new System.EventHandler(this.ImageBox_MouseEnter);
			this.mPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			this.mPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
			this.mPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
			this.mWindow.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseWheel);
			this.mWindow.Resize += new System.EventHandler(this.Window_Resize);

			// States
			this.mFitToWindow = false;
		}

		/// <summary>
		/// Creates a simple red cross as a bitmap and displays it in the picture box.
		/// </summary>
		private void RedCross() {
			Bitmap bmp = new Bitmap(mWindow.Width, mWindow.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
			Graphics gr;
			gr = Graphics.FromImage(bmp);
			Pen pencil = new Pen(Color.Red, 5);
			gr.DrawLine(pencil, 0, 0, mWindow.Width, mWindow.Height);
			gr.DrawLine(pencil, 0, mWindow.Height, mWindow.Width, 0);
			mPictureBox.Image = bmp;
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
			get { return this.mPictureBox.Image; }
			set {
				this.mPictureBox.Image = value;
				this.mFitToWindow = true;
				if (this.mPictureBox.Image != null)
					this.FitIntoWindow();
				if (this.ImageChanged != null)
					this.ImageChanged(this, this.GetArguments());
			}
		}

		/// <summary>
		/// Sets or gets the size mode for the image box.
		/// </summary>
		[Browsable(false)]
		public PictureBoxSizeMode SizeMode {
			get { return this.mPictureBox.SizeMode; }
			set { this.mPictureBox.SizeMode = value; }
		}

		/// <summary>
		/// Set or gets the border style for the image box.
		/// </summary>
		[Browsable(false)]
		public BorderStyle Border {
			get { return this.mWindow.BorderStyle; }
			set { this.mWindow.BorderStyle = value; }
		}

		/// <summary>
		/// Sets or gets the back color of the window.
		/// </summary>
		[Browsable(false)]
		public new System.Drawing.Color BackColor {
			get { return this.mWindow.BackColor; }
			set { this.mWindow.BackColor = value; }
		}
		#endregion

		#region Zoom Methods
		/// <summary>
		/// Makes the PictureBox dimensions larger to effect the Zoom.
		/// </summary>
		/// <param name="zoomFactor">The factor that the image will be increased by.</param>
		public void ZoomIn(double zoomFactor) {

			// Calculate the ratios of the mouse position to 
			// the dimensions of the picture box and window.
			this.UpdateMouseTargetRatios();

			// Grab the screen dimensions.
			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.

			// Calculate the maximum size allowed by this machine's resolution.
			int maxWidth = ZOOM_MIN_MAX * screen.Width;
			int maxHeight = ZOOM_MIN_MAX * screen.Height;

			// Attempt to zoom in.
			if ((this.mPictureBox.Width < maxWidth) &&
				(this.mPictureBox.Height < maxHeight)) {

				// Calculate what the new width and height might be.
				int width = Convert.ToInt32(this.mPictureBox.Width * zoomFactor);
				int height = Convert.ToInt32(this.mPictureBox.Height * zoomFactor);

				// Test and set the new dimensions for the picture box depending on the maximum allowed size.
				if (width > maxWidth) {
					this.mPictureBox.Size = new System.Drawing.Size(maxWidth, Convert.ToInt32(maxWidth / (double)width * height));
				} else if (height > maxHeight) {
					this.mPictureBox.Size = new System.Drawing.Size(Convert.ToInt32(maxHeight / (double)height * width), maxHeight);
				} else {
					this.mPictureBox.Size = new System.Drawing.Size(width, height);
				}

				// Stretch and align the image into the picture box.
				this.mPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.AlignPictureBox();

				// Raise the event.
				if (this.ZoomedIn != null) {
					this.ZoomedIn(this, this.GetArguments());
				}
			}
		}

		/// <summary>
		/// Makes the PictureBox dimensions smaller to effect the Zoom.
		/// </summary>
		/// <param name="zoomFactor">The factor that the image will be shrunk by.</param>
		public void ZoomOut(double zoomFactor) {

			// Calculate the ratios of the mouse position to 
			// the dimensions of the picture box and window.
			this.UpdateMouseTargetRatios();

			// Grab the screen dimensions.
			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.

			// Calculate the minimum size allowed by this machine's resolution.
			int minWidth = screen.Width / ZOOM_MIN_MAX;
			int minHeight = screen.Height / ZOOM_MIN_MAX;

			// Attempt to zoom out.
			if ((this.mPictureBox.Width > minWidth) &&
				(this.mPictureBox.Height > minHeight)) {

				// Calculate what the new width and height might be.
				int width = Convert.ToInt32(this.mPictureBox.Width / zoomFactor);
				int height = Convert.ToInt32(this.mPictureBox.Height / zoomFactor);

				// Test and set the new dimensions for the picture box depending on the minimum allowed size.
				if (width < minWidth) {
					this.mPictureBox.Size = new System.Drawing.Size(minWidth, Convert.ToInt32(minWidth / (double)width * height));
				} else if (height < minHeight) {
					this.mPictureBox.Size = new System.Drawing.Size(Convert.ToInt32(minHeight / (double)height * width), minHeight);
				} else {
					this.mPictureBox.Size = new System.Drawing.Size(width, height);
				}

				// Stretch and align the image into the picture box.
				this.mPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				this.AlignPictureBox();

				// Raise the event.
				if (this.ZoomedOut != null) {
					this.ZoomedOut(this, this.GetArguments());
				}
			}
		}

		/// <summary>
		/// Checks if the image size exceeds the size of the window.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool IsImageExceedingWindow() {
			if ((this.mWindow != null) &&
				(this.mPictureBox != null) &&
				(this.mPictureBox.Image != null))
					if ((this.mWindow.Width < this.mPictureBox.Image.Width) ||
						(this.mWindow.Height < this.mPictureBox.Image.Height))
						return true;
			return false;
		}

		/// <summary>
		/// Checks if the picture box goes out of size of the window.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool IsPictureExceedingWindow() {
			if ((this.mWindow != null) &&
				(this.mPictureBox != null))
				if ((this.mWindow.Width < this.mPictureBox.Width) ||
					(this.mWindow.Height < this.mPictureBox.Height))
						return true;
			return false;
		}

		/// <summary>
		/// Checks if the picture box falls within the size of the window.
		/// </summary>
		/// <returns>True if it does, false otherwise.</returns>
		private bool IsPictureWithinWindow() {
			if ((this.mWindow != null) &&
				(this.mPictureBox != null))
				if ((this.mWindow.Width >= this.mPictureBox.Width) ||
					(this.mWindow.Height >= this.mPictureBox.Height))
					return true;
			return false;
		}

		/// <summary>
		/// If the image is larger than the window, scale it down to fit within the window.
		/// </summary>
		internal void FitIntoWindow() {

			ImageBoxDelegate fitIntoWindow = delegate() {

				// Test if the image goes out of the window's bounds.
				if (this.IsImageExceedingWindow()) {
					this.FitToWindow();
				} else {
					// Resize the picture box to the image's 
					// dimensions if the dimensions fit inside the window.
					this.mPictureBox.Width = this.mPictureBox.Image.Width;
					this.mPictureBox.Height = this.mPictureBox.Image.Height;
				}

				// Make sure the image fills the picture box.
				this.mPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

				// Realign the picture box with the window.
				this.UpdateMouseTargetRatios();
				this.AlignPictureBox();
			};

			if (this.InvokeRequired)
				this.Invoke(fitIntoWindow);
			else
				fitIntoWindow();
		}

		/// <summary>
		/// Attempts to stretch or shrink the image so at least a single pair 
		/// of edges, horizontal or vertical, meet with the edge of the window.
		/// </summary>
		internal void FitUpToWindow() {

			ImageBoxDelegate fitUpToWindow = delegate() {
				this.FitToWindow();

				// Realign the picture box with the window.
				this.UpdateMouseTargetRatios();
				this.AlignPictureBox();
			};

			if (this.InvokeRequired)
				this.Invoke(fitUpToWindow);
			else
				fitUpToWindow();
		}

		/// <summary>
		/// 
		/// </summary>
		internal void StretchToWindow() {

			ImageBoxDelegate stretchToWindow = delegate() {

			};

			if (this.InvokeRequired)
				this.Invoke(stretchToWindow);
			else
				stretchToWindow();
		}

		/// <summary>
		/// Resizes the image so that at least one pair of the edges, horizontal or vertical, touches 
		/// the edge of the box, and the other pair touching or within the bounds of the image box.
		/// </summary>
		private void FitToWindow() {

			// Calculate image to window ratio for width and height.
			double widthRatio = this.mPictureBox.Image.Width / (double)this.mWindow.Width;
			double heightRatio = this.mPictureBox.Image.Height / (double)this.mWindow.Height;

			// Compare which ratio is larger, and size the picture box accordingly.
			if (widthRatio > heightRatio) {
				this.mPictureBox.Width = Convert.ToInt32(this.mPictureBox.Image.Width / widthRatio);
				this.mPictureBox.Height = Convert.ToInt32(this.mPictureBox.Image.Height / widthRatio);
			} else {
				this.mPictureBox.Width = Convert.ToInt32(this.mPictureBox.Image.Width / heightRatio);
				this.mPictureBox.Height = Convert.ToInt32(this.mPictureBox.Image.Height / heightRatio);
			}
		}

		/// <summary>
		/// Updates the ratios used in the calculation of AlignPictureBox() using the
		/// current position of the mouse over both the picture box and window controls.
		/// </summary>
		private void UpdateMouseTargetRatios() {

			// Calculate the ratios of the mouse position to the dimensions of the picture box and window.
			System.Drawing.Point mouseToWindow = this.mWindow.PointToClient(Control.MousePosition);
			System.Drawing.Point mouseToPictureBox = this.mPictureBox.PointToClient(Control.MousePosition);
			this.mXMouseToWinRatio = mouseToWindow.X / (double)this.mWindow.Size.Width;
			this.mYMouseToWinRatio = mouseToWindow.Y / (double)this.mWindow.Size.Height;
			this.mXMouseToPicRatio = mouseToPictureBox.X / (double)this.mPictureBox.Size.Width;
			this.mYMouseToPicRatio = mouseToPictureBox.Y / (double)this.mPictureBox.Size.Height;
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
			this.mXMouseToWinRatio = (this.mWindow.Width < this.mPictureBox.Width) ? this.mXMouseToWinRatio : CENTER;
			this.mYMouseToWinRatio = (this.mWindow.Height < this.mPictureBox.Height) ? this.mYMouseToWinRatio : CENTER;
			this.mXMouseToPicRatio = (this.mWindow.Width < this.mPictureBox.Width) ? this.mXMouseToPicRatio : CENTER;
			this.mYMouseToPicRatio = (this.mWindow.Height < this.mPictureBox.Height) ? this.mYMouseToPicRatio : CENTER;

			// Calculate the location of the picture box on the window where the points overlap.
			int xPoint = (int)((this.mWindow.Width * mXMouseToWinRatio) - (this.mPictureBox.Width * mXMouseToPicRatio));
			int yPoint = (int)((this.mWindow.Height * mYMouseToWinRatio) - (this.mPictureBox.Height * mYMouseToPicRatio));

			// Attempt to align the the points together.
			this.mPictureBox.Location = this.RestrictedPictureBoxPosition(xPoint, yPoint);
			this.mPictureBox.Invalidate();
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

			if (this.mWindow.Width < this.mPictureBox.Width) {
				xPoint = (xPoint < 0) ? xPoint : 0;
				xPoint = ((xPoint + this.mPictureBox.Width) > this.mWindow.Width)
					? xPoint : (this.mWindow.Width - this.mPictureBox.Width);
			} else {
				xPoint = (xPoint >= 0) ? xPoint : 0;
				xPoint = ((xPoint + this.mPictureBox.Width) <= this.mWindow.Width)
					? xPoint : (this.mWindow.Width - this.mPictureBox.Width);
			}

			if (this.mWindow.Height < this.mPictureBox.Height) {
				yPoint = (yPoint < 0) ? yPoint : 0;
				yPoint = ((yPoint + this.mPictureBox.Height) > this.mWindow.Height)
					? yPoint : (this.mWindow.Height - this.mPictureBox.Height);
			} else {
				yPoint = (yPoint >= 0) ? yPoint : 0;
				yPoint = ((yPoint + this.mPictureBox.Height) <= this.mWindow.Height)
					? yPoint : (this.mWindow.Height - this.mPictureBox.Height);
			}
			
			return new System.Drawing.Point(xPoint, yPoint);
		}

		/// <summary>
		/// Creates a set of zoom arguments using the current state of the image box.
		/// </summary>
		/// <returns>The new set of arguments.</returns>
		private ZoomEventArgs GetArguments() {
			if (this.mPictureBox.Image == null)
				return null;
			return new ZoomEventArgs() {
						Magnification = this.mPictureBox.Width / this.mPictureBox.Image.Width,
						OriginalWidth = this.mPictureBox.Image.Width,
						OriginalHeight = this.mPictureBox.Image.Height,
						CurrentWidth = this.mPictureBox.Width,
						CurrentHeight = this.mPictureBox.Height
					};
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
			if (this.mWindow.ClientRectangle.Contains(this.mWindow.PointToClient(Control.MousePosition))) {

				this.mFitToWindow = false;
				if (e.Delta < 0)
					this.ZoomOut(MOUSE_ZOOM_FACTOR);
				else
					this.ZoomIn(MOUSE_ZOOM_FACTOR);
			}
		}

		/// <summary>
		/// Bring the image box into focus when the mouse enters its area.
		/// </summary>
		/// <param name="sender">ImageBox Controls</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBox_MouseEnter(object sender, EventArgs e) {
			if (this.IsActive(this.ParentForm.Handle) && (this.mPictureBox.Focused == false))
				this.mPictureBox.Focus();
		}

		/// <summary>
		/// Detects when the mouse is entering panning mode and records
		/// the state and changes the mouse cursor to a closed hand.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event arguments.</param>
		private void PictureBox_MouseDown(object sender, MouseEventArgs e) {
			if (this.IsPictureExceedingWindow() && (e.Button == MouseButtons.Left)
				&& this.mPictureBox.ClientRectangle.Contains(this.mPictureBox.PointToClient(Control.MousePosition))) {
				this.mPictureBox.Cursor = this.mPanningHandClosed;
				this.mPanningMode = true;
				this.mPrevMouseLocation = e.Location;
			}
		}

		/// <summary>
		/// Changes the mouse cursor when the mouse leaves panning mode.
		/// </summary>
		/// <param name="sender">PictureBox</param>
		/// <param name="e">Event arguments.</param>
		private void PictureBox_MouseUp(object sender, MouseEventArgs e) {
			if (this.IsPictureExceedingWindow() && (e.Button == MouseButtons.Left)) {
				this.mPictureBox.Cursor = this.mPanningHandOpen;
			}
			this.mPanningMode = false;
		}

		/// <summary>
		/// When the mouse enters panning mode, the image will be shifted in the
		/// same direction that the mouse is moving to achieve the panning effect.
		/// </summary>
		/// <param name="sender">PictureBox</param>
		/// <param name="e">Event arguments.</param>
		private void PictureBox_MouseMove(object sender, MouseEventArgs e) {

			if (this.mPanningMode) {
				this.mCurrMouseLocation = e.Location;
				System.Drawing.Point location = this.mPictureBox.Location;
				int deltaX = this.mCurrMouseLocation.X - mPrevMouseLocation.X;
				int deltaY = this.mCurrMouseLocation.Y - mPrevMouseLocation.Y;
				int newX = location.X + deltaX;
				int newY = location.Y + deltaY;

				this.mPictureBox.Location = this.RestrictedPictureBoxPosition(newX, newY);
			} else {
				if (this.IsPictureExceedingWindow()) {
					this.mPictureBox.Cursor = this.mPanningHandOpen;
				} else {
					this.mPictureBox.Cursor = System.Windows.Forms.Cursors.Arrow;
				}
			}
		}
		#endregion

		#region Keyboard Event Handlers
		/// <summary>
		/// Processes key commands when they are triggered by user input.
		/// </summary>
		/// <param name="msg">Windows message with details of the user input.</param>
		/// <param name="keyData">Contains data about which key events occurred.</param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			if (this.ProcessCmdKeyEvent != null)
				this.ProcessCmdKeyEvent(keyData);

			return base.ProcessCmdKey(ref msg, keyData);
		}

		internal event Action<System.Windows.Forms.Keys> ProcessCmdKeyEvent;
		#endregion

		#region Control Handlers
		/// <summary>
		/// If the image is in "fit to window" state, the
		/// image will be resized on the control's resize event.
		/// </summary>
		/// <param name="sender">Window</param>
		/// <param name="e">Event arguments.</param>
		private void Window_Resize(object sender, EventArgs e) {
			if (this.mFitToWindow)
				this.FitIntoWindow();
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

		/// <summary>
		/// This event is raised when the magnification on the image increases.
		/// </summary>
		public event ZoomEventHandler ZoomedIn;

		/// <summary>
		/// This event is raised when the magnification on the image decreases.
		/// </summary>
		public event ZoomEventHandler ZoomedOut;

		/// <summary>
		/// This event is raised when a new image is assigned.
		/// </summary>
		public event ZoomEventHandler ImageChanged;
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

	#region Arguments
	public class ZoomEventArgs : EventArgs {

		/// <summary>
		/// The amount of times the image is zoomed in or out on.
		/// </summary>
		public double Magnification {
			get;
			set;
		}

		/// <summary>
		/// The original width of the image.
		/// </summary>
		public int OriginalWidth {
			get;
			set;
		}

		/// <summary>
		/// The original height of the image.
		/// </summary>
		public int OriginalHeight {
			get;
			set;
		}

		/// <summary>
		/// The width the image is at now with zoom.
		/// </summary>
		public int CurrentWidth {
			get;
			set;
		}

		/// <summary>
		/// The height the image is at now with zoom.
		/// </summary>
		public int CurrentHeight {
			get;
			set;
		}
	}
	#endregion

	#region Delegates
	public delegate void ZoomEventHandler(object sender, ZoomEventArgs e);
	#endregion
}
