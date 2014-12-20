// KeyboardShortcut.cs
// Authored by Jesse Z. Zhong
#region Usings
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
#endregion

namespace AnimatedGifViewer {
	[Serializable]
	public class KeyboardShortcut : INotifyPropertyChanged {

		#region Shortcuts Class
		/// <summary>
		/// Serves as an identification key for shortcuts.
		/// </summary>
		[Serializable]
		public sealed class Shortcuts {

			#region Initialization
			/// <summary>
			/// Initializes the shortcut with the name.
			/// </summary>
			/// <param name="name"></param>
			public Shortcuts(string name) {
				this.mName = name;
			}

			/// <summary>
			/// Default constructor for serialization.
			/// </summary>
			public Shortcuts() {
				this.mName = String.Empty;
			}
			#endregion

			#region Properties
			/// <summary>
			/// Returns the string with the name of the shortcut.
			/// </summary>
			public string Name {
				get {
					return this.mName;
				}
				set {
					this.mName = value;
				}
			}
			#endregion

			#region Members
			private string mName;
			#endregion

			#region Overloads
			/// <summary>
			/// Overloads the base method to return the shortcut name.
			/// </summary>
			/// <returns>Shortcut name.</returns>
			public override string ToString() {
				return this.mName;
			}

			/// <summary>
			/// Tests if the names of two shortcuts match.
			/// </summary>
			/// <param name="a">Left operand.</param>
			/// <param name="b">Right operand.</param>
			/// <returns>True if names match.</returns>
			public static bool operator ==(Shortcuts a, Shortcuts b) {
				if (System.Object.ReferenceEquals(a, b))
					return true;

				if (((object)a == null) || ((object)b == null))
					return false;

				return a.Name == b.Name;
			}

			/// <summary>
			/// Tests if the names of two shortcuts do not match.
			/// </summary>
			/// <param name="a">Left operand.</param>
			/// <param name="b">Right operand.</param>
			/// <returns>True if names do not match.</returns>
			public static bool operator !=(Shortcuts a, Shortcuts b) {
				return !(a == b);
			}

			/// <summary>
			/// Tests if the passed object is a shortcut and if its name matches this shortcut's name.
			/// </summary>
			/// <param name="obj">The object in question.</param>
			/// <returns>True if it is a shortcut and the name matches.</returns>
			public override bool Equals(object obj) {
				if (obj == null)
					return false;

				Shortcuts shortcut = obj as Shortcuts;
				if ((System.Object)shortcut == null)
					return false;

				return this.mName == shortcut.Name;
			}

			/// <summary>
			/// Tests if the passed shortcut's name matches this shortcut's name.
			/// </summary>
			/// <param name="shortcut">The shortcut in question.</param>
			/// <returns>True if names match.</returns>
			public bool Equals(Shortcuts shortcut) {
				if ((object)shortcut == null)
					return false;

				return this.mName == shortcut.Name;
			}

			/// <summary>
			/// Returns the hash code for the shortcut.
			/// </summary>
			/// <returns>The hash code.</returns>
			public override int GetHashCode() {
				return this.mName.GetHashCode();
			}
			#endregion
		}
		#endregion

		#region Globals
		public const string SHORTCUT_PROPERTY_NAME = "Shortcut";
		public const string PRIMARY_KEY_PROPERTY_NAME = "PrimaryKey";
		public const string SECONDARY_KEY_PROPERTY_NAME = "SecondaryKey";

		public static readonly List<KeyboardShortcut> DEFAULT_SHORTCUTS = new List<KeyboardShortcut>();

		public static readonly Shortcuts NextImage = 
			AddShortcutWithDefault("Next Image",						Keys.Right,		Keys.None);
		public static readonly Shortcuts PreviousImage = 
			AddShortcutWithDefault("Previous Image",					Keys.Left,		Keys.None);
		public static readonly Shortcuts EnterFullscreen = 
			AddShortcutWithDefault("Enter Fullscreen",					Keys.Up,		Keys.None);
		public static readonly Shortcuts ExitFullscreen = 
			AddShortcutWithDefault("Exit Fullscreen",					Keys.Down,		Keys.None);
		public static readonly Shortcuts ToggleSidePanel = 
			AddShortcutWithDefault("Toggle Side Panel",					Keys.Tab,		Keys.None);
		public static readonly Shortcuts RotateImageClockwise = 
			AddShortcutWithDefault("Rotate Image Clockwise",			Keys.OemPeriod, Keys.None);
		public static readonly Shortcuts RotateImageCounterClockwise = 
			AddShortcutWithDefault("Rotate Image Counter Clockwise",	Keys.Oemcomma,	Keys.None);
		#endregion

		#region Members
		private Shortcuts mShortcut;
		private Keys mPrimaryKey;
		private Keys mSecondaryKey;
		#endregion

		#region Properties
		/// <summary>
		/// The name of the keyboard shortcut.
		/// </summary>
		public Shortcuts Shortcut {
			get {
				return this.mShortcut;
			}
			set {
				this.mShortcut = value;
				this.NotifyPropertyChanged(SHORTCUT_PROPERTY_NAME);
			}
		}

		/// <summary>
		/// The main key used for the shortcut.
		/// </summary>
		public Keys PrimaryKey {
			get {
				return this.mPrimaryKey;
			}
			set {
				this.mPrimaryKey = value;
				this.NotifyPropertyChanged(PRIMARY_KEY_PROPERTY_NAME);
			}
		}

		/// <summary>
		/// The alternate key used for the shortcut.
		/// </summary>
		public Keys SecondaryKey {
			get {
				return this.mSecondaryKey;
			}
			set {
				this.mSecondaryKey = value;
				this.NotifyPropertyChanged(SECONDARY_KEY_PROPERTY_NAME);
			}
		}
		#endregion

		#region Initialization
		/// <summary>
		/// Initializes and instance of a keyboard shortcut.
		/// </summary>
		/// <param name="shortcut">Name of the shortcut.</param>
		/// <param name="primaryKey">Main key used for the shortcut.</param>
		/// <param name="secondaryKey">Alternate key used by the shortcut.</param>
		public KeyboardShortcut(Shortcuts shortcut, Keys primaryKey, Keys secondaryKey) {
			this.mShortcut = shortcut;
			this.mPrimaryKey = primaryKey;
			this.mSecondaryKey = secondaryKey;
		}

		/// <summary>
		/// Default constructor for serialization.
		/// </summary>
		public KeyboardShortcut() {
			this.Shortcut = NextImage;
			this.PrimaryKey = Keys.None;
			this.SecondaryKey = Keys.None;
		}

		/// <summary>
		/// Creates a shortcut in the system with default settings.
		/// </summary>
		/// <param name="shortcutName">Name of the shortcut.</param>
		/// <param name="primaryKey">Primary key of the shortcut.</param>
		/// <param name="secondaryKey">Secondary key of the shortcut.</param>
		/// <returns>Returns the shortcut reference object.</returns>
		private static Shortcuts AddShortcutWithDefault(string shortcutName, Keys primaryKey, Keys secondaryKey) {
			Shortcuts shortcut = new Shortcuts(shortcutName);
			DEFAULT_SHORTCUTS.Add(new KeyboardShortcut(shortcut, primaryKey, secondaryKey));
			return shortcut;
		}
		#endregion

		#region Property Events
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Triggers an event indicating that a property has been changed.
		/// </summary>
		/// <param name="name">Name of the property that has been changed.</param>
		private void NotifyPropertyChanged(string name) {
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
		#endregion

		#region Overloads
		/// <summary>
		/// Tests if the members of the keyboard shortcut are equal.
		/// </summary>
		/// <param name="a">Left operand.</param>
		/// <param name="b">Right operand.</param>
		/// <returns>True if all members match.</returns>
		public static bool operator ==(KeyboardShortcut a, KeyboardShortcut b) {
			if (System.Object.ReferenceEquals(a, b))
				return true;

			if (((object)a == null) || ((object)b == null))
				return false;

			return (a.mShortcut == b.mShortcut)
				&& (a.mPrimaryKey == b.mPrimaryKey)
				&& (a.mSecondaryKey == b.mSecondaryKey);
		}

		/// <summary>
		/// Tests if any of the members are different.
		/// </summary>
		/// <param name="a">Left operand.</param>
		/// <param name="b">Right operand.</param>
		/// <returns>True if any of the members differ.</returns>
		public static bool operator !=(KeyboardShortcut a, KeyboardShortcut b) {
			return !(a == b);
		}

		/// <summary>
		/// Tests if the passed object is a keyboard shortcut and has matching members.
		/// </summary>
		/// <param name="obj">Object in question.</param>
		/// <returns>True if it is a keyboard shortcut and all members match.</returns>
		public override bool Equals(object obj) {
			if (obj == null)
				return false;

			KeyboardShortcut keyboardShortcut = obj as KeyboardShortcut;
			if ((System.Object)keyboardShortcut == null)
				return false;

			return (keyboardShortcut.mShortcut == this.mShortcut)
				&& (keyboardShortcut.mPrimaryKey == this.mPrimaryKey)
				&& (keyboardShortcut.mSecondaryKey == this.mSecondaryKey);
		}

		/// <summary>
		/// Test if the passed keyboard shortcut has matching members.
		/// </summary>
		/// <param name="keyboardShortcut">Keyboard shortcut in question.</param>
		/// <returns>True if all members match.</returns>
		public bool Equals(KeyboardShortcut keyboardShortcut) {
			if ((object)keyboardShortcut == null)
				return false;

			return (keyboardShortcut.mShortcut == this.mShortcut)
				&& (keyboardShortcut.mPrimaryKey == this.mPrimaryKey)
				&& (keyboardShortcut.mSecondaryKey == this.mSecondaryKey);
		}

		/// <summary>
		/// Returns the hash code for a keyboard shortcut.
		/// </summary>
		/// <returns>Hash code.</returns>
		public override int GetHashCode() {
			int code = 13;
			code = (code * 7) + this.mShortcut.GetHashCode();
			code = (code * 7) + this.mPrimaryKey.GetHashCode();
			code = (code * 7) + this.mSecondaryKey.GetHashCode();
			return code;
		}
		#endregion
	}
}
