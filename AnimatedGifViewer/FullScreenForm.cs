﻿// FullScreenForm.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

using ClockworkControls;
#endregion

namespace AnimatedGifViewer {
	public partial class FullScreenForm : Form {

		#region Constants
		/// <summary>
		/// Indicates the amount of seconds that 
		/// the mouse is inactive before it is hidden.
		/// </summary>
		/// <remarks>The time is measured in seconds.</remarks>
		private const int ACTIVITY_THRESHOLD = 2;
		#endregion

		#region Members
		private Timer mActivityTimer;
		private TimeSpan mActivityThreshold;
		private DateTime mLastMouseMove;
		private bool mCursorHidden;
		#endregion

		#region Properties
		/// <summary>
		/// The image box for the full screen window.
		/// </summary>
		internal ImageBox ImageBox {
			get;
			set;
		}
		#endregion

		#region Initialization
		/// <summary>
		/// Initialize form components.
		/// </summary>
		public FullScreenForm() {
			this.InitializeComponent();
			this.InitializeFullScreenForm();
			this.InitializeImageBox();
			this.InitializeMouseActivity();
		}

		/// <summary>
		/// Set up the full screen effect for the form.
		/// </summary>
		private void InitializeFullScreenForm() {
			this.WindowState = FormWindowState.Maximized;
			this.FormBorderStyle = FormBorderStyle.None;
			this.TopMost = true;

			// Set to handle keyboard events.
			this.KeyPreview = true;

			SetWinFullScreen(this.Handle);
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
		}

		/// <summary>
		/// Initialize the members relating to the mouse activity.
		/// </summary>
		private void InitializeMouseActivity() {

			MouseHandler mouseHandler = new MouseHandler();
			mouseHandler.MouseMoved += FullScreenForm_MouseMoved;
			Application.AddMessageFilter(mouseHandler);

			this.mActivityTimer = new Timer();
			this.mActivityTimer.Tick += this.ActivityTimer_Tick;
			this.mActivityTimer.Interval = 100;
			this.mActivityTimer.Enabled = true;
			this.mActivityThreshold = TimeSpan.FromSeconds(ACTIVITY_THRESHOLD);
			this.mCursorHidden = false;
		}
		#endregion

		#region Timer Handlers
		/// <summary>
		/// Shows the mouse if has been hidden.
		/// </summary>
		private void FullScreenForm_MouseMoved() {
			this.mLastMouseMove = DateTime.Now;
			if (this.mCursorHidden) {
				Cursor.Show();
				this.mCursorHidden = false;
			}
		}

		/// <summary>
		/// Checks how long the mouse is inactive and hides the cursor 
		/// after no input is detected after a certain amount of time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActivityTimer_Tick(object sender, EventArgs e) {
			TimeSpan elapsedTime = DateTime.Now - this.mLastMouseMove;
			if (((elapsedTime >= this.mActivityThreshold) != this.mCursorHidden) && this.Visible) {
				Cursor.Hide();
				this.mCursorHidden = true;
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
