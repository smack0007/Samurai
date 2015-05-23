using Samurai.Graphics;
using System;

namespace Samurai.GLFW
{
    public sealed class GameWindow : IGraphicsHostContext
    {
        IntPtr window;
        string title;
        Point position;
        Size size;
        double mouseX, mouseY;

		GLFWNative.CharFun charCallback;
		GLFWNative.CursorPosFun cursorPosCallback;
		GLFWNative.KeyFun keyCallback;
		GLFWNative.MouseButtonFun mouseButtonCallback;
		GLFWNative.ScrollFun scrollCallback;
		GLFWNative.WindowPosFun windowPosCallback;
		GLFWNative.WindowSizeFun windowSizeCallback;

        KeyEventArgs keyEventArgs;
        KeyPressEventArgs keyPressEventArgs;

        MouseEventArgs mouseEventArgs;
        MouseButtonEventArgs mouseButtonEventArgs;
        MouseWheelEventArgs mouseWheelEventArgs;

        public string Title
        {
            get { return this.title; }
            
            set
            {
				GLFWNative.SetWindowTitle(this.window, value);
                this.title = value;
            }
        }

        public Size Size
        {
            get { return this.size; }
			set { GLFWNative.SetWindowSize(this.window, value.Width, value.Height); }
        }

        public int Width
        {
            get { return this.size.Width; }
			set { GLFWNative.SetWindowSize(this.window, value, this.size.Height); }
        }

        public int Height
        {
            get { return this.size.Height; }
			set { GLFWNative.SetWindowSize(this.window, this.size.Width, value); }
        }

		public bool IsFullscreen
		{
			get;
			private set;
		}
		        
        public event EventHandler Resize;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<KeyPressEventArgs> KeyPress;

        public event EventHandler<MouseEventArgs> MouseMove;

        public event EventHandler<MouseButtonEventArgs> MouseButtonDown;

        public event EventHandler<MouseButtonEventArgs> MouseButtonUp;

        public event EventHandler<MouseWheelEventArgs> MouseWheel;
                        
        internal GameWindow(GameOptions options)
		{
			GLFWNative.WindowHint(GLFWNative.VISIBLE, 0);
			GLFWNative.WindowHint(GLFWNative.RESIZABLE, options.WindowResizable ? 1 : 0);

			IntPtr monitor = IntPtr.Zero;

			if (options.WindowIsFullscreen)
			{
				monitor = GLFWNative.GetPrimaryMonitor();
				this.IsFullscreen = true;
			}

			this.window = GLFWNative.CreateWindow(options.WindowWidth, options.WindowHeight, string.Empty, monitor, IntPtr.Zero);
                       
            this.title = string.Empty;
            this.size = new Size(options.WindowWidth, options.WindowHeight);
            this.position = new Point(0, 0);

            // It is important to hold references to the callbacks so that the GC does not garbage collect the delegates.

            this.windowPosCallback = this.OnWindowMove;
			GLFWNative.SetWindowPosCallback(this.window, this.windowPosCallback);

            this.windowSizeCallback = this.OnWindowResize;
			GLFWNative.SetWindowSizeCallback(this.window, this.windowSizeCallback);

            this.charCallback = this.OnChar;
			GLFWNative.SetCharCallback(this.window, this.charCallback);

            this.keyCallback = this.OnKey;
			GLFWNative.SetKeyCallback(this.window, this.keyCallback);

            this.cursorPosCallback = this.OnCursorPos;
			GLFWNative.SetCursorPosCallback(this.window, this.cursorPosCallback);

            this.mouseButtonCallback = this.OnMouseButton;
			GLFWNative.SetMouseButtonCallback(this.window, this.mouseButtonCallback);

            this.scrollCallback = this.OnScroll;
			GLFWNative.SetScrollCallback(this.window, this.scrollCallback);

			GLFWNative.MakeContextCurrent(this.window);

            this.keyEventArgs = new KeyEventArgs();
            this.keyPressEventArgs = new KeyPressEventArgs();
            this.mouseEventArgs = new MouseEventArgs();
            this.mouseButtonEventArgs = new MouseButtonEventArgs();
            this.mouseWheelEventArgs = new MouseWheelEventArgs();
        }

		IntPtr IGraphicsHostContext.GetProcAddress(string name)
		{
			return GLFWNative.GetProcAddress(name);
		}

		bool IGraphicsHostContext.MakeCurrent()
		{
			GLFWNative.MakeContextCurrent(this.window);
			return true;
		}

        public void Dispose()
        {
			GLFWNative.Terminate();
        }

        private void OnWindowMove(IntPtr window, int xpos, int ypos)
        {
            this.position = new Point(xpos, ypos);
        }

        private void OnWindowResize(IntPtr window, int width, int height)
        {
            this.size = new Size(width, height);

            if (this.Resize != null)
                this.Resize(this, EventArgs.Empty);
        }
    
        private void OnChar(IntPtr window, uint codepoint)
        {
            if (this.KeyPress != null)
            {
                this.keyPressEventArgs.KeyChar = (char)codepoint;
                this.KeyPress(this, this.keyPressEventArgs);
            }
        }

        private void OnKey(IntPtr window, int key, int scancode, int action, int mods)
        {
			if (action == GLFWNative.PRESS)
            {
                if (this.KeyDown != null)
                {
					this.keyEventArgs.Key = GLFWNative.ConvertKeyCode(key);
                    this.KeyDown(this, this.keyEventArgs);
                }
            }
			else if (action == GLFWNative.RELEASE)
            {
                if (this.KeyUp != null)
                {
					this.keyEventArgs.Key = GLFWNative.ConvertKeyCode(key);
                    this.KeyUp(this, this.keyEventArgs);
                }
            }
        }

        private void OnCursorPos(IntPtr window, double xpos, double ypos)
        {
            this.mouseX = xpos;
            this.mouseY = ypos;

            if (this.MouseMove != null)
            {
                this.mouseEventArgs.X = xpos;
                this.mouseEventArgs.Y = ypos;
                this.MouseMove(this, this.mouseEventArgs);
            }
        }
     
        private void OnMouseButton(IntPtr window, int button, int action, int mods)
        {
			if (action == GLFWNative.PRESS)
            {
                if (this.MouseButtonDown != null)
                {
					this.mouseButtonEventArgs.Button = GLFWNative.ConvertMouseButton(button);
                    this.mouseButtonEventArgs.X = this.mouseX;
                    this.mouseButtonEventArgs.Y = this.mouseY;
                    this.MouseButtonDown(this, this.mouseButtonEventArgs);
                }
            }
			else if (action == GLFWNative.RELEASE)
            {
                if (this.MouseButtonUp != null)
                {
					this.mouseButtonEventArgs.Button = GLFWNative.ConvertMouseButton(button);
                    this.mouseButtonEventArgs.X = this.mouseX;
                    this.mouseButtonEventArgs.Y = this.mouseY;
                    this.MouseButtonUp(this, this.mouseButtonEventArgs);
                }
            }
        }

        private void OnScroll(IntPtr window, double xoffset, double yoffset)
        {
            if (this.MouseWheel != null)
            {
                this.mouseWheelEventArgs.WheelDelta = yoffset;
                this.mouseButtonEventArgs.X = this.mouseX;
                this.mouseButtonEventArgs.Y = this.mouseY;
                this.MouseWheel(this, this.mouseWheelEventArgs);
            }
        }
                             
		internal bool ShouldClose()
		{
			return GLFWNative.WindowShouldClose(this.window) != 0;
		}

        internal void SetShouldClose(bool shouldClose)
        {
			GLFWNative.SetWindowShouldClose(this.window, shouldClose ? 1 : 0);
        }

		public void Show()
		{
			GLFWNative.ShowWindow(this.window);
		}

		public void Hide()
		{
			GLFWNative.HideWindow(this.window);
		}

		public void SwapBuffers()
		{
			GLFWNative.SwapBuffers(this.window);
		}
    }
}
