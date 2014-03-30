using System;

namespace Samurai
{
	public class GraphicsDevice : IDisposable
	{
		IGraphicsContext context;

		Color4 clearColor;
		Rectangle viewport;

		public Color4 ClearColor
		{
			get { return this.clearColor; }

			set
			{
				this.clearColor = value;
				GL.ClearColor(value.R, value.G, value.B, value.A);
			}
		}

		public Rectangle Viewport
		{
			get { return this.viewport; }
			
			set
			{
				this.viewport = value;
				GL.Viewport(value.X, value.Y, value.Width, value.Height);
			}
		}

		public GraphicsDevice(IGraphicsContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			this.context = context;
		}

		~GraphicsDevice()
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

		}
				
		public void Begin()
		{
			GL.Clear(GL.ColorBufferBit | GL.DepthBufferBit);
		}

		public void Draw<T>(DrawBuffer<T> drawBuffer)
			where T : struct
		{
			if (drawBuffer == null)
				throw new ArgumentNullException("drawBuffer");

			GL.DrawArrays(GL.Triangles, 0, drawBuffer.VertexCount);
		}

		public void End()
		{
			this.context.SwapBuffers();
		}
	}
}
