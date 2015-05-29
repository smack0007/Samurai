using System;

namespace Samurai.Graphics
{
	public sealed class StaticVertexBuffer : VertexBuffer
	{
		public StaticVertexBuffer(GraphicsContext graphics, VertexElement[] vertexElements, DataBuffer data)
			: base(graphics)
		{
			if (vertexElements == null)
				throw new ArgumentNullException("vertexElements");

			this.SetVertexDescription(vertexElements);
			this.SetDataInternal(data, 0, data.Length, GLContext.StaticDraw);
		}
	}

	public sealed class StaticVertexBuffer<T> : VertexBuffer<T>
		where T : struct
	{
		public StaticVertexBuffer(GraphicsContext graphics, T[] data)
			: base(graphics)
		{
			this.SetDataInternal(data, 0, data.Length, GLContext.StaticDraw);
		}
	}
}
