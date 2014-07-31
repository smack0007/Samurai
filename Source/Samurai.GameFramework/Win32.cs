#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.GameFramework
{
	internal static class Win32
	{
		public const int CDS_FULLSCREEN = 0x00000004;

		public const int DISP_CHANGE_SUCCESSFUL = 0;

		public const int DM_BITSPERPEL = 0x40000;
		public const int DM_PELSWIDTH = 0x80000;
		public const int DM_PELSHEIGHT = 0x100000;

		public const int PM_REMOVE = 0x0001;

		public const int WM_KEYDOWN = 0x0100;
		public const int WM_KEYUP = 0x0101;
		public const int WM_CHAR = 0x0102;
		public const int WM_SYSKEYDOWN = 0x0104;
		public const int WM_SYSKEYUP = 0x0105;
		public const int WM_UNICHAR = 0x0109;
		public const int WM_SYSCOMMAND = 0x0112;
		public const int WM_MOUSEMOVE = 0x0200;
		public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_RBUTTONDOWN = 0x0204;
		public const int WM_RBUTTONUP = 0x0205;
		public const int WM_MBUTTONDOWN = 0x0207;
		public const int WM_MBUTTONUP = 0x0208;
		public const int WM_ENTERSIZEMOVE = 0x0231;
		public const int WM_EXITSIZEMOVE = 0x0232;
		public const int WM_MOUSEHOVER = 0x02A1;
		public const int WM_MOUSELEAVE = 0x02A3;

		public const int SC_MINIMIZE = 0xF020;
		public const int SC_RESTORE = 0xF120;

		public const int TME_HOVER = 0x0001;
		public const int TME_LEAVE = 0x0002;

		public static ushort LowWord(uint value)
		{
			return (ushort)value;
		}

		public static ushort HighWord(uint value)
		{
			return (ushort)(value >> 16);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int ChangeDisplaySettings([MarshalAs(UnmanagedType.LPStruct)] DevMode lpDevMode, uint dwflags);

		[DllImport("user32.dll")]
		public static extern int ShowCursor(bool bShow);
		
		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("user32.dll")]
		public static extern IntPtr DispatchMessage([In] ref Message msg);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

		[DllImport("user32.dll")]
		public static extern bool TranslateMessage([In] ref Message msg);

		[StructLayout(LayoutKind.Sequential)]
		internal struct Message
		{
			public IntPtr hWnd;
			public int msg;
			public IntPtr wParam;
			public IntPtr lParam;
			public uint time;
			public Point p;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct Point
		{
			public int X;
			public int Y;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class DevMode
		{
			const int CCHDEVICENAME = 32;
			const int CCHFORMNAME = 32;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHDEVICENAME)]
			public char[] dmDeviceName;
			public short dmSpecVersion;
			public short dmDriverVersion;
			public short dmSize;
			public short dmDriverExtra;
			public int dmFields;
			public DevModeUnion u;
			public short dmColor;
			public short dmDuplex;
			public short dmYResolution;
			public short dmTTOption;
			public short dmCollate;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHFORMNAME)]
			public char[] dmFormName;
			public short dmLogPixels;
			public int dmBitsPerPel;
			public int dmPelsWidth;
			public int dmPelsHeight;
			public int dmDisplayFlagsOrdmNup;
			public int dmDisplayFrequency;
			public int dmICMMethod;
			public int dmICMIntent;
			public int dmMediaType;
			public int dmDitherType;
			public int dmReserved1;
			public int dmReserved2;
			public int dmPanningWidth;
			public int dmPanningHeight;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct DevModeUnion
		{
			[FieldOffset(0)]
			public short dmOrientation;
			[FieldOffset(2)]
			public short dmPaperSize;
			[FieldOffset(4)]
			public short dmPaperLength;
			[FieldOffset(6)]
			public short dmPaperWidth;
			[FieldOffset(8)]
			public short dmScale;
			[FieldOffset(10)]
			public short dmCopies;
			[FieldOffset(12)]
			public short dmDefaultSource;
			[FieldOffset(14)]
			public short dmPrintQuality;

			[FieldOffset(0)]
			public int dmPosition_x;
			[FieldOffset(4)]
			public int dmPosition_y;

			[FieldOffset(0)]
			public int dmDisplayOrientation;

			[FieldOffset(0)]
			public int dmDisplayFixedOutput;
		}
	}
}
#endif