#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	internal static class Win32
	{
		internal const uint PFD_DOUBLEBUFFER = 0x00000001;
		internal const uint PFD_DRAW_TO_WINDOW = 0x00000004;
		internal const uint PFD_SUPPORT_OPENGL = 0x00000020;
		internal const uint PFD_TYPE_RGBA = 0;
		internal const uint PFD_MAIN_PLANE = 0;

		internal const int BITSPIXEL = 12;

		[DllImport("gdi32.dll")]
		internal static extern int ChoosePixelFormat(IntPtr hdc, ref PixelFormatDescriptor ppfd);

		[DllImport("user32.dll")]
		internal static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("gdi32.dll")]
		internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("user32.dll")]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32.dll")]
		internal static extern int SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);

		[StructLayout(LayoutKind.Sequential)]
		internal struct PixelFormatDescriptor
		{
			public ushort nSize;
			public ushort nVersion;
			public uint dwFlags;
			public byte iPixelType;
			public byte cColorBits;
			public byte cRedBits;
			public byte cRedShift;
			public byte cGreenBits;
			public byte cGreenShift;
			public byte cBlueBits;
			public byte cBlueShift;
			public byte cAlphaBits;
			public byte cAlphaShift;
			public byte cAccumBits;
			public byte cAccumRedBits;
			public byte cAccumGreenBits;
			public byte cAccumBlueBits;
			public byte cAccumAlphaBits;
			public byte cDepthBits;
			public byte cStencilBits;
			public byte cAuxBuffers;
			public byte iLayerType;
			public byte bReserved;
			public uint dwLayerMask;
			public uint dwVisibleMask;
			public uint dwDamageMask;
		}
	}
}
#endif