using Samurai.Graphics;
using System;

namespace Samurai.GameFramework
{
    public sealed class GameWindow
    {
#if WINDOWS
		Win32GameWindow window;
#endif

		Game game;

        Point position;
        Size size;
        double mouseX, mouseY;
		        
        KeyEventArgs keyEventArgs;
        KeyPressEventArgs keyPressEventArgs;

        MouseEventArgs mouseEventArgs;
        MouseButtonEventArgs mouseButtonEventArgs;
        MouseWheelEventArgs mouseWheelEventArgs;

		public IntPtr Handle
		{
			get { return this.window.Handle; }
		}

        public string Title
        {
			get { return this.window.Title; }
            set { this.window.Title = value; }
        }

        public Size Size
        {
            get { return this.size; }
            set { }
        }

        public int Width
        {
            get { return this.size.Width; }
            set {  }
        }

        public int Height
        {
            get { return this.size.Height; }
            set {  }
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
                        
        internal GameWindow(Game game, GameOptions options)
		{	
			this.game = game;

#if WINDOWS
			this.window = new Win32GameWindow();
#endif
			
			this.window.Tick += this.Window_Tick;
                       
            this.size = new Size(options.WindowWidth, options.WindowHeight);
            this.position = new Point(0, 0);

            this.keyEventArgs = new KeyEventArgs();
            this.keyPressEventArgs = new KeyPressEventArgs();
            this.mouseEventArgs = new MouseEventArgs();
            this.mouseButtonEventArgs = new MouseButtonEventArgs();
            this.mouseWheelEventArgs = new MouseWheelEventArgs();
        }

        public void Dispose()
        {
            //GLFW.Terminate();
			this.window.Dispose();
        }

		public void Run()
		{
			this.window.Run();
		}

		private void Window_Tick(object sender, EventArgs e)
		{
			this.game.Tick();
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
			//if (action == GLFW.PRESS)
			//{
			//	if (this.KeyDown != null)
			//	{
			//		this.keyEventArgs.Key = GLFW.ConvertKeyCode(key);
			//		this.KeyDown(this, this.keyEventArgs);
			//	}
			//}
			//else if (action == GLFW.RELEASE)
			//{
			//	if (this.KeyUp != null)
			//	{
			//		this.keyEventArgs.Key = GLFW.ConvertKeyCode(key);
			//		this.KeyUp(this, this.keyEventArgs);
			//	}
			//}
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
			//if (action == GLFW.PRESS)
			//{
			//	if (this.MouseButtonDown != null)
			//	{
			//		this.mouseButtonEventArgs.Button = GLFW.ConvertMouseButton(button);
			//		this.mouseButtonEventArgs.X = this.mouseX;
			//		this.mouseButtonEventArgs.Y = this.mouseY;
			//		this.MouseButtonDown(this, this.mouseButtonEventArgs);
			//	}
			//}
			//else if (action == GLFW.RELEASE)
			//{
			//	if (this.MouseButtonUp != null)
			//	{
			//		this.mouseButtonEventArgs.Button = GLFW.ConvertMouseButton(button);
			//		this.mouseButtonEventArgs.X = this.mouseX;
			//		this.mouseButtonEventArgs.Y = this.mouseY;
			//		this.MouseButtonUp(this, this.mouseButtonEventArgs);
			//	}
			//}
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
			//return GLFW.WindowShouldClose(this.window) != 0;
			return false;
		}

        internal void SetShouldClose(bool shouldClose)
        {
			//GLFW.SetWindowShouldClose(this.window, shouldClose ? 1 : 0);
        }

		public void Show()
		{
			//GLFW.ShowWindow(this.window);
		}

		public void Hide()
		{
			//GLFW.HideWindow(this.window);
		}

		public void SwapBuffers()
		{
			//GLFW.SwapBuffers(this.window);
		}
    }
}
