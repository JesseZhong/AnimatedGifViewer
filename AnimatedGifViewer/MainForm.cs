﻿// MainForm.cs
// Authored by Jesse Z. Zhong
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell;

namespace AnimatedGifViewer {

	public partial class MainForm : Form {

		#region Constants
		/// <summary>
		/// The vertical padding above and below the image box.
		/// </summary>
		private const int IMG_BOX_H_PAD = 118;

		/// <summary>
		/// The filter used by the program to scan
		/// for image files in the working directory.
		/// </summary>
		/// <remarks>Note: Windows file system is case-insensitive.</remarks>
		private const string FILE_TYPES = "*.bmp|*.gif|*.jpg|*.jpeg|*.png|*.tiff|*.ico";

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
		private ImageBox ImageBox;
		private ToolTip ToolTip;

		private List<string> filenames;
		private int filenameIndex;
		private string[] arguments;
		private string loadedFile;

		private FileSystemWatcher watcher = new FileSystemWatcher();
		private Dictionary<Button, ButtonImageSet> buttonImages = new Dictionary<Button, ButtonImageSet>();

		private delegate void LoadFileNames();
		private LoadFileNames loadFileNames;

		private delegate void FormDelegate();
		private Mutex mutex = new Mutex();
		#endregion

		#region Work
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
			this.filenameIndex = 0;
			this.arguments = args;
			this.filenames = new List<string>();
			this.EnableButtons(false);
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeImageBox() {
			this.ImageBox = new ImageBox();
			this.ImageBox.Border = System.Windows.Forms.BorderStyle.None;
			this.ImageBox.Anchor = (System.Windows.Forms.AnchorStyles)
				(AnchorStyles.Top | AnchorStyles.Bottom | 
				AnchorStyles.Left | AnchorStyles.Right) ;

			this.ImageBox.Location = new Point(0, 24);
			this.ImageBox.Margin = new System.Windows.Forms.Padding(0);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(this.Width, 
				(this.Height > IMG_BOX_H_PAD ? this.Height - IMG_BOX_H_PAD : this.Height));
			this.ImageBox.TabIndex = 0;
			this.ImageBox.TabStop = false;
			this.Controls.Add(this.ImageBox);
		}

		/// <summary>
		/// Initializes the system file watcher to
		/// begin watching for file changed, deleted,
		/// created, or renamed events to be raised.
		/// </summary>
		private void InitFileWatcher() {
			this.watcher = new FileSystemWatcher();
			this.watcher.Path = Directory.GetCurrentDirectory();
			this.watcher.NotifyFilter = NotifyFilters.LastAccess |
				NotifyFilters.LastWrite | NotifyFilters.FileName |
				NotifyFilters.DirectoryName;
			//this.watcher.Filter = FILE_TYPES;

			// Add event handlers.
			this.watcher.Changed += new FileSystemEventHandler(this.FileSystem_Changed);
			this.watcher.Created += new FileSystemEventHandler(this.FileSystem_Changed);
			this.watcher.Deleted += new FileSystemEventHandler(this.FileSystem_Changed);
			this.watcher.Renamed += new RenamedEventHandler(this.FileSystem_Renamed);

			// Start watching for events.
			this.watcher.EnableRaisingEvents = true;
		}

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
			this.ImageBox.Image = this.LoadImage(filename);

			// Set the working directory to that of the passed file.
			string workingDirectory = Path.GetDirectoryName(filename);
			Directory.SetCurrentDirectory(workingDirectory);

			// Initialize the file watcher.
			this.InitFileWatcher();

			// Initialize the delegate so that it will grab all the
			// files in the same directory as the originally loaded file.
			#region this.loadFileNames
			this.loadFileNames = delegate() {
				this.mutex.WaitOne();

				// Search the directory for other images.
				this.filenames = this.GetFiles(workingDirectory, FILE_TYPES);

				// Disable buttons and clear the image
				// box if no images exist in the folder.
				if (!this.filenames.Any()) {
					FormDelegate disableButtons = delegate() {
						this.EnableButtons(false);
					};
					this.Invoke(disableButtons);
					this.ImageBox.Image = null;
					return;
				}

				// Find the index of the filename in the list of filenames.
				// Set the current filename index to that of the filename's.
				if (this.filenames.Contains(this.loadedFile))
					this.filenameIndex = this.filenames.FindIndex(delegate(string name) {
						return name == this.loadedFile;
					});

				// Attempt to load the original image into the image box.
				if (this.CheckFilenamesBounds(this.filenameIndex))
					this.ImageBox.Image = this.LoadImage(this.filenames[this.filenameIndex]);
				else
					this.ImageBox.Image = null;

				// Enable the buttons.
				FormDelegate enableButtons = delegate() {
					this.EnableButtons(true);
				};
				this.Invoke(enableButtons);
				this.mutex.ReleaseMutex();
			};
			#endregion

			// Grab all the files from the directory.
			this.loadFileNames();
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

			foreach (KeyValuePair<Button, ButtonImageSet> item in this.buttonImages) {
				item.Key.BackgroundImage = item.Value.GetImage(state);
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
		/// Check if an index is within the bounds of filenames.
		/// </summary>
		/// <param name="index">The index in question.</param>
		/// <returns>True if in bounds, false otherwise.</returns>
		private bool CheckFilenamesBounds(int index) {
			if ((index < this.filenames.Count) && (index >= 0))
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
			var bytes = File.ReadAllBytes(filename);
			var ms = new MemoryStream(bytes);
			var img = Image.FromStream(ms);
			this.loadedFile = filename;

			// Change the title of the form to have the file name.
			this.Text = String.Format("{0} - {1}",
				Path.GetFileName(filename), 
				global::AnimatedGifViewer.Properties.Resources.ProgramName);

			return img;
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
		/// Attempts to delete the currently
		/// displayed image in the image box. 
		/// </summary>
		private void DeleteImage() {

			// Attempt to delete the current file.
			if (this.filenames.Any() && this.CheckFilenamesBounds(this.filenameIndex)) {
				bool deleted = FileOperationAPIWrapper.Send(this.filenames[this.filenameIndex]);
				if (deleted)
					this.NextButton.PerformClick();
			}
		}
		#endregion

		#region Loading
		/// <summary>
		/// Loads additional content when the form is created.
		/// </summary>
		/// <param name="sender">MainForm</param>
		/// <param name="e">Event arguments.</param>
		private void MainForm_Load(object sender, EventArgs e) {

			// MainForm.
			this.Text = global::AnimatedGifViewer.Properties.Resources.ProgramName;

			// Image box.
			this.ImageBox.SizeMode = PictureBoxSizeMode.CenterImage;

			// Set button tooltips.
			this.ToolTip = new ToolTip();
			this.ToolTip.AutomaticDelay = 5000;
			this.ToolTip.InitialDelay = 1000;
			this.ToolTip.ReshowDelay = 500;
			this.ToolTip.ShowAlways = true;

			this.ToolTip.SetToolTip(this.NextButton, global::AnimatedGifViewer.Properties.Resources.NextButtonToolTip);
			this.ToolTip.SetToolTip(this.PrevButton, global::AnimatedGifViewer.Properties.Resources.PrevButtonToolTip);
			this.ToolTip.SetToolTip(this.FullScreenButton, global::AnimatedGifViewer.Properties.Resources.FullScreenButtonToolTip);
			this.ToolTip.SetToolTip(this.SizeButton, global::AnimatedGifViewer.Properties.Resources.SizeButtonToolTip);
			this.ToolTip.SetToolTip(this.RotateCounterButton, global::AnimatedGifViewer.Properties.Resources.RotateCounterButtonToolTip);
			this.ToolTip.SetToolTip(this.RotateClockwiseButton, global::AnimatedGifViewer.Properties.Resources.RotateClockwiseButtonToolTip);
			this.ToolTip.SetToolTip(this.DeleteButton, global::AnimatedGifViewer.Properties.Resources.DeleteButtonToolTip);

			// Load the image sets for each button.
			this.buttonImages.Add(this.PrevButton, 
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Previous));
			this.buttonImages.Add(this.NextButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Next));
			this.buttonImages.Add(this.FullScreenButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_FullScreen));
			this.buttonImages.Add(this.SizeButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_Size));
			this.buttonImages.Add(this.RotateCounterButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_RotateCounter));
			this.buttonImages.Add(this.RotateClockwiseButton,
				new ButtonImageSet(global::AnimatedGifViewer.Properties.Resources.Button_RotateClockwise));
			this.buttonImages.Add(this.DeleteButton,
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

			// Form deactivated event.
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);

			// Set to handle keyboard events.
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
			if (this.arguments.Any())
				this.OpenImageFile(this.arguments[0]);
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
			foreach (KeyValuePair<Button, ButtonImageSet> item in this.buttonImages) {
				if (item.Key.Enabled) {
					item.Key.BackgroundImage = item.Value.GetImage(ButtonImageSet.EState.Active);
				}
			}
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
			if(this.filenames.Any())
				this.ImageBox.Image = this.LoadImage(this.NextImage());
		}

		/// <summary>
		/// Attempts to load the previous image
		/// in the directory into the image box.
		/// </summary>
		/// <param name="sender">PrevButton</param>
		/// <param name="e">Event arguments.</param>
		private void PrevButton_Click(object sender, EventArgs e) {
			if (this.filenames.Any())
				this.ImageBox.Image = this.LoadImage(this.PrevImage());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FullScreenButton_Click(object sender, EventArgs e) {

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
		#endregion

		#region File System Handlers
		/// <summary>
		/// Attempts to store all the file names in the current working directory.
		/// </summary>
		/// <param name="sender">FileSystemWatcher</param>
		/// <param name="e">File system event arguments.</param>
		private void FileSystem_Changed(object sender, FileSystemEventArgs e) {
			if (this.loadFileNames != null)
				this.loadFileNames();
		}

		/// <summary>
		/// Attempts to store all the file names in the current working directory.
		/// </summary>
		/// <param name="sender">FileSystemWatcher</param>
		/// <param name="e">Renamed event arguments.</param>
		private void FileSystem_Renamed(object sender, RenamedEventArgs e) {
			if (this.loadFileNames != null)
				this.loadFileNames();
		}
		#endregion

		#region Keyboard Handlers
		/// <summary>
		/// Interprets keys as keyboard shortcuts for certain buttons
		/// and functions and performs clicks or actions for those items.
		/// </summary>
		/// <param name="msg">Windows message with details of the user input.</param>
		/// <param name="keyData">Contains data about which key events occurred.</param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

			if ((keyData == Keys.Left) ||
				(keyData == Keys.A)) {
				this.PrevButton.PerformClick();
			}

			if ((keyData == Keys.Right) ||
				(keyData == Keys.D)) {
				this.NextButton.PerformClick();
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
				this.RotateCounterButton.PerformClick();
			}

			if (keyData == Keys.OemPeriod) {
				this.RotateClockwiseButton.PerformClick();
			}

			if (keyData == Keys.Delete) {
				this.DeleteImage();
			}

			return base.ProcessCmdKey(ref msg, keyData);
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
				if (this.buttonImages.ContainsKey(button))
					button.BackgroundImage = this.buttonImages[button].GetImage(ButtonImageSet.EState.Hover);
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
				if (this.buttonImages.ContainsKey(button))
					button.BackgroundImage = this.buttonImages[button].GetImage(ButtonImageSet.EState.Active);
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
				if (this.buttonImages.ContainsKey(button))
					button.BackgroundImage = this.buttonImages[button].GetImage(ButtonImageSet.EState.Clicked);
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
				if (this.buttonImages.ContainsKey(button))
					button.BackgroundImage = this.buttonImages[button].GetImage(ButtonImageSet.EState.Hover);
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
		/// Makes the background of current window transparent
		/// </summary>
		public void SetAeroGlassTransparency() {
			this.BackColor = Color.Transparent;
		}

		/// <summary>
		/// Excludes a Control from the AeroGlass frame.
		/// </summary>
		/// <param name="control">The control to exclude.</param>
		/// <remarks>Many non-WPF rendered controls (i.e., the ExplorerBrowser control) will not 
		/// render properly on top of an AeroGlass frame. </remarks>
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

				// Extend the Frame into client area
				DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
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
		}

		#endregion
		#endregion
	}
}
