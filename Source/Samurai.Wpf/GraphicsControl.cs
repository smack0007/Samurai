using Samurai;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Samurai.Wpf
{
    public class GraphicsControl : HwndHost, IGraphicsHost
    {
        private const string WindowClass = "SamuraiWpfGraphicsControlClass";

        IntPtr hWnd;

        int oldWindowWidth, oldWindowHeight;        

        IntPtr IGraphicsHost.Handle
        {
            get { return this.hWnd; }
        }

        int IGraphicsHost.Width
        {
            get { return (int)this.ActualWidth; }
        }

        int IGraphicsHost.Height
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
                this.Graphics = new GraphicsContext(this);
                this.Graphics.Viewport = new Rectangle(0, 0, (int)this.ActualWidth, (int)this.ActualHeight);
                this.OnGraphicsContextCreated(new GraphicsContextEventArgs(this.Graphics));

                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }

            return new HandleRef(this, this.hWnd);
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
    }
}
