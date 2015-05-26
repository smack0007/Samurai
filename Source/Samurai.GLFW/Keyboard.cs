using System;

namespace Samurai.GLFW
{
	public class Keyboard : DisposableObject
	{
		GameWindow window;
		bool[] keys;
		bool[] oldKeys;

		public Keyboard(GameWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			this.window = window;
			this.window.KeyDown += this.Window_KeyDown;
			this.window.KeyUp += this.Window_KeyUp;

			this.keys = new bool[Enum.GetNames(typeof(Key)).Length];
			this.oldKeys = new bool[this.keys.Length];
		}

		protected override void DisposeManagedResources()
		{
			this.window.KeyDown -= this.Window_KeyDown;
			this.window.KeyUp -= this.Window_KeyUp;
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			this.keys[(int)e.Key] = true;
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			this.keys[(int)e.Key] = false;
		}

		public bool IsKeyDown(Key key)
		{
			return this.keys[(int)key];
		}

		public bool IsKeyUp(Key key)
		{
			return !this.keys[(int)key];
		}

		public bool IsKeyPressed(Key key)
		{
			return this.keys[(int)key] && !this.oldKeys[(int)key];
		}

		public void SwapBuffers()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.oldKeys[i] = this.keys[i];
			}
		}
	}
}
