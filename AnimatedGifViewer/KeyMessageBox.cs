// KeyMessageBox.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace AnimatedGifViewer {

	/// <summary>
	/// Will display a dialog form with text only.
	/// The form will close when a key is pressed.
	/// The key will then be returned.
	/// </summary>
	public partial class KeyMessageBox : Form {

		#region Members
		private static Keys mKeyPressed;
		private static KeyMessageBox mKeyMessageBox;
		#endregion

		#region Initialize
		/// <summary>
		/// Initializes and instance of the form.
		/// </summary>
		private KeyMessageBox() {
			this.InitializeComponent();
			this.KeyPreview = true;
		}
		#endregion

		#region Show
		/// <summary>
		/// Displays a centered dialog form with a message. It awaits 
		/// for a user key press before closing and returning the input.
		/// </summary>
		/// <param name="parent">The parent form.</param>
		/// <param name="message">Message to display in the form.</param>
		/// <returns>The key sequence the user pressed.</returns>
		public static Keys Show(Form parent, string message) {
			mKeyMessageBox = new KeyMessageBox();
			mKeyMessageBox.Message.Text = message;
			mKeyMessageBox.StartPosition = FormStartPosition.CenterParent;
			mKeyMessageBox.ShowDialog(parent);
			return mKeyPressed;
		}
		#endregion

		#region Keyboard Handlers
		/// <summary>
		/// Processes key commands when they are triggered by user input.
		/// </summary>
		/// <param name="msg">Windows message with details of the user input.</param>
		/// <param name="keyData">Contains data about which key events occurred.</param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			mKeyPressed = keyData;
			mKeyMessageBox.Dispose();
			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion
	}
}
