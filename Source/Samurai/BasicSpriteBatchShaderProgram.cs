using System;
using System.Reflection;

namespace Samurai
{
	public class BasicSpriteBatchShaderProgram : DisposableObject, ISpriteBatchShaderProgram
	{
		public ShaderProgram ShaderProgram
		{
			get;
			private set;
		}

		public BasicSpriteBatchShaderProgram(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			Assembly assembly = typeof(BasicSpriteBatchShaderProgram).Assembly;
			this.ShaderProgram = new ShaderProgram(
				graphics,
				VertexShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.BasicSpriteBatchShader.vert")),
				FragmentShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.BasicSpriteBatchShader.frag")));
		}

		protected override void DisposeManagedResources()
		{
			this.ShaderProgram.Dispose();
			this.ShaderProgram = null;
		}
				
		public void SetProjectionMatrix(ref Matrix4 projection)
		{
			this.ShaderProgram.SetMatrix("inProjection", ref projection);
		}

		public void SetSampler(Texture texture)
		{
			this.ShaderProgram.SetSampler("fragSampler", texture);
		}
	}
}
