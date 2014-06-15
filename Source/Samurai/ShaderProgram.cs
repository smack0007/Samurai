using System;
using System.Collections.Generic;

namespace Samurai
{
	public sealed class ShaderProgram : DisposableObject
	{
		GraphicsDevice graphicsDevice;
		VertexShader vertexShader;
		FragmentShader fragmentShader;

		internal uint Handle
		{
			get;
			private set;
		}
		
		public ShaderProgram(GraphicsDevice graphicsDevice, VertexShader vertexShader, FragmentShader fragmentShader)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			if (vertexShader == null)
				throw new ArgumentNullException("vertexShader");

			if (fragmentShader == null)
				throw new ArgumentNullException("fragmentShader");

			this.graphicsDevice = graphicsDevice;
			this.Handle = GL.CreateProgram();

			this.vertexShader = vertexShader;
			GL.AttachShader(this.Handle, this.vertexShader.Handle);

			this.fragmentShader = fragmentShader;
			GL.AttachShader(this.Handle, this.fragmentShader.Handle);

			GL.LinkProgram(this.Handle);
		}

		~ShaderProgram()
		{
			this.Dispose(false);
		}

		protected override void DisposeManagedResources()
		{
			if (!this.vertexShader.IsDisposed)
				this.vertexShader.Dispose();

			if (!this.fragmentShader.IsDisposed)
				this.fragmentShader.Dispose();
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteProgram(this.Handle);
		}
				
		public void SetMatrix(string name, ref Matrix4 value)
		{
			int location = GL.GetUniformLocation(this.Handle, name);
			GL.UniformMatrix4(location, ref value);
		}

		public void SetSampler(string name, Texture texture)
		{
			int location = GL.GetUniformLocation(this.Handle, name);
			GL.Uniform1i(location, (int)texture.Index);
		}
	}
}
