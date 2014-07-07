using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	/// <summary>
	/// Represents an RGBA color.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Color4 : IEquatable<Color4>
	{
		/// <summary>
		/// Size of Color4 in bytes.
		/// </summary>
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Color4));

		#region Static Colors

		public static readonly Color4 AliceBlue = new Color4(0xF0, 0xF8, 0xFF, 0xFF);
		public static readonly Color4 AntiqueWhite = new Color4(0xFA, 0xEB, 0xD7, 0xFF);
		public static readonly Color4 Aqua = new Color4(0x00, 0xFF, 0xFF, 0xFF);
		public static readonly Color4 Aquamarine = new Color4(0x7F, 0xFF, 0xD4, 0xFF);
		public static readonly Color4 Azure = new Color4(0xF0, 0xFF, 0xFF, 0xFF);
		public static readonly Color4 Beige = new Color4(0xF5, 0xF5, 0xDC, 0xFF);
		public static readonly Color4 Bisque = new Color4(0xFF, 0xE4, 0xC4, 0xFF);
		public static readonly Color4 Black = new Color4(0x00, 0x00, 0x00, 0xFF);
		public static readonly Color4 BlanchedAlmond = new Color4(0xFF, 0xEB, 0xCD, 0xFF);
		public static readonly Color4 Blue = new Color4(0x00, 0x00, 0xFF, 0xFF);
		public static readonly Color4 BlueViolet = new Color4(0x8A, 0x2B, 0xE2, 0xFF);
		public static readonly Color4 Brown = new Color4(0xA5, 0x2A, 0x2A, 0xFF);
		public static readonly Color4 BurlyWood = new Color4(0xDE, 0xB8, 0x87, 0xFF);
		public static readonly Color4 CadetBlue = new Color4(0x5F, 0x9E, 0xA0, 0xFF);
		public static readonly Color4 Chartreuse = new Color4(0x7F, 0xFF, 0x00, 0xFF);
		public static readonly Color4 Chocolate = new Color4(0xD2, 0x69, 0x1E, 0xFF);
		public static readonly Color4 Coral = new Color4(0xFF, 0x7F, 0x50, 0xFF);
		public static readonly Color4 CornflowerBlue = new Color4(0x64, 0x95, 0xED, 0xFF);
		public static readonly Color4 Cornsilk = new Color4(0xFF, 0xF8, 0xDC, 0xFF);
		public static readonly Color4 Crimson = new Color4(0xDC, 0x14, 0x3C, 0xFF);
		public static readonly Color4 Cyan = new Color4(0x00, 0xFF, 0xFF, 0xFF);
		public static readonly Color4 DarkBlue = new Color4(0x00, 0x00, 0x8B, 0xFF);
		public static readonly Color4 DarkCyan = new Color4(0x00, 0x8B, 0x8B, 0xFF);
		public static readonly Color4 DarkGoldenRod = new Color4(0xB8, 0x86, 0x0B, 0xFF);
		public static readonly Color4 DarkGray = new Color4(0xA9, 0xA9, 0xA9, 0xFF);
		public static readonly Color4 DarkGrey = new Color4(0xA9, 0xA9, 0xA9, 0xFF);
		public static readonly Color4 DarkGreen = new Color4(0x00, 0x64, 0x00, 0xFF);
		public static readonly Color4 DarkKhaki = new Color4(0xBD, 0xB7, 0x6B, 0xFF);
		public static readonly Color4 DarkMagenta = new Color4(0x8B, 0x00, 0x8B, 0xFF);
		public static readonly Color4 DarkOliveGreen = new Color4(0x55, 0x6B, 0x2F, 0xFF);
		public static readonly Color4 Darkorange = new Color4(0xFF, 0x8C, 0x00, 0xFF);
		public static readonly Color4 DarkOrchid = new Color4(0x99, 0x32, 0xCC, 0xFF);
		public static readonly Color4 DarkRed = new Color4(0x8B, 0x00, 0x00, 0xFF);
		public static readonly Color4 DarkSalmon = new Color4(0xE9, 0x96, 0x7A, 0xFF);
		public static readonly Color4 DarkSeaGreen = new Color4(0x8F, 0xBC, 0x8F, 0xFF);
		public static readonly Color4 DarkSlateBlue = new Color4(0x48, 0x3D, 0x8B, 0xFF);
		public static readonly Color4 DarkSlateGray = new Color4(0x2F, 0x4F, 0x4F, 0xFF);
		public static readonly Color4 DarkSlateGrey = new Color4(0x2F, 0x4F, 0x4F, 0xFF);
		public static readonly Color4 DarkTurquoise = new Color4(0x00, 0xCE, 0xD1, 0xFF);
		public static readonly Color4 DarkViolet = new Color4(0x94, 0x00, 0xD3, 0xFF);
		public static readonly Color4 DeepPink = new Color4(0xFF, 0x14, 0x93, 0xFF);
		public static readonly Color4 DeepSkyBlue = new Color4(0x00, 0xBF, 0xFF, 0xFF);
		public static readonly Color4 DimGray = new Color4(0x69, 0x69, 0x69, 0xFF);
		public static readonly Color4 DimGrey = new Color4(0x69, 0x69, 0x69, 0xFF);
		public static readonly Color4 DodgerBlue = new Color4(0x1E, 0x90, 0xFF, 0xFF);
		public static readonly Color4 FireBrick = new Color4(0xB2, 0x22, 0x22, 0xFF);
		public static readonly Color4 FloralWhite = new Color4(0xFF, 0xFA, 0xF0, 0xFF);
		public static readonly Color4 ForestGreen = new Color4(0x22, 0x8B, 0x22, 0xFF);
		public static readonly Color4 Fuchsia = new Color4(0xFF, 0x00, 0xFF, 0xFF);
		public static readonly Color4 Gainsboro = new Color4(0xDC, 0xDC, 0xDC, 0xFF);
		public static readonly Color4 GhostWhite = new Color4(0xF8, 0xF8, 0xFF, 0xFF);
		public static readonly Color4 Gold = new Color4(0xFF, 0xD7, 0x00, 0xFF);
		public static readonly Color4 GoldenRod = new Color4(0xDA, 0xA5, 0x20, 0xFF);
		public static readonly Color4 Gray = new Color4(0x80, 0x80, 0x80, 0xFF);
		public static readonly Color4 Grey = new Color4(0x80, 0x80, 0x80, 0xFF);
		public static readonly Color4 Green = new Color4(0x00, 0x80, 0x00, 0xFF);
		public static readonly Color4 GreenYellow = new Color4(0xAD, 0xFF, 0x2F, 0xFF);
		public static readonly Color4 HoneyDew = new Color4(0xF0, 0xFF, 0xF0, 0xFF);
		public static readonly Color4 HotPink = new Color4(0xFF, 0x69, 0xB4, 0xFF);
		public static readonly Color4 IndianRed = new Color4(0xCD, 0x5C, 0x5C, 0xFF);
		public static readonly Color4 Indigo = new Color4(0x4B, 0x00, 0x82, 0xFF);
		public static readonly Color4 Ivory = new Color4(0xFF, 0xFF, 0xF0, 0xFF);
		public static readonly Color4 Khaki = new Color4(0xF0, 0xE6, 0x8C, 0xFF);
		public static readonly Color4 Lavender = new Color4(0xE6, 0xE6, 0xFA, 0xFF);
		public static readonly Color4 LavenderBlush = new Color4(0xFF, 0xF0, 0xF5, 0xFF);
		public static readonly Color4 LawnGreen = new Color4(0x7C, 0xFC, 0x00, 0xFF);
		public static readonly Color4 LemonChiffon = new Color4(0xFF, 0xFA, 0xCD, 0xFF);
		public static readonly Color4 LightBlue = new Color4(0xAD, 0xD8, 0xE6, 0xFF);
		public static readonly Color4 LightCoral = new Color4(0xF0, 0x80, 0x80, 0xFF);
		public static readonly Color4 LightCyan = new Color4(0xE0, 0xFF, 0xFF, 0xFF);
		public static readonly Color4 LightGoldenRodYellow = new Color4(0xFA, 0xFA, 0xD2, 0xFF);
		public static readonly Color4 LightGray = new Color4(0xD3, 0xD3, 0xD3, 0xFF);
		public static readonly Color4 LightGrey = new Color4(0xD3, 0xD3, 0xD3, 0xFF);
		public static readonly Color4 LightGreen = new Color4(0x90, 0xEE, 0x90, 0xFF);
		public static readonly Color4 LightPink = new Color4(0xFF, 0xB6, 0xC1, 0xFF);
		public static readonly Color4 LightSalmon = new Color4(0xFF, 0xA0, 0x7A, 0xFF);
		public static readonly Color4 LightSeaGreen = new Color4(0x20, 0xB2, 0xAA, 0xFF);
		public static readonly Color4 LightSkyBlue = new Color4(0x87, 0xCE, 0xFA, 0xFF);
		public static readonly Color4 LightSlateGray = new Color4(0x77, 0x88, 0x99, 0xFF);
		public static readonly Color4 LightSlateGrey = new Color4(0x77, 0x88, 0x99, 0xFF);
		public static readonly Color4 LightSteelBlue = new Color4(0xB0, 0xC4, 0xDE, 0xFF);
		public static readonly Color4 LightYellow = new Color4(0xFF, 0xFF, 0xE0, 0xFF);
		public static readonly Color4 Lime = new Color4(0x00, 0xFF, 0x00, 0xFF);
		public static readonly Color4 LimeGreen = new Color4(0x32, 0xCD, 0x32, 0xFF);
		public static readonly Color4 Linen = new Color4(0xFA, 0xF0, 0xE6, 0xFF);
		public static readonly Color4 Magenta = new Color4(0xFF, 0x00, 0xFF, 0xFF);
		public static readonly Color4 Maroon = new Color4(0x80, 0x00, 0x00, 0xFF);
		public static readonly Color4 MediumAquaMarine = new Color4(0x66, 0xCD, 0xAA, 0xFF);
		public static readonly Color4 MediumBlue = new Color4(0x00, 0x00, 0xCD, 0xFF);
		public static readonly Color4 MediumOrchid = new Color4(0xBA, 0x55, 0xD3, 0xFF);
		public static readonly Color4 MediumPurple = new Color4(0x93, 0x70, 0xD8, 0xFF);
		public static readonly Color4 MediumSeaGreen = new Color4(0x3C, 0xB3, 0x71, 0xFF);
		public static readonly Color4 MediumSlateBlue = new Color4(0x7B, 0x68, 0xEE, 0xFF);
		public static readonly Color4 MediumSpringGreen = new Color4(0x00, 0xFA, 0x9A, 0xFF);
		public static readonly Color4 MediumTurquoise = new Color4(0x48, 0xD1, 0xCC, 0xFF);
		public static readonly Color4 MediumVioletRed = new Color4(0xC7, 0x15, 0x85, 0xFF);
		public static readonly Color4 MidnightBlue = new Color4(0x19, 0x19, 0x70, 0xFF);
		public static readonly Color4 MintCream = new Color4(0xF5, 0xFF, 0xFA, 0xFF);
		public static readonly Color4 MistyRose = new Color4(0xFF, 0xE4, 0xE1, 0xFF);
		public static readonly Color4 Moccasin = new Color4(0xFF, 0xE4, 0xB5, 0xFF);
		public static readonly Color4 NavajoWhite = new Color4(0xFF, 0xDE, 0xAD, 0xFF);
		public static readonly Color4 Navy = new Color4(0x00, 0x00, 0x80, 0xFF);
		public static readonly Color4 OldLace = new Color4(0xFD, 0xF5, 0xE6, 0xFF);
		public static readonly Color4 Olive = new Color4(0x80, 0x80, 0x00, 0xFF);
		public static readonly Color4 OliveDrab = new Color4(0x6B, 0x8E, 0x23, 0xFF);
		public static readonly Color4 Orange = new Color4(0xFF, 0xA5, 0x00, 0xFF);
		public static readonly Color4 OrangeRed = new Color4(0xFF, 0x45, 0x00, 0xFF);
		public static readonly Color4 Orchid = new Color4(0xDA, 0x70, 0xD6, 0xFF);
		public static readonly Color4 PaleGoldenRod = new Color4(0xEE, 0xE8, 0xAA, 0xFF);
		public static readonly Color4 PaleGreen = new Color4(0x98, 0xFB, 0x98, 0xFF);
		public static readonly Color4 PaleTurquoise = new Color4(0xAF, 0xEE, 0xEE, 0xFF);
		public static readonly Color4 PaleVioletRed = new Color4(0xD8, 0x70, 0x93, 0xFF);
		public static readonly Color4 PapayaWhip = new Color4(0xFF, 0xEF, 0xD5, 0xFF);
		public static readonly Color4 PeachPuff = new Color4(0xFF, 0xDA, 0xB9, 0xFF);
		public static readonly Color4 Peru = new Color4(0xCD, 0x85, 0x3F, 0xFF);
		public static readonly Color4 Pink = new Color4(0xFF, 0xC0, 0xCB, 0xFF);
		public static readonly Color4 Plum = new Color4(0xDD, 0xA0, 0xDD, 0xFF);
		public static readonly Color4 PowderBlue = new Color4(0xB0, 0xE0, 0xE6, 0xFF);
		public static readonly Color4 Purple = new Color4(0x80, 0x00, 0x80, 0xFF);
		public static readonly Color4 Red = new Color4(0xFF, 0x00, 0x00, 0xFF);
		public static readonly Color4 RosyBrown = new Color4(0xBC, 0x8F, 0x8F, 0xFF);
		public static readonly Color4 RoyalBlue = new Color4(0x41, 0x69, 0xE1, 0xFF);
		public static readonly Color4 SaddleBrown = new Color4(0x8B, 0x45, 0x13, 0xFF);
		public static readonly Color4 Salmon = new Color4(0xFA, 0x80, 0x72, 0xFF);
		public static readonly Color4 SandyBrown = new Color4(0xF4, 0xA4, 0x60, 0xFF);
		public static readonly Color4 SeaGreen = new Color4(0x2E, 0x8B, 0x57, 0xFF);
		public static readonly Color4 SeaShell = new Color4(0xFF, 0xF5, 0xEE, 0xFF);
		public static readonly Color4 Sienna = new Color4(0xA0, 0x52, 0x2D, 0xFF);
		public static readonly Color4 Silver = new Color4(0xC0, 0xC0, 0xC0, 0xFF);
		public static readonly Color4 SkyBlue = new Color4(0x87, 0xCE, 0xEB, 0xFF);
		public static readonly Color4 SlateBlue = new Color4(0x6A, 0x5A, 0xCD, 0xFF);
		public static readonly Color4 SlateGray = new Color4(0x70, 0x80, 0x90, 0xFF);
		public static readonly Color4 SlateGrey = new Color4(0x70, 0x80, 0x90, 0xFF);
		public static readonly Color4 Snow = new Color4(0xFF, 0xFA, 0xFA, 0xFF);
		public static readonly Color4 SpringGreen = new Color4(0x00, 0xFF, 0x7F, 0xFF);
		public static readonly Color4 SteelBlue = new Color4(0x46, 0x82, 0xB4, 0xFF);
		public static readonly Color4 Tan = new Color4(0xD2, 0xB4, 0x8C, 0xFF);
		public static readonly Color4 Teal = new Color4(0x00, 0x80, 0x80, 0xFF);
		public static readonly Color4 Thistle = new Color4(0xD8, 0xBF, 0xD8, 0xFF);
		public static readonly Color4 Tomato = new Color4(0xFF, 0x63, 0x47, 0xFF);
		public static readonly Color4 Transparent = new Color4(0x00, 0x00, 0x00, 0x00);
		public static readonly Color4 Turquoise = new Color4(0x40, 0xE0, 0xD0, 0xFF);
		public static readonly Color4 Violet = new Color4(0xEE, 0x82, 0xEE, 0xFF);
		public static readonly Color4 Wheat = new Color4(0xF5, 0xDE, 0xB3, 0xFF);
		public static readonly Color4 White = new Color4(0xFF, 0xFF, 0xFF, 0xFF);
		public static readonly Color4 WhiteSmoke = new Color4(0xF5, 0xF5, 0xF5, 0xFF);
		public static readonly Color4 Yellow = new Color4(0xFF, 0xFF, 0x00, 0xFF);
		public static readonly Color4 YellowGreen = new Color4(0x9A, 0xCD, 0x32, 0xFF);

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
		/// The alpha value.
		/// </summary>
		public byte A;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <param name="a"></param>
		public Color4(byte r, byte g, byte b, byte a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		public override string ToString()
		{
			return "{ " + this.R + ", " + this.G + ", " + this.B + ", " + this.A + " }";
		}

		/// <summary>
		/// Converts the Color4 to a uint.
		/// </summary>
		/// <returns></returns>
		public uint ToUint()
		{
			return ((uint)this.R << 24) + ((uint)this.G << 16) + ((uint)this.B << 8) + (uint)this.A;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Color4))
				return false;

			return this.Equals((Color4)obj);
		}

		/// <summary>
		/// Returns true if other represents the same RGBA color.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Color4 other)
		{
			return this.R == other.R &&
				   this.G == other.G &&
				   this.B == other.B &&
				   this.A == other.A;
		}
	}
}
