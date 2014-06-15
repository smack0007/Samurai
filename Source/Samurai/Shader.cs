using System;
using System.IO;
using System.Text;

namespace Samurai
{
	public class Shader : DisposableObject
	{
		GraphicsContext graphics;
		string source;

		internal uint Handle
		{
			get;
			private set;
		}

		internal Shader(GraphicsContext graphics, uint shaderType)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.graphics = graphics;
					
			this.Handle = GL.CreateShder(shaderType);
		}

		~Shader()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteShader(this.Handle);
		}

		internal static void Compile<T>(T shader, string source)
			where T : Shader
		{
			if (shader == null)
				throw new ArgumentNullException("shader");

			if (source == null)
				throw new ArgumentNullException("source");
					
			GL.ShaderSource(shader.Handle, source);

			GL.CompileShader(shader.Handle);

			if (GL.GetShader(shader.Handle, GL.CompileStatus) == 0)
			{
				string infoLog = GL.GetShaderInfoLog(shader.Handle);
				throw new SamuraiException(string.Format("Failed to compile {0}: {1}", typeof(T).Name, infoLog));
			}
		}
	}
}
