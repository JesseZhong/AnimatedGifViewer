using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;

namespace AnimatedGifViewer {

	public partial class MainForm : GlassForm {

		// Note: Windows file system is case-insensitive.
		private const string FILE_TYPES = "*.bmp|*.gif|*.jpg|*.jpeg|*.png|*.tiff|*.ico";
		private const string FILE_FILTER =
			"All Image Files |*.bmp;*.dib;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.png;*.tiff;*.ico|" +
			"Bitmap Files (*.bmp; *.dib)|*.bmp;*.dib|" +
			"JPEG (*.jpg; *.jpeg; *.jpe; *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|" +
			"GIF (*.gif)|*.gif|" +
			"PNG (*.png)|*.png|" +
			"TIFF (*.tiff)|*.tiff|" +
			"ICO (*.ico)|*.ico|" +
			"All Files|*.*";
		
		private List<string> filenames;
		private int filenameIndex;
		private string[] arguments;

		/// <summary>
		/// Initializes the components of the main form.
		/// If a file name is passed in through the arguments
		/// of the program, attempt to open the image.
		/// </summary>
		/// <param name="args">Program arguments.</param>
		public MainForm(string[] args = null) {

			// Initialize the form's components.
			this.InitializeComponent();

			// Initialize variables.
			this.filenameIndex = 0;
			this.arguments = args;
		}

		/// <summary>
		/// Attempts to open an image and load all other image 
		/// file names, from the same directory, into the program.
		/// </summary>
		/// <param name="filename">Path of the file that will be loaded.</param>
		/// <returns>Returns true if the file exists and was loaded.</returns>
		private bool OpenImageFile(string filename) {
			if (!File.Exists(filename)) {

				// Disable all the buttons.
				this.EnableButtons(false);
				return false;
			}

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

			// Enable the buttons.
			this.EnableButtons(true);
			return true;
		}

		/// <summary>
		/// Sets the state of the buttons. 
		/// </summary>
		/// <param name="enable">
		/// New status of the buttons.
		/// True: all buttons are enabled.
		/// False: all buttons are disabled.
		/// </param>
		private void EnableButtons(bool enable) {
			this.NextButton.Enabled = enable;
			this.PrevButton.Enabled = enable;
			this.FullScreenButton.Enabled = enable;
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
		}

		/// <summary>
		/// If a file name was passed through arguments,
		/// attempt to open file into the image box.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Shown(object sender, EventArgs e) {

			// Checks if there was a filename passed.
			if (this.arguments.Any())
				this.OpenImageFile(this.arguments[0]);
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

		/// <summary>
		/// Opens a file dialog that allows the user to choose an image to open.
		/// The image will be opened in the image box. The file names of other images
		/// in the directory will be stored, so the user can cycle through them.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenMenuItem_Click(object sender, EventArgs e) {

			// Create a open file dialog with the current directory and file filter.
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
			openFileDialog.Filter = FILE_FILTER;
			openFileDialog.RestoreDirectory = false;

			// Attempt to open a file the user chooses.
			if (openFileDialog.ShowDialog() == DialogResult.OK) 
				this.OpenImageFile(openFileDialog.FileName);
		}

		/// <summary>
		/// Prompts the user and attempts to delete the current image's file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteMenuItem_Click(object sender, EventArgs e) {

			// Confirm with the user that they wish to delete the file.
			//if(MessageBox.Show("Are you sure you wish to move this"))
		}
	}
}
