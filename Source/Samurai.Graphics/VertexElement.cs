using System;

namespace Samurai.Graphics
{
	public struct VertexElement
	{
		public VertexElementType Type;

		public int Size;

		public VertexElement(VertexElementType type, int size)
		{
			this.Type = type;
			this.Size = size;
		}
	}
}
