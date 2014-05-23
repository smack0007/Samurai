using System;

namespace Samurai
{
	public class GraphicsDevice : DisposableObject
	{
		IGraphicsContext context;

		Color4 clearColor;
		Rectangle viewport;

		bool[] textures;
		uint nextTexture;

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

			this.textures = new bool[32];
		}

		~GraphicsDevice()
		{
			this.Dispose(false);
		}
		
		internal uint AllocateTextureIndex()
		{
			for (uint i = 0; i < this.textures.Length; i++)
			{
				uint index = this.nextTexture + i;

				if (index >= this.textures.Length)
					index -= (uint)this.textures.Length;

				if (!this.textures[index])
				{					
					this.textures[index] = true;
					this.nextTexture = index + 1;

					if (this.nextTexture >= this.textures.Length)
						this.nextTexture -= (uint)this.textures.Length;
					
					return index;
				}
			}

			throw new SamuraiException("Maximum number of Textures allocated. Dispose of at least one Texture before allocating another.");
		}

		internal void DeallocateTextureIndex(uint index)
		{
			this.textures[index] = false;
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
