using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	public enum VertexElementType : uint
	{
		Double = GLContext.Double,

		Float = GLContext.Float,
		
		Int = GLContext.Int,

		Short = GLContext.Short,

		UnsignedByte = GLContext.UnsignedByte,

		UnsignedInt = GLContext.UnsignedInt,

		UnsignedShort = GLContext.UnsignedShort
	}
}
