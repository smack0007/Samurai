using System;
using System.Reflection;

namespace Samurai.Graphics
{
	/// <summary>
	/// Implements the most basic ShaderProgram needed to use SpriteRenderer.
	/// </summary>
	public class BasicSpriteShaderProgram : DisposableObject, ISpriteShaderProgram
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
		public BasicSpriteShaderProgram(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			Assembly assembly = typeof(BasicSpriteShaderProgram).Assembly;
			this.ShaderProgram = new ShaderProgram(
				graphics,
                VertexShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.Graphics.BasicSpriteShader.vert")),
                FragmentShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.Graphics.BasicSpriteShader.frag")));
		}

		protected override void DisposeManagedResources()
		{
			this.ShaderProgram.Dispose();
			this.ShaderProgram = null;
		}
				
		/// <summary>
		/// Sets the projection matrix.
		/// </summary>
		/// <param name="projection"></param>
		public void SetProjectionMatrix(ref Matrix4 projection)
		{
			this.ShaderProgram.SetValue("inProjection", ref projection);
		}

		/// <summary>
		/// Sets the sampler texture to be used.
		/// </summary>
		/// <param name="texture"></param>
		public void SetSampler(Texture2D texture)
		{
			this.ShaderProgram.SetValue("fragSampler", texture);
		}
	}
}
