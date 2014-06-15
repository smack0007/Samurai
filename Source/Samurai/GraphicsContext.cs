﻿using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	public class GraphicsContext : DisposableObject
	{
		IGraphicsHost host;

		Color4 clearColor;
		Rectangle viewport;

		bool[] textures;
		uint nextTexture;

		public Rectangle Viewport
		{
			get { return this.viewport; }
			
			set
			{
				this.viewport = value;
				GL.Viewport(value.X, value.Y, value.Width, value.Height);
			}
		}

		public GraphicsContext(IGraphicsHost context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			this.host = context;

			this.textures = new bool[32];

			this.clearColor = Color4.CornflowerBlue;
			GL.ClearColor(this.clearColor.R / 255.0f, this.clearColor.G / 255.0f, this.clearColor.B / 255.0f, this.clearColor.A / 255.0f);
		}

		~GraphicsContext()
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
