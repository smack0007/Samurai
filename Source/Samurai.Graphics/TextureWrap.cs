using System;

namespace Samurai.Graphics
{
	public enum TextureWrap : uint
	{
		Clamp = GLContext.ClampToEdge,

		Repeat = GLContext.Repeat
	}
}
