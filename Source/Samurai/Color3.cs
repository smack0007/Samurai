using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Color3
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Color3));

		#region Static Colors

		public static readonly Color3 AliceBlue = new Color3(0xF0 / 255.0f, 0xF8 / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 AntiqueWhite = new Color3(0xFA / 255.0f, 0xEB / 255.0f, 0xD7 / 255.0f);
		public static readonly Color3 Aqua = new Color3(0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 Aquamarine = new Color3(0x7F / 255.0f, 0xFF / 255.0f, 0xD4 / 255.0f);
		public static readonly Color3 Azure = new Color3(0xF0 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 Beige = new Color3(0xF5 / 255.0f, 0xF5 / 255.0f, 0xDC / 255.0f);
		public static readonly Color3 Bisque = new Color3(0xFF / 255.0f, 0xE4 / 255.0f, 0xC4 / 255.0f);
		public static readonly Color3 Black = new Color3(0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 BlanchedAlmond = new Color3(0xFF / 255.0f, 0xEB / 255.0f, 0xCD / 255.0f);
		public static readonly Color3 Blue = new Color3(0x00 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 BlueViolet = new Color3(0x8A / 255.0f, 0x2B / 255.0f, 0xE2 / 255.0f);
		public static readonly Color3 Brown = new Color3(0xA5 / 255.0f, 0x2A / 255.0f, 0x2A / 255.0f);
		public static readonly Color3 BurlyWood = new Color3(0xDE / 255.0f, 0xB8 / 255.0f, 0x87 / 255.0f);
		public static readonly Color3 CadetBlue = new Color3(0x5F / 255.0f, 0x9E / 255.0f, 0xA0 / 255.0f);
		public static readonly Color3 Chartreuse = new Color3(0x7F / 255.0f, 0xFF / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 Chocolate = new Color3(0xD2 / 255.0f, 0x69 / 255.0f, 0x1E / 255.0f);
		public static readonly Color3 Coral = new Color3(0xFF / 255.0f, 0x7F / 255.0f, 0x50 / 255.0f);
		public static readonly Color3 CornflowerBlue = new Color3(0x64 / 255.0f, 0x95 / 255.0f, 0xED / 255.0f);
		public static readonly Color3 Cornsilk = new Color3(0xFF / 255.0f, 0xF8 / 255.0f, 0xDC / 255.0f);
		public static readonly Color3 Crimson = new Color3(0xDC / 255.0f, 0x14 / 255.0f, 0x3C / 255.0f);
		public static readonly Color3 Cyan = new Color3(0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 DarkBlue = new Color3(0x00 / 255.0f, 0x00 / 255.0f, 0x8B / 255.0f);
		public static readonly Color3 DarkCyan = new Color3(0x00 / 255.0f, 0x8B / 255.0f, 0x8B / 255.0f);
		public static readonly Color3 DarkGoldenRod = new Color3(0xB8 / 255.0f, 0x86 / 255.0f, 0x0B / 255.0f);
		public static readonly Color3 DarkGray = new Color3(0xA9 / 255.0f, 0xA9 / 255.0f, 0xA9 / 255.0f);
		public static readonly Color3 DarkGrey = new Color3(0xA9 / 255.0f, 0xA9 / 255.0f, 0xA9 / 255.0f);
		public static readonly Color3 DarkGreen = new Color3(0x00 / 255.0f, 0x64 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 DarkKhaki = new Color3(0xBD / 255.0f, 0xB7 / 255.0f, 0x6B / 255.0f);
		public static readonly Color3 DarkMagenta = new Color3(0x8B / 255.0f, 0x00 / 255.0f, 0x8B / 255.0f);
		public static readonly Color3 DarkOliveGreen = new Color3(0x55 / 255.0f, 0x6B / 255.0f, 0x2F / 255.0f);
		public static readonly Color3 Darkorange = new Color3(0xFF / 255.0f, 0x8C / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 DarkOrchid = new Color3(0x99 / 255.0f, 0x32 / 255.0f, 0xCC / 255.0f);
		public static readonly Color3 DarkRed = new Color3(0x8B / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 DarkSalmon = new Color3(0xE9 / 255.0f, 0x96 / 255.0f, 0x7A / 255.0f);
		public static readonly Color3 DarkSeaGreen = new Color3(0x8F / 255.0f, 0xBC / 255.0f, 0x8F / 255.0f);
		public static readonly Color3 DarkSlateBlue = new Color3(0x48 / 255.0f, 0x3D / 255.0f, 0x8B / 255.0f);
		public static readonly Color3 DarkSlateGray = new Color3(0x2F / 255.0f, 0x4F / 255.0f, 0x4F / 255.0f);
		public static readonly Color3 DarkSlateGrey = new Color3(0x2F / 255.0f, 0x4F / 255.0f, 0x4F / 255.0f);
		public static readonly Color3 DarkTurquoise = new Color3(0x00 / 255.0f, 0xCE / 255.0f, 0xD1 / 255.0f);
		public static readonly Color3 DarkViolet = new Color3(0x94 / 255.0f, 0x00 / 255.0f, 0xD3 / 255.0f);
		public static readonly Color3 DeepPink = new Color3(0xFF / 255.0f, 0x14 / 255.0f, 0x93 / 255.0f);
		public static readonly Color3 DeepSkyBlue = new Color3(0x00 / 255.0f, 0xBF / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 DimGray = new Color3(0x69 / 255.0f, 0x69 / 255.0f, 0x69 / 255.0f);
		public static readonly Color3 DimGrey = new Color3(0x69 / 255.0f, 0x69 / 255.0f, 0x69 / 255.0f);
		public static readonly Color3 DodgerBlue = new Color3(0x1E / 255.0f, 0x90 / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 FireBrick = new Color3(0xB2 / 255.0f, 0x22 / 255.0f, 0x22 / 255.0f);
		public static readonly Color3 FloralWhite = new Color3(0xFF / 255.0f, 0xFA / 255.0f, 0xF0 / 255.0f);
		public static readonly Color3 ForestGreen = new Color3(0x22 / 255.0f, 0x8B / 255.0f, 0x22 / 255.0f);
		public static readonly Color3 Fuchsia = new Color3(0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 Gainsboro = new Color3(0xDC / 255.0f, 0xDC / 255.0f, 0xDC / 255.0f);
		public static readonly Color3 GhostWhite = new Color3(0xF8 / 255.0f, 0xF8 / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 Gold = new Color3(0xFF / 255.0f, 0xD7 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 GoldenRod = new Color3(0xDA / 255.0f, 0xA5 / 255.0f, 0x20 / 255.0f);
		public static readonly Color3 Gray = new Color3(0x80 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f);
		public static readonly Color3 Grey = new Color3(0x80 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f);
		public static readonly Color3 Green = new Color3(0x00 / 255.0f, 0x80 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 GreenYellow = new Color3(0xAD / 255.0f, 0xFF / 255.0f, 0x2F / 255.0f);
		public static readonly Color3 HoneyDew = new Color3(0xF0 / 255.0f, 0xFF / 255.0f, 0xF0 / 255.0f);
		public static readonly Color3 HotPink = new Color3(0xFF / 255.0f, 0x69 / 255.0f, 0xB4 / 255.0f);
		public static readonly Color3 IndianRed = new Color3(0xCD / 255.0f, 0x5C / 255.0f, 0x5C / 255.0f);
		public static readonly Color3 Indigo = new Color3(0x4B / 255.0f, 0x00 / 255.0f, 0x82 / 255.0f);
		public static readonly Color3 Ivory = new Color3(0xFF / 255.0f, 0xFF / 255.0f, 0xF0 / 255.0f);
		public static readonly Color3 Khaki = new Color3(0xF0 / 255.0f, 0xE6 / 255.0f, 0x8C / 255.0f);
		public static readonly Color3 Lavender = new Color3(0xE6 / 255.0f, 0xE6 / 255.0f, 0xFA / 255.0f);
		public static readonly Color3 LavenderBlush = new Color3(0xFF / 255.0f, 0xF0 / 255.0f, 0xF5 / 255.0f);
		public static readonly Color3 LawnGreen = new Color3(0x7C / 255.0f, 0xFC / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 LemonChiffon = new Color3(0xFF / 255.0f, 0xFA / 255.0f, 0xCD / 255.0f);
		public static readonly Color3 LightBlue = new Color3(0xAD / 255.0f, 0xD8 / 255.0f, 0xE6 / 255.0f);
		public static readonly Color3 LightCoral = new Color3(0xF0 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f);
		public static readonly Color3 LightCyan = new Color3(0xE0 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 LightGoldenRodYellow = new Color3(0xFA / 255.0f, 0xFA / 255.0f, 0xD2 / 255.0f);
		public static readonly Color3 LightGray = new Color3(0xD3 / 255.0f, 0xD3 / 255.0f, 0xD3 / 255.0f);
		public static readonly Color3 LightGrey = new Color3(0xD3 / 255.0f, 0xD3 / 255.0f, 0xD3 / 255.0f);
		public static readonly Color3 LightGreen = new Color3(0x90 / 255.0f, 0xEE / 255.0f, 0x90 / 255.0f);
		public static readonly Color3 LightPink = new Color3(0xFF / 255.0f, 0xB6 / 255.0f, 0xC1 / 255.0f);
		public static readonly Color3 LightSalmon = new Color3(0xFF / 255.0f, 0xA0 / 255.0f, 0x7A / 255.0f);
		public static readonly Color3 LightSeaGreen = new Color3(0x20 / 255.0f, 0xB2 / 255.0f, 0xAA / 255.0f);
		public static readonly Color3 LightSkyBlue = new Color3(0x87 / 255.0f, 0xCE / 255.0f, 0xFA / 255.0f);
		public static readonly Color3 LightSlateGray = new Color3(0x77 / 255.0f, 0x88 / 255.0f, 0x99 / 255.0f);
		public static readonly Color3 LightSlateGrey = new Color3(0x77 / 255.0f, 0x88 / 255.0f, 0x99 / 255.0f);
		public static readonly Color3 LightSteelBlue = new Color3(0xB0 / 255.0f, 0xC4 / 255.0f, 0xDE / 255.0f);
		public static readonly Color3 LightYellow = new Color3(0xFF / 255.0f, 0xFF / 255.0f, 0xE0 / 255.0f);
		public static readonly Color3 Lime = new Color3(0x00 / 255.0f, 0xFF / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 LimeGreen = new Color3(0x32 / 255.0f, 0xCD / 255.0f, 0x32 / 255.0f);
		public static readonly Color3 Linen = new Color3(0xFA / 255.0f, 0xF0 / 255.0f, 0xE6 / 255.0f);
		public static readonly Color3 Magenta = new Color3(0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 Maroon = new Color3(0x80 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 MediumAquaMarine = new Color3(0x66 / 255.0f, 0xCD / 255.0f, 0xAA / 255.0f);
		public static readonly Color3 MediumBlue = new Color3(0x00 / 255.0f, 0x00 / 255.0f, 0xCD / 255.0f);
		public static readonly Color3 MediumOrchid = new Color3(0xBA / 255.0f, 0x55 / 255.0f, 0xD3 / 255.0f);
		public static readonly Color3 MediumPurple = new Color3(0x93 / 255.0f, 0x70 / 255.0f, 0xD8 / 255.0f);
		public static readonly Color3 MediumSeaGreen = new Color3(0x3C / 255.0f, 0xB3 / 255.0f, 0x71 / 255.0f);
		public static readonly Color3 MediumSlateBlue = new Color3(0x7B / 255.0f, 0x68 / 255.0f, 0xEE / 255.0f);
		public static readonly Color3 MediumSpringGreen = new Color3(0x00 / 255.0f, 0xFA / 255.0f, 0x9A / 255.0f);
		public static readonly Color3 MediumTurquoise = new Color3(0x48 / 255.0f, 0xD1 / 255.0f, 0xCC / 255.0f);
		public static readonly Color3 MediumVioletRed = new Color3(0xC7 / 255.0f, 0x15 / 255.0f, 0x85 / 255.0f);
		public static readonly Color3 MidnightBlue = new Color3(0x19 / 255.0f, 0x19 / 255.0f, 0x70 / 255.0f);
		public static readonly Color3 MintCream = new Color3(0xF5 / 255.0f, 0xFF / 255.0f, 0xFA / 255.0f);
		public static readonly Color3 MistyRose = new Color3(0xFF / 255.0f, 0xE4 / 255.0f, 0xE1 / 255.0f);
		public static readonly Color3 Moccasin = new Color3(0xFF / 255.0f, 0xE4 / 255.0f, 0xB5 / 255.0f);
		public static readonly Color3 NavajoWhite = new Color3(0xFF / 255.0f, 0xDE / 255.0f, 0xAD / 255.0f);
		public static readonly Color3 Navy = new Color3(0x00 / 255.0f, 0x00 / 255.0f, 0x80 / 255.0f);
		public static readonly Color3 OldLace = new Color3(0xFD / 255.0f, 0xF5 / 255.0f, 0xE6 / 255.0f);
		public static readonly Color3 Olive = new Color3(0x80 / 255.0f, 0x80 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 OliveDrab = new Color3(0x6B / 255.0f, 0x8E / 255.0f, 0x23 / 255.0f);
		public static readonly Color3 Orange = new Color3(0xFF / 255.0f, 0xA5 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 OrangeRed = new Color3(0xFF / 255.0f, 0x45 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 Orchid = new Color3(0xDA / 255.0f, 0x70 / 255.0f, 0xD6 / 255.0f);
		public static readonly Color3 PaleGoldenRod = new Color3(0xEE / 255.0f, 0xE8 / 255.0f, 0xAA / 255.0f);
		public static readonly Color3 PaleGreen = new Color3(0x98 / 255.0f, 0xFB / 255.0f, 0x98 / 255.0f);
		public static readonly Color3 PaleTurquoise = new Color3(0xAF / 255.0f, 0xEE / 255.0f, 0xEE / 255.0f);
		public static readonly Color3 PaleVioletRed = new Color3(0xD8 / 255.0f, 0x70 / 255.0f, 0x93 / 255.0f);
		public static readonly Color3 PapayaWhip = new Color3(0xFF / 255.0f, 0xEF / 255.0f, 0xD5 / 255.0f);
		public static readonly Color3 PeachPuff = new Color3(0xFF / 255.0f, 0xDA / 255.0f, 0xB9 / 255.0f);
		public static readonly Color3 Peru = new Color3(0xCD / 255.0f, 0x85 / 255.0f, 0x3F / 255.0f);
		public static readonly Color3 Pink = new Color3(0xFF / 255.0f, 0xC0 / 255.0f, 0xCB / 255.0f);
		public static readonly Color3 Plum = new Color3(0xDD / 255.0f, 0xA0 / 255.0f, 0xDD / 255.0f);
		public static readonly Color3 PowderBlue = new Color3(0xB0 / 255.0f, 0xE0 / 255.0f, 0xE6 / 255.0f);
		public static readonly Color3 Purple = new Color3(0x80 / 255.0f, 0x00 / 255.0f, 0x80 / 255.0f);
		public static readonly Color3 Red = new Color3(0xFF / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 RosyBrown = new Color3(0xBC / 255.0f, 0x8F / 255.0f, 0x8F / 255.0f);
		public static readonly Color3 RoyalBlue = new Color3(0x41 / 255.0f, 0x69 / 255.0f, 0xE1 / 255.0f);
		public static readonly Color3 SaddleBrown = new Color3(0x8B / 255.0f, 0x45 / 255.0f, 0x13 / 255.0f);
		public static readonly Color3 Salmon = new Color3(0xFA / 255.0f, 0x80 / 255.0f, 0x72 / 255.0f);
		public static readonly Color3 SandyBrown = new Color3(0xF4 / 255.0f, 0xA4 / 255.0f, 0x60 / 255.0f);
		public static readonly Color3 SeaGreen = new Color3(0x2E / 255.0f, 0x8B / 255.0f, 0x57 / 255.0f);
		public static readonly Color3 SeaShell = new Color3(0xFF / 255.0f, 0xF5 / 255.0f, 0xEE / 255.0f);
		public static readonly Color3 Sienna = new Color3(0xA0 / 255.0f, 0x52 / 255.0f, 0x2D / 255.0f);
		public static readonly Color3 Silver = new Color3(0xC0 / 255.0f, 0xC0 / 255.0f, 0xC0 / 255.0f);
		public static readonly Color3 SkyBlue = new Color3(0x87 / 255.0f, 0xCE / 255.0f, 0xEB / 255.0f);
		public static readonly Color3 SlateBlue = new Color3(0x6A / 255.0f, 0x5A / 255.0f, 0xCD / 255.0f);
		public static readonly Color3 SlateGray = new Color3(0x70 / 255.0f, 0x80 / 255.0f, 0x90 / 255.0f);
		public static readonly Color3 SlateGrey = new Color3(0x70 / 255.0f, 0x80 / 255.0f, 0x90 / 255.0f);
		public static readonly Color3 Snow = new Color3(0xFF / 255.0f, 0xFA / 255.0f, 0xFA / 255.0f);
		public static readonly Color3 SpringGreen = new Color3(0x00 / 255.0f, 0xFF / 255.0f, 0x7F / 255.0f);
		public static readonly Color3 SteelBlue = new Color3(0x46 / 255.0f, 0x82 / 255.0f, 0xB4 / 255.0f);
		public static readonly Color3 Tan = new Color3(0xD2 / 255.0f, 0xB4 / 255.0f, 0x8C / 255.0f);
		public static readonly Color3 Teal = new Color3(0x00 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f);
		public static readonly Color3 Thistle = new Color3(0xD8 / 255.0f, 0xBF / 255.0f, 0xD8 / 255.0f);
		public static readonly Color3 Tomato = new Color3(0xFF / 255.0f, 0x63 / 255.0f, 0x47 / 255.0f);
		public static readonly Color3 Transparent = new Color3(0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 Turquoise = new Color3(0x40 / 255.0f, 0xE0 / 255.0f, 0xD0 / 255.0f);
		public static readonly Color3 Violet = new Color3(0xEE / 255.0f, 0x82 / 255.0f, 0xEE / 255.0f);
		public static readonly Color3 Wheat = new Color3(0xF5 / 255.0f, 0xDE / 255.0f, 0xB3 / 255.0f);
		public static readonly Color3 White = new Color3(0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color3 WhiteSmoke = new Color3(0xF5 / 255.0f, 0xF5 / 255.0f, 0xF5 / 255.0f);
		public static readonly Color3 Yellow = new Color3(0xFF / 255.0f, 0xFF / 255.0f, 0x00 / 255.0f);
		public static readonly Color3 YellowGreen = new Color3(0x9A / 255.0f, 0xCD / 255.0f, 0x32 / 255.0f);

		#endregion

		/// <summary>
		/// The red value.
		/// </summary>
		public float R;

		/// <summary>
		/// The green value.
		/// </summary>
		public float G;

		/// <summary>
		/// The blue value.
		/// </summary>
		public float B;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public Color3(float r, float g, float b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}

		public override string ToString()
		{
			return "{ " + this.R + ", " + this.G + ", " + this.B + " }";
		}
	}
}
