﻿using System;

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

		public void Draw<T>(VertexBuffer<T> vertexBuffer)
			where T : struct
		{
			if (vertexBuffer == null)
				throw new ArgumentNullException("vertexBuffer");

			GL.BindVertexArray(vertexBuffer.vertexArray);
			GL.DrawArrays(GL.Triangles, 0, vertexBuffer.Count);
		}

		public void Draw<TVertex, TIndex>(VertexBuffer<TVertex> vertexBuffer, IndexBuffer<TIndex> indexBuffer)
			where TVertex : struct
			where TIndex : struct
		{
			if (vertexBuffer == null)
				throw new ArgumentNullException("vertexBuffer");

			if (indexBuffer == null)
				throw new ArgumentNullException("indexBuffer");

			GL.BindVertexArray(vertexBuffer.vertexArray);
			GL.BindBuffer(GL.ElementArrayBuffer, indexBuffer.buffer);
			GL.DrawElements(GL.Triangles, indexBuffer.Count, indexBuffer.dataType, IntPtr.Zero);
		}

		public void End()
		{
			this.context.SwapBuffers();
		}
	}
}
