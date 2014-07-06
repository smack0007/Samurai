using System;

namespace Samurai.Graphics
{
	public interface ISpriteBatchShaderProgram
	{
		ShaderProgram ShaderProgram { get; }

		void SetProjectionMatrix(ref Matrix4 matrix);

		void SetSampler(Texture texture);
	}
}
