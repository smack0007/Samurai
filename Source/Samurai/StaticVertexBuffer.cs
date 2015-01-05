using System;

namespace Samurai
{
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
