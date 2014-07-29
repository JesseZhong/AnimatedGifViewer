// DrawingControl.cs
// Refer to: http://stackoverflow.com/questions/487661/how-do-i-suspend-painting-for-a-control-and-its-children
#region Usings
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
#endregion
namespace AnimatedGifViewer {

	/// <summary>
	/// This class allows you to suspend the drawing of a control, letting you
	/// complete any calculations or rendering before you continue to draw.
	/// </summary>
	public class DrawingControl {

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

		private const int WM_SETREDRAW = 11;

		public static void SuspendDrawing(Control parent) {
			SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
		}

		public static void ResumeDrawing(Control parent) {
			SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
			parent.Refresh();
		}
	}
}
