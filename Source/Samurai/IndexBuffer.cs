using System;

namespace Samurai
{
	public abstract class IndexBuffer<T> : GraphicsObject
		where T : struct
	{
		GraphicsContext graphics;
		
		internal uint dataType;
		internal uint buffer;

		public int Count
		{
			get;
			private set;
		}

		internal IndexBuffer(GraphicsContext graphics)
			: base(graphics)
		{
			Type dataType = typeof(T);

			if (dataType == typeof(byte))
			{
				this.dataType = GL.UnsignedByte;
			}
			else if (dataType == typeof(ushort))
			{
				this.dataType = GL.UnsignedShort;
			}
			else if (dataType == typeof(uint))
			{
				this.dataType = GL.UnsignedInt;
			}
			else
			{
				throw new InvalidOperationException("IndexBuffer(s) may only have a data type of byte, ushort, or uint.");
			}

			this.buffer = GL.GenBuffer();
		}

		~IndexBuffer()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteBuffer(this.buffer);
		}

		internal void SetDataInternal(T[] data, int index, int count, uint usage)
		{
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);
			GL.BufferData(GL.ArrayBuffer, data, index, count, GL.DynamicDraw);
			this.Count = count;
		}
	}
}
