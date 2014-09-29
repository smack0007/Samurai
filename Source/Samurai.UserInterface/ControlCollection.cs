using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.UserInterface
{
	public class ControlCollection : IEnumerable<Control>
	{
		Control parent;
		List<Control> controls;
		Action<Control> onAdd;
		Action<Control> onRemove;

		public ControlCollection(Control parent, Action<Control> onAdd, Action<Control> onRemove)
		{
			if (parent == null)
				throw new ArgumentNullException("parent");

			this.parent = parent;
			this.controls = new List<Control>();
			this.onAdd = onAdd;
			this.onRemove = onRemove;
		}

		public IEnumerator<Control> GetEnumerator()
		{
			return this.controls.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Add(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			if (control.Parent != null && control.Parent is IControlContainer)
				((IControlContainer)control.Parent).Controls.Remove(control);

			control.Parent = this.parent;
			this.controls.Add(control);

			if (this.onAdd != null)
				this.onAdd(control);
		}

		public void Remove(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			if (control.Parent != this.parent)
				throw new InvalidOperationException("The given Control does not belong to the ControlCollection.");

			if (this.controls.Remove(control))
			{
				control.Parent = null;

				if (this.onRemove != null)
					this.onRemove(control);
			}
		}

		public void Update(TimingState time, IControlInputHandler input)
		{
			foreach (Control control in this.controls)
				control.Update(time, input);
		}

		public void Draw(IControlRenderer renderer)
		{
			foreach (Control control in this.controls)
				control.Draw(renderer);
		}
	}
}
