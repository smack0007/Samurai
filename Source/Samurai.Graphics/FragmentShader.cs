using System;
using System.IO;

namespace Samurai.Graphics
{
	/// <summary>
	/// Shader which processes a fragment from the rasterization process into a set of colors and a single depth value.
	/// </summary>
	public class FragmentShader : Shader
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="graphics">A handle to the GraphicsContext.</param>
		private FragmentShader(GraphicsContext graphics)
			: base(graphics, GLContext.FragmentShader)
		{
		}

		/// <summary>
		/// Compiles a FragmentShader from a string.
		/// </summary>
		/// <param name="graphics">A handle to the GraphicsContext.</param>
		/// <param name="source">The source of the shader.</param>
		/// <returns></returns>
		public static FragmentShader Compile(GraphicsContext graphics, string source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			FragmentShader shader = new FragmentShader(graphics);
			Shader.Compile<FragmentShader>(shader, source);
			return shader;
		}

		/// <summary>
		/// Compiles a FragmentShader from a stream.
		/// </summary>
		/// <param name="graphics">A handle to the GraphicsContext.</param>
		/// <param name="stream">A stream containing the source of the shader.</param>
		/// <returns></returns>
		public static FragmentShader Compile(GraphicsContext graphics, Stream stream)
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
