using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Samurai
{
	public abstract class VertexBuffer<T> : DisposableObject
		where T : struct
	{
		GraphicsDevice graphicsDevice;

		internal uint vertexArray;
		internal uint buffer;

		public int Count
		{
			get;
			private set;
		}

		internal VertexBuffer(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			this.graphicsDevice = graphicsDevice;

			this.vertexArray = GL.GenVertexArray();
			GL.BindVertexArray(this.vertexArray);

			this.buffer = GL.GenBuffer();
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);

			Type vertexType = typeof(T);

			uint index = 0;
			foreach (var fieldInfo in vertexType.GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				GL.EnableVertexAttribArray(index);
				uint type = GLUtils.GetVertexAttribPointerTypeForType(fieldInfo.FieldType);
				GL.VertexAttribPointer(index, Marshal.SizeOf(fieldInfo.FieldType) / 4, type, true, Marshal.SizeOf(vertexType), Marshal.OffsetOf(vertexType, fieldInfo.Name));
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

		internal void SetDataInternal(T[] data, uint usage)
		{
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);
			GL.BufferData(GL.ArrayBuffer, data, usage);
			this.Count = data.Length;
		}
	}
}
