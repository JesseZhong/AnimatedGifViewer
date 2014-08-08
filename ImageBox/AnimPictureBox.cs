// AnimPictureBox.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.ComponentModel;
#endregion

namespace ClockworkControls {
	public class AnimPictureBox : System.Windows.Forms.PictureBox {

		#region Constants
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
		/// <summary>
		/// Indicates whether the image loaded into the
		/// animated picture box is an animated GIF.
		/// </summary>
		private bool mIsAnimGIF = false;

		/// <summary>
		/// Records the number of frames a loaded animated GIF has.
		/// </summary>
		private int mGifFrameCount = 0;

		/// <summary>
		/// Indicates the current frame of a loaded animated GIF
		/// that is being displayed in the animated picture box.
		/// </summary>
		private int mGifFrameIndex = 0;

		/// <summary>
		/// Records the delay times of each frame in a loaded animated GIF.
		/// </summary>
		private int[] mGifFrameDelays;

		/// <summary>
		/// This is the timer used for timing the animations.
		/// </summary>
		private System.Windows.Forms.Timer mGifTimer = new System.Windows.Forms.Timer();
		#endregion

		#region Instance
		/// <summary>
		/// Assign the tick event handler to the timer.
		/// </summary>
		public AnimPictureBox() {
			this.mGifTimer.Tick += new System.EventHandler(this.gifTimer_Tick);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the image that is displayed in AnimPictureBox.
		/// Override and hides the base property to extend support for
		/// loading in animated GIFs for specialized drawing.
		/// </summary>
		[Browsable(false)]
		public new System.Drawing.Image Image {
			get {
				return base.Image;
			}
			set {
				if (base.Image != null)
					base.Image = null;

				base.Image = value;
				if ((base.Image != null) && (base.Image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)) {
					this.mGifFrameCount = base.Image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time);
					if (this.mGifFrameCount > 1) {
						this.mIsAnimGIF = true;
						this.mGifFrameDelays = new int[this.mGifFrameCount];
						byte[] times = base.Image.GetPropertyItem(0x5100).Value;
						for (int frame = 0; frame < this.mGifFrameCount; frame++) {
							int delay = BitConverter.ToInt32(times, 4 * frame) * GIF_DELAY_MULTIPLIER - GIF_DELAY_EPSILON;
							this.mGifFrameDelays[frame] = (delay < MIN_GIF_DELAY) ? MIN_GIF_DELAY : delay;
						}

						this.mGifFrameIndex = 0;
						this.mGifTimer.Interval = this.mGifFrameDelays[0];

						if (!this.mGifTimer.Enabled)
							this.mGifTimer.Start();
						return;
					}
				}
				this.mIsAnimGIF = false;
				if (this.mGifTimer.Enabled)
					this.mGifTimer.Stop();
			}
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Increments to the next frame in a GIF animation when
		/// the delay time of the previous frame has been elapsed.
		/// </summary>
		/// <param name="sender">gifTimer</param>
		/// <param name="e">Event arguments.</param>
		private void gifTimer_Tick(object sender, EventArgs e) {
			this.mGifFrameIndex++;
			if (this.mGifFrameIndex >= this.mGifFrameCount)
				this.mGifFrameIndex = 0;
			this.mGifTimer.Interval = this.mGifFrameDelays[this.mGifFrameIndex];
			this.Refresh();
		}

		/// <summary>
		/// Overrides the base OnPaint() to draw a specific animated GIF frame when requested.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			if ((this.Image != null) && ((this.Width > this.Image.Width) || (this.Height > this.Image.Height))) {
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			}
			if (this.mIsAnimGIF) {
				base.Image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Time, this.mGifFrameIndex);
				e.Graphics.DrawImage(base.Image, this.ClientRectangle);
			} else
				base.OnPaint(e);
		}
		#endregion
	}
}
