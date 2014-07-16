using System;
using System.Diagnostics;
using Samurai.Graphics;

namespace Samurai.GameFramework
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
		public GameGraphicsContext Graphics
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

			if (GLFW.Init() == 0)
				throw new SamuraiException("GLFW initialization failed.");

			GLFW.RegisterErrorCallback();

			GLFW.WindowHint(GLFW.CLIENT_API, GLFW.OPENGL_API);
			GLFW.WindowHint(GLFW.CONTEXT_VERSION_MAJOR, 3);
			GLFW.WindowHint(GLFW.CONTEXT_VERSION_MINOR, 3);
			GLFW.WindowHint(GLFW.OPENGL_PROFILE, GLFW.OPENGL_CORE_PROFILE);
			
			this.Window = new GameWindow(this.options);
			this.Window.Resize += this.Window_Resize;

			this.Graphics = new GameGraphicsContext(this.Window);
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
				GLFW.Terminate();
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
				GLFW.PollEvents();

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
