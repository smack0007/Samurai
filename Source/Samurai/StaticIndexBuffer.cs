using System;

namespace Samurai
{
	public class StaticIndexBuffer<T> : IndexBuffer<T>
		where T : struct
	{
		public StaticIndexBuffer(GraphicsContext graphics, T[] data)
			: base(graphics)
		{
			this.SetDataInternal(data, 0, data.Length, GLContext.StaticDraw);
		}
	}
}
