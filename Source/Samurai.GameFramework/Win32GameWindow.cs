using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samurai.GameFramework
{
	internal sealed class Win32GameWindow : Form
	{
		bool isRunning;

		int keyCode;
		event EventHandler<KeyPressEventArgs> keyPressEvent;
		KeyPressEventArgs keyPressEventArgs;

		event EventHandler<CancelEventArgs> closeEvent;
		CancelEventArgs closeEventArgs;

		/// <summary>
		/// Gets or sets the text of the window.
		/// </summary>
		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		/// <summary>
		/// The width of the game display area.
		/// </summary>
		public int DisplayWidth
		{
			get { return this.ClientSize.Width; }
			set { this.ClientSize = new System.Drawing.Size(value, this.ClientSize.Height); }
		}

		/// <summary>
		/// Tthe height of the game display area.
		/// </summary>
		public int DisplayHeight
		{
			get { return this.ClientSize.Height; }
			set { this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, value); }
		}

		/// <summary>
		/// Triggered when idle time is available.
		/// </summary>
		public event EventHandler Tick;

		/// <summary>
		/// Triggered when the game window is minimized.
		/// </summary>
		public event EventHandler Resume;

		/// <summary>
		/// Triggered when the game window is restored.
		/// </summary>
		public event EventHandler Pause;

		/// <summary>
		/// Triggered when the game window is being closed.
		/// </summary>
		public new event EventHandler<CancelEventArgs> Closing
		{
			add { this.closeEvent += value; }
			remove { this.closeEvent -= value; }
		}

		/// <summary>
		/// Triggered when a key is pressed.
		/// </summary>
		public new event EventHandler<KeyPressEventArgs> KeyPress
		{
			add { this.keyPressEvent += value; }
			remove { this.keyPressEvent -= value; }
		}

		/// <summary>
		/// Triggered when the size of the client area of the window changes.
		/// </summary>
		public event EventHandler DisplaySizeChanged;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Win32GameWindow()
			: base()
		{
			this.Cursor = Cursors.Arrow;
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.KeyPreview = true;

			//this.Icon = Snowball.GameFramework.Properties.Resources.Icon;

			this.keyPressEventArgs = new KeyPressEventArgs();

			this.closeEventArgs = new CancelEventArgs();
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyValue == 18) // Disable alt key menu activation
				e.Handled = true;

			base.OnKeyDown(e);
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);

			if (this.DisplaySizeChanged != null)
				this.DisplaySizeChanged(this, e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				this.closeEventArgs.Cancel = false;

				if (this.closeEvent != null)
					this.closeEvent(this, this.closeEventArgs);

				e.Cancel = this.closeEventArgs.Cancel;
			}

			base.OnFormClosing(e);
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			this.Exit();
		}

		private void TriggerPause()
		{
			if (this.Pause != null)
				this.Pause(this, EventArgs.Empty);
		}

		private void TriggerResume()
		{
			if (this.Resume != null)
				this.Resume(this, EventArgs.Empty);
		}

		/// <summary>
		/// Begins the message pump.
		/// </summary>
		public void Run()
		{
			this.isRunning = true;

			this.Show();

			Win32.Message message;

			while (this.isRunning)
			{
				if (Win32.PeekMessage(out message, IntPtr.Zero, 0, 0, Win32.PM_REMOVE))
				{
					switch (message.msg)
					{
						case Win32.WM_KEYDOWN:
							this.keyCode = (int)message.wParam;
							break;

						case Win32.WM_CHAR:
						case Win32.WM_UNICHAR:
							//this.keyPressEventArgs.KeyCode = this.keyCode;
							this.keyPressEventArgs.KeyChar = (char)message.wParam;

							if (this.keyPressEvent != null)
								this.keyPressEvent(this, this.keyPressEventArgs);

							break;

						case Win32.WM_ENTERSIZEMOVE:
							this.TriggerPause();
							break;

						case Win32.WM_EXITSIZEMOVE:
							this.TriggerResume();
							break;

						case Win32.WM_SYSCOMMAND:
							if (message.wParam == (IntPtr)Win32.SC_MINIMIZE)
							{
								this.TriggerPause();
							}
							else if (message.wParam == (IntPtr)Win32.SC_RESTORE)
							{
								this.TriggerResume();
							}
							break;

						case Win32.WM_SYSKEYDOWN:
						case Win32.WM_SYSKEYUP:

							break;
					}

					Win32.TranslateMessage(ref message);
					Win32.DispatchMessage(ref message);
				}
				else
				{
					if (this.Tick != null)
						this.Tick(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Forces the game to shutdown.
		/// </summary>
		public void Exit()
		{
			this.isRunning = false;
		}
	}
}
