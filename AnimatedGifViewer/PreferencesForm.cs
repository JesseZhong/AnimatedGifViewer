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

			this.mShortcutsList.Add(new KeyboardShortcut("Next", Keys.Right, Keys.None));
			this.mShortcutsList.Add(new KeyboardShortcut("Previous", Keys.Left, Keys.None));
			
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

			this.ShortcutsGridView.DataSource = this.mShortcutsList;
		}
		#endregion

		#region Work
		/// <summary>
		/// 
		/// </summary>
		/// <param name="shortcutName"></param>
		/// <param name="primaryKey"></param>
		/// <param name="secondaryKey"></param>
		private void AddShortcut(string shortcutName, string primaryKey, string secondaryKey) {

		}
		#endregion

		#region Keyboard Shortcut Events
		private void ShortcutsGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
			const string message = "Enter a new key or press ESC to cancel.";
			var result = KeyMessageBox.Show(this, message);
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
		private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.Hide();
			e.Cancel = true;
		}
		#endregion
	}
}
