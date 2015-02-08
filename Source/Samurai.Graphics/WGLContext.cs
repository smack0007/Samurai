#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	internal class WGLContext : IDisposable
	{
		private const string Library = "opengl32.dll";

        private const uint ContextFlagsArb = 0x2094;

        private const uint ContextMajorVersionArb = 0x2091;

        private const uint ContextMinorVersionArb = 0x2092;

		[DllImport(Library, EntryPoint = "wglCreateContext", ExactSpelling = true)]
		private static extern IntPtr CreateContext(IntPtr hDc);

		[DllImport(Library, EntryPoint = "wglDeleteContext", ExactSpelling = true)]
        private static extern bool DeleteContext(IntPtr oldContext);

		[DllImport(Library, EntryPoint = "wglGetProcAddress", ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(string lpszProc);

		[DllImport(Library, EntryPoint = "wglMakeCurrent", ExactSpelling = true)]
        private static extern bool MakeCurrent(IntPtr hDc, IntPtr newContext);

		[DllImport(Library, EntryPoint = "wglSwapBuffers", ExactSpelling = true)]
        private static extern bool SwapBuffers(IntPtr hdc);

        private delegate IntPtr CreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);

		private IntPtr hWnd;
		private IntPtr hDC;
		private IntPtr hRC;

		public WGLContext(IntPtr hWnd, int versionMajor, int versionMinor)
		{
			this.hWnd = hWnd;
			this.hDC = Win32.GetDC(hWnd);

			if (this.hDC == IntPtr.Zero)
				throw new SamuraiException("Could not get a device context (hDC).");

			Win32.PixelFormatDescriptor pfd = new Win32.PixelFormatDescriptor();
			pfd.nSize = (ushort)Marshal.SizeOf(typeof(Win32.PixelFormatDescriptor));
			pfd.nVersion = 1;
			pfd.dwFlags = (Win32.PFD_SUPPORT_OPENGL | Win32.PFD_DRAW_TO_WINDOW | Win32.PFD_DOUBLEBUFFER);
			pfd.iPixelType = (byte)Win32.PFD_TYPE_RGBA;
			pfd.cColorBits = (byte)Win32.GetDeviceCaps(this.hDC, Win32.BITSPIXEL);
			pfd.cDepthBits = 32;
			pfd.iLayerType = (byte)Win32.PFD_MAIN_PLANE;

			int pixelformat = Win32.ChoosePixelFormat(this.hDC, ref pfd);

			if (pixelformat == 0)
				throw new SamuraiException("Could not find A suitable pixel format.");

			if (Win32.SetPixelFormat(this.hDC, pixelformat, ref pfd) == 0)
				throw new SamuraiException("Could not set the pixel format.");

			IntPtr tempContext = CreateContext(this.hDC);

			if (tempContext == IntPtr.Zero)
				throw new SamuraiException("Unable to create temporary render context.");

			if (!MakeCurrent(hDC, tempContext))
				throw new SamuraiException("Unable to make temporary render context current.");

			int[] attribs = new int[]
			{
				(int)ContextMajorVersionArb, versionMajor,
				(int)ContextMinorVersionArb, versionMinor,
                (int)ContextFlagsArb, (int)0,
				0
			};

			CreateContextAttribsARB createContextAttribs = (CreateContextAttribsARB)this.GetProcAddress<CreateContextAttribsARB>("wglCreateContextAttribsARB");
			this.hRC = createContextAttribs(this.hDC, IntPtr.Zero, attribs);

			MakeCurrent(IntPtr.Zero, IntPtr.Zero);
			DeleteContext(tempContext);

			if (this.hRC == IntPtr.Zero)
				throw new SamuraiException("Unable to create render context.");

			if (!MakeCurrent(this.hDC, this.hRC))
				throw new SamuraiException("Unable to make render context current.");
		}

		public void Dispose()
		{
			MakeCurrent(IntPtr.Zero, IntPtr.Zero);
			DeleteContext(this.hRC);

			Win32.ReleaseDC(this.hWnd, this.hDC);
		}

		public object GetProcAddress<T>(string name)
		{
			Type delegateType = typeof(T);

			IntPtr proc = GetProcAddress(name);

			if (proc == IntPtr.Zero)
				throw new SamuraiException(string.Format("Failed to load GL extension function: {0}.", name));

			return Marshal.GetDelegateForFunctionPointer(proc, delegateType);
		}

        public bool MakeCurrent()
        {
            return MakeCurrent(this.hDC, this.hRC);
        }

		public void SwapBuffers()
		{
			SwapBuffers(this.hDC);
		}
	}
}
#endif