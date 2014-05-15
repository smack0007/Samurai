using System;

namespace Samurai
{
	public class DynamicIndexBuffer<T> : IndexBuffer<T>
		where T : struct
	{
		public DynamicIndexBuffer(GraphicsDevice graphicsDevice)
			: base(graphicsDevice)
		{
		}

		public void SetData(T[] data)
		{
			this.SetDataInternal(data, GL.DynamicDraw);
		}
	}
}
