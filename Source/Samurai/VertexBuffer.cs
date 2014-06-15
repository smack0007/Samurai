using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Samurai
{
	public abstract class VertexBuffer<T> : DisposableObject
		where T : struct
	{
		GraphicsContext graphicsDevice;

		internal uint vertexArray;
		internal uint buffer;

		public int Count
		{
			get;
			private set;
		}

		internal VertexBuffer(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.graphicsDevice = graphics;

			this.vertexArray = GL.GenVertexArray();
			GL.BindVertexArray(this.vertexArray);

			this.buffer = GL.GenBuffer();
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);

			Type vertexType = typeof(T);

			uint index = 0;
			foreach (var fieldInfo in vertexType.GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				GL.EnableVertexAttribArray(index);
				uint type = GLHelper.GetVertexAttribPointerTypeForType(fieldInfo.FieldType);
				int size = GLHelper.GetVertexAttribPointerSizeForType(fieldInfo.FieldType);
				GL.VertexAttribPointer(index, size, type, true, Marshal.SizeOf(vertexType), Marshal.OffsetOf(vertexType, fieldInfo.Name));
				index++;
			}
		}

		~VertexBuffer()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteVertexArray(this.vertexArray);
			GL.DeleteBuffer(this.buffer);
		}

		internal void SetDataInternal(T[] data, int index, int count, uint usage)
		{
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);
			GL.BufferData(GL.ArrayBuffer, data, index, count, usage);
			this.Count = count;
		}
	}
}
