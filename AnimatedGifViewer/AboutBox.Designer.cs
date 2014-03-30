namespace AnimatedGifViewer {
	partial class AboutBox {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
			this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.LogoBox = new System.Windows.Forms.PictureBox();
			this.CopyrightLabel = new System.Windows.Forms.Label();
			this.CompanyNameLabel = new System.Windows.Forms.Label();
			this.DescriptionBox = new System.Windows.Forms.TextBox();
			this.OkayButton = new System.Windows.Forms.Button();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.ProductNameLabel = new System.Windows.Forms.Label();
			this.TableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LogoBox)).BeginInit();
			this.SuspendLayout();
			// 
			// TableLayoutPanel
			// 
			this.TableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.TableLayoutPanel.ColumnCount = 2;
			this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.79825F));
			this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.20175F));
			this.TableLayoutPanel.Controls.Add(this.LogoBox, 0, 0);
			this.TableLayoutPanel.Controls.Add(this.CopyrightLabel, 1, 2);
			this.TableLayoutPanel.Controls.Add(this.CompanyNameLabel, 1, 3);
			this.TableLayoutPanel.Controls.Add(this.DescriptionBox, 1, 4);
			this.TableLayoutPanel.Controls.Add(this.OkayButton, 1, 5);
			this.TableLayoutPanel.Controls.Add(this.VersionLabel, 1, 1);
			this.TableLayoutPanel.Controls.Add(this.ProductNameLabel, 1, 0);
			this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanel.Location = new System.Drawing.Point(9, 9);
			this.TableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.TableLayoutPanel.Name = "TableLayoutPanel";
			this.TableLayoutPanel.RowCount = 6;
			this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.441861F));
			this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.906977F));
			this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.02326F));
			this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.67442F));
			this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.44444F));
			this.TableLayoutPanel.Size = new System.Drawing.Size(456, 215);
			this.TableLayoutPanel.TabIndex = 0;
			// 
			// LogoBox
			// 
			this.LogoBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LogoBox.Location = new System.Drawing.Point(3, 3);
			this.LogoBox.Name = "LogoBox";
			this.TableLayoutPanel.SetRowSpan(this.LogoBox, 6);
			this.LogoBox.Size = new System.Drawing.Size(139, 209);
			this.LogoBox.TabIndex = 12;
			this.LogoBox.TabStop = false;
			// 
			// CopyrightLabel
			// 
			this.CopyrightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CopyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CopyrightLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.CopyrightLabel.Location = new System.Drawing.Point(155, 52);
			this.CopyrightLabel.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.CopyrightLabel.MaximumSize = new System.Drawing.Size(0, 17);
			this.CopyrightLabel.Name = "CopyrightLabel";
			this.CopyrightLabel.Size = new System.Drawing.Size(298, 17);
			this.CopyrightLabel.TabIndex = 21;
			this.CopyrightLabel.Text = "Copyright";
			this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CompanyNameLabel
			// 
			this.CompanyNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CompanyNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CompanyNameLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.CompanyNameLabel.Location = new System.Drawing.Point(155, 69);
			this.CompanyNameLabel.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.CompanyNameLabel.MaximumSize = new System.Drawing.Size(0, 17);
			this.CompanyNameLabel.Name = "CompanyNameLabel";
			this.CompanyNameLabel.Size = new System.Drawing.Size(298, 17);
			this.CompanyNameLabel.TabIndex = 22;
			this.CompanyNameLabel.Text = "Company Name";
			this.CompanyNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DescriptionBox
			// 
			this.DescriptionBox.BackColor = System.Drawing.Color.White;
			this.DescriptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DescriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DescriptionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DescriptionBox.ForeColor = System.Drawing.SystemColors.GrayText;
			this.DescriptionBox.Location = new System.Drawing.Point(151, 100);
			this.DescriptionBox.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.DescriptionBox.Multiline = true;
			this.DescriptionBox.Name = "DescriptionBox";
			this.DescriptionBox.ReadOnly = true;
			this.DescriptionBox.Size = new System.Drawing.Size(302, 75);
			this.DescriptionBox.TabIndex = 23;
			this.DescriptionBox.TabStop = false;
			this.DescriptionBox.Text = "Description";
			// 
			// OkayButton
			// 
			this.OkayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkayButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.OkayButton.Location = new System.Drawing.Point(378, 189);
			this.OkayButton.Name = "OkayButton";
			this.OkayButton.Size = new System.Drawing.Size(75, 23);
			this.OkayButton.TabIndex = 24;
			this.OkayButton.Text = "&OK";
			this.OkayButton.Click += new System.EventHandler(this.OkayButton_Click);
			// 
			// VersionLabel
			// 
			this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.VersionLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.VersionLabel.Location = new System.Drawing.Point(155, 36);
			this.VersionLabel.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.VersionLabel.MaximumSize = new System.Drawing.Size(0, 17);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(298, 16);
			this.VersionLabel.TabIndex = 0;
			this.VersionLabel.Text = "Version";
			this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ProductNameLabel
			// 
			this.ProductNameLabel.AutoSize = true;
			this.ProductNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ProductNameLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.ProductNameLabel.Location = new System.Drawing.Point(151, 6);
			this.ProductNameLabel.Margin = new System.Windows.Forms.Padding(6, 6, 3, 0);
			this.ProductNameLabel.MaximumSize = new System.Drawing.Size(0, 32);
			this.ProductNameLabel.Name = "ProductNameLabel";
			this.ProductNameLabel.Size = new System.Drawing.Size(78, 29);
			this.ProductNameLabel.TabIndex = 19;
			this.ProductNameLabel.Text = "Name";
			this.ProductNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// AboutBox
			// 
			this.AcceptButton = this.OkayButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(474, 233);
			this.Controls.Add(this.TableLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox";
			this.Padding = new System.Windows.Forms.Padding(9);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.AboutBox_Load);
			this.TableLayoutPanel.ResumeLayout(false);
			this.TableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.LogoBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
		private System.Windows.Forms.PictureBox LogoBox;
		private System.Windows.Forms.Label ProductNameLabel;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.Label CopyrightLabel;
		private System.Windows.Forms.Label CompanyNameLabel;
		private System.Windows.Forms.TextBox DescriptionBox;
		private System.Windows.Forms.Button OkayButton;
	}
}
