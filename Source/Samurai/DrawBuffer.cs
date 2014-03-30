using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Samurai
{
	public class DrawBuffer<T> : DisposableObject
		where T : struct
	{
		GraphicsDevice graphicsDevice;

		uint vertexArray;
		uint buffer;

		public int VertexCount
		{
			get;
			private set;
		}

		public DrawBuffer(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
				throw new ArgumentNullException("graphicsDevice");

			this.graphicsDevice = graphicsDevice;

			this.vertexArray = GL.GenVertexArray();
			GL.BindVertexArray(this.vertexArray);

			this.buffer = GL.GenBuffer();
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);

			uint index = 0;

			Type vertexType = typeof(T);
			foreach (var fieldInfo in vertexType.GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				GL.EnableVertexAttribArray(index);
				uint type = GLUtils.GetVertexAttribPointerTypeForType(fieldInfo.FieldType);
				GL.VertexAttribPointer(index, Marshal.SizeOf(fieldInfo.FieldType) / 4, type, true, Marshal.SizeOf(vertexType), Marshal.OffsetOf(vertexType, fieldInfo.Name));
				index++;
			}
		}

		~DrawBuffer()
		{
			this.Dispose(false);
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteVertexArray(this.vertexArray);
			GL.DeleteBuffer(this.buffer);
		}

		public void SetData(T[] data)
		{
			GL.BindBuffer(GL.ArrayBuffer, this.buffer);
			GL.BufferData(GL.ArrayBuffer, data, GL.DynamicDraw);

			this.VertexCount = data.Length;
		}
	}
}
