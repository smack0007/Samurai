﻿using System;

namespace Samurai
{
	public enum DepthFunc : uint
	{
		Always = GLContext.Always,

		Equal = GLContext.Equal,
		
		GreaterThanOrEqual = GLContext.Gequal,

		GreaterThan = GLContext.Greater,
		
		LessThanOrEqual = GLContext.Lequal,
		
		Less = GLContext.Less,
		
		Never = GLContext.Never,
		
		NotEqual = GLContext.Notequal
	}
}