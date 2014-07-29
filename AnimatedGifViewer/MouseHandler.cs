// MouseHandler.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace AnimatedGifViewer {
	public class MouseHandler : IMessageFilter {
		private const int WM_MOUSEMOVE = 0x0200;

		public delegate void MouseEvent();
		public event MouseEvent MouseMoved;

		/// <summary>
		/// Trigger mouse moved event when a hooked 
		/// mouse message indicates the mouse has moved.
		/// </summary>
		/// <param name="msg">The received message.</param>
		/// <returns>Return false to allow the message to continue to other programs.</returns>
		public bool PreFilterMessage(ref Message msg) {
			if ((msg.Msg == WM_MOUSEMOVE) && (this.MouseMoved != null))
				this.MouseMoved();

			return false;
		}
	}
}
