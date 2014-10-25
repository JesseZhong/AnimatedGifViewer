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
			this.PreferenceTab = new System.Windows.Forms.TabControl();
			this.GeneralTab = new System.Windows.Forms.TabPage();
			this.KeyboardShortcutsTab = new System.Windows.Forms.TabPage();
			this.SlideShowTab = new System.Windows.Forms.TabPage();
			this.KeyboardShortcutsPanel = new System.Windows.Forms.Panel();
			this.SocialMediaTab = new System.Windows.Forms.TabPage();
			this.KeyboardShortcutsListView = new System.Windows.Forms.ListView();
			this.Shortcut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.PrimaryKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SecondaryKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.PreferenceTab.SuspendLayout();
			this.KeyboardShortcutsTab.SuspendLayout();
			this.KeyboardShortcutsPanel.SuspendLayout();
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
			this.KeyboardShortcutsTab.Controls.Add(this.KeyboardShortcutsPanel);
			this.KeyboardShortcutsTab.Location = new System.Drawing.Point(4, 22);
			this.KeyboardShortcutsTab.Name = "KeyboardShortcutsTab";
			this.KeyboardShortcutsTab.Padding = new System.Windows.Forms.Padding(3);
			this.KeyboardShortcutsTab.Size = new System.Drawing.Size(472, 284);
			this.KeyboardShortcutsTab.TabIndex = 1;
			this.KeyboardShortcutsTab.Text = "Keyboard Shortcuts";
			this.KeyboardShortcutsTab.UseVisualStyleBackColor = true;
			// 
			// SlideShowTab
			// 
			this.SlideShowTab.Location = new System.Drawing.Point(4, 22);
			this.SlideShowTab.Name = "SlideShowTab";
			this.SlideShowTab.Padding = new System.Windows.Forms.Padding(3);
			this.SlideShowTab.Size = new System.Drawing.Size(741, 423);
			this.SlideShowTab.TabIndex = 3;
			this.SlideShowTab.Text = "Slide Show";
			this.SlideShowTab.UseVisualStyleBackColor = true;
			// 
			// KeyboardShortcutsPanel
			// 
			this.KeyboardShortcutsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.KeyboardShortcutsPanel.Controls.Add(this.KeyboardShortcutsListView);
			this.KeyboardShortcutsPanel.Location = new System.Drawing.Point(0, 0);
			this.KeyboardShortcutsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.KeyboardShortcutsPanel.Name = "KeyboardShortcutsPanel";
			this.KeyboardShortcutsPanel.Size = new System.Drawing.Size(472, 284);
			this.KeyboardShortcutsPanel.TabIndex = 0;
			// 
			// SocialMediaTab
			// 
			this.SocialMediaTab.Location = new System.Drawing.Point(4, 22);
			this.SocialMediaTab.Name = "SocialMediaTab";
			this.SocialMediaTab.Padding = new System.Windows.Forms.Padding(3);
			this.SocialMediaTab.Size = new System.Drawing.Size(741, 423);
			this.SocialMediaTab.TabIndex = 2;
			this.SocialMediaTab.Text = "Social Media";
			this.SocialMediaTab.UseVisualStyleBackColor = true;
			// 
			// KeyboardShortcutsListView
			// 
			this.KeyboardShortcutsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.KeyboardShortcutsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Shortcut,
            this.PrimaryKey,
            this.SecondaryKey});
			this.KeyboardShortcutsListView.Location = new System.Drawing.Point(0, 0);
			this.KeyboardShortcutsListView.Margin = new System.Windows.Forms.Padding(0);
			this.KeyboardShortcutsListView.Name = "KeyboardShortcutsListView";
			this.KeyboardShortcutsListView.Size = new System.Drawing.Size(472, 284);
			this.KeyboardShortcutsListView.TabIndex = 2;
			this.KeyboardShortcutsListView.UseCompatibleStateImageBehavior = false;
			// 
			// Shortcut
			// 
			this.Shortcut.Text = "Shortcut";
			// 
			// PrimaryKey
			// 
			this.PrimaryKey.Text = "Primary Key";
			// 
			// SecondaryKey
			// 
			this.SecondaryKey.Text = "Secondary Key";
			// 
			// Preferences
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 334);
			this.Controls.Add(this.PreferenceTab);
			this.Name = "Preferences";
			this.Text = "Preferences";
			this.PreferenceTab.ResumeLayout(false);
			this.KeyboardShortcutsTab.ResumeLayout(false);
			this.KeyboardShortcutsPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl PreferenceTab;
		private System.Windows.Forms.TabPage GeneralTab;
		private System.Windows.Forms.TabPage KeyboardShortcutsTab;
		private System.Windows.Forms.TabPage SlideShowTab;
		private System.Windows.Forms.Panel KeyboardShortcutsPanel;
		private System.Windows.Forms.TabPage SocialMediaTab;
		private System.Windows.Forms.ListView KeyboardShortcutsListView;
		private System.Windows.Forms.ColumnHeader Shortcut;
		private System.Windows.Forms.ColumnHeader PrimaryKey;
		private System.Windows.Forms.ColumnHeader SecondaryKey;
	}
}