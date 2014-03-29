using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace AnimatedGifViewer {
	partial class AboutBox : Form {

		

		/// <summary>
		/// Initializes the about box's components.
		/// </summary>
		public AboutBox() {
			this.InitializeComponent();
			this.Text = String.Format("About {0}", AssemblyTitle);
			this.ProductNameLabel.Text = AssemblyProduct;
			this.VersionLabel.Text = String.Format("Version {0}", AssemblyVersion);
			this.CopyrightLabel.Text = AssemblyCopyright;
			this.CompanyNameLabel.Text = AssemblyCompany;
			this.DescriptionBox.Text = AssemblyDescription;

			System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromControl(this).WorkingArea;	// Excludes the task bar.
			this.Location = new System.Drawing.Point((screen.Width - this.Width) / 2,
				(screen.Height - this.Height) / 2);
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0) {
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "") {
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion {
			get {
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0) {
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyProduct {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0) {
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0) {
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0) {
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		private void OkayButton_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void AboutBox_Load(object sender, EventArgs e) {

			// Font adjustment.
			PrivateFontCollection fontCollection = new PrivateFontCollection();
			byte[] fontData = global::AnimatedGifViewer.Properties.Resources.myriad_web_pro;
			IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
			Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
			fontCollection.AddMemoryFont(fontPtr, fontData.Length);
			Marshal.FreeCoTaskMem(fontPtr);

			this.ProductNameLabel.Font = new System.Drawing.Font(fontCollection.Families[0], 18.0f);
			System.Drawing.Font font = new System.Drawing.Font(fontCollection.Families[0], 9.0f);
			this.VersionLabel.Font = font;
			this.CopyrightLabel.Font = font;
			this.CompanyNameLabel.Font = font;
			this.DescriptionBox.Font = font;
		}
	}
}
