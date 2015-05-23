using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Samurai;

namespace Samurai.Graphics
{
	public class GraphicsContext : DisposableObject
	{
        IGraphicsHostContext host;

		Color4 clearColor;
		Rectangle viewport;
		Rectangle? scissor;

		bool blendEnabled = false;
		SourceBlendFactor sourceFactor = SourceBlendFactor.One;
		DestinationBlendFactor destinationFactor = DestinationBlendFactor.Zero;

		FrontFace frontFace = FrontFace.CounterClockwise;
		CullMode cullMode = CullMode.None;
		ColorMask colorMask = ColorMask.All;

		bool depthBufferEnabled = false;
		DepthFunction depthBufferFunction = DepthFunction.Less;
		bool depthBufferWriteEnabled = true;

		bool stencilBufferEnabled = false;
		StencilFunction stencilFunction = StencilFunction.Always;
		int stencilReferenceValue = 0;
		uint stencilMask = 0;
		StencilOperation stencilFail = StencilOperation.Keep;
		StencilOperation stencilDepthFail = StencilOperation.Keep;
		StencilOperation stencilPass = StencilOperation.Keep;
		uint stencilWriteMask = 0xFFFFFFFF;
								
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

		public bool BlendEnabled
		{
			get { return this.blendEnabled; }

			set
			{
				if (value != this.blendEnabled)
				{
					this.blendEnabled = value;
					this.ApplyBlendCap();
				}
			}
		}

		public SourceBlendFactor SourceBlendFactor
		{
			get { return this.sourceFactor; }

			set
			{
				if (value != this.sourceFactor)
				{
					this.sourceFactor = value;
					this.ApplyBlendFunc();
				}
			}
		}

		public DestinationBlendFactor DestinationBlendFactor
		{
			get { return this.destinationFactor; }

			set
			{
				if (value != this.destinationFactor)
				{
					this.destinationFactor = value;
					this.ApplyBlendFunc();
				}
			}
		}

		public bool DepthBufferEnabled
		{
			get { return this.depthBufferEnabled; }

			set
			{
				if (value != this.depthBufferEnabled)
				{
					this.depthBufferEnabled = value;
					this.ApplyDepthTestCap();
				}
			}
		}

		public DepthFunction DepthFunction
		{
			get { return this.depthBufferFunction; }

			set
			{
				if (value != this.depthBufferFunction)
				{
					this.depthBufferFunction = value;
					this.ApplyDepthFunc();
				}
			}
		}

		public bool DepthBufferWriteEnabled
		{
			get { return this.depthBufferWriteEnabled; }

			set
			{
				this.depthBufferWriteEnabled = value;
				this.ApplyDepthMask();
			}
		}

		public FrontFace FrontFace
		{
			get { return this.frontFace; }

			set
			{
				if (value != this.frontFace)
				{
					this.frontFace = value;
					this.ApplyFrontFace();
				}
			}
		}

		public CullMode CullMode
		{
			get { return this.cullMode; }

			set
			{
				if (value != this.cullMode)
				{
					this.cullMode = value;
					this.ApplyCullMode();
				}
			}
		}

		public ColorMask ColorMask
		{
			get { return this.colorMask; }

			set
			{
				if (value != this.colorMask)
				{
					this.colorMask = value;
					this.ApplyColorMask();
				}
			}
		}

		public bool StencilBufferEnabled
		{
			get { return this.stencilBufferEnabled; }

			set
			{
				if (value != this.stencilBufferEnabled)
				{
					this.stencilBufferEnabled = value;
					this.ApplyStencilTestCap();
				}
			}
		}

		public StencilFunction StencilFunction
		{
			get { return this.stencilFunction; }

			set
			{
				if (value != this.stencilFunction)
				{
					this.stencilFunction = value;
					this.ApplyStencilFunc();
				}
			}
		}

		public int StencilReferenceValue
		{
			get { return this.stencilReferenceValue; }

			set
			{
				if (value != this.stencilReferenceValue)
				{
					this.stencilReferenceValue = value;
					this.ApplyStencilFunc();
				}
			}
		}

		public uint StencilMask
		{
			get { return this.stencilMask; }

			set
			{
				if (value != this.stencilMask)
				{
					this.stencilMask = value;
					this.ApplyStencilFunc();
				}
			}
		}

		public StencilOperation StencilFail
		{
			get { return this.stencilFail; }

			set
			{
				if (value != this.stencilFail)
				{
					this.stencilFail = value;
					this.ApplyStencilOp();
				}
			}
		}

		public StencilOperation StencilDepthFail
		{
			get { return this.stencilDepthFail; }

			set
			{
				if (value != this.stencilDepthFail)
				{
					this.stencilDepthFail = value;
					this.ApplyStencilOp();
				}
			}
		}

		public StencilOperation StencilPass
		{
			get { return this.stencilPass; }

			set
			{
				if (value != this.stencilPass)
				{
					this.stencilPass = value;
					this.ApplyStencilOp();
				}
			}
		}

		public uint StencilWriteMask
		{
			get { return this.stencilWriteMask; }

			set
			{
				if (value != this.stencilWriteMask)
				{
					this.stencilWriteMask = value;
					this.ApplyStencilWriteMask();
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
		
		public GraphicsContext(IGraphicsHostContext host)
		{
            if (host == null)
                throw new ArgumentNullException("host");

            this.host = host;

			this.GL = new GLContext(this.host);

			this.textures = new bool[32];

			this.scissor = null;
			this.ToggleCap(GLContext.ScissorTestCap, false);

			this.ApplyBlendCap();
			this.ApplyBlendFunc();

			this.ApplyDepthTestCap();
			this.ApplyDepthFunc();
			this.ApplyDepthMask();

			this.ApplyFrontFace();
			this.ApplyCullMode();
			this.ApplyColorMask();

			this.ApplyStencilTestCap();
			this.ApplyStencilFunc();
			this.ApplyStencilOp();
			this.ApplyStencilWriteMask();
						
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
		
		private void ToggleCap(uint cap, bool isEnabled)
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

		private void ApplyBlendCap()
		{
			this.ToggleCap(GLContext.BlendCap, this.BlendEnabled);
		}

		private void ApplyBlendFunc()
		{
			this.GL.BlendFunc((uint)this.SourceBlendFactor, (uint)this.DestinationBlendFactor);
		}

		private void ApplyDepthTestCap()
		{
			this.ToggleCap(GLContext.DepthTestCap, this.DepthBufferEnabled);
		}

		private void ApplyDepthFunc()
		{
			this.GL.DepthFunc((uint)this.DepthFunction);
		}

		private void ApplyDepthMask()
		{
			this.GL.DepthMask(this.DepthBufferWriteEnabled);
		}

		private void ApplyFrontFace()
		{
			this.GL.FrontFace((uint)this.FrontFace);
		}

		private void ApplyCullMode()
		{
			if (this.CullMode == CullMode.None)
			{
				this.ToggleCap(GLContext.CullFaceCap, false);
			}
			else
			{
				this.ToggleCap(GLContext.CullFaceCap, true);
				this.GL.CullFace((uint)this.CullMode);
			}
		}

		private void ApplyColorMask()
		{
			this.GL.ColorMask(
				(this.ColorMask & ColorMask.Red) == ColorMask.Red,
				(this.ColorMask & ColorMask.Green) == ColorMask.Green,
				(this.ColorMask & ColorMask.Blue) == ColorMask.Blue,
				(this.ColorMask & ColorMask.Alpha) == ColorMask.Alpha);
		}

		private void ApplyStencilTestCap()
		{
			this.ToggleCap(GLContext.StencilTestCap, this.StencilBufferEnabled);
		}

		private void ApplyStencilFunc()
		{
			this.GL.StencilFunc((uint)this.StencilFunction, this.StencilReferenceValue, this.StencilMask);
		}

		private void ApplyStencilOp()
		{
			this.GL.StencilOp((uint)this.StencilFail, (uint)this.StencilDepthFail, (uint)this.StencilPass);
		}

		private void ApplyStencilWriteMask()
		{
			this.GL.StencilMask(this.StencilWriteMask);
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
