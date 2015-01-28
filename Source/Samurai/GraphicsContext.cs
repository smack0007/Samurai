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
		Rectangle? scissor;

		BlendState blendState;
		DepthBufferState depthBufferState;
		RasterizerState rasterizerState;
		StencilBufferState stencilBufferState;
								
		bool[] textures;
		uint nextTexture;

		List<GraphicsObject> graphicsObjects;

		ShaderProgram shaderProgram;

		internal GLContext GL
		{
			get;
			private set;
		}

		internal bool IsDisposing
		{
			get;
			private set;
		}

        public Color4 ClearColor
        {
            get { return this.clearColor; }

            set
            {
                if (!value.Equals(this.clearColor))
                {
                    this.clearColor = value;
                    this.GL.ClearColor(value.R / 255.0f, value.G / 255.0f, value.B / 255.0f, value.A / 255.0f);
                }
            }
        }

		public Rectangle Viewport
		{
			get { return this.viewport; }
			
			set
			{
				this.viewport = value;
                this.GL.Viewport(value.X, this.host.Height - value.Y - value.Height, value.Width, value.Height);
			}
		}

		public Rectangle? Scissor
		{
			get { return this.scissor; }

			set
			{
				this.scissor = value;

				if (this.scissor.HasValue)
				{
					this.ToggleCap(GLContext.ScissorTestCap, true);
					this.GL.Scissor(this.scissor.Value.X, this.host.Height - value.Value.Y - value.Value.Height, value.Value.Width, value.Value.Height);
				}
				else
				{
					this.ToggleCap(GLContext.ScissorTestCap, false);
				}
			}
		}

		public BlendState BlendState
		{
			get { return this.blendState; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("BlendState");

				if (value != this.blendState)
				{
					this.blendState.SetGraphicsContext(null);
					this.blendState = value;
					this.blendState.SetGraphicsContext(this);					
				}
			}
		}

		public DepthBufferState DepthBufferState
		{
			get { return this.depthBufferState; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("DepthBufferState");

				if (value != this.depthBufferState)
				{
					this.depthBufferState.SetGraphicsContext(null);
					this.depthBufferState = value;
					this.depthBufferState.SetGraphicsContext(this);
				}
			}
		}

		public RasterizerState RasterizerState
		{
			get { return this.rasterizerState; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("RasterizerState");

				if (value != this.rasterizerState)
				{
					this.rasterizerState.SetGraphicsContext(null);
					this.rasterizerState = value;
					this.rasterizerState.SetGraphicsContext(this);
				}
			}
		}

		public StencilBufferState StencilBufferState
		{
			get { return this.stencilBufferState; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("StencilBufferState");

				if (value != this.stencilBufferState)
				{
					this.stencilBufferState.SetGraphicsContext(null);
					this.stencilBufferState = value;
					this.stencilBufferState.SetGraphicsContext(this);
				}
			}
		}

		public ShaderProgram ShaderProgram
		{
			get { return this.shaderProgram; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("ShaderProgram");

				if (value!= this.shaderProgram)
				{
					this.shaderProgram = value;
					this.GL.UseProgram(value.Handle);
				}
			}
		}

		public bool PrependShaderVersionDirective { get; set; }
		
		public GraphicsContext(IGraphicsHost host)
		{
            if (host == null)
                throw new ArgumentNullException("host");

            this.host = host;

			this.GL = new GLContext(this.host.Handle);

			this.textures = new bool[32];

			this.scissor = null;
			this.ToggleCap(GLContext.ScissorTestCap, false);

			this.blendState = new BlendState();
			this.blendState.SetGraphicsContext(this);

			this.depthBufferState = new DepthBufferState();
			this.depthBufferState.SetGraphicsContext(this);

			this.rasterizerState = new RasterizerState();
			this.rasterizerState.SetGraphicsContext(this);

			this.stencilBufferState = new StencilBufferState();
			this.stencilBufferState.SetGraphicsContext(this);
						
			this.clearColor = Color4.CornflowerBlue;
			this.GL.ClearColor(this.clearColor.R / 255.0f, this.clearColor.G / 255.0f, this.clearColor.B / 255.0f, this.clearColor.A / 255.0f);
												
			this.graphicsObjects = new List<GraphicsObject>();

			this.PrependShaderVersionDirective = true;
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

			this.GL.Dispose();
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
		
		internal void ToggleCap(uint cap, bool isEnabled)
		{
			if (isEnabled)
			{
				this.GL.Enable(cap);
			}
			else
			{
				this.GL.Disable(cap);
			}
		}	
		
        public void Clear()
        {
            this.GL.Clear(GLContext.ColorBufferBit | GLContext.DepthBufferBit);
        }

		public void Clear(Color4 color)
		{
            this.ClearColor = color;
            this.Clear();
		}

        public GraphicsContextDescription GetDescription()
        {
            return new GraphicsContextDescription(this);
        }

        public bool MakeCurrent()
        {
            return this.GL.MakeCurrent();
        }
		
        public void SwapBuffers()
		{
			this.GL.SwapBuffers();
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

			this.GL.BindVertexArray(vertexBuffer.vertexArray);
			this.GL.DrawArrays((uint)type, startVertex, vertexCount);
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

			this.GL.BindVertexArray(vertexBuffer.vertexArray);
			this.GL.BindBuffer(GLContext.ElementArrayBuffer, indexBuffer.buffer);
			this.GL.DrawElements((uint)type, indexCount, indexBuffer.dataType, (IntPtr)(startIndex * sizeOfTIndex));
		}
	}
}
