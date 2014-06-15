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

		public BasicSpriteBatchShaderProgram(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			Assembly assembly = typeof(BasicSpriteBatchShaderProgram).Assembly;
			this.ShaderProgram = new ShaderProgram(
				graphicsDevice,
				VertexShader.Compile(graphicsDevice, assembly.GetManifestResourceStream("Samurai.BasicSpriteBatchShader.vert")),
				FragmentShader.Compile(graphicsDevice, assembly.GetManifestResourceStream("Samurai.BasicSpriteBatchShader.frag")));
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
