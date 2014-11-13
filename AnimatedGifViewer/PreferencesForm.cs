// PreferencesForm.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
#endregion

namespace AnimatedGifViewer {
	public partial class PreferencesForm : Form {

		#region Constants and Statics
		/// <summary>
		/// The message prompt displayed when warning the user that they will be discarding any changes they made.
		/// </summary>
		private const string DISCARD_PROMPT_MESSAGE = "Are you sure you want to discard your changes?";

		/// <summary>
		/// The color the text of a cell is changed to when its key is changed.
		/// </summary>
		private static readonly Color KEY_CHANGE_COLOR = Color.Red;
		#endregion

		#region Members
		private List<KeyboardShortcut> mShortcutsList;
		private Dictionary<KeyboardShortcut.Shortcuts, KeyboardShortcut> mShortcutsLookupList;
		private BindingList<KeyboardShortcut> mShortcutsDisplayList;
		private bool mShortcutsChanged;
		private string mAssemblyProduct;
		#endregion

		#region Initialization
		/// <summary>
		/// Initializes the preference form and its features.
		/// </summary>
		public PreferencesForm() {

            // Get assembly information.
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            this.mAssemblyProduct = (attributes.Length == 0) ? "" : ((AssemblyProductAttribute)attributes[0]).Product;

			// Initialize data members.
			this.mShortcutsChanged = false;

			this.InitializeComponent();
			this.InitializeShortcuts();
		}

		/// <summary>
		/// Initialize the shortcuts data grid view and other shortcuts related members.
		/// <para>Attempts to load existing shortcut settings</para>
		/// </summary>
		private void InitializeShortcuts() {

			this.mShortcutsList = new List<KeyboardShortcut>();
			this.mShortcutsDisplayList = new BindingList<KeyboardShortcut>();
			this.mShortcutsLookupList = new Dictionary<KeyboardShortcut.Shortcuts, KeyboardShortcut>();
			
			this.ShortcutsGridView.AutoGenerateColumns = false;
			this.ShortcutsGridView.RowHeadersVisible = false;
			
			DataGridViewTextBoxColumn shortcutColumn = new DataGridViewTextBoxColumn();
			shortcutColumn.Name = KeyboardShortcut.SHORTCUT_PROPERTY_NAME;
			shortcutColumn.DataPropertyName = KeyboardShortcut.SHORTCUT_PROPERTY_NAME;
			shortcutColumn.HeaderText = "Shortcut";
			shortcutColumn.ReadOnly = true;
			shortcutColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			shortcutColumn.DefaultCellStyle.Padding = new Padding(4);

			// Selection doesn't stand out.
			shortcutColumn.DefaultCellStyle.ForeColor = SystemColors.ControlText;
			shortcutColumn.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
			shortcutColumn.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark;
			shortcutColumn.DefaultCellStyle.BackColor = SystemColors.ControlDark;

			this.ShortcutsGridView.Columns.Add(shortcutColumn);

			DataGridViewTextBoxColumn primaryKeyColumn = new DataGridViewTextBoxColumn();
			primaryKeyColumn.Name = KeyboardShortcut.PRIMARY_KEY_PROPERTY_NAME;
			primaryKeyColumn.DataPropertyName = KeyboardShortcut.PRIMARY_KEY_PROPERTY_NAME;
			primaryKeyColumn.HeaderText = "Primary Key";
			primaryKeyColumn.ReadOnly = true;
			primaryKeyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ShortcutsGridView.Columns.Add(primaryKeyColumn);

			DataGridViewTextBoxColumn secondaryKeyColumn = new DataGridViewTextBoxColumn();
			secondaryKeyColumn.Name = KeyboardShortcut.SECONDARY_KEY_PROPERTY_NAME;
			secondaryKeyColumn.DataPropertyName = KeyboardShortcut.SECONDARY_KEY_PROPERTY_NAME;
			secondaryKeyColumn.HeaderText = "Secondary Key";
			secondaryKeyColumn.ReadOnly = true;
			secondaryKeyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ShortcutsGridView.Columns.Add(secondaryKeyColumn);

			this.ShortcutsGridView.DataSource = this.mShortcutsDisplayList;

			// Load keyboard shortcut settings.
			System.Collections.ObjectModel.ObservableCollection<KeyboardShortcut> shortcuts = global::AnimatedGifViewer.Properties.Settings.Default.KeyboardShortcuts;
			Dictionary<KeyboardShortcut.Shortcuts, KeyboardShortcut> hashedShortcuts = new Dictionary<KeyboardShortcut.Shortcuts, KeyboardShortcut>();
			if ((shortcuts != null) && (shortcuts.Any())) {
				for (int i = 0, count = shortcuts.Count; i < count; i++) {
					KeyboardShortcut shortcut = shortcuts[i];
					hashedShortcuts[shortcut.Shortcut] = shortcut;
				}
			}

			// Attempt to load in individual shortcuts, filling in missing ones with defaults.
			for (int i = 0; i < KeyboardShortcut.DEFAULT_SHORTCUTS.Count; i++) {
				KeyboardShortcut shortcut;
				KeyboardShortcut defaultShortcut = KeyboardShortcut.DEFAULT_SHORTCUTS[i];
				this.AddShortcut(hashedShortcuts.TryGetValue(defaultShortcut.Shortcut, out shortcut) ? shortcut : defaultShortcut);
			}
		}
		#endregion

		#region General Methods
		/// <summary>
		/// Indicates if any changes have been made in the preference form.
		/// </summary>
		/// <returns>True if any changes were made.</returns>
		private bool ChangesMade() {
			return this.mShortcutsChanged;
		}
		#endregion

		#region Keyboard Shortcuts Methods
		/// <summary>
		/// Looks up the primary and secondary keys for a given shortcut.
		/// </summary>
		/// <remarks>An exception is thrown if the shortcut requested is unrecognized.</remarks>
		/// <param name="shortcut">The shortcut in question.</param>
		/// <returns>The keyboard shortcuts.</returns>
		public KeyboardShortcut GetKeys(KeyboardShortcut.Shortcuts shortcut) {
			KeyboardShortcut result;
			if (this.mShortcutsLookupList.TryGetValue(shortcut, out result))
				return result;

			throw new Exception(String.Format("Failed to find shortcut {0}.", shortcut.Name));
		}

		/// <summary>
		/// Adds a new shortcut to the internal lists of shortcuts.
		/// </summary>
		/// <param name="shortcut">The name of the shortcut.</param>
		/// <param name="primaryKey">The main key used for the shortcut.</param>
		/// <param name="secondaryKey">The alternate key used for the shortcut.</param>
		private void AddShortcut(KeyboardShortcut.Shortcuts shortcut, Keys primaryKey, Keys secondaryKey) {
			this.AddShortcut(new KeyboardShortcut(shortcut, primaryKey, secondaryKey));
		}

		/// <summary>
		/// Adds a new shortcut to the internal lists of shortcuts.
		/// </summary>
		/// <param name="keyboardShortcut">The new shortcut.</param>
		private void AddShortcut(KeyboardShortcut keyboardShortcut) {
			this.mShortcutsDisplayList.Add(new KeyboardShortcut(keyboardShortcut.Shortcut, keyboardShortcut.PrimaryKey, keyboardShortcut.SecondaryKey));

			// Created shortcut used for internal storage and look up. Same object to be referenced in a iterative list or by hash in a map.
			KeyboardShortcut shortcut = new KeyboardShortcut(keyboardShortcut.Shortcut, keyboardShortcut.PrimaryKey, keyboardShortcut.SecondaryKey);
			this.mShortcutsList.Add(shortcut);
			this.mShortcutsLookupList[shortcut.Shortcut] = shortcut;
		}

		/// <summary>
		/// Confirm the list for new shortcuts. Applies changes to the system.
		/// </summary>
		private void ConfirmNewKeys() {
			System.Diagnostics.Debug.Assert(this.mShortcutsList.Count == this.mShortcutsDisplayList.Count);
			for (int i = 0, count = this.mShortcutsList.Count; i < KeyboardShortcut.DEFAULT_SHORTCUTS.Count; i++) {
				KeyboardShortcut shortcut = this.mShortcutsList[i];
				KeyboardShortcut displayShortcut = this.mShortcutsDisplayList[i];
				if (i < count) {
					if(shortcut != displayShortcut) {
						shortcut.Shortcut = displayShortcut.Shortcut;
						shortcut.PrimaryKey = displayShortcut.PrimaryKey;
						shortcut.SecondaryKey = displayShortcut.SecondaryKey;

						// Reset the cell colors.
						DataGridViewCell primaryCell = (DataGridViewCell)ShortcutsGridView.Rows[i].Cells[this.ShortcutsGridView.Columns[KeyboardShortcut.PRIMARY_KEY_PROPERTY_NAME].Index];
						DataGridViewCell secondaryCell = (DataGridViewCell)ShortcutsGridView.Rows[i].Cells[this.ShortcutsGridView.Columns[KeyboardShortcut.SECONDARY_KEY_PROPERTY_NAME].Index];
						primaryCell.Style.ForeColor = SystemColors.ControlText;
						secondaryCell.Style.ForeColor = SystemColors.ControlText;
					}
				} else
					this.AddShortcut(KeyboardShortcut.DEFAULT_SHORTCUTS[i]);
			}
			this.CancelButton.Enabled = false;
			this.ApplyButton.Enabled = false;
			this.mShortcutsChanged = false;

			// Save shortcuts to settings.
			this.SaveShortcuts();
		}

		/// <summary>
		/// Resets the list for shortcut changes. Denies the changes to the system.
		/// </summary>
		private void ClearNewKeys() {
			System.Diagnostics.Debug.Assert(this.mShortcutsList.Count == this.mShortcutsDisplayList.Count);
			for (int i = 0, count = this.mShortcutsList.Count; i < KeyboardShortcut.DEFAULT_SHORTCUTS.Count; i++) {
				KeyboardShortcut shortcut = this.mShortcutsList[i];
				KeyboardShortcut displayShortcut = this.mShortcutsDisplayList[i];
				if (i < count) {
					if (shortcut != displayShortcut) {
						displayShortcut.Shortcut = shortcut.Shortcut;
						displayShortcut.PrimaryKey = shortcut.PrimaryKey;
						displayShortcut.SecondaryKey = shortcut.SecondaryKey;

						// Reset the cell colors.
						DataGridViewCell primaryCell = (DataGridViewCell)ShortcutsGridView.Rows[i].Cells[this.ShortcutsGridView.Columns[KeyboardShortcut.PRIMARY_KEY_PROPERTY_NAME].Index];
						DataGridViewCell secondaryCell = (DataGridViewCell)ShortcutsGridView.Rows[i].Cells[this.ShortcutsGridView.Columns[KeyboardShortcut.SECONDARY_KEY_PROPERTY_NAME].Index];
						primaryCell.Style.ForeColor = SystemColors.ControlText;
						secondaryCell.Style.ForeColor = SystemColors.ControlText;
					}
				} else
					this.AddShortcut(KeyboardShortcut.DEFAULT_SHORTCUTS[i]);
			}
			this.CancelButton.Enabled = false;
			this.ApplyButton.Enabled = false;
			this.mShortcutsChanged = false;
		}

		/// <summary>
		/// Resets the shortcuts to the programs default list.
		/// </summary>
		/// <remarks>Changes cell text color of the keys that have been changed.</remarks>
		private void ResetShortcuts() {
			for (int i = 0, count = this.mShortcutsDisplayList.Count; i < KeyboardShortcut.DEFAULT_SHORTCUTS.Count; i++) {
				KeyboardShortcut shortcut = this.mShortcutsList[i];
				KeyboardShortcut defaultShortcut = KeyboardShortcut.DEFAULT_SHORTCUTS[i];
				KeyboardShortcut displayShortcut = this.mShortcutsDisplayList[i];
				if (i < count) {
					if (defaultShortcut != displayShortcut) {
						displayShortcut.Shortcut = defaultShortcut.Shortcut;
						displayShortcut.PrimaryKey = defaultShortcut.PrimaryKey;
						displayShortcut.SecondaryKey = defaultShortcut.SecondaryKey;

						// Change text colors.
						DataGridViewCell primaryCell = (DataGridViewCell)ShortcutsGridView.Rows[i].Cells[this.ShortcutsGridView.Columns[KeyboardShortcut.PRIMARY_KEY_PROPERTY_NAME].Index];
						DataGridViewCell secondaryCell = (DataGridViewCell)ShortcutsGridView.Rows[i].Cells[this.ShortcutsGridView.Columns[KeyboardShortcut.SECONDARY_KEY_PROPERTY_NAME].Index];
						primaryCell.Style.ForeColor = (defaultShortcut.PrimaryKey == shortcut.PrimaryKey) ? SystemColors.ControlText : KEY_CHANGE_COLOR;
						secondaryCell.Style.ForeColor = (defaultShortcut.SecondaryKey == shortcut.SecondaryKey) ? SystemColors.ControlText : KEY_CHANGE_COLOR;
					}
				} else
					this.AddShortcut(KeyboardShortcut.DEFAULT_SHORTCUTS[i]);
			}
			this.CancelButton.Enabled = true;
			this.ApplyButton.Enabled = true;
			this.mShortcutsChanged = true;
		}

		/// <summary>
		/// 
		/// </summary>
		private void SaveShortcuts() {
			global::AnimatedGifViewer.Properties.Settings.Default.KeyboardShortcuts = new System.Collections.ObjectModel.ObservableCollection<KeyboardShortcut>(this.mShortcutsList);
			global::AnimatedGifViewer.Properties.Settings.Default.Save();
		}
		#endregion

		#region Keyboard Shortcut Events
		/// <summary>
		/// Prompts user for a key for a shortcut when a cell is clicked.
		/// <para>Pressing Escape will cancel process.</para>
		/// <remarks>Changed cells have the text color changed.</remarks>
		/// </summary>
		/// <param name="sender">ShortcutsGridView.</param>
		/// <param name="e">Event arguments.</param>
		private void ShortcutsGridView_CellClick(object sender, DataGridViewCellEventArgs e) {

			// Ensure that only the key cells are selected. -1 is for headers.
			if ((e.ColumnIndex < 1) || (e.RowIndex < 0))
				return;

			// Display message box, prompting user for a new key for the shortcut.
			const string message = "Enter a new key or press ESC to cancel.";
			Keys result = KeyMessageBox.Show(this, message);

			// If the result is Escape, cancel key change.
			if(Keys.Escape == result)
				return;

			// Check if the entered key is different from the original in the cell.
			DataGridViewCell cell = (DataGridViewCell)ShortcutsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if ((Keys)cell.Value != result) {

				// Change the new displayed key.
				cell.Value = result;

				// Change cell text color, indicating that it has been changed.
				cell.Style.ForeColor = KEY_CHANGE_COLOR;

				// Indicate that a change has been made and activate cancel and apply buttons.
				this.mShortcutsChanged = true;
				this.CancelButton.Enabled = true;
				this.ApplyButton.Enabled = true;

				// De-select the cell.
				cell.Selected = false;
			}
		}

		/// <summary>
		/// Applies the new shortcut changes to the program when the apply button is clicked.
		/// </summary>
		/// <param name="sender">ApplyButton.</param>
		/// <param name="e">Event arguments.</param>
		private void ApplyButton_Click(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(this.ConfirmNewKeys));
			else
				this.ConfirmNewKeys();
		}

		/// <summary>
		/// Prompts the user if they wish to clear their 
		/// shortcut changes when the cancel button is clicked.
		/// </summary>
		/// <param name="sender">CancelButton.</param>
		/// <param name="e">Event arguments.</param>
		private void CancelButton_Click(object sender, EventArgs e) {
			DialogResult result = MessageBox.Show(DISCARD_PROMPT_MESSAGE, this.mAssemblyProduct, MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes) {
				if (this.InvokeRequired)
					this.Invoke(new MethodInvoker(this.ClearNewKeys));
				else
					this.ClearNewKeys();
			}
		}

		/// <summary>
		/// Set the displayed shortcuts to the program defaults when the default button is clicked.
		/// </summary>
		/// <param name="sender">DefaultButton.</param>
		/// <param name="e">Event arguments.</param>
		private void DefaultButton_Click(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(this.ResetShortcuts));
			else
				this.ResetShortcuts();
		}
		#endregion

		#region Form Events
		/// <summary>
		/// Hides the form instead of closing it when the close button is clicked.
		/// <para>Prompt user about changes made.</para>
		/// </summary>
		/// <param name="sender">PreferenceForm.</param>
		/// <param name="e">Event arguments.</param>
		private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = true;

			if (this.ChangesMade()) {
				DialogResult result = MessageBox.Show(DISCARD_PROMPT_MESSAGE, this.mAssemblyProduct, MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes) {

					// Clear new shortcuts.
					if (this.mShortcutsChanged)
						this.ClearNewKeys();
				} else {

					return;
				}

				this.SaveShortcuts();
			}

			this.Hide();
		}
		#endregion
	}
}
