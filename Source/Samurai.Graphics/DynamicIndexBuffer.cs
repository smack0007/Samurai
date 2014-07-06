using System;

namespace Samurai.Graphics
{
	/// <summary>
	/// An IndexBuffer which can be written to dynamically.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DynamicIndexBuffer<T> : IndexBuffer<T>
		where T : struct
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="graphics">Handle to the GraphicsContext.</param>
		public DynamicIndexBuffer(GraphicsContext graphics)
			: base(graphics)
		{
		}

		/// <summary>
		/// Sets the IndexBuffer data.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void SetData(T[] data)
		{
			this.SetDataInternal(data, 0, data.Length, GLContext.DynamicDraw);
		}

		/// <summary>
		/// Sets the IndexBuffer data to a portion of the array.
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
