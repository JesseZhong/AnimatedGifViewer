// SystemBrowser.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
#endregion

namespace ClockworkControls {

	public partial class SystemBrowser : System.Windows.Forms.UserControl {

		#region Members
		private ImageList mImageList;
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public SystemBrowser() {
			this.InitializeComponent();
			this.InitializeListView();
			this.InitializeTextBox();
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeListView() {
			this.mImageList = new ImageList();
			this.ListView.SmallImageList = this.mImageList;
			this.ListView.View = View.SmallIcon;
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeTextBox() {

		}

		/// <summary>
		/// 
		/// </summary>
		public void UpdateBrowser() {
			DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());

			ListViewItem item;
			FileInfo[] files = dir.GetFiles();
			this.ListView.BeginUpdate();

			for (int i = 0, count = files.Length; i < count; i++) {

				FileInfo file = files[i];
				Icon iconForFile = SystemIcons.WinLogo;

				item = new ListViewItem(file.Name, i);
				iconForFile = Icon.ExtractAssociatedIcon(file.FullName);

				if (!this.mImageList.Images.ContainsKey(file.Extension)) {
					iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
					this.mImageList.Images.Add(file.Extension, iconForFile);
				}
				item.ImageKey = file.Extension;
				this.ListView.Items.Add(item);
			}

			this.ListView.EndUpdate();

			Console.WriteLine(this.ListView.Items.Count);
		}
	}
}
