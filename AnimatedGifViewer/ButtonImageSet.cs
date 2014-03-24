// ButtonImageSet.cs
// Authored by Jesse Z. Zhong
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace AnimatedGifViewer {
	public class ButtonImageSet {

		public enum EState {
			Inactive,
			Active,
			Hover,
			Clicked
		}

		private readonly int clips = Enum.GetNames(typeof(EState)).Length;
		private List<Image> images = new List<Image>();

		/// <summary>
		/// Constructor.
		/// Divides the image into parts according
		/// to the number of button states.
		/// </summary>
		/// <param name="image">The image that will be divided and used.</param>
		public ButtonImageSet(Image image) {
			this.DivideImage(image);
		}

		/// <summary>
		/// Returns an image from the image set 
		/// depending on the state the user passed.
		/// </summary>
		/// <param name="state">Button state specified by EState.</param>
		/// <returns>Image corresponding to state.</returns>
		public Image GetImage(EState state) {
			if (!this.images.Any() || (this.images.Count != clips))
				throw new Exception("GetImage(): Insufficient number of images.");

			return this.images[(int)state];
		}

		/// <summary>
		/// Divides the image into parts according
		/// to the number of button states.
		/// </summary>
		/// <param name="image">The image that will be divided and used.</param>
		private void DivideImage(Image image) {

			int width = image.Width;
			int height = image.Height;
			if ((width == 0) || (height == 0))
				throw new Exception("DivideImage(): Width or height cannot be zero.");

			int clipWidth = (int)(width / clips);
			if (clipWidth == 0)
				throw new Exception("DivideImage(): Image is too small to be used.");

			for (int i = 0; i < clips; i++) {

				Image newImage = new Bitmap(clipWidth, height);
				using (Graphics graphics = Graphics.FromImage(newImage)) {
					graphics.DrawImage(image,
						new RectangleF(0, 0, newImage.Width, newImage.Height),
						new RectangleF(i * clipWidth, 0, clipWidth, newImage.Height), 
						GraphicsUnit.Pixel);
				}
				this.images.Add(newImage);
			}
		}
	}
}
