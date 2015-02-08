using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	public enum StencilFunction : uint
	{
		Never = GLContext.Never,
		
		Less = GLContext.Less,

		LessThanOrEqual = GLContext.Lequal,

		Greater = GLContext.Greater,

		GreaterThanOrEqual = GLContext.Gequal,

		Equal = GLContext.Equal,

		NotEqual = GLContext.Notequal,

		Always = GLContext.Always
	}
}
