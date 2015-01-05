using System;

namespace Samurai
{
	public enum TextureWrap : uint
	{
		Clamp = GLContext.ClampToEdge,

		Repeat = GLContext.Repeat
	}
}
