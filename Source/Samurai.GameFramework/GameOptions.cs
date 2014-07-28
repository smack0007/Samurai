using System;

namespace Samurai.GameFramework
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

		public Size WindowSize
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
			this.WindowSize = new Size(1280, 720);
		}
	}
}
