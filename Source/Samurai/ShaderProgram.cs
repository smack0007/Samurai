using System;
using System.Collections.Generic;

namespace Samurai
{
	public class ShaderProgram : DisposableObject
	{
		GraphicsDevice graphicsDevice;
		Dictionary<ShaderType, Shader> shaders;

		internal uint Handle
		{
			get;
			private set;
		}

		public bool IsLinked
		{
			get;
			private set;
		}

		public ShaderProgram(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			this.graphicsDevice = graphicsDevice;
			this.Handle = GL.CreateProgram();

			this.shaders = new Dictionary<ShaderType, Shader>();
		}

		~ShaderProgram()
		{
			this.Dispose(false);
		}

		protected override void DisposeManagedResources()
		{
			foreach (Shader shader in this.shaders.Values)
			{
				if (!shader.IsDisposed)
					shader.Dispose();
			}
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteProgram(this.Handle);
		}

		public void AttachShader(Shader shader)
		{
			if (shader == null)
				throw new ArgumentNullException("shader");

			if (this.shaders.ContainsKey(shader.Type))
				throw new SamuraiException(string.Format("ShaderProgram already contains a Shader of type {0}.", shader.Type.ToString()));

			GL.AttachShader(this.Handle, shader.Handle);

			this.shaders[shader.Type] = shader;
		}

		public void Link()
		{
			if (this.IsLinked)
				throw new SamuraiException("ShaderProgram has already been linked.");

			GL.LinkProgram(this.Handle);

			this.IsLinked = true;
		}

		public void Use()
		{
			GL.UseProgram(this.Handle);
		}

		public void SetMatrix(string name, ref Matrix4 value)
		{
			int location = GL.GetUniformLocation(this.Handle, name);
			GL.UniformMatrix4(location, ref value);
		}
	}
}
