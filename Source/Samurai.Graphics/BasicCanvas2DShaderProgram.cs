using System;
using System.Reflection;

namespace Samurai.Graphics
{
    /// <summary>
    /// Implements the most basic ShaderProgram needed to use Canvas2DRenderer.
    /// </summary>
    public class BasicCanvas2DShaderProgram : DisposableObject, ICanvas2DShaderProgram
    {
        /// <summary>
		/// Gets the underlying ShaderProgram.
		/// </summary>
		public ShaderProgram ShaderProgram
		{
			get;
			private set;
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="graphics">Handle to the GraphicsContext.</param>
        public BasicCanvas2DShaderProgram(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			Assembly assembly = typeof(BasicSpriteShaderProgram).Assembly;
			this.ShaderProgram = new ShaderProgram(
				graphics,
                VertexShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.Graphics.BasicCanvas2DShader.vert")),
                FragmentShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.Graphics.BasicCanvas2DShader.frag")));
		}

		protected override void DisposeManagedResources()
		{
			this.ShaderProgram.Dispose();
			this.ShaderProgram = null;
		}
				
		/// <summary>
		/// Sets the transform matrix.
		/// </summary>
		/// <param name="transform"></param>
		public void SetTransform(ref Matrix4 transform)
		{
			this.ShaderProgram.SetValue("inTransform", ref transform);
		}

		/// <summary>
		/// Sets the sampler texture to be used.
		/// </summary>
		/// <param name="texture"></param>
		public void SetSampler(Texture2D texture)
		{
			this.ShaderProgram.SetSampler("fragSampler", texture);
		}
    }
}
