#if WINDOWS
using System;
using System.Runtime.InteropServices;

namespace Samurai.Input
{
	/// <summary>
	/// Reads input from a keyboard.
	/// </summary>
	public sealed class Keyboard
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetKeyboardState(byte[] lpKeyState);

		const int KeyCount = 256;
		const byte HighBitOnlyMask = 0x80;

		byte[] keys;
		byte[] oldKeys;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Keyboard()
		{
			this.keys = new byte[KeyCount];
			this.oldKeys = new byte[KeyCount];
		}

		/// <summary>
		/// Updates the state of the keyboard.
		/// </summary>
		public void Update()
		{
			for (int i = 0; i < KeyCount; i++)
				this.oldKeys[i] = this.keys[i];

			GetKeyboardState(this.keys);
		}

		/// <summary>
		/// Returns true if the given key is currently down.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool IsKeyDown(Key key)
		{
			return (this.keys[(int)key] & HighBitOnlyMask) != 0;
		}

		/// <summary>
		/// Returns true if the given key is currently down and last update was not.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool IsKeyPressed(Key key)
		{
			// If key currently down and was not down before.
			return ((this.keys[(int)key] & HighBitOnlyMask) != 0) &&
				   ((this.oldKeys[(int)key] & HighBitOnlyMask) == 0);
		}

		/// <summary>
		/// Returns true if the given key is currently up.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool IsKeyUp(Key key)
		{
			return (this.keys[(int)key] & HighBitOnlyMask) == 0;
		}

		/// <summary>
		/// Returns true if the given key is currently up and last update was not.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool IsKeyReleased(Key key)
		{
			// If key not currently pressed and was pressed before.
			return ((this.keys[(int)key] & HighBitOnlyMask) == 0) &&
				   ((this.oldKeys[(int)key] & HighBitOnlyMask) != 0);
		}
	}
}
#endif