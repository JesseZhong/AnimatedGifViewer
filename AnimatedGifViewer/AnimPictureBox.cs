﻿// AnimPictureBox.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.ComponentModel;
#endregion

namespace AnimatedGifViewer {
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

		#region Instance
		/// <summary>
		/// Assign the tick event handler to the timer.
		/// </summary>
		public AnimPictureBox() {
			this.gifTimer.Tick += new System.EventHandler(this.gifTimer_Tick);
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
				base.Image = value;
				if (base.Image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid) {
					this.gifFrameCount = base.Image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time);
					if (this.gifFrameCount > 1) {
						this.isAnimGIF = true;
						this.gifFrameDelays = new int[this.gifFrameCount];
						byte[] times = base.Image.GetPropertyItem(0x5100).Value;
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

		#region Event Handlers
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
		/// Overrides the base OnPaint() to draw a specific animated GIF frame when requested.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			if (this.isAnimGIF) {
				base.Image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Time, this.gifFrameIndex);
				e.Graphics.DrawImage(base.Image, this.ClientRectangle);
			} else
				base.OnPaint(e);
		}
		#endregion
	}
}
