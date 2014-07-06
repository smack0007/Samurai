using System;

namespace Samurai.Graphics
{
	public abstract class IndexBuffer<T> : GraphicsObject
		where T : struct
	{		
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
				this.dataType = GLContext.UnsignedByte;
			}
			else if (dataType == typeof(ushort))
			{
				this.dataType = GLContext.UnsignedShort;
			}
			else if (dataType == typeof(uint))
			{
				this.dataType = GLContext.UnsignedInt;
			}
			else
			{
				throw new InvalidOperationException("IndexBuffer(s) may only have a data type of byte, ushort, or uint.");
			}

			this.buffer = this.Graphics.GL.GenBuffer();
		}

		~IndexBuffer()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			this.Graphics.GL.DeleteBuffer(this.buffer);
		}

		internal void SetDataInternal(T[] data, int index, int count, uint usage)
		{
			this.Graphics.GL.BindBuffer(GLContext.ArrayBuffer, this.buffer);
			this.Graphics.GL.BufferData(GLContext.ArrayBuffer, data, index, count, GLContext.DynamicDraw);
			this.Count = count;
		}
	}
}
