using System;

namespace Samurai
{
	public abstract class IndexBuffer<T> : DisposableObject
		where T : struct
	{
		GraphicsDevice graphicsDevice;
		
		internal uint dataType;
		internal uint buffer;

		public int Count
		{
			get;
			private set;
		}

		internal IndexBuffer(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			this.graphicsDevice = graphicsDevice;

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
