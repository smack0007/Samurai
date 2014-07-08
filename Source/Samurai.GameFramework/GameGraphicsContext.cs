using Samurai.Graphics;
using System;

namespace Samurai.GameFramework
{
	public sealed class GameGraphicsContext : GraphicsContext
	{
		GameWindow window;

		internal GameGraphicsContext(GameWindow window)
			: base(GLFW.GetProcAddress)
		{
			this.window = window;
		}

		public override void SwapBuffers()
		{
			this.window.SwapBuffers();
		}
	}
}
