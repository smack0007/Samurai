using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samurai.Samples.Common
{
    public class RenderForm : Form, IGraphicsHost
    {
		[StructLayout(LayoutKind.Sequential)]
		private struct MSG
		{
			public IntPtr hWnd;
			public uint message;
			public IntPtr wParam;
			public IntPtr lParam;
			public uint time;
			public System.Drawing.Point pt;
		}

		[DllImport("user32.dll")]
		private static extern int PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

		TimeSpan elapsed;
		Stopwatch stopwatch;
		const long TimeBetweenTicks = 1000 / 60;
		long lastTick;
		long nextTick;

		public GraphicsContext Graphics
		{
			get;
			private set;
		}

		public RenderForm()
		{
			this.Text = "Samurai Sample";
			this.Size = new System.Drawing.Size(800, 600);

			this.Graphics = new GraphicsContext(this);
			this.Graphics.Viewport = new Rectangle(0, 0, this.Width, this.Height);

			Application.Idle += this.Application_Idle;
		}

		protected override void Dispose(bool disposing)
		{
			this.Graphics.Dispose();

			Application.Idle -= this.Application_Idle;

			base.Dispose(disposing);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			this.elapsed = TimeSpan.Zero;
			this.stopwatch = new Stopwatch();

			this.stopwatch.Start();
			long elapsed = this.stopwatch.ElapsedMilliseconds;
			this.lastTick = elapsed;
			this.nextTick = elapsed;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			MSG msg;
			while (PeekMessage(out msg, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0)
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
		}

		public static void Run<T>()
			where T: RenderForm, new()
		{
			Application.EnableVisualStyles();

			T form = new T();
			Application.Run(form);
		}

		protected virtual void Update(TimeSpan elapsed)
		{
		}

		protected virtual void Draw(TimeSpan elapsed)
		{
		}
    }
}
