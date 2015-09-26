using Samurai.Graphics;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Samurai.Wpf
{
    public class GraphicsControl : HwndHost, IGraphicsHostContext
    {
        private const string WindowClass = "SamuraiWpfGraphicsControlClass";

        private IntPtr hWnd;
        private IntPtr hDC;
        private IntPtr hRC;

        int oldWindowWidth, oldWindowHeight;        

        int IGraphicsHostContext.Width
        {
            get { return (int)this.ActualWidth; }
        }

        int IGraphicsHostContext.Height
        {
            get { return (int)this.ActualHeight; }
        }

        public GraphicsContext Graphics
        {
            get;
            private set;
        }

        public bool AutoResizeViewport
        {
            get;
            set;
        }
                
        public event EventHandler<GraphicsContextEventArgs> GraphicsContextCreated;

        public event EventHandler<GraphicsContextEventArgs> Render;

        public GraphicsControl()
            : base()
        {
            this.AutoResizeViewport = true;
        }

        protected override HandleRef BuildWindowCore(HandleRef hWndParent)
        {
            this.hWnd = CreateHostWindow(hWndParent.Handle);

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.CreateWglContext();

                this.Graphics = new GraphicsContext(this);
                this.Graphics.Viewport = new Rectangle(0, 0, (int)this.ActualWidth, (int)this.ActualHeight);
                this.OnGraphicsContextCreated(new GraphicsContextEventArgs(this.Graphics));

                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }

            return new HandleRef(this, this.hWnd);
        }

        private void CreateWglContext()
        {
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

            IntPtr tempContext = WGL.CreateContext(this.hDC);

            if (tempContext == IntPtr.Zero)
                throw new SamuraiException("Unable to create temporary render context.");

            if (!WGL.MakeCurrent(hDC, tempContext))
                throw new SamuraiException("Unable to make temporary render context current.");

            int[] attribs = new int[]
            {
                (int)WGL.ContextMajorVersionArb, 3,
                (int)WGL.ContextMinorVersionArb, 3,
                (int)WGL.ContextFlagsArb, (int)0,
                0
            };

            IntPtr proc = this.GetProcAddress("wglCreateContextAttribsARB");
            WGL.CreateContextAttribsARB createContextAttribs = (WGL.CreateContextAttribsARB)Marshal.GetDelegateForFunctionPointer(proc, typeof(WGL.CreateContextAttribsARB));
            this.hRC = createContextAttribs(this.hDC, IntPtr.Zero, attribs);

            WGL.MakeCurrent(IntPtr.Zero, IntPtr.Zero);
            WGL.DeleteContext(tempContext);

            if (this.hRC == IntPtr.Zero)
                throw new SamuraiException("Unable to create render context.");

            if (!WGL.MakeCurrent(this.hDC, this.hRC))
                throw new SamuraiException("Unable to make render context current.");
        }

        private void DeleteWglContext()
        {
            WGL.MakeCurrent(IntPtr.Zero, IntPtr.Zero);
            WGL.DeleteContext(this.hRC);

            Win32.ReleaseDC(this.hWnd, this.hDC);
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (this.Graphics.MakeCurrent())
            {
                this.OnRender(new GraphicsContextEventArgs(this.Graphics));
                this.Graphics.SwapBuffers();
            }
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            this.Graphics.Dispose();
            this.DeleteWglContext();
            Win32.DestroyWindow(this.hWnd);
            this.hWnd = IntPtr.Zero;
        }

        private void RegisterWindowClass()
        {
            Win32.WNDCLASSEX wndClass = new Win32.WNDCLASSEX();
            wndClass.cbSize = (uint)Marshal.SizeOf(wndClass);
            wndClass.hInstance = Win32.GetModuleHandle(null);
            wndClass.lpfnWndProc = Win32.DefaultWindowProc;
            wndClass.lpszClassName = WindowClass;
            wndClass.hCursor = Win32.LoadCursor(IntPtr.Zero, Win32.IDC_ARROW);

            Win32.RegisterClassEx(ref wndClass);
        }

        private IntPtr CreateHostWindow(IntPtr hWndParent)
        {
            RegisterWindowClass();

            return Win32.CreateWindowEx(
                0,
                WindowClass,
                "",
                Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN,
                0,
                0,
                (int)Width,
                (int)Height,
                hWndParent,
                IntPtr.Zero,
                IntPtr.Zero,
                0);
        }

        protected override IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32.WM_PAINT:
                    if (!DesignerProperties.GetIsInDesignMode(this))
                    {
                        if (this.Graphics.MakeCurrent())
                        {
                            this.OnRender(new GraphicsContextEventArgs(this.Graphics));
                            this.Graphics.SwapBuffers();
                        }
                    }
                    break;
            }

            return base.WndProc(hWnd, msg, wParam, lParam, ref handled);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (this.AutoResizeViewport)
            {
                Rectangle oldViewport = this.Graphics.Viewport;
                this.Graphics.Viewport = new Rectangle(
                    oldViewport.X,
                    oldViewport.Y,
                    oldViewport.Width + ((int)this.ActualWidth - this.oldWindowWidth),
                    oldViewport.Height + ((int)this.ActualHeight - this.oldWindowHeight));
            }

            this.oldWindowWidth = (int)this.ActualWidth;
            this.oldWindowHeight = (int)this.ActualHeight;

            base.OnRenderSizeChanged(sizeInfo);
        }

        protected virtual void OnGraphicsContextCreated(GraphicsContextEventArgs e)
        {
            this.oldWindowWidth = (int)this.ActualWidth;
            this.oldWindowHeight = (int)this.ActualHeight;

            if (this.GraphicsContextCreated != null)
                this.GraphicsContextCreated(this, e);
        }

        protected virtual void OnRender(GraphicsContextEventArgs e)
        {
            if (this.Render != null)
                this.Render(this, e);
        }

        public IntPtr GetProcAddress(string name)
        {
            return WGL.GetProcAddress(name);
        }

        public bool MakeCurrent()
        {
            return WGL.MakeCurrent(this.hDC, this.hRC);
        }

        public void SwapBuffers()
        {
            WGL.SwapBuffers(this.hDC);
        }
    }
}
