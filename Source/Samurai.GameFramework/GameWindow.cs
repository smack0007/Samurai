using Samurai.Graphics;
using System;

namespace Samurai.GameFramework
{
    public sealed class GameWindow : IHostControl
    {
#if WINDOWS
		Win32GameWindow window;
#endif

		Game game;
		
		public IntPtr Handle
		{
			get { return this.window.Handle; }
		}

        public string Title
        {
			get { return this.window.Title; }
            set { this.window.Title = value; }
        }

		public Point Position
		{
			get { return this.window.Position; }
			set { this.window.Position = value; }
		}

		public int X
		{
			get { return this.Position.X; }
			set { this.Position = new Point(value, this.Y); }
		}

		public int Y
		{
			get { return this.Position.Y; }
			set { this.Position = new Point(this.X, value); }
		}

        public Size Size
        {
            get { return this.window.Size; }
			set { this.window.Size = value; }
        }

        public int Width
        {
            get { return this.Size.Width; }
			set { this.window.Size = new Size(value, this.Height); }
        }

        public int Height
        {
            get { return this.Size.Height; }
			set { this.window.Size = new Size(this.Width, value); }
        }

		public bool IsFullscreen
		{
			get;
			private set;
		}
		        
        public event EventHandler Resize;

        public event EventHandler<TextInputEventArgs> TextInput;
                        
        internal GameWindow(Game game, GameOptions options)
		{	
			this.game = game;

#if WINDOWS
			this.window = new Win32GameWindow(options);
#endif
			
			this.window.Tick += this.Window_Tick;
			this.window.SizeChanged += this.Window_Resize;
			this.window.TextInput += this.Window_TextInput;

			this.window.Size = options.WindowSize;
        }

        public void Dispose()
        {
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

        private void Window_Resize(object sender, EventArgs e)
        {
            if (this.Resize != null)
                this.Resize(this, EventArgs.Empty);
        }
    
        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (this.TextInput != null)
                this.TextInput(this, e);
        }
		    
		public void Show()
		{
			this.window.Show();
		}

		public void Hide()
		{
			this.window.Hide();
		}

		public void Exit()
		{
			this.window.Exit();
		}
    }
}
