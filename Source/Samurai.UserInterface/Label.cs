using System;
using Samurai.Graphics;

namespace Samurai.UserInterface
{
	public class Label : Control
	{
		public static readonly ControlProperty<string> TextProperty =
			ControlProperty<string>.Create(computeFunc: ControlProperty<string>.IgnoreParentComputeFunc, defaultValue: string.Empty);

		public string Text
		{
			get { return GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public Label()
			: base()
		{
		}
								
		public override void Draw(IControlRenderer renderer)
		{
			if (renderer == null)
				throw new ArgumentNullException("renderer");
						
			if (!string.IsNullOrEmpty(this.Text))
				renderer.DrawString(this.Font, this.Text, this.Position, this.ForegroundColor);
		}
	}
}
