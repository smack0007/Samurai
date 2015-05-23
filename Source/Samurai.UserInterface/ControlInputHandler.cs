using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samurai.Input;

namespace Samurai.UserInterface
{
	public class ControlInputHandler : IControlInputHandler
	{
		Mouse mouse;
				
		public ControlInputHandler(IGraphicsHostControl hostControl)
		{
			this.mouse = new Mouse(hostControl);
		}

		public void Update(TimingState time, Control root)
		{
			if (time == null)
				throw new ArgumentNullException("time");

			if (root == null)
				throw new ArgumentNullException("root");

			this.mouse.Update(time);

			this.UpdateControl(root, time);
		}

		private void UpdateControl(Control control, TimingState time)
		{
			Rectangle controlRect = control.Rectangle;



			if (control is IControlContainer)
			{
				IControlContainer container = (IControlContainer)control;

				foreach (Control child in container.Controls)
					this.UpdateControl(control, time);
			}
		}

        public Point CursorPosition
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsPrimaryButtonPressed
        {
            get { throw new NotImplementedException(); }
        }
    }
}
