using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
    public static class AssemblyExtensions
    {
        public static ShaderProgram LoadShaderProgram(this Assembly assembly, GraphicsContext graphics, string vertexShaderName, string fragmentShaderName)
        {
            Stream vertexShaderStream = assembly.GetManifestResourceStream(vertexShaderName);

            if (vertexShaderStream == null)
                throw new FileNotFoundException(string.Format("Unable to load \"{0}\" from assembly.", vertexShaderName));

            Stream fragmentShaderStream = assembly.GetManifestResourceStream(fragmentShaderName);

            if (fragmentShaderStream == null)
                throw new FileNotFoundException(string.Format("Unable to load \"{0}\" from assembly.", fragmentShaderName));

            return new ShaderProgram(
                graphics,
                VertexShader.Compile(graphics, vertexShaderStream),
                FragmentShader.Compile(graphics, fragmentShaderStream));
        }
    }
}
