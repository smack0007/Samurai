using System;
using System.IO;

namespace Samurai
{
	public class VertexShader : Shader
	{
		private VertexShader(GraphicsContext graphics)
			: base(graphics, GLContext.VertexShader)
		{
		}

		public static VertexShader Compile(GraphicsContext graphics, string source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			VertexShader shader = new VertexShader(graphics);
			Shader.Compile<VertexShader>(shader, source);
			return shader;
		}

		public static VertexShader Compile(GraphicsContext graphics, Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			using (StreamReader sr = new StreamReader(stream))
			{
				string source = sr.ReadToEnd();
				return Compile(graphics, source);
			}
		}
	}
}
