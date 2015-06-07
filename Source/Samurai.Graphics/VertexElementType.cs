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

	public static class VertexElementTypeExtensions
	{
		public static int GetSizeInBytes(this VertexElementType type)
		{
			switch (type)
			{
				case VertexElementType.Double:
					return 8;

				case VertexElementType.Float:
				case VertexElementType.Int:
				case VertexElementType.UnsignedInt:
					return 4;

				case VertexElementType.Short:
					return 2;

				case VertexElementType.UnsignedByte:
					return 1;
			}

			throw new SamuraiException(string.Format("Unable to determine size in bytes for VertexElementType {0}.", type));
		}
	}
}
