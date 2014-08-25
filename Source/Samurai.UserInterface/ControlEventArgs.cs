using System;

namespace Samurai.UserInterface
{
	public class ControlEventArgs : EventArgs
	{
		public Control Control
		{
			get;
			internal set;
		}

		public ControlEventArgs()
			: base()
		{
		}
	}
}
