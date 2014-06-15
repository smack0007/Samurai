using System;

namespace Samurai
{
	public interface ISpriteBatchShaderProgram
	{
		ShaderProgram ShaderProgram { get; }

		void SetProjectionMatrix(ref Matrix4 matrix);

		void SetSampler(Texture texture);
	}
}
