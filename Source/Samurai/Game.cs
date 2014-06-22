using System;
using System.Diagnostics;

namespace Samurai
{
	/// <summary>
	/// Base clsas for Samurai games.
	/// </summary>
	public abstract class Game : IDisposable
	{		
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
		/// Gets or sets the desired frames per second.
		/// </summary>
		protected int FramesPerSecond
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether the Viewport of the GraphicsContext should be automatically resized.
		/// </summary>
		protected bool AutoResizeViewport
		{
			get;
			set;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game()
		{
			this.FramesPerSecond = 60;
			this.AutoResizeViewport = true;

			if (GLFW.Init() == 0)
				throw new SamuraiException("GLFW initialization failed.");

			GLFW.RegisterErrorCallback();

			GLFW.WindowHint(GLFW.CLIENT_API, GLFW.OPENGL_API);
			GLFW.WindowHint(GLFW.CONTEXT_VERSION_MAJOR, 3);
			GLFW.WindowHint(GLFW.CONTEXT_VERSION_MINOR, 3);
			GLFW.WindowHint(GLFW.OPENGL_PROFILE, GLFW.OPENGL_CORE_PROFILE);

			this.Window = new GameWindow();
			this.Window.Resize += this.Window_Resize;
			
			GL.Init();

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
				GLFW.Terminate();
			}
		}
				
		private void Window_Resize(object sender, EventArgs e)
		{
			if (this.AutoResizeViewport)
				this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
		}

		/// <summary>
		/// Starts the game loop.
		/// </summary>
		public void Run()
		{
			Stopwatch stopwatch = new Stopwatch();

			long nextTick;
			int timeBetweenTicks = 1000 / this.FramesPerSecond;
			TimeSpan elapsed = TimeSpan.FromMilliseconds(timeBetweenTicks);

			stopwatch.Start();
			nextTick = stopwatch.ElapsedMilliseconds;

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
