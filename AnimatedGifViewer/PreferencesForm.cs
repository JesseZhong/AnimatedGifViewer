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
		/// List of default shortcuts and their keyboard keys.
		/// </summary>
		public static readonly KeyboardShortcut[] DEFAULT_SHORTCUTS = {
			new KeyboardShortcut("Next Image", Keys.Right, Keys.None),
			new KeyboardShortcut("Previous Image", Keys.Left, Keys.None),
			new KeyboardShortcut("Enter Fullscreen", Keys.Up, Keys.None),
			new KeyboardShortcut("Exit Fullscreen", Keys.Down, Keys.None),
			new KeyboardShortcut("Toggle Side Panel", Keys.Tab, Keys.None),
			new KeyboardShortcut("Rotate Image Clockwise", Keys.OemPeriod, Keys.None),
			new KeyboardShortcut("Rotate Image Counter Clockwise", Keys.Oemcomma, Keys.None)
		};

		/// <summary>
		/// The number of shortcuts that are in the program.
		/// </summary>
		public static readonly int NUMBER_OF_SHORTCUTS = DEFAULT_SHORTCUTS.Length;

		/// <summary>
		/// 
		/// </summary>
		private const string DISCARD_PROMPT_MESSAGE = "Are you sure you want to discard your changes?";
		#endregion

		#region Members
		private List<KeyboardShortcut> mShortcutsList;
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
			this.InitializeShortcutsGridView();
		}

		/// <summary>
		/// Adds columns to the shortcuts grid view.
		/// </summary>
		private void InitializeShortcutsGridView() {

			this.mShortcutsList = new List<KeyboardShortcut>();
			this.mShortcutsDisplayList = new BindingList<KeyboardShortcut>();
			
			this.ShortcutsGridView.AutoGenerateColumns = false;
			
			DataGridViewTextBoxColumn shortcutColumn = new DataGridViewTextBoxColumn();
			shortcutColumn.DataPropertyName = "Shortcut";
			shortcutColumn.HeaderText = "Shortcut";
			shortcutColumn.ReadOnly = true;
			shortcutColumn.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlDark;
			shortcutColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

		/// <summary>
		/// Fills out lists of shortcuts using user settings and defaults.
		/// </summary>
		/// <param name="sender">PreferencesForm.</param>
		/// <param name="e">Event arguments.</param>
		private void PreferencesForm_Load(object sender, EventArgs e) {
			
			// Load keyboard shortcut settings.
			Keys[] primaryShortcuts = AnimatedGifViewer.Properties.Settings.Default.PrimaryShortcuts;
			Keys[] secondaryShortcuts = AnimatedGifViewer.Properties.Settings.Default.SecondaryShortcuts;

			int primaryLen = primaryShortcuts.Length;
			int secondaryLen = secondaryShortcuts.Length;

			for (int i = 0; i < NUMBER_OF_SHORTCUTS; i++) {

				KeyboardShortcut defaultShortcut = DEFAULT_SHORTCUTS[i];
				this.AddShortcut(defaultShortcut.Shortcut,
					(i < primaryLen) ? primaryShortcuts[i] : defaultShortcut.PrimaryKey,
					(i < secondaryLen) ? secondaryShortcuts[i] : defaultShortcut.SecondaryKey);
			}
		}
		#endregion

		#region Keyboard Shortcuts Methods
		/// <summary>
		/// Adds a new shortcut for the program.
		/// </summary>
		/// <param name="shortcutName">The name of the shortcut.</param>
		/// <param name="primaryKey">The main key used for the shortcut.</param>
		/// <param name="secondaryKey">The alternate key used for the shortcut.</param>
		private void AddShortcut(string shortcutName, Keys primaryKey, Keys secondaryKey) {
			this.mShortcutsDisplayList.Add(new KeyboardShortcut(shortcutName, primaryKey, secondaryKey));
			this.mShortcutsList.Add(new KeyboardShortcut(shortcutName, primaryKey, secondaryKey));
		}

		/// <summary>
		/// Confirm the list for new shortcuts. Applies changes to the system.
		/// </summary>
		private void ConfirmNewKeys() {
			System.Diagnostics.Debug.Assert(this.mShortcutsList.Count == this.mShortcutsDisplayList.Count);
			for (int i = 0, count = this.mShortcutsList.Count; i < NUMBER_OF_SHORTCUTS; i++) {
				KeyboardShortcut shortcut = this.mShortcutsList[i];
				KeyboardShortcut displayShortcut = this.mShortcutsDisplayList[i];
				if ((i < count) && (shortcut != displayShortcut)) {
					shortcut.Shortcut = displayShortcut.Shortcut;
					shortcut.PrimaryKey = displayShortcut.PrimaryKey;
					shortcut.SecondaryKey = displayShortcut.SecondaryKey;
				} else {
					displayShortcut.Shortcut = DEFAULT_SHORTCUTS[i].Shortcut;
					displayShortcut.PrimaryKey = DEFAULT_SHORTCUTS[i].PrimaryKey;
					displayShortcut.SecondaryKey = DEFAULT_SHORTCUTS[i].SecondaryKey;
				}
			}
			this.CancelButton.Enabled = false;
			this.ApplyButton.Enabled = false;
			this.mShortcutsChanged = false;
		}

		/// <summary>
		/// Resets the list for shortcut changes. Denies the changes to the system.
		/// </summary>
		private void ClearNewKeys() {
			System.Diagnostics.Debug.Assert(this.mShortcutsList.Count == this.mShortcutsDisplayList.Count);
			for (int i = 0, count = this.mShortcutsList.Count; i < NUMBER_OF_SHORTCUTS; i++) {
				KeyboardShortcut shortcut = this.mShortcutsList[i];
				KeyboardShortcut displayShortcut = this.mShortcutsDisplayList[i];
				if ((i < count) && (shortcut != displayShortcut)) {
					displayShortcut.Shortcut = shortcut.Shortcut;
					displayShortcut.PrimaryKey = shortcut.PrimaryKey;
					displayShortcut.SecondaryKey = shortcut.SecondaryKey;
				} else {
					displayShortcut.Shortcut = DEFAULT_SHORTCUTS[i].Shortcut;
					displayShortcut.PrimaryKey = DEFAULT_SHORTCUTS[i].PrimaryKey;
					displayShortcut.SecondaryKey = DEFAULT_SHORTCUTS[i].SecondaryKey;
				}
			}
			this.CancelButton.Enabled = false;
			this.ApplyButton.Enabled = false;
			this.mShortcutsChanged = false;
		}

		/// <summary>
		/// Resets the shortcuts to the programs default list.
		/// </summary>
		private void ResetShortcuts() {
			this.ShortcutsGridView.DataSource = new BindingList<KeyboardShortcut>(DEFAULT_SHORTCUTS);
			this.CancelButton.Enabled = true;
			this.ApplyButton.Enabled = true;
			this.mShortcutsChanged = true;
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

		#region Keyboard Shortcut Events
		/// <summary>
		/// Prompts user for a key for a shortcut when a cell is clicked.
		/// <para>Pressing Escape will cancel process.</para>
		/// </summary>
		/// <param name="sender">ShortcutsGridView.</param>
		/// <param name="e">Event arguments.</param>
		private void ShortcutsGridView_CellClick(object sender, DataGridViewCellEventArgs e) {

			// Ensure that only the key cells are selected. -1 is for headers.
			if ((e.ColumnIndex == 0) || (e.RowIndex == -1))
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

				// Change cell text color to red, indicating that it has been changed.
				cell.Style.ForeColor = System.Drawing.Color.Red;

				// Indicate that a change has been made and activate cancel and apply buttons.
				this.mShortcutsChanged = true;
				this.CancelButton.Enabled = true;
				this.ApplyButton.Enabled = true;
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
			}

			this.Hide();
		}
		#endregion
	}
}
