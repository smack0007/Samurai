using System;

namespace Samurai
{
	public class DynamicIndexBuffer<T> : IndexBuffer<T>
		where T : struct
	{
		public DynamicIndexBuffer(GraphicsContext graphics)
			: base(graphics)
		{
		}

		public void SetData(T[] data)
		{
			this.SetDataInternal(data, 0, data.Length, GL.DynamicDraw);
		}

		public void SetData(T[] data, int startIndex, int length)
		{
			this.SetDataInternal(data, startIndex, length, GL.DynamicDraw);
		}
	}
}
