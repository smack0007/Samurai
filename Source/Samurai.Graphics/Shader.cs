using System;
using System.IO;
using System.Text;

namespace Samurai.Graphics
{
	public class Shader : GraphicsObject
	{
		internal uint Handle
		{
			get;
			private set;
		}

		internal Shader(GraphicsContext graphics, uint shaderType)
			: base(graphics)
		{					
			this.Handle = this.Graphics.GL.CreateShder(shaderType);
		}

		~Shader()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			this.Graphics.GL.DeleteShader(this.Handle);
		}

		internal static void Compile<T>(T shader, string source)
			where T : Shader
		{
			if (shader == null)
				throw new ArgumentNullException("shader");

			if (source == null)
				throw new ArgumentNullException("source");

			if (shader.Graphics.PrependShaderVersionDirective)
				source = string.Concat(shader.Graphics.GL.ShaderVersionDirective, Environment.NewLine, source);

			shader.Graphics.GL.ShaderSource(shader.Handle, source);

			shader.Graphics.GL.CompileShader(shader.Handle);

			if (shader.Graphics.GL.GetShader(shader.Handle, GLContext.CompileStatus) == 0)
			{
				string infoLog = shader.Graphics.GL.GetShaderInfoLog(shader.Handle);
				throw new ShaderCompilationException(string.Format("Failed to compile {0}.", typeof(T).Name), infoLog);
			}
		}
	}
}
