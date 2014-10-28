namespace AnimatedGifViewer {
	partial class PreferencesForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.PreferenceTab = new System.Windows.Forms.TabControl();
			this.GeneralTab = new System.Windows.Forms.TabPage();
			this.KeyboardShortcutsTab = new System.Windows.Forms.TabPage();
			this.ShortcutsGridView = new System.Windows.Forms.DataGridView();
			this.SocialMediaTab = new System.Windows.Forms.TabPage();
			this.SlideShowTab = new System.Windows.Forms.TabPage();
			this.DefaultButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.PreferenceTab.SuspendLayout();
			this.KeyboardShortcutsTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ShortcutsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// PreferenceTab
			// 
			this.PreferenceTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PreferenceTab.Controls.Add(this.GeneralTab);
			this.PreferenceTab.Controls.Add(this.KeyboardShortcutsTab);
			this.PreferenceTab.Controls.Add(this.SocialMediaTab);
			this.PreferenceTab.Controls.Add(this.SlideShowTab);
			this.PreferenceTab.Location = new System.Drawing.Point(12, 12);
			this.PreferenceTab.Name = "PreferenceTab";
			this.PreferenceTab.Padding = new System.Drawing.Point(0, 0);
			this.PreferenceTab.SelectedIndex = 0;
			this.PreferenceTab.Size = new System.Drawing.Size(480, 310);
			this.PreferenceTab.TabIndex = 0;
			// 
			// GeneralTab
			// 
			this.GeneralTab.Location = new System.Drawing.Point(4, 22);
			this.GeneralTab.Margin = new System.Windows.Forms.Padding(0);
			this.GeneralTab.Name = "GeneralTab";
			this.GeneralTab.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTab.Size = new System.Drawing.Size(472, 284);
			this.GeneralTab.TabIndex = 0;
			this.GeneralTab.Text = "General";
			this.GeneralTab.UseVisualStyleBackColor = true;
			// 
			// KeyboardShortcutsTab
			// 
			this.KeyboardShortcutsTab.Controls.Add(this.ApplyButton);
			this.KeyboardShortcutsTab.Controls.Add(this.CancelButton);
			this.KeyboardShortcutsTab.Controls.Add(this.DefaultButton);
			this.KeyboardShortcutsTab.Controls.Add(this.ShortcutsGridView);
			this.KeyboardShortcutsTab.Location = new System.Drawing.Point(4, 22);
			this.KeyboardShortcutsTab.Name = "KeyboardShortcutsTab";
			this.KeyboardShortcutsTab.Padding = new System.Windows.Forms.Padding(3);
			this.KeyboardShortcutsTab.Size = new System.Drawing.Size(472, 284);
			this.KeyboardShortcutsTab.TabIndex = 1;
			this.KeyboardShortcutsTab.Text = "Keyboard Shortcuts";
			this.KeyboardShortcutsTab.UseVisualStyleBackColor = true;
			// 
			// ShortcutsGridView
			// 
			this.ShortcutsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ShortcutsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.ShortcutsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ShortcutsGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.ShortcutsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.ShortcutsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.ShortcutsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
			this.ShortcutsGridView.Location = new System.Drawing.Point(0, 0);
			this.ShortcutsGridView.Name = "ShortcutsGridView";
			this.ShortcutsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.ShortcutsGridView.Size = new System.Drawing.Size(472, 263);
			this.ShortcutsGridView.TabIndex = 1;
			// 
			// SocialMediaTab
			// 
			this.SocialMediaTab.Location = new System.Drawing.Point(4, 22);
			this.SocialMediaTab.Name = "SocialMediaTab";
			this.SocialMediaTab.Padding = new System.Windows.Forms.Padding(3);
			this.SocialMediaTab.Size = new System.Drawing.Size(472, 261);
			this.SocialMediaTab.TabIndex = 2;
			this.SocialMediaTab.Text = "Social Media";
			this.SocialMediaTab.UseVisualStyleBackColor = true;
			// 
			// SlideShowTab
			// 
			this.SlideShowTab.Location = new System.Drawing.Point(4, 22);
			this.SlideShowTab.Name = "SlideShowTab";
			this.SlideShowTab.Padding = new System.Windows.Forms.Padding(3);
			this.SlideShowTab.Size = new System.Drawing.Size(472, 261);
			this.SlideShowTab.TabIndex = 3;
			this.SlideShowTab.Text = "Slide Show";
			this.SlideShowTab.UseVisualStyleBackColor = true;
			// 
			// DefaultButton
			// 
			this.DefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.DefaultButton.Location = new System.Drawing.Point(0, 261);
			this.DefaultButton.Name = "DefaultButton";
			this.DefaultButton.Size = new System.Drawing.Size(75, 23);
			this.DefaultButton.TabIndex = 2;
			this.DefaultButton.Text = "Default";
			this.DefaultButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.Location = new System.Drawing.Point(397, 261);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(75, 23);
			this.CancelButton.TabIndex = 3;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// ApplyButton
			// 
			this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ApplyButton.Location = new System.Drawing.Point(316, 261);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(75, 23);
			this.ApplyButton.TabIndex = 4;
			this.ApplyButton.Text = "Apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			// 
			// PreferencesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 334);
			this.Controls.Add(this.PreferenceTab);
			this.Name = "PreferencesForm";
			this.Text = "Preferences";
			this.PreferenceTab.ResumeLayout(false);
			this.KeyboardShortcutsTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ShortcutsGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl PreferenceTab;
		private System.Windows.Forms.TabPage GeneralTab;
		private System.Windows.Forms.TabPage KeyboardShortcutsTab;
		private System.Windows.Forms.TabPage SlideShowTab;
		private System.Windows.Forms.TabPage SocialMediaTab;
		private System.Windows.Forms.DataGridView ShortcutsGridView;
		private System.Windows.Forms.Button DefaultButton;
		private System.Windows.Forms.Button ApplyButton;
		private System.Windows.Forms.Button CancelButton;
	}
}