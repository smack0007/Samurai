#if WINDOWS
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

		TextInputEventArgs textInputEventArgs;

		event EventHandler<CancelEventArgs> closeEvent;
		CancelEventArgs closeEventArgs;

		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		public Point Position
		{
			get
			{
				System.Drawing.Point location = this.Location;
				return new Point(location.X, location.Y);
			}

			set
			{
				this.Location = new System.Drawing.Point(value.X, value.Y);
			}
		}

		public new Size Size
		{
			get
			{
				System.Drawing.Size clientSize = this.ClientSize;
				return new Size(clientSize.Width, clientSize.Height);
			}

			set
			{
				this.ClientSize = new System.Drawing.Size(value.Width, value.Height);
			}
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
		public event EventHandler<TextInputEventArgs> TextInput;

		public event EventHandler PositionChanged;

		public new event EventHandler SizeChanged;

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
			this.Visible = false;

			//this.Icon = Snowball.GameFramework.Properties.Resources.Icon;

			this.textInputEventArgs = new TextInputEventArgs();

			this.closeEventArgs = new CancelEventArgs();
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyValue == 18) // Disable alt key menu activation
				e.Handled = true;

			base.OnKeyDown(e);
		}

		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);

			if (this.PositionChanged != null)
				this.PositionChanged(this, EventArgs.Empty);
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);

			if (this.SizeChanged != null)
				this.SizeChanged(this, EventArgs.Empty);
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
						case Win32.WM_CHAR:
						case Win32.WM_UNICHAR:
							this.textInputEventArgs.Char = (char)message.wParam;

							if (this.TextInput != null)
								this.TextInput(this, this.textInputEventArgs);

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
#endif