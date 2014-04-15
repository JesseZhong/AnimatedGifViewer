// AnimPictureBox.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Drawing;
using System.ComponentModel;
#endregion

namespace AnimatedGifViewer {
	public class AnimPictureBox : System.Windows.Forms.Control {

		#region Constants
		/// <summary>
		/// This is the amount of time, in milliseconds, before 
		/// the picture box, and the image in it, is redrawn.
		/// </summary>
		private const int REDRAW_INTERVAL = 10;

		/// <summary>
		/// This is the minimum supported delay, in milliseconds,
		/// that the animated picture box can support.
		/// </summary>
		private const int MIN_GIF_DELAY = 20;

		/// <summary>
		/// Delay time is measured by 1/100ths of a second, or 10ms.
		/// Values read from the file must be adjusted by this multiplier
		/// before being stored and used.
		/// </summary>
		private const int GIF_DELAY_MULTIPLIER = 10;

		/// <summary>
		/// Accounts for the time it takes to call for a redraw,
		/// buffer the new image, and print it to the screen.
		/// </summary>
		private const int GIF_DELAY_EPSILON = 8;
		#endregion

		#region Members
		private System.Drawing.Image image;

		/// <summary>
		/// Indicates that the control is finished initializing.
		/// </summary>
		private bool initializationComplete;

		/// <summary>
		/// Indicates that the control is being disposed.
		/// </summary>
		private bool isDisposing;

		#region Animating GIFs
		/// <summary>
		/// Indicates whether the image loaded into the
		/// animated picture box is an animated GIF.
		/// </summary>
		private bool isAnimGIF = false;

		/// <summary>
		/// Records the number of frames a loaded animated GIF has.
		/// </summary>
		private int gifFrameCount = 0;

		/// <summary>
		/// Indicates the current frame of a loaded animated GIF
		/// that is being displayed in the animated picture box.
		/// </summary>
		private int gifFrameIndex = 0;

		/// <summary>
		/// Records the delay times of each frame in a loaded animated GIF.
		/// </summary>
		private int[] gifFrameDelays;

		/// <summary>
		/// This is the timer used for timing the animations.
		/// </summary>
		private System.Windows.Forms.Timer gifTimer = new System.Windows.Forms.Timer();
		#endregion

		#region Double Buffered Drawing
		private System.Drawing.BufferedGraphicsContext backbufferContext;
		private System.Drawing.BufferedGraphics backbufferGraphics;
		private System.Drawing.Graphics drawingGraphics;

		private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		#endregion
		#endregion

		#region Instance
		/// <summary>
		/// Assign the tick event handler to the timer.
		/// </summary>
		public AnimPictureBox() {
			this.InitializeComponents();

			// Set the control styles that will be used to double buffer.
			this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);

			// Assign the buffer context.
			this.backbufferContext = System.Drawing.BufferedGraphicsManager.Current;
			this.initializationComplete = true;

			// Create the buffers.
			this.RecreateBuffers();

			// Begin drawing.
			this.Redraw();

			#region Redraw Timer
			this.timer.Interval = REDRAW_INTERVAL;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			this.timer.Start();
			#endregion

			// Assign the GIF delay timer tick event.
			this.gifTimer.Tick += new System.EventHandler(this.gifTimer_Tick);
		}

		/// <summary>
		/// Initialize the components of the picture box control.
		/// </summary>
		private void InitializeComponents() {
			this.SuspendLayout();
			this.Name = "AnimPictureBox";
			this.Size = new System.Drawing.Size(348, 340);
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			this.isDisposing = true;
			if (disposing) {
				if (this.image != null)
					this.image.Dispose();
				if (this.backbufferGraphics != null)
					this.backbufferGraphics.Dispose();
				if (this.backbufferContext != null)
					this.backbufferContext.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the image that is displayed in AnimPictureBox.
		/// Override and hides the base property to extend support for
		/// loading in animated GIFs for specialized drawing.
		/// </summary>
		[Browsable(false)]
		public System.Drawing.Image Image {
			get {
				return this.image;
			}
			set {
				if (this.image != null)
					this.image.Dispose();

				this.image = value;
				if ((this.image != null) && (this.image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)) {
					this.gifFrameCount = this.image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time);
					if (this.gifFrameCount > 1) {
						this.isAnimGIF = true;
						this.gifFrameDelays = new int[this.gifFrameCount];
						byte[] times = this.image.GetPropertyItem(0x5100).Value;
						for (int frame = 0; frame < this.gifFrameCount; frame++) {
							int delay = BitConverter.ToInt32(times, 4 * frame) * GIF_DELAY_MULTIPLIER - GIF_DELAY_EPSILON;
							this.gifFrameDelays[frame] = (delay < MIN_GIF_DELAY) ? MIN_GIF_DELAY : delay;
						}

						this.gifFrameIndex = 0;
						this.gifTimer.Interval = this.gifFrameDelays[0];

						if (!this.gifTimer.Enabled)
							this.gifTimer.Start();
						return;
					}
				}
				this.isAnimGIF = false;
				if (this.gifTimer.Enabled)
					this.gifTimer.Stop();
			}
		}
		#endregion

		#region Drawing
		/// <summary>
		/// Increments to the next frame in a GIF animation when
		/// the delay time of the previous frame has been elapsed.
		/// </summary>
		/// <param name="sender">gifTimer</param>
		/// <param name="e">Event arguments.</param>
		private void gifTimer_Tick(object sender, EventArgs e) {
			this.gifFrameIndex++;
			if (this.gifFrameIndex >= this.gifFrameCount)
				this.gifFrameIndex = 0;
			this.gifTimer.Interval = this.gifFrameDelays[this.gifFrameIndex];
			this.Refresh();
		}

		/// <summary>
		/// Redraws the control periodically.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_Tick(object sender, EventArgs e) {
			this.Redraw();
		}

		/// <summary>
		/// Redraws the control when it is resized.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			this.RecreateBuffers();
			this.Redraw();
		}

		/// <summary>
		/// Recreates the buffers used for buffering, 
		/// swapping, and printing the images to screen.
		/// </summary>
		private void RecreateBuffers() {

			// Ensure that the control is initialized and is not being disposed.
			if (!this.initializationComplete || this.isDisposing)
				return;

			// Create a new buffer with he size of the control. + 1 ensures the buffer is never of size 0, 0.
			this.backbufferContext.MaximumBuffer = new System.Drawing.Size(this.Width + 1, this.Height + 1);

			// If an old drawing surface was created, dispose of it first.
			if (this.backbufferGraphics != null)
				this.backbufferGraphics.Dispose();

			// Create a new back buffer for the buffer.
			this.backbufferGraphics = backbufferContext.Allocate(this.CreateGraphics(),
				new System.Drawing.Rectangle(0, 0, Math.Max(this.Width, 1), Math.Max(this.Height, 1)));

			// Assign the drawing surface of the back buffer to the member variable for future reference.
			this.drawingGraphics = this.backbufferGraphics.Graphics;

			// Set smoothing mode to anti aliasing.
			this.drawingGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// Repaint the control.
			this.Invalidate();
		}

		/// <summary>
		/// Attempts to draw the image to the buffers.
		/// </summary>
		protected void Redraw() {
			if (this.image != null) {
				if (this.isAnimGIF)
					this.image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Time, this.gifFrameIndex);
				this.drawingGraphics.DrawImage(this.image, this.ClientRectangle);
			}

			// Force the control to invalidate and update.
			this.Refresh();
		}

		/// <summary>
		/// Overrides the base OnPaint() to render the buffered images.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			if (!this.isDisposing && this.backbufferGraphics != null)
				backbufferGraphics.Render(e.Graphics);
		}
		#endregion
	}
}
