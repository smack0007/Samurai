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
					
			this.Window = new GameWindow(this, this.options);
			this.Window.Resize += this.Window_Resize;

			this.Graphics = new GraphicsContext(this.Window.Handle);
			//this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
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
			}
		}
				
		private void Window_Resize(object sender, EventArgs e)
		{
			if (this.options.AutoResizeViewport)
				this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
		}

		Stopwatch stopwatch;
		long nextTick;
		int timeBetweenTicks; 
		TimeSpan elapsed;

		/// <summary>
		/// Starts the game loop.
		/// </summary>
		public void Run()
		{
			this.stopwatch = new Stopwatch();
			this.timeBetweenTicks = 1000 / this.options.FramesPerSecond;
			this.elapsed = TimeSpan.FromMilliseconds(timeBetweenTicks); 

			this.stopwatch.Start();
			this.nextTick = stopwatch.ElapsedMilliseconds;

			this.Window.Show();

			this.Initialize();

			this.Window.Run();

			//while (!this.Window.ShouldClose())
			//{
			//	
			//}

			this.Shutdown();
		}

		internal void Tick()
		{
			if (this.stopwatch.ElapsedMilliseconds >= this.nextTick)
			{
				this.Update(elapsed);
				this.Draw(elapsed);

				this.nextTick += this.timeBetweenTicks;
			}
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
