using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Samurai.Graphics
{
	public abstract class VertexBuffer : GraphicsObject
	{
		internal uint vertexArray;
		internal uint buffer;

		public int Length
		{
			get;
			protected set;
		}

		internal VertexBuffer(GraphicsContext graphics)
			: base(graphics)
		{
			this.vertexArray = this.Graphics.GL.GenVertexArray();
			this.Graphics.GL.BindVertexArray(this.vertexArray);

			this.buffer = this.Graphics.GL.GenBuffer();
			this.Graphics.GL.BindBuffer(GLContext.ArrayBuffer, this.buffer);
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

		private static int GetSizeInBytes(VertexElementType type)
		{
			switch (type)
			{
				case VertexElementType.Double:
					return 8;

				case VertexElementType.Float:
				case VertexElementType.Int:
				case VertexElementType.UnsignedInt:
					return 4;

				case VertexElementType.Short:
					return 2;
					
				case VertexElementType.UnsignedByte:
					return 1;
			}

			throw new SamuraiException(string.Format("Unable to determine size in bytes for VertexElementType {0}.", type));
		}

		internal void SetVertexDescription(VertexElement[] elements)
		{			
			int vertexSize = 0;
			uint offset = 0;

			for (int i = 0; i < elements.Length; i++)
			{
				vertexSize += elements[i].Size * GetSizeInBytes(elements[i].Type);
			}

			for (int i = 0; i < elements.Length; i++)
			{
				this.Graphics.GL.EnableVertexAttribArray((uint)i);
				this.Graphics.GL.VertexAttribPointer((uint)i, elements[i].Size, (uint)elements[i].Type, true, vertexSize, (IntPtr)offset);

				offset += (uint)(elements[i].Size * GetSizeInBytes(elements[i].Type));
			}
		}

		internal void SetDataInternal(DataBuffer data, int offset, int length, uint usage)
		{
			this.Graphics.GL.BindBuffer(GLContext.ArrayBuffer, this.buffer);
			this.Graphics.GL.BufferData(GLContext.ArrayBuffer, data.DataPointer, offset, length, usage);
			this.Length = length;
		}
	}

	public abstract class VertexBuffer<T> : VertexBuffer
		where T : struct
	{
		internal VertexBuffer(GraphicsContext graphics)
			: base(graphics)
		{			
			Type vertexType = typeof(T);

			var fields = vertexType.GetFields(BindingFlags.Public | BindingFlags.Instance);
			var vertexElements = new VertexElement[fields.Length];

			int i = 0;
			foreach (var field in fields.OrderBy(x => x.MetadataToken))
			{
				if (field.FieldType == typeof(Color3) || field.FieldType == typeof(Color4))
				{
					vertexElements[i].Type = VertexElementType.UnsignedByte;
				}
				else if (field.FieldType == typeof(Vector2) || field.FieldType == typeof(Vector3))
				{
					vertexElements[i].Type = VertexElementType.Float;
				}
				else
				{
					throw new SamuraiException(string.Format("Unable to determine VertexElementType for .NET type {0}.", field.FieldType));
				}

				if (field.FieldType == typeof(Vector2))
				{
					vertexElements[i].Size = 2;
				}
				else if (field.FieldType == typeof(Color3) || field.FieldType == typeof(Vector3))
				{
					vertexElements[i].Size = 3;
				}
				else if (field.FieldType == typeof(Color4))
				{
					vertexElements[i].Size = 4;
				}
				else
				{
					throw new SamuraiException(string.Format("Unable to determine size of VertexElement for .NET type {0}.", field.FieldType));
				}
				
				i++;
			}

			this.SetVertexDescription(vertexElements);
		}
				
		internal void SetDataInternal(T[] data, int index, int count, uint usage)
		{
			this.Graphics.GL.BindBuffer(GLContext.ArrayBuffer, this.buffer);
			this.Graphics.GL.BufferData(GLContext.ArrayBuffer, data, index, count, usage);
			this.Length = count;
		}
	}
}
