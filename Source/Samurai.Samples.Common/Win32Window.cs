using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samurai.Samples.Common
{
    internal class Win32Window : Form, IGraphicsHost
    {
		[StructLayout(LayoutKind.Sequential)]
		private struct MSG
		{
			public IntPtr hWnd;
			public uint message;
			public IntPtr wParam;
			public IntPtr lParam;
			public uint time;
			public System.Drawing.Point pt;
		}

		[DllImport("user32.dll")]
		private static extern int PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

		public Action Initialized { get; set; }

		public Action Idle { get; set; }

		public Action Resized { get; set; }

		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		static Win32Window()
		{
			Application.EnableVisualStyles();
		}

		public Win32Window()
		{
			this.Text = "Samurai Sample";
			this.Size = new System.Drawing.Size(800, 600);

			Application.Idle += this.Application_Idle;
		}

		protected override void Dispose(bool disposing)
		{
			Application.Idle -= this.Application_Idle;

			base.Dispose(disposing);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			this.Initialized();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			MSG msg;
			while (PeekMessage(out msg, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0)
			{
				this.Idle();
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			if (this.Resized != null)
				this.Resized();
		}

		public void Run()
		{
			Application.Run(this);
		}
    }
}
