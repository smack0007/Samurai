using System;

namespace Samurai.Graphics
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
	}
}
