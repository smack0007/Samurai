using System;

namespace Samurai.Graphics
{
	public interface ISpriteShaderProgram
	{
		ShaderProgram ShaderProgram { get; }

		void SetProjectionMatrix(ref Matrix4 matrix);

		void SetSampler(Texture2D texture);
	}
}
