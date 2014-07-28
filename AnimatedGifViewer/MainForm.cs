// MainForm.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell;
#endregion

namespace AnimatedGifViewer {
	public partial class MainForm : Form {

		#region Constants
		/// <summary>
		/// The vertical padding above and below the image box.
		/// </summary>
		private const int IMG_BOX_H_PAD = 94;

		/// <summary>
		/// The filter used by the program to scan
		/// for image files in the working directory.
		/// </summary>
		/// <remarks>Note: Windows file system is case-insensitive.</remarks>
		private const string FILE_TYPES = "*.bmp|*.dib|*.jpg|*.jpeg|*.jpe|*.jfif|*.gif|*.png|*.tiff|*.ico";

		/// <summary>
		/// The filter used by the file dialog to let the 
		/// user choose what types of files to view and open.
		/// </summary>
		private const string FILE_FILTER =
			"All Image Files |*.bmp;*.dib;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.png;*.tiff;*.ico|" +
			"Bitmap Files (*.bmp; *.dib)|*.bmp;*.dib|" +
			"JPEG (*.jpg; *.jpeg; *.jpe; *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|" +
			"GIF (*.gif)|*.gif|" +
			"PNG (*.png)|*.png|" +
			"TIFF (*.tiff)|*.tiff|" +
			"ICO (*.ico)|*.ico|" +
			"All Files|*.*";
		#endregion

		#region Members
		private ImageBox mImageBox;
		private MainFormImageBoxMenu mImageBoxMenu;
		private FullScreenForm mFullScreenForm;
		private System.Windows.Forms.ToolTip mToolTip;
		private System.Windows.Forms.TrackBar mSlider;

		private List<string> mFilenames;
		private int mFilenameIndex;
		private string[] mArguments;
		private string mLoadedFile;
		private string mAssemblyProduct;

		private FileSystemWatcher mWatcher = new FileSystemWatcher();
		private Dictionary<Button, ButtonImageSet> mButtonImages = new Dictionary<Button, ButtonImageSet>();

		private delegate void MainFormDelegate();
		private MainFormDelegate mLoadedFilenames;
		#endregion

		#region Work
		/// <summary>
		/// Attempts to open an image and load all other image 
		/// file names, from the same directory, into the program.
		/// This function will also initialize a delegate that will
		/// update the list for the file names in the directory when
		/// ever the file system detects a file is created, deleted,
		/// changed, or renamed.
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
			this.mImageBox.Image = this.LoadImage(filename);

			// Set the working directory to that of the passed file.
			string workingDirectory = Path.GetDirectoryName(filename);
			Directory.SetCurrentDirectory(workingDirectory);

			// Initialize the file watcher.
			this.InitFileWatcher();

			// Initialize the delegate so that it will grab all the
			// files in the same directory as the originally loaded file.
			#region this.loadFileNames
			this.mLoadedFilenames = delegate() {

				// Search the directory for other images.
				this.mFilenames = this.GetFiles(workingDirectory, FILE_TYPES);

				// Disable buttons and clear the image
				// box if no images exist in the folder.
				if (!this.mFilenames.Any()) {
					this.EnableButtons(false);
					this.mImageBox.Image = null;
					return;
				}

				// Find the index of the filename in the list of filenames.
				// Set the current filename index to that of the filename's.
				if (this.mFilenames.Contains(this.mLoadedFile))
					this.mFilenameIndex = this.mFilenames.FindIndex(delegate(string name) {
						return name == this.mLoadedFile;
					});

				// Attempt to load the original image into the image box.
				if (this.CheckFilenamesBounds(this.mFilenameIndex))
					this.mImageBox.Image = this.LoadImage(this.mFilenames[this.mFilenameIndex]);
				else
					this.mImageBox.Image = null;

				// Enable the buttons.
				this.EnableButtons(true);
			};
			#endregion

			// Grab all the files from the directory.
			this.mLoadedFilenames();
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
			this.SizeButton.Enabled = enable;
			this.RotateCounterButton.Enabled = enable;
			this.RotateClockwiseButton.Enabled = enable;
			this.DeleteButton.Enabled = enable;

			ButtonImageSet.EState state = (enable)
				? ButtonImageSet.EState.Active
				: ButtonImageSet.EState.Inactive;

			foreach (KeyValuePair<Button, ButtonImageSet> item in this.mButtonImages) {
				item.Key.BackgroundImage = item.Value.GetImage(state);
			}

			// Add tool tips to the buttons.
			this.mToolTip.SetToolTip(this.NextButton, enable ? global::AnimatedGifViewer.Properties.Resources.NextButtonToolTip : "");
			this.mToolTip.SetToolTip(this.PrevButton, enable ? global::AnimatedGifViewer.Properties.Resources.PrevButtonToolTip : "");
			this.mToolTip.SetToolTip(this.FullScreenButton, enable ? global::AnimatedGifViewer.Properties.Resources.FullScreenButtonToolTip : "");
			this.mToolTip.SetToolTip(this.SizeButton, enable ? global::AnimatedGifViewer.Properties.Resources.SizeButtonToolTip : "");
			this.mToolTip.SetToolTip(this.RotateCounterButton, enable ? global::AnimatedGifViewer.Properties.Resources.RotateCounterButtonToolTip : "");
			this.mToolTip.SetToolTip(this.RotateClockwiseButton, enable ? global::AnimatedGifViewer.Properties.Resources.RotateClockwiseButtonToolTip : "");
			this.mToolTip.SetToolTip(this.DeleteButton, enable ? global::AnimatedGifViewer.Properties.Resources.DeleteButtonToolTip : "");
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
		/// Returns the name of a image file format when given the extension.
		/// </summary>
		/// <param name="filename">The extension. It must be led by a '.'</param>
		/// <returns>The name of the file format.</returns>
		private string GetFormatName(string extension) {
			switch (extension.ToLower()) {
				case @".bmp":
				case @".dib":
					return "Bitmap";
				case @".jpg":
				case @".jpeg":
				case @".jpe":
				case @".jfif":
					return "JPEG";
				case @".gif":
					return "GIF";
				case @".png":
					return "PNG";
				case @".tiff":
					return "TIFF";
				case @".ico":
					return "ICO";
				case @"":
					return "File";
				default:
					return "Unknown File Format";
			}
		}

		/// <summary>
		/// Returns the image format provided a file extension.
		/// </summary>
		/// <param name="extension">The extension. It must be led by a '.'</param>
		/// <returns>The file format.</returns>
		private System.Drawing.Imaging.ImageFormat GetFormat(string extension) {
			switch (extension.ToLower()) {
				case @".bmp":
				case @".dib":
					return System.Drawing.Imaging.ImageFormat.Bmp;
				case @".jpg":
				case @".jpeg":
				case @".jpe":
				case @".jfif":
					return System.Drawing.Imaging.ImageFormat.Jpeg;
				case @".gif":
					return System.Drawing.Imaging.ImageFormat.Gif;
				case @".png":
					return System.Drawing.Imaging.ImageFormat.Png;
				case @".tiff":
					return System.Drawing.Imaging.ImageFormat.Tiff;
				case @".ico":
					return System.Drawing.Imaging.ImageFormat.Icon;
				default:
					return null;
			}
		}

		/// <summary>
		/// Check if an index is within the bounds of filenames.
		/// </summary>
		/// <param name="index">The index in question.</param>
		/// <returns>True if in bounds, false otherwise.</returns>
		private bool CheckFilenamesBounds(int index) {
			if ((this.mFilenames.Any()) && 
				(index < this.mFilenames.Count) && 
				(index >= 0))
				return true;
			return false;
		}

		/// <summary>
		/// Loads an image from file, makes a copy 
		/// to memory, and then releases the file.
		/// </summary>
		/// <param name="filename">Name of the image.</param>
		/// <returns>Returns a copy of the image.</returns>
		private Image LoadImage(string filename) {

			if (File.Exists(filename)) {

				byte[] bytes = File.ReadAllBytes(filename);
				MemoryStream ms = new MemoryStream(bytes);
				Image img = Image.FromStream(ms);
				this.mLoadedFile = filename;

				// Change the title of the form to have the file name.
				MainFormDelegate changeText = delegate() {
					this.Text = String.Format("{0} - {1}",
						Path.GetFileName(filename),
						this.mAssemblyProduct);
				};
				if (this.InvokeRequired)
					this.Invoke(changeText);
				else
					changeText();

				return img;

			} else
				return null;
		}
		
		/// <summary>
		/// Increments the current file index up 
		/// and returns the next image's filename.
		/// </summary>
		/// <returns>Filename of the next image.</returns>
		private string NextImage() {

			// Check if there are any files.
			if (!this.mFilenames.Any())
				throw new Exception("NextImage(): Illegal procedure. There are no files to be found.");

			this.mFilenameIndex = ((this.mFilenameIndex + 1) < this.mFilenames.Count) ? (this.mFilenameIndex + 1) : 0;
			return this.mFilenames[this.mFilenameIndex];
		}

		/// <summary>
		/// Decrements the current file index down
		/// and returns the previous image's filename.
		/// </summary>
		/// <returns>Filename of the previous image.</returns>
		private string PrevImage() {

			// Check if there are any files.
			if (!this.mFilenames.Any())
				throw new Exception("PrevImage(): Illegal procedure. There are no files to be found.");

			this.mFilenameIndex = ((this.mFilenameIndex - 1) < 0) ? (this.mFilenames.Count - 1) : (this.mFilenameIndex - 1);
			return this.mFilenames[this.mFilenameIndex];
		}

		/// <summary>
		/// Rotates the current image in the image box.
		/// </summary>
		/// <remarks>
		/// The original file is loaded, rotated, and then saved.
		/// Because of the way System.Drawing.Image treats GIFs,
		/// GIFs will be ignored, as to not damage the original image.
		/// </remarks>
		private void RotateImage(System.Drawing.RotateFlipType rotateType) {

			// Ignore if the file is a GIF.
			string ext = Path.GetExtension(this.mLoadedFile);
			if ("GIF" == this.GetFormatName(ext))
				return;

			// Check if the image is on the disk.
			if (File.Exists(this.mLoadedFile)) {

				// Load the file into memory, rotate it, and save it.
				Image imgFile = System.Drawing.Image.FromFile(this.mLoadedFile);
				imgFile.RotateFlip(rotateType);
				imgFile.Save(this.mLoadedFile);
			}
		}

		/// <summary>
		/// Attempts to delete the currently
		/// displayed image in the image box. 
		/// </summary>
		private void DeleteImage() {

			// Attempt to delete the current file.
			if (this.mFilenames.Any() && this.CheckFilenamesBounds(this.mFilenameIndex)) {
				bool deleted = FileOperationAPIWrapper.Send(this.mFilenames[this.mFilenameIndex]);
				if (deleted)
					this.NextButton.PerformClick();
			}
		}

		/// <summary>
		/// Gives the user a dialog to save a copy of the current image.
		/// </summary>
		private void MakeImageCopy() {

			// Ensure that the file is still in the directory.
			if (File.Exists(this.mLoadedFile)) {
				Stream stream;
				SaveFileDialog saveFileDialog = new SaveFileDialog();

				// Check the file name's extension and add a filter
				// to the save file dialog with the same extension.
				string ext = Path.GetExtension(this.mLoadedFile);
				if((ext != String.Empty) && (this.mImageBox.Image != null))
					saveFileDialog.Filter = this.GetFormatName(ext) + "|*" + ext;

				saveFileDialog.RestoreDirectory = false;
				saveFileDialog.FileName = Path.GetFileName(this.mLoadedFile);

				// Show the dialog.
				if (saveFileDialog.ShowDialog() == DialogResult.OK) {

					// Check that the file name selected isn't the same as 
					// the original. Pretend it is saved if it is the original.
					if (saveFileDialog.FileName != this.mLoadedFile) {

						if ((stream = saveFileDialog.OpenFile()) != null) {

							// Check again that the original file exists 
							// before attempting to write to the new file.
							if (File.Exists(this.mLoadedFile) &&
								(saveFileDialog.FileName != string.Empty)) {
								byte[] bytes = File.ReadAllBytes(this.mLoadedFile);
								stream.Write(bytes, 0, bytes.Length);

							} else {
								string message = "The original file \""
									+ Path.GetFileName(this.mLoadedFile)
									+ " could not be found.";
								const string caption = "File Missing";
								DialogResult result = MessageBox.Show(message, caption,
									MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							stream.Close();
						}
					}
				}
			}
		}

		/// <summary>
		/// Makes a copy of the current image box image to the clipboard.
		/// </summary>
		private void CopyImageToClipboard() {
			if(File.Exists(this.mLoadedFile))
				Clipboard.SetImage(this.mImageBox.Image);
		}

		/// <summary>
		/// Displays the properties dialog for the image in the image box.
		/// </summary>
		private void ShowImageProperties() {
			if (File.Exists(this.mLoadedFile))
				ShowFileProperties(this.mLoadedFile);
		}

		/// <summary>
		/// Sets the current image in the image box as the desktop background.
		/// </summary>
		/// <remarks>
		/// For security reasons, only BMPs are allowed to be used for
		/// desktop backgrounds. As a result, any image that is to be
		/// used as a background must be first converted into a BMP.
		/// </remarks>
		private void SetAsDesktopBackground() {
			if (File.Exists(this.mLoadedFile)) {
				string tempPath = Path.Combine(Path.GetTempPath(), "Wallpaper.bmp");
				this.mImageBox.Image.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

				// In the event that the user or the program does not have system permissions
				// to use the temp directory, the file will not be saved and will not be found
				// by this program. A check must be in place in the event that that happens.
				if(File.Exists(tempPath))
					SystemParametersInfo(SPI_SETDESKWALLPAPER, 0,
						tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
			}
		}

		/// <summary>
		/// Opens the windows explorer with the location of the current image. 
		/// </summary>
		/// <remarks>The file will be selected when the explorer is opened.</remarks>
		private void OpenFileLocation() {
			if (File.Exists(this.mLoadedFile)) {
				System.Diagnostics.Process.Start("explorer.exe", "/select, " + this.mLoadedFile);
			}
		}

		/// <summary>
		/// Attempts to assign the loaded image into the full screen image box.
		/// </summary>
		private void ShowImageInFullScreen() {
			if (this.mFullScreenForm.Visible) {
				MainFormDelegate assign = delegate() {
					this.mFullScreenForm.ImageBox.Image = this.mImageBox.Image;
					this.mFullScreenForm.ImageBox.FitUpToWindow();
				};
				if (this.mFullScreenForm.InvokeRequired)
					this.mFullScreenForm.Invoke(assign);
				else
					assign();
			}
		}
		#endregion

		#region Loading
		/// <summary>
		/// Initializes the components of the main form.
		/// If a file name is passed in through the arguments
		/// of the program, attempt to open the image.
		/// </summary>
		/// <param name="args">Program arguments.</param>
		public MainForm(string[] args = null) {

			// Initialize the form's components.
			this.InitializeComponent();
			this.InitializeImageBox();

			// Initialize variables.
			this.mFilenameIndex = 0;
			this.mArguments = args;
			this.mFilenames = new List<string>();

			// Get assembly information.
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			this.mAssemblyProduct = (attributes.Length == 0) ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
		}

		/// <summary>
		/// Initializes the image box to fit into and anchor
		/// onto the MainForm.
		/// </summary>
		private void InitializeImageBox() {
			this.mImageBox = new ImageBox();
			this.mImageBox.Border = System.Windows.Forms.BorderStyle.None;
			this.mImageBox.Anchor = (System.Windows.Forms.AnchorStyles)
				(AnchorStyles.Top | AnchorStyles.Bottom |
				AnchorStyles.Left | AnchorStyles.Right);

			this.mImageBox.Location = new Point(0, 24);
			this.mImageBox.Margin = new System.Windows.Forms.Padding(0);
			this.mImageBox.Name = "ImageBox";
			this.mImageBox.Size = new System.Drawing.Size(this.ClientSize.Width,
				(this.ClientSize.Height > IMG_BOX_H_PAD ? this.ClientSize.Height - IMG_BOX_H_PAD : this.ClientSize.Height));
			this.mImageBox.TabIndex = 0;
			this.mImageBox.TabStop = false;
			this.Controls.Add(this.mImageBox);

			// ImageBoxMenu.
			this.mImageBoxMenu = new MainFormImageBoxMenu();
			this.mImageBoxMenu.Name = "ImageBoxMenu";
			this.mImageBoxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.mImageBoxMenu.Size = new System.Drawing.Size(180, 70);
			this.mImageBox.ContextMenuStrip = this.mImageBoxMenu;

			// Context menu event handlers.
			this.mImageBoxMenu.SetAsDesktopMenuItem.Click += new System.EventHandler(this.ImageBoxMenuSetAsDesktop);
			this.mImageBoxMenu.OpenLocationMenuItem.Click += new System.EventHandler(this.ImageBoxMenuOpenLocation);
			this.mImageBoxMenu.RotateClockwiseMenuItem.Click += new System.EventHandler(this.ImageBoxMenuRotateClockwise_Click);
			this.mImageBoxMenu.RotateCounterCMenuItem.Click += new System.EventHandler(this.ImageBoxMenuRotateCounterC_Click);
			this.mImageBoxMenu.CopyMenuItem.Click += new System.EventHandler(this.ImageBoxMenuCopy_Click);
			this.mImageBoxMenu.DeleteMenuItem.Click += new System.EventHandler(this.ImageBoxMenuDelete_Click);
			this.mImageBoxMenu.PropertiesMenuItem.Click += new System.EventHandler(this.ImageBoxMenuProperties_Click);
		}

		/// <summary>
		/// Initializes the system file watcher to
		/// begin watching for file changed, deleted,
		/// created, or renamed events to be raised.
		/// </summary>
		private void InitFileWatcher() {
			this.mWatcher = new FileSystemWatcher();
			this.mWatcher.Path = Directory.GetCurrentDirectory();
			this.mWatcher.NotifyFilter = NotifyFilters.LastAccess |
				NotifyFilters.LastWrite | NotifyFilters.FileName |
				NotifyFilters.DirectoryName;
			//this.watcher.Filter = FILE_TYPES;

			// Add event handlers.
			this.mWatcher.Changed += new FileSystemEventHandler(this.FileSystem_Changed);
			this.mWatcher.Created += new FileSystemEventHandler(this.FileSystem_Changed);
			this.mWatcher.Deleted += new FileSystemEventHandler(this.FileSystem_Changed);
			this.mWatcher.Renamed += new RenamedEventHandler(this.FileSystem_Renamed);

			// Start watching for events.
			this.mWatcher.EnableRaisingEvents = true;
		}

		/// <summary>
		/// Loads additional content when the form is created.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Load(object sender, EventArgs e) {

			// MainForm.
			this.Text = this.mAssemblyProduct;
			this.Size = global::AnimatedGifViewer.Properties.Settings.Default.FormSize;
			this.Location = global::AnimatedGifViewer.Properties.Settings.Default.FormLocation;
			this.WindowState = global::AnimatedGifViewer.Properties.Settings.Default.FormWindowState;

			// Image box.
			this.mImageBox.SizeMode = PictureBoxSizeMode.CenterImage;

			// Full Screen Form.
			this.mFullScreenForm = new FullScreenForm();
			this.mFullScreenForm.Hide();

			// Tool tip settings.
			this.mToolTip = new ToolTip();
			this.mToolTip.AutomaticDelay = 5000;
			this.mToolTip.InitialDelay = 1000;
			this.mToolTip.ReshowDelay = 500;
			this.mToolTip.ShowAlways = true;

			// Slider.
			this.mSlider = new System.Windows.Forms.TrackBar();
			
			// Buttons.
			this.EnableButtons(false);

			// Load the image sets for each button.
			this.mButtonImages.Add(this.PrevButton, 
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Previous));
			this.mButtonImages.Add(this.NextButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Next));
			this.mButtonImages.Add(this.FullScreenButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_FullScreen));
			this.mButtonImages.Add(this.SizeButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Size));
			this.mButtonImages.Add(this.RotateCounterButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_RotateCounter));
			this.mButtonImages.Add(this.RotateClockwiseButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_RotateClockwise));
			this.mButtonImages.Add(this.DeleteButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Delete));

			// Mouse enter events.
			this.PrevButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.NextButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.FullScreenButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.SizeButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.RotateCounterButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.RotateClockwiseButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.DeleteButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);

			// Mouse leave events.
			this.PrevButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			this.NextButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			this.FullScreenButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			this.SizeButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			this.RotateCounterButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			this.RotateClockwiseButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			this.DeleteButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);

			// Mouse down events.
			this.PrevButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
			this.NextButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
			this.FullScreenButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
			this.SizeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
			this.RotateCounterButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
			this.RotateClockwiseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
			this.DeleteButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);

			// Mouse up events.
			this.PrevButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
			this.NextButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
			this.FullScreenButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
			this.SizeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
			this.RotateCounterButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
			this.RotateClockwiseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
			this.DeleteButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);

			// Form events.
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
			this.Move += new System.EventHandler(this.MainForm_Move);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);

			// Set to handle keyboard events.
			this.mFullScreenForm.ProcessCmdKeyEvent += new Action<Keys>(this.KeyDownHandler);
			this.mFullScreenForm.ProcessCmdKeyEvent += this.FullScreenForm_ExitFullScreen;
			this.KeyPreview = true;
		}

		/// <summary>
		/// If a file name was passed through arguments,
		/// attempt to open file into the image box.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Shown(object sender, EventArgs e) {

			// Checks if there was a filename passed.
			if (this.mArguments.Any())
				this.OpenImageFile(this.mArguments[0]);
		}
		#endregion

		#region Form Handlers
		/// <summary>
		/// Return visual components to their default 
		/// state when the form falls out of focus.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Deactivate(object sender, EventArgs e) {

			// Change the state image for each 
			// button when the form falls out of focus.
			foreach (KeyValuePair<Button, ButtonImageSet> item in this.mButtonImages) {
				if (item.Key.Enabled) {
					item.Key.BackgroundImage = item.Value.GetImage(ButtonImageSet.EState.Active);
				}
			}
		}

		/// <summary>
		/// Save the form's location to the user settings when the form is moved.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Move(object sender, EventArgs e) {
			if (this.WindowState != FormWindowState.Maximized)
				global::AnimatedGifViewer.Properties.Settings.Default.FormLocation = this.Location;
			global::AnimatedGifViewer.Properties.Settings.Default.FormWindowState = this.WindowState;
			global::AnimatedGifViewer.Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Save the form's size to the user settings when the form is resized.
		/// If there is a state change, correct the form's size accordingly.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Resize(object sender, EventArgs e) {
			if (this.WindowState != FormWindowState.Maximized)
				global::AnimatedGifViewer.Properties.Settings.Default.FormSize = this.Size;
			global::AnimatedGifViewer.Properties.Settings.Default.FormWindowState = this.WindowState;
			global::AnimatedGifViewer.Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Save user settings when the form closes.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Closing(object sender, FormClosingEventArgs e) {
			global::AnimatedGifViewer.Properties.Settings.Default.FormWindowState = this.WindowState;
			if (this.WindowState != FormWindowState.Maximized) {
				global::AnimatedGifViewer.Properties.Settings.Default.FormSize = this.Size;
				global::AnimatedGifViewer.Properties.Settings.Default.FormLocation = this.Location;
			}
			global::AnimatedGifViewer.Properties.Settings.Default.Save();
		}
		#endregion

		#region Button Handlers
		/// <summary>
		/// Attempts to load the next image
		/// in the directory into the image box.
		/// </summary>
		/// <param name="sender">NextButton</param>
		/// <param name="e">Event arguments.</param>
		private void NextButton_Click(object sender, EventArgs e) {
			if (this.mFilenames.Any()) {
				this.mImageBox.Image = this.LoadImage(this.NextImage());
				this.ShowImageInFullScreen();
			}
		}

		/// <summary>
		/// Attempts to load the previous image
		/// in the directory into the image box.
		/// </summary>
		/// <param name="sender">PrevButton</param>
		/// <param name="e">Event arguments.</param>
		private void PrevButton_Click(object sender, EventArgs e) {
			if (this.mFilenames.Any()) {
				this.mImageBox.Image = this.LoadImage(this.PrevImage());
				this.ShowImageInFullScreen();
			}
		}

		/// <summary>
		/// Shows the FullScreenForm when the full screen button is pressed.
		/// </summary>
		/// <param name="sender">FullScreenButton</param>
		/// <param name="e">Event arguments.</param>
		private void FullScreenButton_Click(object sender, EventArgs e) {
			if (this.mFilenames.Any()) {
				this.mFullScreenForm.Show();
				this.ShowImageInFullScreen();
				this.Hide();
			}
		}

		/// <summary>
		/// Rotates the image 90 degrees counterclockwise 
		/// when the rotate counterclockwise button is clicked.
		/// </summary>
		/// <param name="sender">RotateCounterButton</param>
		/// <param name="e">Event arguments.</param>
		private void RotateCounterButton_Click(object sender, EventArgs e) {
			this.RotateImage(System.Drawing.RotateFlipType.Rotate270FlipNone);
		}

		/// <summary>
		/// Rotates the image 90 degrees clockwise when
		/// the rotate clockwise button is clicked.
		/// </summary>
		/// <param name="sender">RotateClockwiseButton</param>
		/// <param name="e">Event arguments.</param>
		private void RotateClockwiseButton_Click(object sender, EventArgs e) {
			this.RotateImage(System.Drawing.RotateFlipType.Rotate90FlipNone);
		}

		/// <summary>
		/// Attempts to delete the current image.
		/// </summary>
		/// <param name="sender">DeleteButton</param>
		/// <param name="e">Event arguments.</param>
		private void DeleteButton_Click(object sender, EventArgs e) {
			this.DeleteImage();
		}
		#endregion

		#region Menu Bar Handlers
		/// <summary>
		/// Opens a file dialog that allows the user to choose an image to open.
		/// The image will be opened in the image box. The file names of other images
		/// in the directory will be stored, so the user can cycle through them.
		/// </summary>
		/// <param name="sender">OpenMenuItem</param>
		/// <param name="e">Event arguments.</param>
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
		/// <param name="sender">DeleteMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void DeleteMenuItem_Click(object sender, EventArgs e) {
			this.DeleteImage();
		}

		/// <summary>
		/// Allow the user to save a copy of the original file shown
		/// in the image box when the make copy menu item is clicked.
		/// </summary>
		/// <param name="sender">MakeCopyMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void MakeCopyMenuItem_Click(object sender, EventArgs e) {
			this.MakeImageCopy();
		}

		/// <summary>
		/// Copies the current image in the image box to the 
		/// clipboard when the copy menu item is clicked.
		/// </summary>
		/// <param name="sender">CopyMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void CopyMenuItem_Click(object sender, EventArgs e) {
			this.CopyImageToClipboard();
		}

		/// <summary>
		/// Displays the image box image's file properties when the properties menu item is clicked.
		/// </summary>
		/// <param name="sender">PropertiesMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void PropertiesMenuItem_Click(object sender, EventArgs e) {
			this.ShowImageProperties();
		}

		/// <summary>
		/// Exits the MainForm and the program when the exit menu item is clicked.
		/// </summary>
		/// <param name="sender">ExitMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ExitMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Creates and shows the about form when the about menu item is clicked.
		/// </summary>
		/// <param name="sender">AboutMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void AboutMenuItem_Click(object sender, EventArgs e) {
			AboutBox aboutBox = new AboutBox();
			aboutBox.Show();
		}
		#endregion

		#region Full Screen Form Handlers
		/// <summary>
		/// Attempts to hide the full screen form and bring back the main form.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		private void FullScreenForm_ExitFullScreen(Keys keyData) {
			if ((keyData == Keys.Escape) && !this.Visible) {
				this.mFullScreenForm.Hide();
				this.Show();
			}
		}
		#endregion

		#region Image Box Menu Handlers
		/// <summary>
		/// Opens the current image's file location when the image
		/// box context menu item, open file location, is clicked.
		/// </summary>
		/// <param name="sender">ImageBoxMenu.OpenLocationMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuOpenLocation(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => { this.OpenFileLocation(); }));
			else
				this.OpenFileLocation();
		}

		/// <summary>
		/// Sets the current image as the desktop background when the
		/// image context menu item, set as desktop background, is clicked.
		/// </summary>
		/// <param name="sender">ImageBoxMenu.SetAsDesktop</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuSetAsDesktop(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => { this.SetAsDesktopBackground(); }));
			else
				this.SetAsDesktopBackground();
		}

		/// <summary>
		/// Rotates the image 90 degrees clockwise when the image
		/// box context menu item, rotate clockwise, is clicked.
		/// </summary>
		/// <param name="sender">ImageBoxMenu.RotateClockwiseMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuRotateClockwise_Click(object sender, EventArgs e) {
			MainFormDelegate rotate = delegate() {
				this.RotateImage(System.Drawing.RotateFlipType.Rotate90FlipNone);
			};
			if (this.InvokeRequired)
				this.Invoke(rotate);
			else
				rotate();
		}

		/// <summary>
		/// Rotates the image 90 degrees counterclockwise when the image
		/// box context menu item, rotate counterclockwise, is clicked.
		/// </summary>
		/// <param name="sender">ImageBoxMenu.RotateCounterCMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuRotateCounterC_Click(object sender, EventArgs e) {
			MainFormDelegate rotate = delegate() {
				this.RotateImage(System.Drawing.RotateFlipType.Rotate270FlipNone);
			};
			if (this.InvokeRequired)
				this.Invoke(rotate);
			else
				rotate();
		}

		/// <summary>
		/// Copies the image in the image box to the clipboard when 
		/// the image box context menu item, copy, is clicked.
		/// </summary>
		/// <param name="sender">ImageBoxMenu.CopyMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuCopy_Click(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => { this.CopyImageToClipboard(); }));
			else
				this.CopyImageToClipboard();
		}

		/// <summary>
		/// Deletes the file of the image in the image box when
		/// the image box context menu item, delete, is clicked.
		/// </summary>
		/// <param name="sender">IamgeBoxMenu.DeleteMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuDelete_Click(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => { this.DeleteImage(); }));
			else
				this.DeleteImage();
		}

		/// <summary>
		/// Displays the file properties of the image in the image box
		/// when the image box context menu item, delete, is clicked.
		/// </summary>
		/// <param name="sender">IamgeBoxMenu.PropertiesMenuItem</param>
		/// <param name="e">Event arguments.</param>
		private void ImageBoxMenuProperties_Click(object sender, EventArgs e) {
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => { this.ShowImageProperties(); }));
			else
				this.ShowImageProperties();
		}
		#endregion

		#region File System Handlers
		/// <summary>
		/// Attempts to store all the file names in the current working directory.
		/// </summary>
		/// <param name="sender">FileSystemWatcher</param>
		/// <param name="e">File system event arguments.</param>
		private void FileSystem_Changed(object sender, FileSystemEventArgs e) {
			if (this.mLoadedFilenames != null) {
				if (this.InvokeRequired)
					this.Invoke(this.mLoadedFilenames);
				else
					this.mLoadedFilenames();
			}
		}

		/// <summary>
		/// Attempts to store all the file names in the current working directory.
		/// </summary>
		/// <param name="sender">FileSystemWatcher</param>
		/// <param name="e">Renamed event arguments.</param>
		private void FileSystem_Renamed(object sender, RenamedEventArgs e) {
			if (this.mLoadedFilenames != null) {
				if (this.InvokeRequired)
					this.Invoke(this.mLoadedFilenames);
				else
					this.mLoadedFilenames();
			}
		}
		#endregion

		#region Keyboard Handlers
		/// <summary>
		/// Processes key commands when they are triggered by user input.
		/// </summary>
		/// <param name="msg">Windows message with details of the user input.</param>
		/// <param name="keyData">Contains data about which key events occurred.</param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			this.KeyDownHandler(keyData);
			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>
		/// Interprets keys as keyboard shortcuts for certain buttons
		/// and functions and performs clicks or actions for those items.
		/// </summary>
		/// <param name="keyData">Contains data about which key events occurred.</param>
		internal void KeyDownHandler(Keys keyData) {

			MainFormDelegate handKeys = delegate() {

				if ((keyData == Keys.Left) ||
					(keyData == Keys.A)) {
					this.PrevButton_Click(this, null);
				}

				if ((keyData == Keys.Right) ||
					(keyData == Keys.D)) {
					this.NextButton_Click(this, null);
				}

				if ((keyData == Keys.Up) ||
					(keyData == Keys.W)) {
					this.FullScreenButton.PerformClick();
				}

				if ((keyData == Keys.Down) ||
					(keyData == Keys.S)) {
					this.FitSizeButton.PerformClick();
				}

				if (keyData == Keys.Oemcomma) {
					this.RotateCounterButton_Click(this, null);
				}

				if (keyData == Keys.OemPeriod) {
					this.RotateClockwiseButton_Click(this, null);
				}

				if (keyData == Keys.Delete) {
					this.DeleteImage();
				}
			};

			if (this.InvokeRequired)
				this.Invoke(handKeys);
			else
				handKeys();
		}
		#endregion

		#region Mouse Handlers
		/// <summary>
		/// Changes the image of the button to the
		/// hover state if the button is enabled.
		/// </summary>
		/// <param name="sender">Button that is sending the event.</param>
		/// <param name="e">Event arguments.</param>
		private void Button_MouseEnter(object sender, EventArgs e) {
			
			Button button = (Button)sender;
			if (button.Enabled) {
				if (this.mButtonImages.ContainsKey(button))
					button.BackgroundImage = this.mButtonImages[button].GetImage(ButtonImageSet.EState.Hover);
			}
		}

		/// <summary>
		/// Changes the image of the button to the
		/// active state if the button is enabled.
		/// </summary>
		/// <param name="sender">Button that is sending the event.</param>
		/// <param name="e">Event arguments.</param>
		private void Button_MouseLeave(object sender, EventArgs e) {

			Button button = (Button)sender;
			if (button.Enabled) {
				if (this.mButtonImages.ContainsKey(button))
					button.BackgroundImage = this.mButtonImages[button].GetImage(ButtonImageSet.EState.Active);
			}
		}

		/// <summary>
		/// Changes the image of the button to the
		/// clicked/pressed state if the button is enabled.
		/// </summary>
		/// <param name="sender">Button that is sending the event.</param>
		/// <param name="e">Event arguments.</param>
		private void Button_MouseDown(object sender, MouseEventArgs e) {

			Button button = (Button)sender;
			if (button.Enabled) {
				if (this.mButtonImages.ContainsKey(button))
					button.BackgroundImage = this.mButtonImages[button].GetImage(ButtonImageSet.EState.Clicked);
			}
		}

		/// <summary>
		/// Changes the image of the button to the
		/// active state if the button is enabled.
		/// </summary>
		/// <param name="sender">Button that is sending the event.</param>
		/// <param name="e">Event arguments.</param>
		private void Button_MouseUp(object sender, MouseEventArgs e) {

			Button button = (Button)sender;
			if (button.Enabled) {
				if (this.mButtonImages.ContainsKey(button))
					button.BackgroundImage = this.mButtonImages[button].GetImage(ButtonImageSet.EState.Hover);
			}
		}
		#endregion

		#region Glass Form
		#region Properties

		/// <summary>
		/// Get determines if AeroGlass is enabled on the desktop. Set enables/disables AreoGlass on the desktop.
		/// </summary>
		public static bool AeroGlassCompositionEnabled {
			set {
				DesktopWindowManagerNativeMethods.DwmEnableComposition(
					value ? CompositionEnable.Enable : CompositionEnable.Disable);
			}
			get {
				return DesktopWindowManagerNativeMethods.DwmIsCompositionEnabled();
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Fires when the availability of Glass effect changes.
		/// </summary>
		public event EventHandler<AeroGlassCompositionChangedEventArgs> AeroGlassCompositionChanged;

		#endregion

		#region Operations
		/// <summary>
		/// Makes the background of current window transparent.
		/// </summary>
		public void SetAeroGlassTransparency() {
			this.BackColor = Color.Transparent;
		}

		/// <summary>
		/// Excludes a Control from the AeroGlass frame.
		/// </summary>
		/// <param name="control">The control to exclude.</param>
		/// <remarks>
		/// Many non-WPF rendered controls (i.e., the ExplorerBrowser control) will not 
		/// render properly on top of an AeroGlass frame.
		/// This method will only work on a single control at any given time, as it
		/// relies on Margin to set the area that won't be rendered with Aero.
		/// </remarks>
		public void ExcludeControlFromAeroGlass(Control control) {
			if (control == null) { throw new ArgumentNullException("control"); }

			if (AeroGlassCompositionEnabled) {
				Rectangle clientScreen = this.RectangleToScreen(this.ClientRectangle);
				Rectangle controlScreen = control.RectangleToScreen(control.ClientRectangle);

				Margins margins = new Margins();
				margins.LeftWidth = controlScreen.Left - clientScreen.Left;
				margins.RightWidth = clientScreen.Right - controlScreen.Right;
				margins.TopHeight = controlScreen.Top - clientScreen.Top;
				margins.BottomHeight = clientScreen.Bottom - controlScreen.Bottom;

				// Extend the Frame into client area.
				DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
			}
		}

		/// <summary>
		/// Resets the AeroGlass exclusion area.
		/// </summary>
		public void ResetAeroGlass() {
			if (this.Handle != IntPtr.Zero) {
				Margins margins = new Margins(true);
				DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
			}
		}
		#endregion

		#region Implementation
		/// <summary>
		/// Catches the DWM messages to this window and fires the appropriate event.
		/// </summary>
		/// <param name="m"></param>

		protected override void WndProc(ref System.Windows.Forms.Message m) {
			if (m.Msg == DWMMessages.WM_DWMCOMPOSITIONCHANGED
				|| m.Msg == DWMMessages.WM_DWMNCRENDERINGCHANGED) {
				if (AeroGlassCompositionChanged != null) {
					AeroGlassCompositionChanged.Invoke(this,
						new AeroGlassCompositionChangedEventArgs(AeroGlassCompositionEnabled));
				}
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// Initializes the Form for AeroGlass
		/// </summary>
		/// <param name="e">The arguments for this event</param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.ResetAeroGlass();

			// Change all the background colors of 
			// buttons to black for Aero transparency.
			foreach (Control control in this.Controls) {
				if (control is Button) {
					Button button = (Button)control;
					button.BackColor = Color.Black;
					button.FlatAppearance.MouseOverBackColor = Color.Black;
					button.FlatAppearance.MouseDownBackColor = Color.Black;
				}
			}

			// Change the background color of the 
			// form to black for Aero transparency.
			this.BackColor = Color.Black;

			// Exclude the image box and menu strip from being influenced by Aero.
			if (AeroGlassCompositionEnabled) {
				Rectangle clientScreen = this.RectangleToScreen(this.ClientRectangle);
				Rectangle controlScreen = Rectangle.Union(this.MenuStrip.RectangleToScreen(this.MenuStrip.ClientRectangle),
					this.mImageBox.RectangleToScreen(this.mImageBox.ClientRectangle));


				Margins margins = new Margins();
				margins.LeftWidth = controlScreen.Left - clientScreen.Left;
				margins.RightWidth = clientScreen.Right - controlScreen.Right;
				margins.TopHeight = controlScreen.Top - clientScreen.Top;
				margins.BottomHeight = clientScreen.Bottom - controlScreen.Bottom;

				// Extend the Frame into client area.
				DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
			}
		}

		#endregion
		#endregion

		#region Drawing
		private const int WS_EX_COMPOSITED = 0x02000000;

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;

				// Adds style that double buffers the form
				// and all controls to prevent flickering.
				cp.ExStyle |= WS_EX_COMPOSITED;
				return cp;
			}
		}
		#endregion

		#region File Properties
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHELLEXECUTEINFO {
			public int cbSize;
			public uint fMask;
			public IntPtr hwnd;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpVerb;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpFile;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpParameters;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpDirectory;
			public int nShow;
			public IntPtr hInstApp;
			public IntPtr lpIDList;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpClass;
			public IntPtr hkeyClass;
			public uint dwHotKey;
			public IntPtr hIcon;
			public IntPtr hProcess;
		}

		private const int SW_SHOW = 5;
		private const uint SEE_MASK_INVOKEIDLIST = 12;
		public static bool ShowFileProperties(string Filename) {
			SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
			info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
			info.lpVerb = "properties";
			info.lpFile = Filename;
			info.nShow = SW_SHOW;
			info.fMask = SEE_MASK_INVOKEIDLIST;
			return ShellExecuteEx(ref info);
		}
		#endregion

		#region Desktop Wallpaper
		const int SPI_SETDESKWALLPAPER = 20;
		const int SPIF_UPDATEINIFILE = 0x01;
		const int SPIF_SENDWININICHANGE = 0x02;

		public enum Style : int {
			Tiled,
			Centered,
			Stretched
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
		#endregion

		#region Program Entry Point
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
		}
		#endregion
	}
}
