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

		// Note: Windows file system is case-insensitive.
		private const string FILE_TYPES = "*.bmp|*.gif|*.exig|*.jpg|*.jpeg|*.png|*.tiff";
		
		private List<string> filenames;
		private int filenameIndex;

		public MainForm(string[] args = null) {

			// Initialize the form's components.
			this.InitializeComponent();

			// Initialize variables.
			this.filenameIndex = 0;

			// Checks if there was a filename 
			// passed and if that file exists.
			string filename;
			if (args.Any() && File.Exists(filename = args[0])) {

				// Load the file into the image box.
				this.ImageBox.Load(filename);

				// Set the working directory to that of the passed file.
				string workingDirectory = Path.GetDirectoryName(filename);
				Directory.SetCurrentDirectory(workingDirectory);

				// Search the directory for other images.
				filenames = this.GetFiles(workingDirectory, FILE_TYPES);

				// Find the index of the filename in the list of filenames.
				// Set the current filename index to that of the filename's.
				if (this.filenames.Contains(filename))
					this.filenameIndex = this.filenames.FindIndex(delegate(string name) {
						return name == filename;
					});

				Console.WriteLine(this.filenames.Count);
				Console.WriteLine(this.filenameIndex);

			} else {

				// Disable all the buttons.
				this.NextButton.Enabled = false;
				this.PrevButton.Enabled = false;
				this.FullScreenButton.Enabled = false;
			}
		}

		/// <summary>
		/// Returns a list of files of a path,
		/// according to the specified search pattern.
		/// </summary>
		/// <param name="path">Path where the files are to be searched for.</param>
		/// <param name="searchPattern">Criteria, delimited by a '|', used for a targetted file search.</param>
		/// <returns></returns>
		private List<string> GetFiles(string path, string searchPattern) {
			if (!Directory.Exists(path))
				throw new Exception("GetFiles(): path does not exist.");

			string[] searchPatterns = searchPattern.Split('|');
			List<string> filenames = new List<string>();
			foreach (string pattern in searchPatterns)
				filenames.AddRange(Directory.GetFiles(path, pattern));
			filenames.Sort();
			return filenames;
		}

		/// <summary>
		/// Increments the current file index up 
		/// and returns the next image's filename.
		/// </summary>
		/// <returns>Filename of the next image.</returns>
		private string NextImage() {

			// Check if there are any files.
			if (!this.filenames.Any())
				throw new Exception("NextImage(): Illegal procedure. There are no files to be found.");

			this.filenameIndex = ((this.filenameIndex + 1) < this.filenames.Count) ? (this.filenameIndex + 1) : 0;
			return this.filenames[this.filenameIndex];
		}

		/// <summary>
		/// Decrements the current file index down
		/// and returns the previous image's filename.
		/// </summary>
		/// <returns>Filename of the previous image.</returns>
		private string PrevImage() {

			// Check if there are any files.
			if (!this.filenames.Any())
				throw new Exception("PrevImage(): Illegal procedure. There are no files to be found.");

			this.filenameIndex = ((this.filenameIndex - 1) < 0) ? (this.filenames.Count - 1) : (this.filenameIndex - 1);
			return this.filenames[this.filenameIndex];
		}

		/// <summary>
		/// Loads additional content when the form is created.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e) {
			this.ImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
			
			this.DrawControlBackgrounds = true;
			this.EnhancedRendering = true;
		}

		/// <summary>
		/// Attempts to load the next image
		/// in the directory into the image box.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NextButton_Click(object sender, EventArgs e) {
			this.ImageBox.Load(this.NextImage());
		}

		/// <summary>
		/// Attempts to load the previous image
		/// in the directory into the image box.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrevButton_Click(object sender, EventArgs e) {
			this.ImageBox.Load(this.PrevImage());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FullScreenButton_Click(object sender, EventArgs e) {

		}
	}
}
