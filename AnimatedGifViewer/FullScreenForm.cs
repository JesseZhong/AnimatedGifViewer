using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AnimatedGifViewer {
	public partial class FullScreenForm : Form {

		#region Members
		internal ImageBox ImageBox;
		#endregion

		/// <summary>
		/// Initialize form components.
		/// </summary>
		public FullScreenForm() {
			this.InitializeComponent();
			this.InitializeImageBox();
		}

		/// <summary>
		/// Loads additional content when the form is created.
		/// </summary>
		/// <param name="sender">FullScreenForm</param>
		/// <param name="e">Event arguments.</param>
		private void FullScreenForm_Load(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Maximized;
			this.FormBorderStyle = FormBorderStyle.None;
			this.TopMost = true;
			SetWinFullScreen(this.Handle);

			// Set to handle keyboard events.
			this.KeyPreview = true;
		}

		/// <summary>
		/// Initializes the image box for the form.
		/// </summary>
		private void InitializeImageBox() {
			this.ImageBox = new ImageBox();
			this.ImageBox.Border = System.Windows.Forms.BorderStyle.None;
			this.ImageBox.BackColor = System.Drawing.Color.Black;	// Replace with settings later.
			this.ImageBox.Anchor = (System.Windows.Forms.AnchorStyles)
				(AnchorStyles.Top | AnchorStyles.Bottom |
				AnchorStyles.Left | AnchorStyles.Right);

			this.ImageBox.Location = new Point(0, 0);
			this.ImageBox.Margin = new System.Windows.Forms.Padding(0);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(this.ClientSize.Width,
				this.ClientSize.Height);
			this.ImageBox.TabIndex = 0;
			this.ImageBox.TabStop = false;
			this.Controls.Add(this.ImageBox);

			this.ProcessCmdKeyEvent += new Action<System.Windows.Forms.Keys>(this.HideForm);
		}

		#region Keyboard Event Handlers
		/// <summary>
		/// Attempts to hide the form if the user inputs the escape key.
		/// </summary>
		/// <param name="keyData">The key that is pressed by the user.</param>
		private void HideForm(System.Windows.Forms.Keys keyData) {
			if (keyData == Keys.Escape) {
				this.Hide();
			}
		}

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

		#region Win32
		private const int SM_CXSCREEN = 0;
		private const int SM_CYSCREEN = 1;
		private static IntPtr HWND_TOP = IntPtr.Zero;
		private const int SWP_SHOWWINDOW = 64; // 0x0040

		[DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
		public static extern int GetSystemMetrics(int which);

		[DllImport("user32.dll")]
		public static extern void
			SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
						 int X, int Y, int width, int height, uint flags);  

		public static int ScreenX {
			get { return GetSystemMetrics(SM_CXSCREEN); }
		}

		public static int ScreenY {
			get { return GetSystemMetrics(SM_CYSCREEN); }
		}

		public static void SetWinFullScreen(IntPtr hwnd) {
			SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
		}
		#endregion
	}
}
