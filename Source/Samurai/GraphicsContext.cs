using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Samurai
{
	public class GraphicsContext : DisposableObject
	{
		IGraphicsHost host;

		Color4 clearColor;
		Rectangle viewport;

		bool blendEnabled;
		SourceBlendFactor sourceBlendFactor = SourceBlendFactor.One;
		DestinationBlendFactor destinationBlendFactor = DestinationBlendFactor.Zero;

		bool[] textures;
		uint nextTexture;

		List<GraphicsObject> graphicsObjects;

		internal bool IsDisposing
		{
			get;
			private set;
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

		public bool BlendEnabled
		{
			get { return this.blendEnabled; }

			set
			{
				if (value != this.blendEnabled)
				{
					this.blendEnabled = value;

					if (value)
					{
						GL.Enable(GL.Blend);
					}
					else
					{
						GL.Disable(GL.Blend);
					}
				}
			}
		}

		public SourceBlendFactor SourceBlendFactor
		{
			get { return this.sourceBlendFactor; }

			set
			{
				if (value != this.sourceBlendFactor)
				{
					this.sourceBlendFactor = value;
					GL.BlendFunc((uint)this.sourceBlendFactor, (uint)this.destinationBlendFactor);
				}
			}
		}

		public DestinationBlendFactor DestinationBlendFactor
		{
			get { return this.destinationBlendFactor; }

			set
			{
				if (value != this.destinationBlendFactor)
				{
					this.destinationBlendFactor = value;
					GL.BlendFunc((uint)this.sourceBlendFactor, (uint)this.destinationBlendFactor);
				}
			}
		}

		public GraphicsContext(IGraphicsHost host)
		{
			if (host == null)
				throw new ArgumentNullException("host");

			this.host = host;

			this.textures = new bool[32];

			this.clearColor = Color4.CornflowerBlue;
			GL.ClearColor(this.clearColor.R / 255.0f, this.clearColor.G / 255.0f, this.clearColor.B / 255.0f, this.clearColor.A / 255.0f);

			this.graphicsObjects = new List<GraphicsObject>();
		}

		~GraphicsContext()
		{
			this.Dispose(false);
		}

		protected override void Dispose(bool disposing)
		{
			this.IsDisposing = true;
			base.Dispose(disposing);
		}

		protected override void DisposeManagedResources()
		{
			foreach (GraphicsObject obj in this.graphicsObjects)
			{
				if (!obj.IsDisposed)
					obj.Dispose();
			}

			this.graphicsObjects.Clear();
		}
		
		internal void RegisterGraphicsObject(GraphicsObject obj)
		{
			this.graphicsObjects.Add(obj);
		}

		internal void UnregisterGraphicsObject(GraphicsObject obj)
		{
			this.graphicsObjects.Remove(obj);
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
		
		public void Clear(Color4 color)
		{
			if (!color.Equals(this.clearColor))
			{
				this.clearColor = color;
				GL.ClearColor(this.clearColor.R / 255.0f, this.clearColor.G / 255.0f, this.clearColor.B / 255.0f, this.clearColor.A / 255.0f);
			}

			GL.Clear(GL.ColorBufferBit | GL.DepthBufferBit);
		}

		public void SwapBuffers()
		{
			this.host.SwapBuffers();
		}

		public void SetBlendFunction(SourceBlendFactor source, DestinationBlendFactor destination)
		{
			if (source != this.sourceBlendFactor || destination != this.destinationBlendFactor)
			{
				this.sourceBlendFactor = source;
				this.destinationBlendFactor = destination;
				GL.BlendFunc((uint)this.sourceBlendFactor, (uint)this.destinationBlendFactor);
			}
		}

		public void SetShaderProgram(ShaderProgram shader)
		{
			if (shader == null)
				throw new ArgumentNullException("shader");

			GL.UseProgram(shader.Handle);
		}

		public void Draw<T>(PrimitiveType type, VertexBuffer<T> vertexBuffer)
			where T : struct
		{
			if (vertexBuffer == null)
				throw new ArgumentNullException("vertexBuffer");

			this.Draw(type, vertexBuffer, 0, vertexBuffer.Count);
		}

		public void Draw<T>(PrimitiveType type, VertexBuffer<T> vertexBuffer, int startVertex, int vertexCount)
			where T : struct
		{
			if (vertexBuffer == null)
				throw new ArgumentNullException("vertexBuffer");

			GL.BindVertexArray(vertexBuffer.vertexArray);
			GL.DrawArrays((uint)type, startVertex, vertexCount);
		}

		public void Draw<TVertex, TIndex>(PrimitiveType type, VertexBuffer<TVertex> vertexBuffer, IndexBuffer<TIndex> indexBuffer)
			where TVertex : struct
			where TIndex : struct
		{
			if (vertexBuffer == null)
				throw new ArgumentNullException("vertexBuffer");

			if (indexBuffer == null)
				throw new ArgumentNullException("indexBuffer");

			this.Draw(type, vertexBuffer, indexBuffer, 0, indexBuffer.Count);
		}

		public void Draw<TVertex, TIndex>(PrimitiveType type, VertexBuffer<TVertex> vertexBuffer, IndexBuffer<TIndex> indexBuffer, int startIndex, int indexCount)
			where TVertex : struct
			where TIndex : struct
		{
			if (vertexBuffer == null)
				throw new ArgumentNullException("vertexBuffer");

			if (indexBuffer == null)
				throw new ArgumentNullException("indexBuffer");

			int sizeOfTIndex = Marshal.SizeOf(typeof(TIndex));

			GL.BindVertexArray(vertexBuffer.vertexArray);
			GL.BindBuffer(GL.ElementArrayBuffer, indexBuffer.buffer);
			GL.DrawElements((uint)type, indexCount, indexBuffer.dataType, (IntPtr)(startIndex * sizeOfTIndex));
		}
	}
}
