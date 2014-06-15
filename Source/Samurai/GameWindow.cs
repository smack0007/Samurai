using System;

namespace Samurai
{
    public class GameWindow : IGraphicsHost
    {
        IntPtr window;
        string title;
        Point position;
        Size size;
        double mouseX, mouseY;

        GLFW.CharFun charCallback;
        GLFW.CursorPosFun cursorPosCallback;
        GLFW.KeyFun keyCallback;
        GLFW.MouseButtonFun mouseButtonCallback;
        GLFW.ScrollFun scrollCallback;
        GLFW.WindowPosFun windowPosCallback;
        GLFW.WindowSizeFun windowSizeCallback;

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
                GLFW.SetWindowTitle(this.window, value);
                this.title = value;
            }
        }

        public Size Size
        {
            get { return this.size; }
            set { GLFW.SetWindowSize(this.window, value.Width, value.Height); }
        }

        public int Width
        {
            get { return this.size.Width; }
            set { GLFW.SetWindowSize(this.window, value, this.size.Height); }
        }

        public int Height
        {
            get { return this.size.Height; }
            set { GLFW.SetWindowSize(this.window, this.size.Width, value); }
        }

        public bool IsFullscreen
        {
            get { return false; }
        }

        public event EventHandler Initialize;

        public event EventHandler Shutdown;

        public event EventHandler Idle;

        public event EventHandler Resize;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<KeyPressEventArgs> KeyPress;

        public event EventHandler<MouseEventArgs> MouseMove;

        public event EventHandler<MouseButtonEventArgs> MouseButtonDown;

        public event EventHandler<MouseButtonEventArgs> MouseButtonUp;

        public event EventHandler<MouseWheelEventArgs> MouseWheel;
                        
        public GameWindow()
		{        
            this.window = GLFW.CreateWindow(800, 600, string.Empty, IntPtr.Zero, IntPtr.Zero);
                       
            this.title = string.Empty;
            this.size = new Size(800, 600);
            this.position = new Point(0, 0);

            // It is important to hold references to the callbacks so that the GC does not garbage collect the delegates.

            this.windowPosCallback = this.OnWindowMove;
            GLFW.SetWindowPosCallback(this.window, this.windowPosCallback);

            this.windowSizeCallback = this.OnWindowResize;
            GLFW.SetWindowSizeCallback(this.window, this.windowSizeCallback);

            this.charCallback = this.OnChar;
            GLFW.SetCharCallback(this.window, this.charCallback);

            this.keyCallback = this.OnKey;
            GLFW.SetKeyCallback(this.window, this.keyCallback);

            this.cursorPosCallback = this.OnCursorPos;
            GLFW.SetCursorPosCallback(this.window, this.cursorPosCallback);

            this.mouseButtonCallback = this.OnMouseButton;
            GLFW.SetMouseButtonCallback(this.window, this.mouseButtonCallback);

            this.scrollCallback = this.OnScroll;
            GLFW.SetScrollCallback(this.window, this.scrollCallback);
            
            GLFW.MakeContextCurrent(this.window);

            this.keyEventArgs = new KeyEventArgs();
            this.keyPressEventArgs = new KeyPressEventArgs();
            this.mouseEventArgs = new MouseEventArgs();
            this.mouseButtonEventArgs = new MouseButtonEventArgs();
            this.mouseWheelEventArgs = new MouseWheelEventArgs();
        }

        public void Dispose()
        {
            GLFW.Terminate();
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
            if (action == GLFW.PRESS)
            {
                if (this.KeyDown != null)
                {
                    this.keyEventArgs.Key = GLFW.ConvertKeyCode(key);
                    this.KeyDown(this, this.keyEventArgs);
                }
            }
            else if (action == GLFW.RELEASE)
            {
                if (this.KeyUp != null)
                {
                    this.keyEventArgs.Key = GLFW.ConvertKeyCode(key);
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
            if (action == GLFW.PRESS)
            {
                if (this.MouseButtonDown != null)
                {
                    this.mouseButtonEventArgs.Button = GLFW.ConvertMouseButton(button);
                    this.mouseButtonEventArgs.X = this.mouseX;
                    this.mouseButtonEventArgs.Y = this.mouseY;
                    this.MouseButtonDown(this, this.mouseButtonEventArgs);
                }
            }
            else if (action == GLFW.RELEASE)
            {
                if (this.MouseButtonUp != null)
                {
                    this.mouseButtonEventArgs.Button = GLFW.ConvertMouseButton(button);
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
			return GLFW.WindowShouldClose(this.window) != 0;
		}

        internal void SetShouldClose(bool shouldClose)
        {
            GLFW.SetWindowShouldClose(this.window, shouldClose ? 1 : 0);
        }

		public void SwapBuffers()
		{
			GLFW.SwapBuffers(this.window);
		}
    }
}
