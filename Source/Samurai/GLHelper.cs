using System;

namespace Samurai
{
	public static class GLHelper
	{
		/// <summary>
		/// Decomposes a RGBA pixel into its individual components.
		/// </summary>
		/// <param name="pixel"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <param name="a"></param>
		public static void DecomposePixelRGBA(uint pixel, out byte r, out byte g, out byte b, out byte a)
		{
			r = (byte)((pixel & 0xFF000000) >> 24);
			g = (byte)((pixel & 0x00FF0000) >> 16);
			b = (byte)((pixel & 0x0000FF00) >> 8);
			a = (byte)((pixel & 0x000000FF));
		}

		/// <summary>
		/// Builds a representation of an RGBA pixel.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="a">The alpha component.</param>
		/// <returns></returns>
		public static uint MakePixelRGBA(byte r, byte g, byte b, byte a)
		{
			return ((uint)r << 24) + ((uint)g << 16) + ((uint)b << 8) + (uint)a;
		}

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
