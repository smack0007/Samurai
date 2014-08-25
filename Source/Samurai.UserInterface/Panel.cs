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

		public override void Update(TimeSpan elapsed, IControlInputHandler input)
		{
			this.Controls.Update(elapsed, input);
		}

		public override void Draw(IControlRenderer renderer)
		{
			this.Controls.Draw(renderer);
		}
	}
}
