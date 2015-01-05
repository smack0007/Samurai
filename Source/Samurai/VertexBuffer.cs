using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Samurai
{
	public abstract class VertexBuffer<T> : GraphicsObject
		where T : struct
	{
		internal uint vertexArray;
		internal uint buffer;

		public int Count
		{
			get;
			private set;
		}

		internal VertexBuffer(GraphicsContext graphics)
			: base(graphics)
		{
			this.vertexArray = this.Graphics.GL.GenVertexArray();
			this.Graphics.GL.BindVertexArray(this.vertexArray);

			this.buffer = this.Graphics.GL.GenBuffer();
			this.Graphics.GL.BindBuffer(GLContext.ArrayBuffer, this.buffer);

			Type vertexType = typeof(T);

			uint index = 0;
			foreach (var fieldInfo in vertexType.GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				this.Graphics.GL.EnableVertexAttribArray(index);
				uint type = GLHelper.GetVertexAttribPointerTypeForType(fieldInfo.FieldType);
				int size = GLHelper.GetVertexAttribPointerSizeForType(fieldInfo.FieldType);
				this.Graphics.GL.VertexAttribPointer(index, size, type, true, Marshal.SizeOf(vertexType), Marshal.OffsetOf(vertexType, fieldInfo.Name));
				index++;
			}
		}

		~VertexBuffer()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			this.Graphics.GL.DeleteVertexArray(this.vertexArray);
			this.Graphics.GL.DeleteBuffer(this.buffer);
		}

		internal void SetDataInternal(T[] data, int index, int count, uint usage)
		{
			this.Graphics.GL.BindBuffer(GLContext.ArrayBuffer, this.buffer);
			this.Graphics.GL.BufferData(GLContext.ArrayBuffer, data, index, count, usage);
			this.Count = count;
		}
	}
}
