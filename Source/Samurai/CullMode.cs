using System;

namespace Samurai
{
	public enum CullMode : uint
	{
		None,

		Front = GLContext.Front,

		Back = GLContext.Back,

		FrontAndBack = GLContext.FrontAndBack
	}
}
