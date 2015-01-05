using System;

namespace Samurai.Sprites
{
	public interface ISpriteShaderProgram
	{
		ShaderProgram ShaderProgram { get; }

		void SetTransform(ref Matrix4 transform);

		void SetSampler(Texture2D texture);
	}
}
