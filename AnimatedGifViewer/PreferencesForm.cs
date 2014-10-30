// PreferencesForm.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
#endregion

namespace AnimatedGifViewer {
	public partial class PreferencesForm : Form {

		#region Members
		private BindingList<KeyboardShortcut> mShortcutsList;
		private BindingList<KeyboardShortcut> mShortcutsDisplayList;
		#endregion

		#region Initialization
		/// <summary>
		/// 
		/// </summary>
		public PreferencesForm() {
			this.InitializeComponent();
			this.InitializeListView();
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeListView() {

			this.mShortcutsList = new BindingList<KeyboardShortcut>();
			this.mShortcutsDisplayList = new BindingList<KeyboardShortcut>();

			this.AddShortcut("Next", Keys.Right, Keys.None);
			this.AddShortcut("Previous", Keys.Left, Keys.None);
			
			this.ShortcutsGridView.AutoGenerateColumns = false;
			
			DataGridViewTextBoxColumn shortcutColumn = new DataGridViewTextBoxColumn();
			shortcutColumn.DataPropertyName = "Shortcut";
			shortcutColumn.HeaderText = "Shortcut";
			shortcutColumn.ReadOnly = true;
			shortcutColumn.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ShortcutsGridView.Columns.Add(shortcutColumn);

			DataGridViewTextBoxColumn primaryKeyColumn = new DataGridViewTextBoxColumn();
			primaryKeyColumn.DataPropertyName = "PrimaryKey";
			primaryKeyColumn.HeaderText = "Primary Key";
			primaryKeyColumn.ReadOnly = true;
			primaryKeyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ShortcutsGridView.Columns.Add(primaryKeyColumn);

			DataGridViewTextBoxColumn secondaryKeyColumn = new DataGridViewTextBoxColumn();
			secondaryKeyColumn.DataPropertyName = "SecondaryKey";
			secondaryKeyColumn.HeaderText = "Secondary Key";
			secondaryKeyColumn.ReadOnly = true;
			secondaryKeyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ShortcutsGridView.Columns.Add(secondaryKeyColumn);

			this.ShortcutsGridView.DataSource = this.mShortcutsDisplayList;
		}
		#endregion

		#region Work
		/// <summary>
		/// 
		/// </summary>
		/// <param name="shortcutName"></param>
		/// <param name="primaryKey"></param>
		/// <param name="secondaryKey"></param>
		private void AddShortcut(string shortcutName, Keys primaryKey, Keys secondaryKey) {
			this.mShortcutsList.Add(new KeyboardShortcut(shortcutName, primaryKey, secondaryKey));
			this.mShortcutsDisplayList.Add(new KeyboardShortcut(shortcutName, primaryKey, secondaryKey));
		}

		/// <summary>
		/// 
		/// </summary>
		private void ConfirmNewKeys() {
			System.Diagnostics.Debug.Assert(this.mShortcutsList.Count == this.mShortcutsDisplayList.Count);
			for (int i = 0, count = this.mShortcutsList.Count; i < count; i++) {
				if (this.mShortcutsDisplayList[i] != this.mShortcutsList[i])
					this.mShortcutsList[i] = this.mShortcutsDisplayList[i];
			}
		}
		#endregion

		#region Keyboard Shortcut Events
		private void ShortcutsGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
			DataGridViewCell cell = (DataGridViewCell)ShortcutsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

			// Change cell text color to red, indicating it is the one being changed.
			System.Drawing.Color originalColor = cell.Style.ForeColor;
			cell.Style.ForeColor = System.Drawing.Color.Red;

			const string message = "Enter a new key or press ESC to cancel.";
			var result = KeyMessageBox.Show(this, message);

			cell.Style.ForeColor = originalColor;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ApplyButton_Click(object sender, EventArgs e) {

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CancelButton_Click(object sender, EventArgs e) {
			const string message = "Discard changes?";
			const string caption = "Are you sure?";
			DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes) {

				this.CancelButton.Enabled = false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DefaultButton_Click(object sender, EventArgs e) {

		}
		#endregion

		#region Form Events
		/// <summary>
		/// Hides the form instead of closing it when the close button is clicked.
		/// </summary>
		/// <param name="sender">PreferenceForm.</param>
		/// <param name="e">Event arguments.</param>
		private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.Hide();
			e.Cancel = true;
		}
		#endregion
	}
}
