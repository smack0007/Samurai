using System;

namespace Samurai.GLFW
{
	/// <summary>
	/// Contains options for a Game.
	/// </summary>
	public class GameOptions
	{
		/// <summary>
		/// Gets or sets the desired frames per second for the game.
		/// </summary>
		public int FramesPerSecond
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether or not the viewport will be automatically resized with the GameWindow.
		/// </summary>
		public bool AutoResizeViewport
		{
			get;
			set;
		}

		public bool WindowIsFullscreen
		{
			get;
			set;
		}

		public bool WindowResizable
		{
			get;
			set;
		}

		public int WindowWidth
		{
			get;
			set;
		}

		public int WindowHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public GameOptions()
		{
			this.FramesPerSecond = 60;
			this.AutoResizeViewport = true;
			this.WindowIsFullscreen = false;
			this.WindowResizable = false;
			this.WindowWidth = 800;
			this.WindowHeight = 600;
		}
	}
}
