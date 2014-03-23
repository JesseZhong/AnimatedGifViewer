using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AlphaForms;

namespace AnimatedGifViewer {
	public partial class MainForm : AlphaForm {
		
		private string[] fileNames;
		private int currentFileIndex;

		public MainForm(string[] args = null) {

			// Initialize the form's components.
			this.InitializeComponent();

			// Checks if there was a filename 
			// passed and if that file exists.
			string fileName;
			if (args.Any() && File.Exists(fileName = args[0])) {

				// Set the working directory to that of the passed file.
				string workingDirectory = Path.GetDirectoryName(fileName);
				Directory.SetCurrentDirectory(workingDirectory);

				// Search the directory for other images.
				this.fileNames = Directory.GetFiles(workingDirectory);

			} else {

				// Disable all the buttons.
				this.NextButton.Enabled = false;
				this.PrevButton.Enabled = false;
				this.FullScreenButton.Enabled = false;
			}
		}

		/// <summary>
		/// Increments the current file index up 
		/// and returns the next image's filename.
		/// </summary>
		/// <returns>Filename of the next image.</returns>
		public string NextImage() {

			// Check if there are any files.
			if (!this.fileNames.Any())
				throw new Exception("NextImage(): Illegal procedure. There are no files to be found.");

			this.currentFileIndex = (this.currentFileIndex++ < this.fileNames.Length) ? this.currentFileIndex : 0;
			return this.fileNames[this.currentFileIndex];
		}

		/// <summary>
		/// Decrements the current file index down
		/// and returns the previous image's filename.
		/// </summary>
		/// <returns>Filename of the previous image.</returns>
		public string PrevImage() {

			// Check if there are any files.
			if (!this.fileNames.Any())
				throw new Exception("PrevImage(): Illegal procedure. There are no files to be found.");

			this.currentFileIndex = (this.currentFileIndex-- < 0) ? (this.fileNames.Length - 1) : this.currentFileIndex;
			return this.fileNames[this.currentFileIndex];
		}

		private void MainForm_Load(object sender, EventArgs e) {
			this.DrawControlBackgrounds = true;
			this.EnhancedRendering = true;
		}

		private void PrevButton_Click(object sender, EventArgs e) {

		}

		private void NextButton_Click(object sender, EventArgs e) {

		}

		private void FullScreenButton_Click(object sender, EventArgs e) {

		}
	}
}
