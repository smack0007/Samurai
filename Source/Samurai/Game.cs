using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samurai
{
	public abstract class Game : IDisposable
	{		
		public GameWindow Window
		{
			get;
			private set;
		}

		public GraphicsContext Graphics
		{
			get;
			private set;
		}

		protected int FramesPerSecond
		{
			get;
			set;
		}

		protected bool AutoResizeViewport
		{
			get;
			set;
		}

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
			this.Graphics.ClearColor = Color4.CornflowerBlue;
			this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);

			GL.Enable(GL.Blend);
			GL.BlendFunc(GL.SrcAlpha, GL.OneMinusSrcAlpha);
		}

		~Game()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				GLFW.Terminate();
			}
		}
				
		private void Window_Resize(object sender, EventArgs e)
		{
			if (this.AutoResizeViewport)
				this.Graphics.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
		}

		public void Run()
		{
			Stopwatch stopwatch = new Stopwatch();

			long nextTick;
			int timeBetweenTicks = 1000 / this.FramesPerSecond;
			TimeSpan elapsed = TimeSpan.FromMilliseconds(timeBetweenTicks);

			stopwatch.Start();
			nextTick = stopwatch.ElapsedMilliseconds;
						
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

		public void Exit()
		{
			this.Window.SetShouldClose(true);
		}

		protected virtual void Update(TimeSpan elapsed)
		{
		}

		protected virtual void Draw(TimeSpan elapsed)
		{
		}

		protected virtual void Shutdown()
		{
		}
	}
}
