using System;

namespace Samurai
{
	public sealed class StaticVertexBuffer<T> : VertexBuffer<T>
		where T : struct
	{
		public StaticVertexBuffer(GraphicsDevice graphicsDevice, T[] data)
			: base(graphicsDevice)
		{
			this.SetDataInternal(data, GL.StaticDraw);
		}
	}
}
