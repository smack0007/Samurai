using System;
using System.Text;

namespace Samurai
{
	public class Shader : DisposableObject
	{
		GraphicsDevice graphicsDevice;
		string source;

		internal uint Handle
		{
			get;
			private set;
		}

		public ShaderType Type
		{
			get;
			private set;
		}

		public string Source
		{
			get { return this.source; }

			set
			{
				this.source = value;
				GL.ShaderSource(this.Handle, value);
			}
		}

		public bool IsCompiled
		{
			get;
			private set;
		}

		public Shader(GraphicsDevice graphicsDevice, ShaderType type)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			this.graphicsDevice = graphicsDevice;
			
			uint shaderType = 0;

			switch (type)
			{
				case ShaderType.Vertex: shaderType = GL.VertexShader; break;
				case ShaderType.Fragment: shaderType = GL.FragmentShader; break;
			}
			
			this.Handle = GL.CreateShder(shaderType);
			this.Type = type;
		}

		~Shader()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteShader(this.Handle);
		}

		public void Compile()
		{
			if (this.IsCompiled)
				throw new SamuraiException("Shader has already compiled.");

			GL.CompileShader(this.Handle);

			if (GL.GetShader(this.Handle, GL.CompileStatus) == 0)
			{
				string infoLog = GL.GetShaderInfoLog(this.Handle);
				throw new SamuraiException("Failed to compile shader: " + infoLog);
			}

			this.IsCompiled = true;
		}

		public static Shader Compile(GraphicsDevice graphicsDevice, ShaderType type, string source)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			if (source == null)
				throw new ArgumentNullException("source");

			Shader shader = new Shader(graphicsDevice, type) { Source = source };
			shader.Compile();

			return shader;
		}
	}
}
