using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai.Input;

namespace Samurai.UserInterface
{
	public class MouseEventArgs : EventArgs
	{
		Point position;

		public MouseButton? Button
		{
			get;
			internal set;
		}

		public Point Position
		{
			get { return this.position; }
			internal set { this.position = value; }
		}

		public int X
		{
			get { return this.position.X; }
		}

		public int Y
		{
			get { return this.position.Y; }
		}

		public MouseEventArgs()
			: base()
		{
		}
	}
}
