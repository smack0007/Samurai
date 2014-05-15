using System;

namespace Samurai
{
	public class DynamicVertexBuffer<T> : VertexBuffer<T>
		where T : struct
	{
		public DynamicVertexBuffer(GraphicsDevice graphicsDevice)
			: base(graphicsDevice)
		{
		}

		public void SetData(T[] data)
		{
			this.SetDataInternal(data, GL.DynamicDraw);
		}
	}
}
