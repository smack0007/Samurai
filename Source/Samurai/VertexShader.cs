using System;
using System.IO;

namespace Samurai
{
	public class VertexShader : Shader
	{
		private VertexShader(GraphicsDevice graphicsDevice)
			: base(graphicsDevice, GL.VertexShader)
		{
		}

		public static VertexShader Compile(GraphicsDevice graphicsDevice, string source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			VertexShader shader = new VertexShader(graphicsDevice);
			Shader.Compile<VertexShader>(shader, source);
			return shader;
		}

		public static VertexShader Compile(GraphicsDevice graphicsDevice, Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			using (StreamReader sr = new StreamReader(stream))
			{
				string source = sr.ReadToEnd();
				return Compile(graphicsDevice, source);
			}
		}
	}
}
