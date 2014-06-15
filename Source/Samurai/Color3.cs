using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Color3 : IEquatable<Color3>
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Color3));

		#region Static Colors

		public static readonly Color3 AliceBlue = new Color3(0xF0, 0xF8, 0xFF);
		public static readonly Color3 AntiqueWhite = new Color3(0xFA, 0xEB, 0xD7);
		public static readonly Color3 Aqua = new Color3(0x00, 0xFF, 0xFF);
		public static readonly Color3 Aquamarine = new Color3(0x7F, 0xFF, 0xD4);
		public static readonly Color3 Azure = new Color3(0xF0, 0xFF, 0xFF);
		public static readonly Color3 Beige = new Color3(0xF5, 0xF5, 0xDC);
		public static readonly Color3 Bisque = new Color3(0xFF, 0xE4, 0xC4);
		public static readonly Color3 Black = new Color3(0x00, 0x00, 0x00);
		public static readonly Color3 BlanchedAlmond = new Color3(0xFF, 0xEB, 0xCD);
		public static readonly Color3 Blue = new Color3(0x00, 0x00, 0xFF);
		public static readonly Color3 BlueViolet = new Color3(0x8A, 0x2B, 0xE2);
		public static readonly Color3 Brown = new Color3(0xA5, 0x2A, 0x2A);
		public static readonly Color3 BurlyWood = new Color3(0xDE, 0xB8, 0x87);
		public static readonly Color3 CadetBlue = new Color3(0x5F, 0x9E, 0xA0);
		public static readonly Color3 Chartreuse = new Color3(0x7F, 0xFF, 0x00);
		public static readonly Color3 Chocolate = new Color3(0xD2, 0x69, 0x1E);
		public static readonly Color3 Coral = new Color3(0xFF, 0x7F, 0x50);
		public static readonly Color3 CornflowerBlue = new Color3(0x64, 0x95, 0xED);
		public static readonly Color3 Cornsilk = new Color3(0xFF, 0xF8, 0xDC);
		public static readonly Color3 Crimson = new Color3(0xDC, 0x14, 0x3C);
		public static readonly Color3 Cyan = new Color3(0x00, 0xFF, 0xFF);
		public static readonly Color3 DarkBlue = new Color3(0x00, 0x00, 0x8B);
		public static readonly Color3 DarkCyan = new Color3(0x00, 0x8B, 0x8B);
		public static readonly Color3 DarkGoldenRod = new Color3(0xB8, 0x86, 0x0B);
		public static readonly Color3 DarkGray = new Color3(0xA9, 0xA9, 0xA9);
		public static readonly Color3 DarkGrey = new Color3(0xA9, 0xA9, 0xA9);
		public static readonly Color3 DarkGreen = new Color3(0x00, 0x64, 0x00);
		public static readonly Color3 DarkKhaki = new Color3(0xBD, 0xB7, 0x6B);
		public static readonly Color3 DarkMagenta = new Color3(0x8B, 0x00, 0x8B);
		public static readonly Color3 DarkOliveGreen = new Color3(0x55, 0x6B, 0x2F);
		public static readonly Color3 Darkorange = new Color3(0xFF, 0x8C, 0x00);
		public static readonly Color3 DarkOrchid = new Color3(0x99, 0x32, 0xCC);
		public static readonly Color3 DarkRed = new Color3(0x8B, 0x00, 0x00);
		public static readonly Color3 DarkSalmon = new Color3(0xE9, 0x96, 0x7A);
		public static readonly Color3 DarkSeaGreen = new Color3(0x8F, 0xBC, 0x8F);
		public static readonly Color3 DarkSlateBlue = new Color3(0x48, 0x3D, 0x8B);
		public static readonly Color3 DarkSlateGray = new Color3(0x2F, 0x4F, 0x4F);
		public static readonly Color3 DarkSlateGrey = new Color3(0x2F, 0x4F, 0x4F);
		public static readonly Color3 DarkTurquoise = new Color3(0x00, 0xCE, 0xD1);
		public static readonly Color3 DarkViolet = new Color3(0x94, 0x00, 0xD3);
		public static readonly Color3 DeepPink = new Color3(0xFF, 0x14, 0x93);
		public static readonly Color3 DeepSkyBlue = new Color3(0x00, 0xBF, 0xFF);
		public static readonly Color3 DimGray = new Color3(0x69, 0x69, 0x69);
		public static readonly Color3 DimGrey = new Color3(0x69, 0x69, 0x69);
		public static readonly Color3 DodgerBlue = new Color3(0x1E, 0x90, 0xFF);
		public static readonly Color3 FireBrick = new Color3(0xB2, 0x22, 0x22);
		public static readonly Color3 FloralWhite = new Color3(0xFF, 0xFA, 0xF0);
		public static readonly Color3 ForestGreen = new Color3(0x22, 0x8B, 0x22);
		public static readonly Color3 Fuchsia = new Color3(0xFF, 0x00, 0xFF);
		public static readonly Color3 Gainsboro = new Color3(0xDC, 0xDC, 0xDC);
		public static readonly Color3 GhostWhite = new Color3(0xF8, 0xF8, 0xFF);
		public static readonly Color3 Gold = new Color3(0xFF, 0xD7, 0x00);
		public static readonly Color3 GoldenRod = new Color3(0xDA, 0xA5, 0x20);
		public static readonly Color3 Gray = new Color3(0x80, 0x80, 0x80);
		public static readonly Color3 Grey = new Color3(0x80, 0x80, 0x80);
		public static readonly Color3 Green = new Color3(0x00, 0x80, 0x00);
		public static readonly Color3 GreenYellow = new Color3(0xAD, 0xFF, 0x2F);
		public static readonly Color3 HoneyDew = new Color3(0xF0, 0xFF, 0xF0);
		public static readonly Color3 HotPink = new Color3(0xFF, 0x69, 0xB4);
		public static readonly Color3 IndianRed = new Color3(0xCD, 0x5C, 0x5C);
		public static readonly Color3 Indigo = new Color3(0x4B, 0x00, 0x82);
		public static readonly Color3 Ivory = new Color3(0xFF, 0xFF, 0xF0);
		public static readonly Color3 Khaki = new Color3(0xF0, 0xE6, 0x8C);
		public static readonly Color3 Lavender = new Color3(0xE6, 0xE6, 0xFA);
		public static readonly Color3 LavenderBlush = new Color3(0xFF, 0xF0, 0xF5);
		public static readonly Color3 LawnGreen = new Color3(0x7C, 0xFC, 0x00);
		public static readonly Color3 LemonChiffon = new Color3(0xFF, 0xFA, 0xCD);
		public static readonly Color3 LightBlue = new Color3(0xAD, 0xD8, 0xE6);
		public static readonly Color3 LightCoral = new Color3(0xF0, 0x80, 0x80);
		public static readonly Color3 LightCyan = new Color3(0xE0, 0xFF, 0xFF);
		public static readonly Color3 LightGoldenRodYellow = new Color3(0xFA, 0xFA, 0xD2);
		public static readonly Color3 LightGray = new Color3(0xD3, 0xD3, 0xD3);
		public static readonly Color3 LightGrey = new Color3(0xD3, 0xD3, 0xD3);
		public static readonly Color3 LightGreen = new Color3(0x90, 0xEE, 0x90);
		public static readonly Color3 LightPink = new Color3(0xFF, 0xB6, 0xC1);
		public static readonly Color3 LightSalmon = new Color3(0xFF, 0xA0, 0x7A);
		public static readonly Color3 LightSeaGreen = new Color3(0x20, 0xB2, 0xAA);
		public static readonly Color3 LightSkyBlue = new Color3(0x87, 0xCE, 0xFA);
		public static readonly Color3 LightSlateGray = new Color3(0x77, 0x88, 0x99);
		public static readonly Color3 LightSlateGrey = new Color3(0x77, 0x88, 0x99);
		public static readonly Color3 LightSteelBlue = new Color3(0xB0, 0xC4, 0xDE);
		public static readonly Color3 LightYellow = new Color3(0xFF, 0xFF, 0xE0);
		public static readonly Color3 Lime = new Color3(0x00, 0xFF, 0x00);
		public static readonly Color3 LimeGreen = new Color3(0x32, 0xCD, 0x32);
		public static readonly Color3 Linen = new Color3(0xFA, 0xF0, 0xE6);
		public static readonly Color3 Magenta = new Color3(0xFF, 0x00, 0xFF);
		public static readonly Color3 Maroon = new Color3(0x80, 0x00, 0x00);
		public static readonly Color3 MediumAquaMarine = new Color3(0x66, 0xCD, 0xAA);
		public static readonly Color3 MediumBlue = new Color3(0x00, 0x00, 0xCD);
		public static readonly Color3 MediumOrchid = new Color3(0xBA, 0x55, 0xD3);
		public static readonly Color3 MediumPurple = new Color3(0x93, 0x70, 0xD8);
		public static readonly Color3 MediumSeaGreen = new Color3(0x3C, 0xB3, 0x71);
		public static readonly Color3 MediumSlateBlue = new Color3(0x7B, 0x68, 0xEE);
		public static readonly Color3 MediumSpringGreen = new Color3(0x00, 0xFA, 0x9A);
		public static readonly Color3 MediumTurquoise = new Color3(0x48, 0xD1, 0xCC);
		public static readonly Color3 MediumVioletRed = new Color3(0xC7, 0x15, 0x85);
		public static readonly Color3 MidnightBlue = new Color3(0x19, 0x19, 0x70);
		public static readonly Color3 MintCream = new Color3(0xF5, 0xFF, 0xFA);
		public static readonly Color3 MistyRose = new Color3(0xFF, 0xE4, 0xE1);
		public static readonly Color3 Moccasin = new Color3(0xFF, 0xE4, 0xB5);
		public static readonly Color3 NavajoWhite = new Color3(0xFF, 0xDE, 0xAD);
		public static readonly Color3 Navy = new Color3(0x00, 0x00, 0x80);
		public static readonly Color3 OldLace = new Color3(0xFD, 0xF5, 0xE6);
		public static readonly Color3 Olive = new Color3(0x80, 0x80, 0x00);
		public static readonly Color3 OliveDrab = new Color3(0x6B, 0x8E, 0x23);
		public static readonly Color3 Orange = new Color3(0xFF, 0xA5, 0x00);
		public static readonly Color3 OrangeRed = new Color3(0xFF, 0x45, 0x00);
		public static readonly Color3 Orchid = new Color3(0xDA, 0x70, 0xD6);
		public static readonly Color3 PaleGoldenRod = new Color3(0xEE, 0xE8, 0xAA);
		public static readonly Color3 PaleGreen = new Color3(0x98, 0xFB, 0x98);
		public static readonly Color3 PaleTurquoise = new Color3(0xAF, 0xEE, 0xEE);
		public static readonly Color3 PaleVioletRed = new Color3(0xD8, 0x70, 0x93);
		public static readonly Color3 PapayaWhip = new Color3(0xFF, 0xEF, 0xD5);
		public static readonly Color3 PeachPuff = new Color3(0xFF, 0xDA, 0xB9);
		public static readonly Color3 Peru = new Color3(0xCD, 0x85, 0x3F);
		public static readonly Color3 Pink = new Color3(0xFF, 0xC0, 0xCB);
		public static readonly Color3 Plum = new Color3(0xDD, 0xA0, 0xDD);
		public static readonly Color3 PowderBlue = new Color3(0xB0, 0xE0, 0xE6);
		public static readonly Color3 Purple = new Color3(0x80, 0x00, 0x80);
		public static readonly Color3 Red = new Color3(0xFF, 0x00, 0x00);
		public static readonly Color3 RosyBrown = new Color3(0xBC, 0x8F, 0x8F);
		public static readonly Color3 RoyalBlue = new Color3(0x41, 0x69, 0xE1);
		public static readonly Color3 SaddleBrown = new Color3(0x8B, 0x45, 0x13);
		public static readonly Color3 Salmon = new Color3(0xFA, 0x80, 0x72);
		public static readonly Color3 SandyBrown = new Color3(0xF4, 0xA4, 0x60);
		public static readonly Color3 SeaGreen = new Color3(0x2E, 0x8B, 0x57);
		public static readonly Color3 SeaShell = new Color3(0xFF, 0xF5, 0xEE);
		public static readonly Color3 Sienna = new Color3(0xA0, 0x52, 0x2D);
		public static readonly Color3 Silver = new Color3(0xC0, 0xC0, 0xC0);
		public static readonly Color3 SkyBlue = new Color3(0x87, 0xCE, 0xEB);
		public static readonly Color3 SlateBlue = new Color3(0x6A, 0x5A, 0xCD);
		public static readonly Color3 SlateGray = new Color3(0x70, 0x80, 0x90);
		public static readonly Color3 SlateGrey = new Color3(0x70, 0x80, 0x90);
		public static readonly Color3 Snow = new Color3(0xFF, 0xFA, 0xFA);
		public static readonly Color3 SpringGreen = new Color3(0x00, 0xFF, 0x7F);
		public static readonly Color3 SteelBlue = new Color3(0x46, 0x82, 0xB4);
		public static readonly Color3 Tan = new Color3(0xD2, 0xB4, 0x8C);
		public static readonly Color3 Teal = new Color3(0x00, 0x80, 0x80);
		public static readonly Color3 Thistle = new Color3(0xD8, 0xBF, 0xD8);
		public static readonly Color3 Tomato = new Color3(0xFF, 0x63, 0x47);
		public static readonly Color3 Transparent = new Color3(0x00, 0x00, 0x00);
		public static readonly Color3 Turquoise = new Color3(0x40, 0xE0, 0xD0);
		public static readonly Color3 Violet = new Color3(0xEE, 0x82, 0xEE);
		public static readonly Color3 Wheat = new Color3(0xF5, 0xDE, 0xB3);
		public static readonly Color3 White = new Color3(0xFF, 0xFF, 0xFF);
		public static readonly Color3 WhiteSmoke = new Color3(0xF5, 0xF5, 0xF5);
		public static readonly Color3 Yellow = new Color3(0xFF, 0xFF, 0x00);
		public static readonly Color3 YellowGreen = new Color3(0x9A, 0xCD, 0x32);

		#endregion

		/// <summary>
		/// The red value.
		/// </summary>
		public byte R;

		/// <summary>
		/// The green value.
		/// </summary>
		public byte G;

		/// <summary>
		/// The blue value.
		/// </summary>
		public byte B;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public Color3(byte r, byte g, byte b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}

		public override string ToString()
		{
			return "{ " + this.R + ", " + this.G + ", " + this.B + " }";
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Color3))
				return false;

			return this.Equals((Color3)obj);
		}

		public bool Equals(Color3 other)
		{
			return this.R == other.R &&
				   this.G == other.G &&
				   this.B == other.B;
		}
	}
}
