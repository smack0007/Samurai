﻿using System;
using System.Diagnostics;
using Samurai.Graphics;

namespace Samurai.GLFW
{
	/// <summary>
	/// Base clsas for Samurai games.
	/// </summary>
	public abstract class Game : IDisposable
	{
		GameOptions options;

		/// <summary>
		/// Gets the GameWindow.
		/// </summary>
		public GameWindow Window
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the GraphicsContext.
		/// </summary>
		public GraphicsContext Graphics
		{
			get;
			private set;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game()
			: this(new GameOptions())
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game(GameOptions options)
		{
			if (options == null)
				throw new ArgumentNullException("options");

			this.options = options;

			if (GLFWNative.Init() == 0)
				throw new SamuraiException("GLFW initialization failed.");

			GLFWNative.RegisterErrorCallback();

			GLFWNative.WindowHint(GLFWNative.CLIENT_API, GLFWNative.OPENGL_API);
			GLFWNative.WindowHint(GLFWNative.CONTEXT_VERSION_MAJOR, 3);
			GLFWNative.WindowHint(GLFWNative.CONTEXT_VERSION_MINOR, 3);
			GLFWNative.WindowHint(GLFWNative.OPENGL_PROFILE, GLFWNative.OPENGL_CORE_PROFILE);
			
			this.Window = new GameWindow(this.options);
			this.Window.Resize += this.Window_Resize;

			this.Graphics = new GraphicsContext(this.Window);
			this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~Game()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Disposes of the Game.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Graphics.Dispose();
				GLFWNative.Terminate();
			}
		}
				
		private void Window_Resize(object sender, EventArgs e)
		{
			if (this.options.AutoResizeViewport)
				this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
		}

		/// <summary>
		/// Starts the game loop.
		/// </summary>
		public void Run()
		{
			Stopwatch stopwatch = new Stopwatch();

			long nextTick;
			int timeBetweenTicks = 1000 / this.options.FramesPerSecond;
			TimeSpan elapsed = TimeSpan.FromMilliseconds(timeBetweenTicks);

			stopwatch.Start();
			nextTick = stopwatch.ElapsedMilliseconds;

			this.Window.Show();

			this.Initialize();

			while (!this.Window.ShouldClose())
			{
				GLFWNative.PollEvents();

				if (stopwatch.ElapsedMilliseconds >= nextTick)
				{
					this.Update(elapsed);
					this.Draw(elapsed);

					nextTick += timeBetweenTicks;
				}
			}

			this.Shutdown();
		}

		/// <summary>
		/// Signals the game loop should exit.
		/// </summary>
		public void Exit()
		{
			this.Window.SetShouldClose(true);
		}

		/// <summary>
		/// Called by the Run method when the game is starting. 
		/// </summary>
		protected virtual void Initialize()
		{
		}

		/// <summary>
		/// Called by the Run method when game logic should run.
		/// </summary>
		/// <param name="elapsed"></param>
		protected virtual void Update(TimeSpan elapsed)
		{
		}

		/// <summary>
		/// Called by the Run method when the game should draw.
		/// </summary>
		/// <param name="elapsed"></param>
		protected virtual void Draw(TimeSpan elapsed)
		{
		}

		/// <summary>
		/// Called by the Run method when the game is ending.
		/// </summary>
		protected virtual void Shutdown()
		{
		}
	}
}
