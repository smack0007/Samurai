using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Color4 : IEquatable<Color4>
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Color4));

		#region Static Colors

		public static readonly Color4 AliceBlue = new Color4(0xF0 / 255.0f, 0xF8 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 AntiqueWhite = new Color4(0xFA / 255.0f, 0xEB / 255.0f, 0xD7 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Aqua = new Color4(0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Aquamarine = new Color4(0x7F / 255.0f, 0xFF / 255.0f, 0xD4 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Azure = new Color4(0xF0 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Beige = new Color4(0xF5 / 255.0f, 0xF5 / 255.0f, 0xDC / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Bisque = new Color4(0xFF / 255.0f, 0xE4 / 255.0f, 0xC4 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Black = new Color4(0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 BlanchedAlmond = new Color4(0xFF / 255.0f, 0xEB / 255.0f, 0xCD / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Blue = new Color4(0x00 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 BlueViolet = new Color4(0x8A / 255.0f, 0x2B / 255.0f, 0xE2 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Brown = new Color4(0xA5 / 255.0f, 0x2A / 255.0f, 0x2A / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 BurlyWood = new Color4(0xDE / 255.0f, 0xB8 / 255.0f, 0x87 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 CadetBlue = new Color4(0x5F / 255.0f, 0x9E / 255.0f, 0xA0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Chartreuse = new Color4(0x7F / 255.0f, 0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Chocolate = new Color4(0xD2 / 255.0f, 0x69 / 255.0f, 0x1E / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Coral = new Color4(0xFF / 255.0f, 0x7F / 255.0f, 0x50 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 CornflowerBlue = new Color4(0x64 / 255.0f, 0x95 / 255.0f, 0xED / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Cornsilk = new Color4(0xFF / 255.0f, 0xF8 / 255.0f, 0xDC / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Crimson = new Color4(0xDC / 255.0f, 0x14 / 255.0f, 0x3C / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Cyan = new Color4(0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkBlue = new Color4(0x00 / 255.0f, 0x00 / 255.0f, 0x8B / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkCyan = new Color4(0x00 / 255.0f, 0x8B / 255.0f, 0x8B / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkGoldenRod = new Color4(0xB8 / 255.0f, 0x86 / 255.0f, 0x0B / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkGray = new Color4(0xA9 / 255.0f, 0xA9 / 255.0f, 0xA9 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkGrey = new Color4(0xA9 / 255.0f, 0xA9 / 255.0f, 0xA9 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkGreen = new Color4(0x00 / 255.0f, 0x64 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkKhaki = new Color4(0xBD / 255.0f, 0xB7 / 255.0f, 0x6B / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkMagenta = new Color4(0x8B / 255.0f, 0x00 / 255.0f, 0x8B / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkOliveGreen = new Color4(0x55 / 255.0f, 0x6B / 255.0f, 0x2F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Darkorange = new Color4(0xFF / 255.0f, 0x8C / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkOrchid = new Color4(0x99 / 255.0f, 0x32 / 255.0f, 0xCC / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkRed = new Color4(0x8B / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkSalmon = new Color4(0xE9 / 255.0f, 0x96 / 255.0f, 0x7A / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkSeaGreen = new Color4(0x8F / 255.0f, 0xBC / 255.0f, 0x8F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkSlateBlue = new Color4(0x48 / 255.0f, 0x3D / 255.0f, 0x8B / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkSlateGray = new Color4(0x2F / 255.0f, 0x4F / 255.0f, 0x4F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkSlateGrey = new Color4(0x2F / 255.0f, 0x4F / 255.0f, 0x4F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkTurquoise = new Color4(0x00 / 255.0f, 0xCE / 255.0f, 0xD1 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DarkViolet = new Color4(0x94 / 255.0f, 0x00 / 255.0f, 0xD3 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DeepPink = new Color4(0xFF / 255.0f, 0x14 / 255.0f, 0x93 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DeepSkyBlue = new Color4(0x00 / 255.0f, 0xBF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DimGray = new Color4(0x69 / 255.0f, 0x69 / 255.0f, 0x69 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DimGrey = new Color4(0x69 / 255.0f, 0x69 / 255.0f, 0x69 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 DodgerBlue = new Color4(0x1E / 255.0f, 0x90 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 FireBrick = new Color4(0xB2 / 255.0f, 0x22 / 255.0f, 0x22 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 FloralWhite = new Color4(0xFF / 255.0f, 0xFA / 255.0f, 0xF0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 ForestGreen = new Color4(0x22 / 255.0f, 0x8B / 255.0f, 0x22 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Fuchsia = new Color4(0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Gainsboro = new Color4(0xDC / 255.0f, 0xDC / 255.0f, 0xDC / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 GhostWhite = new Color4(0xF8 / 255.0f, 0xF8 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Gold = new Color4(0xFF / 255.0f, 0xD7 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 GoldenRod = new Color4(0xDA / 255.0f, 0xA5 / 255.0f, 0x20 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Gray = new Color4(0x80 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Grey = new Color4(0x80 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Green = new Color4(0x00 / 255.0f, 0x80 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 GreenYellow = new Color4(0xAD / 255.0f, 0xFF / 255.0f, 0x2F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 HoneyDew = new Color4(0xF0 / 255.0f, 0xFF / 255.0f, 0xF0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 HotPink = new Color4(0xFF / 255.0f, 0x69 / 255.0f, 0xB4 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 IndianRed = new Color4(0xCD / 255.0f, 0x5C / 255.0f, 0x5C / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Indigo = new Color4(0x4B / 255.0f, 0x00 / 255.0f, 0x82 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Ivory = new Color4(0xFF / 255.0f, 0xFF / 255.0f, 0xF0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Khaki = new Color4(0xF0 / 255.0f, 0xE6 / 255.0f, 0x8C / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Lavender = new Color4(0xE6 / 255.0f, 0xE6 / 255.0f, 0xFA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LavenderBlush = new Color4(0xFF / 255.0f, 0xF0 / 255.0f, 0xF5 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LawnGreen = new Color4(0x7C / 255.0f, 0xFC / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LemonChiffon = new Color4(0xFF / 255.0f, 0xFA / 255.0f, 0xCD / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightBlue = new Color4(0xAD / 255.0f, 0xD8 / 255.0f, 0xE6 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightCoral = new Color4(0xF0 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightCyan = new Color4(0xE0 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightGoldenRodYellow = new Color4(0xFA / 255.0f, 0xFA / 255.0f, 0xD2 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightGray = new Color4(0xD3 / 255.0f, 0xD3 / 255.0f, 0xD3 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightGrey = new Color4(0xD3 / 255.0f, 0xD3 / 255.0f, 0xD3 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightGreen = new Color4(0x90 / 255.0f, 0xEE / 255.0f, 0x90 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightPink = new Color4(0xFF / 255.0f, 0xB6 / 255.0f, 0xC1 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightSalmon = new Color4(0xFF / 255.0f, 0xA0 / 255.0f, 0x7A / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightSeaGreen = new Color4(0x20 / 255.0f, 0xB2 / 255.0f, 0xAA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightSkyBlue = new Color4(0x87 / 255.0f, 0xCE / 255.0f, 0xFA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightSlateGray = new Color4(0x77 / 255.0f, 0x88 / 255.0f, 0x99 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightSlateGrey = new Color4(0x77 / 255.0f, 0x88 / 255.0f, 0x99 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightSteelBlue = new Color4(0xB0 / 255.0f, 0xC4 / 255.0f, 0xDE / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LightYellow = new Color4(0xFF / 255.0f, 0xFF / 255.0f, 0xE0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Lime = new Color4(0x00 / 255.0f, 0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 LimeGreen = new Color4(0x32 / 255.0f, 0xCD / 255.0f, 0x32 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Linen = new Color4(0xFA / 255.0f, 0xF0 / 255.0f, 0xE6 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Magenta = new Color4(0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Maroon = new Color4(0x80 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumAquaMarine = new Color4(0x66 / 255.0f, 0xCD / 255.0f, 0xAA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumBlue = new Color4(0x00 / 255.0f, 0x00 / 255.0f, 0xCD / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumOrchid = new Color4(0xBA / 255.0f, 0x55 / 255.0f, 0xD3 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumPurple = new Color4(0x93 / 255.0f, 0x70 / 255.0f, 0xD8 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumSeaGreen = new Color4(0x3C / 255.0f, 0xB3 / 255.0f, 0x71 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumSlateBlue = new Color4(0x7B / 255.0f, 0x68 / 255.0f, 0xEE / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumSpringGreen = new Color4(0x00 / 255.0f, 0xFA / 255.0f, 0x9A / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumTurquoise = new Color4(0x48 / 255.0f, 0xD1 / 255.0f, 0xCC / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MediumVioletRed = new Color4(0xC7 / 255.0f, 0x15 / 255.0f, 0x85 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MidnightBlue = new Color4(0x19 / 255.0f, 0x19 / 255.0f, 0x70 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MintCream = new Color4(0xF5 / 255.0f, 0xFF / 255.0f, 0xFA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 MistyRose = new Color4(0xFF / 255.0f, 0xE4 / 255.0f, 0xE1 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Moccasin = new Color4(0xFF / 255.0f, 0xE4 / 255.0f, 0xB5 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 NavajoWhite = new Color4(0xFF / 255.0f, 0xDE / 255.0f, 0xAD / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Navy = new Color4(0x00 / 255.0f, 0x00 / 255.0f, 0x80 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 OldLace = new Color4(0xFD / 255.0f, 0xF5 / 255.0f, 0xE6 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Olive = new Color4(0x80 / 255.0f, 0x80 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 OliveDrab = new Color4(0x6B / 255.0f, 0x8E / 255.0f, 0x23 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Orange = new Color4(0xFF / 255.0f, 0xA5 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 OrangeRed = new Color4(0xFF / 255.0f, 0x45 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Orchid = new Color4(0xDA / 255.0f, 0x70 / 255.0f, 0xD6 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PaleGoldenRod = new Color4(0xEE / 255.0f, 0xE8 / 255.0f, 0xAA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PaleGreen = new Color4(0x98 / 255.0f, 0xFB / 255.0f, 0x98 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PaleTurquoise = new Color4(0xAF / 255.0f, 0xEE / 255.0f, 0xEE / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PaleVioletRed = new Color4(0xD8 / 255.0f, 0x70 / 255.0f, 0x93 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PapayaWhip = new Color4(0xFF / 255.0f, 0xEF / 255.0f, 0xD5 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PeachPuff = new Color4(0xFF / 255.0f, 0xDA / 255.0f, 0xB9 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Peru = new Color4(0xCD / 255.0f, 0x85 / 255.0f, 0x3F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Pink = new Color4(0xFF / 255.0f, 0xC0 / 255.0f, 0xCB / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Plum = new Color4(0xDD / 255.0f, 0xA0 / 255.0f, 0xDD / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 PowderBlue = new Color4(0xB0 / 255.0f, 0xE0 / 255.0f, 0xE6 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Purple = new Color4(0x80 / 255.0f, 0x00 / 255.0f, 0x80 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Red = new Color4(0xFF / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 RosyBrown = new Color4(0xBC / 255.0f, 0x8F / 255.0f, 0x8F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 RoyalBlue = new Color4(0x41 / 255.0f, 0x69 / 255.0f, 0xE1 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SaddleBrown = new Color4(0x8B / 255.0f, 0x45 / 255.0f, 0x13 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Salmon = new Color4(0xFA / 255.0f, 0x80 / 255.0f, 0x72 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SandyBrown = new Color4(0xF4 / 255.0f, 0xA4 / 255.0f, 0x60 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SeaGreen = new Color4(0x2E / 255.0f, 0x8B / 255.0f, 0x57 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SeaShell = new Color4(0xFF / 255.0f, 0xF5 / 255.0f, 0xEE / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Sienna = new Color4(0xA0 / 255.0f, 0x52 / 255.0f, 0x2D / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Silver = new Color4(0xC0 / 255.0f, 0xC0 / 255.0f, 0xC0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SkyBlue = new Color4(0x87 / 255.0f, 0xCE / 255.0f, 0xEB / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SlateBlue = new Color4(0x6A / 255.0f, 0x5A / 255.0f, 0xCD / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SlateGray = new Color4(0x70 / 255.0f, 0x80 / 255.0f, 0x90 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SlateGrey = new Color4(0x70 / 255.0f, 0x80 / 255.0f, 0x90 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Snow = new Color4(0xFF / 255.0f, 0xFA / 255.0f, 0xFA / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SpringGreen = new Color4(0x00 / 255.0f, 0xFF / 255.0f, 0x7F / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 SteelBlue = new Color4(0x46 / 255.0f, 0x82 / 255.0f, 0xB4 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Tan = new Color4(0xD2 / 255.0f, 0xB4 / 255.0f, 0x8C / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Teal = new Color4(0x00 / 255.0f, 0x80 / 255.0f, 0x80 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Thistle = new Color4(0xD8 / 255.0f, 0xBF / 255.0f, 0xD8 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Tomato = new Color4(0xFF / 255.0f, 0x63 / 255.0f, 0x47 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Transparent = new Color4(0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
		public static readonly Color4 Turquoise = new Color4(0x40 / 255.0f, 0xE0 / 255.0f, 0xD0 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Violet = new Color4(0xEE / 255.0f, 0x82 / 255.0f, 0xEE / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Wheat = new Color4(0xF5 / 255.0f, 0xDE / 255.0f, 0xB3 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 White = new Color4(0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 WhiteSmoke = new Color4(0xF5 / 255.0f, 0xF5 / 255.0f, 0xF5 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 Yellow = new Color4(0xFF / 255.0f, 0xFF / 255.0f, 0x00 / 255.0f, 0xFF / 255.0f);
		public static readonly Color4 YellowGreen = new Color4(0x9A / 255.0f, 0xCD / 255.0f, 0x32 / 255.0f, 0xFF / 255.0f);

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
		/// The alpha value.
		/// </summary>
		public float A;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <param name="a"></param>
		public Color4(float r, float g, float b, float a)
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

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Color4))
				return false;

			return this.Equals((Color4)obj);
		}

		public bool Equals(Color4 other)
		{
			return this.R == other.R &&
				   this.G == other.G &&
				   this.B == other.B &&
				   this.A == other.A;
		}
	}
}
