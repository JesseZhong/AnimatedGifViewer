// Authored by Jesse
#region Usings
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ionic.Zip;
#endregion

namespace ZipTestApp {
    public partial class DisplayForm : Form {

        private string[] mArguments;

        public DisplayForm(string[] args) {
            this.mArguments = args;
            this.InitializeComponent();
        }

        /// <summary>
        /// Read the zip file when the app loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayForm_Load(object sender, EventArgs e) {
            if ((this.mArguments != null) && (this.mArguments.Any())) {
                string fileName = this.mArguments[0];
                string filePath = Directory.GetCurrentDirectory() + @"\" + fileName;

                // Print file path.
                Console.WriteLine("{0}", filePath);

                // Check for the file path's existance.
                if (File.Exists(filePath)) {
                    Console.WriteLine("This file exists.");

                    // Check if the file is a zip file.
                    if (ZipFile.IsZipFile(filePath)) {

                        // Attempt to open the zip file.
                        using (ZipFile file = new ZipFile(filePath)) {
                            Console.WriteLine("Zip file, {0}, has been opened.", fileName);

                            string readmeFile = @".\TestZip\readme.txt";
                            if (file.ContainsEntry(readmeFile)) {
                                Console.WriteLine("Readme file found.");

                                ZipEntry entry = file[readmeFile];
                                this.RichTextBox.Text += String.Format("Info:\n{0}", entry.Info);

                                // Extract the entry into a memory stream.
                                using(MemoryStream ms = new MemoryStream()) {
                                    entry.Extract(ms);

                                    // Reset the position of the stream to the front.
                                    ms.Position = 0;

                                    // Set the stream into a stream reader.
                                    StreamReader sr = new StreamReader(ms);

                                    // Write the contents to the text box.
                                    this.RichTextBox.Text += String.Format("Contents:\n{0}", sr.ReadToEnd());
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Program Entry
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DisplayForm(args));
        }
        #endregion
    }
}