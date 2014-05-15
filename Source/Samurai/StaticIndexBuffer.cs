using System;

namespace Samurai
{
	public class StaticIndexBuffer<T> : IndexBuffer<T>
		where T : struct
	{
		public StaticIndexBuffer(GraphicsDevice graphicsDevice, T[] data)
			: base(graphicsDevice)
		{
			this.SetDataInternal(data, GL.StaticDraw);
		}
	}
}
