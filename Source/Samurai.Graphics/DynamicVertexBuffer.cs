using System;

namespace Samurai.Graphics
{
	public class DynamicVertexBuffer : VertexBuffer
	{
		public DynamicVertexBuffer(GraphicsContext graphics, VertexElement[] vertexElements)
			: base(graphics)
		{
			if (vertexElements == null)
				throw new ArgumentNullException("vertexElements");

			this.SetVertexDescription(vertexElements);
		}

		public void SetData(DataBuffer data)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			this.SetDataInternal(data, 0, data.Length, GLContext.DynamicDraw);
		}
	}

	/// <summary>
	/// A VertexBuffer which can be written to dynamically.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DynamicVertexBuffer<T> : VertexBuffer<T>
		where T : struct
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="graphics">A handle to the GraphicsContext.</param>
		public DynamicVertexBuffer(GraphicsContext graphics)
			: base(graphics)
		{
		}

		/// <summary>
		/// Sets the VertexBuffer data.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void SetData(T[] data)
		{
			this.SetDataInternal(data, 0, data.Length, GLContext.DynamicDraw);
		}
		
		/// <summary>
		/// Sets the VertexBuffer data to a portion of the array.
		/// </summary>
		/// <param name="data">The data to write.</param>
		/// <param name="startIndex">Index of the first element to set.</param>
		/// <param name="length">The number of elements to set.</param>
		public void SetData(T[] data, int startIndex, int length)
		{
			this.SetDataInternal(data, startIndex, length, GLContext.DynamicDraw);
		}
	}
}
