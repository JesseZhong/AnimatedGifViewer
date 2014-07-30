// MouseHandler.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace AnimatedGifViewer {
	public class MouseHandler : IMessageFilter {
		private const int WM_MOUSEMOVE = 0x0200;
		private const int WM_LBUTTONDOWN = 0x201;

		public delegate void MouseEvent();
		public event MouseEvent MouseMoved;
		public event MouseEvent MouseClicked;

		private Point mLastPosition = new Point();

		/// <summary>
		/// Trigger mouse moved event when a hooked 
		/// mouse message indicates the mouse has moved.
		/// </summary>
		/// <param name="msg">The received message.</param>
		/// <returns>Return false to allow the message to continue to other programs.</returns>
		public bool PreFilterMessage(ref Message msg) {

			switch (msg.Msg) {
				case WM_MOUSEMOVE: {
						Point pos = Cursor.Position;

						// Check if the mouse absolute position has truly changed.
						if (pos != this.mLastPosition) {

							this.mLastPosition = pos;

							// Trigger the event.
							if (this.MouseMoved != null)
								this.MouseMoved();
						}
					}
					break;

				case WM_LBUTTONDOWN: {
					if (this.MouseClicked != null)
						this.MouseClicked();
					}
					break;
			}

			return false;
		}
	}
}
