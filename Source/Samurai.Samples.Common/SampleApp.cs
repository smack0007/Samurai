using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Samples.Common
{
	public abstract class SampleApp : IGraphicsHost, IDisposable
	{
#if WINDOWS
		Win32Window window = new Win32Window();
#endif
		TimeSpan elapsed;
		Stopwatch stopwatch;
		const long TimeBetweenTicks = 1000 / 60;
		long lastTick;
		long nextTick;

		public IntPtr Handle
		{
			get { return this.window.Handle; }
		}

		public int Width
		{
			get { return this.window.Width; }
		}

		public int Height
		{
			get { return this.window.Height; }
		}

		public string Title
		{
			get { return this.window.Title; }
			set { this.window.Title = value; }
		}

		public GraphicsContext Graphics
		{
			get;
			private set;
		}

		protected SampleApp()
		{
			this.window.Initialized = this.OnWindowInitialized;
			this.window.Idle = this.OnWindowIdle;
			this.window.Resized = this.OnWindowResized;

			this.Graphics = new GraphicsContext(this.window);
			this.Graphics.Viewport = new Rectangle(0, 0, this.Width, this.Height);
		}

		~SampleApp()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Graphics.Dispose();
				this.window.Dispose();
			}
		}

		private void OnWindowInitialized()
		{
			this.elapsed = TimeSpan.Zero;
			this.stopwatch = new Stopwatch();

			this.stopwatch.Start();
			long elapsed = this.stopwatch.ElapsedMilliseconds;
			this.lastTick = elapsed;
			this.nextTick = elapsed;
		}

		private void OnWindowIdle()
		{
			long totalElapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;

			if (totalElapsedMilliseconds >= this.nextTick)
			{
				this.elapsed = TimeSpan.FromMilliseconds(totalElapsedMilliseconds - this.lastTick);

				this.Update(this.elapsed);
				this.Draw(this.elapsed);
				this.Graphics.SwapBuffers();

				this.lastTick = totalElapsedMilliseconds;
				this.nextTick = totalElapsedMilliseconds + TimeBetweenTicks;
			}
		}

		private void OnWindowResized()
		{
			this.Resize();
		}

		public void Run()
		{
			this.window.Run();
		}

		protected virtual void Update(TimeSpan elapsed)
		{
		}

		protected virtual void Draw(TimeSpan elapsed)
		{
		}

		protected virtual void Resize()
		{
			this.Graphics.Viewport = new Rectangle(0, 0, this.Width, this.Height);
		}
	}
}
