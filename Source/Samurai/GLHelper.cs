using System;

namespace Samurai
{
	internal static class GLHelper
	{
		public static uint MakePixelRGBA(byte r, byte g, byte b, byte a)
		{
			return ((uint)r << 24) + ((uint)g << 16) + ((uint)b << 8) + (uint)a;
		}

		public static void DecomposePixelRGBA(uint pixel, out byte r, out byte g, out byte b, out byte a)
		{
			r = (byte)((pixel & 0xFF000000) >> 24);
			g = (byte)((pixel & 0x00FF0000) >> 16);
			b = (byte)((pixel & 0x0000FF00) >> 8);
			a = (byte)((pixel & 0x000000FF));
		}

		public static uint GetVertexAttribPointerTypeForType(Type type)
		{
			if (type == typeof(Color3) || type == typeof(Color4))
			{
				return GLContext.UnsignedByte;
			}
			else if (type == typeof(Vector2) || type == typeof(Vector3))
			{
				return GLContext.Float;
			}
			
			throw new SamuraiException(string.Format("Unable to determine GL type for .NET type {0}.", type));
		}

		public static int GetVertexAttribPointerSizeForType(Type type)
		{
			if (type == typeof(Vector2))
			{
				return 2;
			}
			else if (type == typeof(Color3) || type == typeof(Vector3))
			{
				return 3;
			}
			else if (type == typeof(Color4))
			{
				return 4;
			}

			throw new SamuraiException(string.Format("Unable to determine GL size for .NET type {0}.", type));
		}
	}
}
