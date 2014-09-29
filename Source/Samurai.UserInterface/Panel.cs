using System;

namespace Samurai.UserInterface
{
	public class Panel : Control, IControlContainer
	{
		public ControlCollection Controls
		{
			get;
			private set;
		}

		public Panel()
		{
			this.Controls = new ControlCollection(this, this.OnControlAdded, this.OnControlRemoved);
		}

		protected virtual void OnControlAdded(Control control)
		{
		}

		protected virtual void OnControlRemoved(Control control)
		{
		}

		protected override void UpdateControl(TimingState time, IControlInputHandler input)
		{
			this.Controls.Update(time, input);
		}

		protected override void DrawControl(IControlRenderer renderer)
		{
			this.Controls.Draw(renderer);
		}
	}
}
