using System;
using System.Reflection;

namespace Samurai
{
	public class BasicSpriteBatchShaderProgram : DisposableObject, ISpriteBatchShaderProgram
	{
		ShaderProgram program;

		public BasicSpriteBatchShaderProgram(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			Assembly assembly = typeof(BasicSpriteBatchShaderProgram).Assembly;
			this.program = new ShaderProgram(
				graphicsDevice,
				VertexShader.Compile(graphicsDevice, assembly.GetManifestResourceStream("Samurai.BasicSpriteBatchShader.vert")),
				FragmentShader.Compile(graphicsDevice, assembly.GetManifestResourceStream("Samurai.BasicSpriteBatchShader.frag")));
		}

		protected override void DisposeManagedResources()
		{
			this.program.Dispose();
			this.program = null;
		}
		
		public void Use()
		{
			this.program.Use();
		}

		public void SetProjectionMatrix(ref Matrix4 projection)
		{
			this.program.SetMatrix("inProjection", ref projection);
		}

		public void SetSampler(Texture texture)
		{
			this.program.SetSampler("fragSampler", texture);
		}
	}
}
