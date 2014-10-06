using System;

namespace Samurai.Graphics
{
    public interface ICanvas2DShaderProgram
    {
        ShaderProgram ShaderProgram { get; }

        void SetTransform(ref Matrix4 transform);

        void SetSampler(Texture2D texture);
    }
}
