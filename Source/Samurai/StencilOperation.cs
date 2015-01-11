using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public enum StencilOperation : uint
	{
		Keep = GLContext.Keep,

		Zero = GLContext.Zero,

		Replace = GLContext.Replace,

		Increment = GLContext.Incr,

		IncrementWrap = GLContext.IncrWrap,

		Decrement = GLContext.Decr,

		DecrementWrap = GLContext.DecrWrap,

		Invert = GLContext.Invert
	}
}
