using System;

namespace Samurai.Graphics
{
    public class GraphicsContextDescription
    {
        public string Vendor
        {
            get;
            private set;
        }

        public string Renderer
        {
            get;
            private set;
        }

        public string Version
        {
            get;
            private set;
        }

        public string ShadingLanguageVersion
        {
            get;
            private set;
        }

        public string[] Extensions
        {
            get;
            private set;
        }

        internal GraphicsContextDescription(GraphicsContext graphics)
        {
            this.Vendor = graphics.GL.GetString(GLContext.Vendor);
            this.Renderer = graphics.GL.GetString(GLContext.Renderer);
            this.Version = graphics.GL.GetString(GLContext.Version);
            this.ShadingLanguageVersion = graphics.GL.GetString(GLContext.ShadingLanguageVersion);

			int numExtensions = graphics.GL.GetIntegerv(GLContext.NumExtensions);

			this.Extensions = new string[numExtensions];

			for (uint i = 0; i < numExtensions; i++)
			{
				this.Extensions[i] = graphics.GL.GetStringi(GLContext.Extensions, i);
			}
        }
    }
}
