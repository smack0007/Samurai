#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Input
{
	/// <summary>
	/// Reads input from a mouse.
	/// </summary>
	public sealed class Mouse
	{
		private const byte VK_LBUTTON = 0x01;
		private const byte VK_RBUTTON = 0x02;
		private const byte VK_MBUTTON = 0x04;
		private const byte VK_XBUTTON1 = 0x05;
		private const byte VK_XBUTTON2 = 0x06;

		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState([In] int vKey); 

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out Win32Point lpPoint);

		[DllImport("user32.dll")]
		private static extern uint GetDoubleClickTime();

		[DllImport("user32.dll")]
		private static extern bool ScreenToClient(IntPtr hWnd, ref Win32Point lpPoint);

		[StructLayout(LayoutKind.Sequential)]
		struct Win32Point
		{
			public int X;
			public int Y;
		}

		const int ButtonCount = 5;

		IHostControl host;

		Point position;
		Point oldPosition;

		bool[] buttons;
		bool[] oldButtons;

		MouseButton? lastClickedButton;
		TimeSpan elapsedSinceClick;
		MouseButton? doubleClickedButton;

		/// <summary>
		/// Returns true if the mouse is within the game window client area.
		/// </summary>
		public bool IsWithinDisplayArea
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the double click rate for the mouse.
		/// </summary>
		public TimeSpan DoubleClickRate
		{
			get;
			private set;
		}

		/// <summary>
		/// The position of the cursor.
		/// </summary>
		public Point Position
		{
			get { return this.position; }
		}

		/// <summary>
		/// The delta of the position from the last update.
		/// </summary>
		public Point PositionDelta
		{
			get { return new Point(this.position.X - this.oldPosition.X, this.position.Y - this.oldPosition.Y); }
		}

		/// <summary>
		/// The X position of the cursor.
		/// </summary>
		public int X
		{
			get { return this.position.X; }
		}

		/// <summary>
		/// The Y position of the cursor.
		/// </summary>
		public int Y
		{
			get { return this.position.Y; }
		}

		/// <summary>
		/// Returns true if the left mouse button is currently down.
		/// </summary>
		public bool LeftButton
		{
			get { return this.buttons[(int)MouseButton.Left]; }
		}

		/// <summary>
		/// Returns true if the right mouse button is currently down.
		/// </summary>
		public bool RightButton
		{
			get { return this.buttons[(int)MouseButton.Right]; }
		}

		/// <summary>
		/// Returns true if the middle mouse button is currently down.
		/// </summary>
		public bool MiddleButton
		{
			get { return this.buttons[(int)MouseButton.Middle]; }
		}

		/// <summary>
		/// Returns true if XButton1 mouse button is currently down.
		/// </summary>
		public bool XButton1
		{
			get { return this.buttons[(int)MouseButton.XButton1]; }
		}

		/// <summary>
		/// Returns true if XButton2 mouse button is currently down.
		/// </summary>
		public bool XButton2
		{
			get { return this.buttons[(int)MouseButton.XButton2]; }
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="host"></param>
		public Mouse(IHostControl host)
		{
			if (host == null)
				throw new ArgumentNullException("host");

			this.host = host;

			this.DoubleClickRate = TimeSpan.FromMilliseconds(GetDoubleClickTime());

			this.position = Point.Zero;
			this.oldPosition = Point.Zero;

			this.buttons = new bool[ButtonCount];
			this.oldButtons = new bool[ButtonCount];
		}

		/// <summary>
		/// Updates the state of the mouse.
		/// </summary>
		public void Update(TimingState time)
		{
			Win32Point point;
			GetCursorPos(out point);
			ScreenToClient(this.host.Handle, ref point);

			if (point.X < 0 ||
				point.Y < 0 ||
				point.X >= this.host.Width ||
				point.Y >= this.host.Height)
			{
				this.IsWithinDisplayArea = false;
			}
			else
			{
				this.IsWithinDisplayArea = true;
			}

			this.oldPosition = this.position;
			this.position = new Point(point.X, point.Y);

			for (int i = 0; i < ButtonCount; i++)
				this.oldButtons[i] = this.buttons[i];

			this.buttons[(int)MouseButton.Left - 1] = (GetAsyncKeyState(VK_LBUTTON) != 0);
			this.buttons[(int)MouseButton.Right - 1] = (GetAsyncKeyState(VK_RBUTTON) != 0);
			this.buttons[(int)MouseButton.Middle - 1] = (GetAsyncKeyState(VK_MBUTTON) != 0);
			this.buttons[(int)MouseButton.XButton1 - 1] = (GetAsyncKeyState(VK_XBUTTON1) != 0);
			this.buttons[(int)MouseButton.XButton2 - 1] = (GetAsyncKeyState(VK_XBUTTON2) != 0);

			// Double click detection.

			this.doubleClickedButton = null;

			if (this.lastClickedButton != null)
			{
				this.elapsedSinceClick += time.ElapsedTime;

				if (this.elapsedSinceClick > this.DoubleClickRate ||
					this.elapsedSinceClick > TimeSpan.FromSeconds(5)) // Give up updating after 5 seconds
				{
					this.lastClickedButton = null;
				}
			}

			MouseButton? clickedButton = null;

			if (this.IsButtonClicked(MouseButton.Left))
			{
				clickedButton = MouseButton.Left;
			}
			else if (this.IsButtonClicked(MouseButton.Right))
			{
				clickedButton = MouseButton.Right;
			}
			else if (this.IsButtonClicked(MouseButton.Middle))
			{
				clickedButton = MouseButton.Middle;
			}
			else if (this.IsButtonClicked(MouseButton.XButton1))
			{
				clickedButton = MouseButton.XButton1;
			}
			else if (this.IsButtonClicked(MouseButton.XButton2))
			{
				clickedButton = MouseButton.XButton2;
			}

			if (clickedButton != null)
			{
				if (clickedButton.Value == this.lastClickedButton)
				{
					if (this.elapsedSinceClick <= this.DoubleClickRate)
					{
						this.doubleClickedButton = clickedButton;
						this.lastClickedButton = null;
						this.elapsedSinceClick = TimeSpan.Zero;
					}
				}
				else
				{
					this.lastClickedButton = clickedButton;
					this.elapsedSinceClick = TimeSpan.Zero;
				}
			}
		}

		/// <summary>
		/// Returns true if the given mouse button is currently down.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsButtonDown(MouseButton button)
		{
			return this.buttons[(int)button - 1];
		}

		/// <summary>
		/// Returns true if the given mouse button is currently down and was not on the last update. Method is same as IsButtonClicked().
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsButtonPressed(MouseButton button)
		{
			return this.buttons[(int)button - 1] && !this.oldButtons[(int)button - 1];
		}

		/// <summary>
		/// Returns true if the given mouse button is currently down and was not on the last update. Method is same as IsButtonPressed().
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsButtonClicked(MouseButton button)
		{
			return this.IsButtonPressed(button);
		}

		/// <summary>
		/// Returns true if the given mouse button is clicked and was clicked twice within the time span specified by DoubleClickRate.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsButtonDoubleClicked(MouseButton button)
		{
			return this.doubleClickedButton != null && this.doubleClickedButton.Value == button;
		}

		/// <summary>
		/// Resets double click tracking for the mouse.
		/// </summary>
		public void ResetDoubleClick()
		{
			this.doubleClickedButton = null;
			this.lastClickedButton = null;
			this.elapsedSinceClick = TimeSpan.Zero;
		}

		/// <summary>
		/// Returns true if the given mouse button is currently up.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsButtonUp(MouseButton button)
		{
			return !this.buttons[(int)button - 1];
		}

		/// <summary>
		/// Returns true if the given mouse button is currently up and was not up on the last update.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsButtonReleased(MouseButton button)
		{
			return !this.buttons[(int)button - 1] && this.oldButtons[(int)button - 1];
		}
	}
}
#endif