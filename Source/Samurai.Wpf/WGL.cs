using System;
using System.Runtime.InteropServices;

namespace Samurai.Wpf
{
    internal static class WGL
    {
        private const string Library = "opengl32.dll";

        public const uint ContextFlagsArb = 0x2094;

        public const uint ContextMajorVersionArb = 0x2091;

        public const uint ContextMinorVersionArb = 0x2092;

        [DllImport(Library, EntryPoint = "wglCreateContext", ExactSpelling = true)]
        public static extern IntPtr CreateContext(IntPtr hDc);

        [DllImport(Library, EntryPoint = "wglDeleteContext", ExactSpelling = true)]
        public static extern bool DeleteContext(IntPtr oldContext);

        [DllImport(Library, EntryPoint = "wglGetProcAddress", ExactSpelling = true)]
        public static extern IntPtr GetProcAddress(string lpszProc);

        [DllImport(Library, EntryPoint = "wglMakeCurrent", ExactSpelling = true)]
        public static extern bool MakeCurrent(IntPtr hDc, IntPtr newContext);

        [DllImport(Library, EntryPoint = "wglSwapBuffers", ExactSpelling = true)]
        public static extern bool SwapBuffers(IntPtr hdc);

        public delegate IntPtr CreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);
    }
}
