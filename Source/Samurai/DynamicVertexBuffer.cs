using System;

namespace Samurai
{
	public class DynamicVertexBuffer<T> : VertexBuffer<T>
		where T : struct
	{
		public DynamicVertexBuffer(GraphicsContext graphics)
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
