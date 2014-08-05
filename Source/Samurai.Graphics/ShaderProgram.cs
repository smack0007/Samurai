using System;
using System.Collections.Generic;

namespace Samurai.Graphics
{
	public sealed class ShaderProgram : GraphicsObject
	{
		VertexShader vertexShader;
		FragmentShader fragmentShader;

		internal uint Handle
		{
			get;
			private set;
		}
		
		public ShaderProgram(GraphicsContext graphics, VertexShader vertexShader, FragmentShader fragmentShader)
			: base(graphics)
		{
			if (vertexShader == null)
				throw new ArgumentNullException("vertexShader");

			if (fragmentShader == null)
				throw new ArgumentNullException("fragmentShader");

			this.Handle = this.Graphics.GL.CreateProgram();

			this.vertexShader = vertexShader;
			this.Graphics.GL.AttachShader(this.Handle, this.vertexShader.Handle);

			this.fragmentShader = fragmentShader;
			this.Graphics.GL.AttachShader(this.Handle, this.fragmentShader.Handle);

			this.Graphics.GL.LinkProgram(this.Handle);
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
			this.Graphics.GL.DeleteProgram(this.Handle);
		}
		
		public void SetValue(string name, float value)
		{
			int location = this.Graphics.GL.GetUniformLocation(this.Handle, name);
			this.Graphics.GL.Uniform1f(location, value);
		}

		public void SetValue(string name, ref Matrix4 value)
		{
			int location = this.Graphics.GL.GetUniformLocation(this.Handle, name);
			this.Graphics.GL.UniformMatrix4(location, ref value);
		}

		public void SetSampler(string name, Texture2D texture)
		{
			int location = this.Graphics.GL.GetUniformLocation(this.Handle, name);
			this.Graphics.GL.Uniform1i(location, (int)texture.Index);
		}
	}
}
