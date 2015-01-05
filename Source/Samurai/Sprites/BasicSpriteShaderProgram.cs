using System;
using System.Reflection;

namespace Samurai.Sprites
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

            this.ShaderProgram = typeof(BasicSpriteShaderProgram).Assembly.LoadShaderProgram(
               graphics,
               "Samurai.Sprites.BasicSpriteShader.vert",
               "Samurai.Sprites.BasicSpriteShader.frag");
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
			this.ShaderProgram.SetValue("vertTransform", ref transform);
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
