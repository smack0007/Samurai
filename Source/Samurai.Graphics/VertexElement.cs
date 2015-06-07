using System;

namespace Samurai.Graphics
{
	public struct VertexElement
	{
		public VertexElementType Type;

		public int Count;

		public int Offset;

		public int Stride;

		public VertexElement(VertexElementType type, int count, int offset, int stride)
		{
			this.Type = type;
			this.Count = count;
			this.Offset = offset;
			this.Stride = stride;
		}
	}
}
