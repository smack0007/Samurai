using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public class FragmentShader : Shader
	{
		private FragmentShader(GraphicsContext graphics)
			: base(graphics, GL.FragmentShader)
		{
		}

		public static FragmentShader Compile(GraphicsContext graphics, string source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			FragmentShader shader = new FragmentShader(graphics);
			Shader.Compile<FragmentShader>(shader, source);
			return shader;
		}

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
