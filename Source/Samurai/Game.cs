using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

		public GraphicsDevice GraphicsDevice
		{
			get;
			private set;
		}

		protected bool AutoResizeViewport
		{
			get;
			set;
		}

		public Game()
		{
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

			this.GraphicsDevice = new GraphicsDevice(this.Window);
			this.GraphicsDevice.ClearColor = Color4.CornflowerBlue;
			this.GraphicsDevice.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);

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
				this.GraphicsDevice.Viewport = new Rectangle(0, 0, this.Window.Width, this.Window.Height);
		}

		public void Run()
		{
			while (!this.Window.ShouldClose())
			{
				GLFW.PollEvents();

				this.Update();
				this.Draw();
			}

			this.Shutdown();
		}

		public void Exit()
		{
			this.Window.SetShouldClose(true);
		}

		protected virtual void Update()
		{
		}

		protected virtual void Draw()
		{
		}

		protected virtual void Shutdown()
		{
		}
	}
}
