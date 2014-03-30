using System;

namespace Samurai
{
	public static class GLUtils
	{
		public static uint GetVertexAttribPointerTypeForType(Type type)
		{
			if (type == typeof(byte))
			{
				return GL.UnsignedByte;
			}
			else if (type == typeof(double))
			{
				return GL.Double;
			}
			else if (type == typeof(float) ||
					 type == typeof(Color3) || type == typeof(Color4) ||
					 type == typeof(Vector2) || type == typeof(Vector3))
			{
				return GL.Float;
			}
			else if (type == typeof(int))
			{
				return GL.Int;
			}
			else if (type == typeof(sbyte))
			{
				return GL.Byte;
			}
			else if (type == typeof(short))
			{
				return GL.Short;
			}
			else if (type == typeof(uint))
			{
				return GL.UnsignedInt;
			}
			else if (type == typeof(ushort))
			{
				return GL.UnsignedShort;
			}

			throw new SamuraiException(string.Format("Unable to determine GL type for .NET type {0}.", type));
		}
	}
}
