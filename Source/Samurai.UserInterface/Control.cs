using System;
using System.Collections.Generic;
using Samurai.Graphics;
using Samurai.Graphics.Sprites;
using Samurai.Input;

namespace Samurai.UserInterface
{
	public abstract class Control
	{
		public static readonly ControlProperty<Color4> BackgroundColorProperty =
			ControlProperty<Color4>.Create(defaultValue: Color4.Transparent);

		public static readonly ControlProperty<Color4> ForegroundColorProperty =
			ControlProperty<Color4>.Create(defaultValue: Color4.White);

		public static readonly ControlProperty<TextureFont> FontProperty =
			ControlProperty<TextureFont>.Create();

		public static readonly ControlProperty<Vector2> PositionProperty =
			ControlProperty<Vector2>.Create();

		public static readonly ControlProperty<Size> SizeProperty =
			ControlProperty<Size>.Create();

		ControlPropertyStore values;
		bool containsCursor;

		public Control Parent
		{
			get;
			internal set;
		}
		
		public Color4 BackgroundColor
		{
			get { return GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}

		public Color4 ForegroundColor
		{
			get { return GetValue(ForegroundColorProperty); }
			set { SetValue(ForegroundColorProperty, value); }
		}

		public TextureFont Font
		{
			get { return GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		public Vector2 Position
		{
			get { return GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public Size Size
		{
			get { return GetValue(SizeProperty); }
			set { SetValue(SizeProperty, value); }
		}

		public Rectangle Rectangle
		{
			get
			{
				Vector2 position = this.Position;
				Size size = this.Size;

				return new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height);
			}
		}

		public bool ContainsCursor
		{
			get { return this.containsCursor; }

			set
			{
				if (value != this.containsCursor)
				{
					this.containsCursor = value;

					if (this.containsCursor)
					{
						this.OnCursorEnter(EventArgs.Empty);
					}
					else
					{
						this.OnCursorLeave(EventArgs.Empty);
					}
				}
			}
		}

		public event EventHandler CursorEnter;

		public event EventHandler CursorLeave;

		public Control()
		{
			this.values = new ControlPropertyStore();
		}
				
		public T GetValue<T>(ControlProperty<T> property)
		{
			if (property == null)
				throw new ArgumentNullException("property");

			T controlValue;
			bool controlHasValue = this.values.TryGetValue(property, out controlValue);
			
			T parentValue = default(T);
			bool parentHasValue = false;

			if (this.Parent != null)
			{
				parentHasValue = this.Parent.values.TryGetValue(property, out parentValue);
			}

			return property.ComputeFunc(property, controlValue, controlHasValue, parentValue, parentHasValue);
		}

		public void SetValue<T>(ControlProperty<T> property, T value)
		{
			if (this.values.SetValue(property, value))
				this.OnPropertyChanged(property);
		}

		protected virtual void OnPropertyChanged(ControlProperty property)
		{
			if (property == BackgroundColorProperty)
			{
				this.OnBackgroundColorChanged(EventArgs.Empty);
			}
			else if (property == ForegroundColorProperty)
			{
				this.OnForegroundColorChanged(EventArgs.Empty);
			}
			else if (property == FontProperty)
			{
				this.OnFontChanged(EventArgs.Empty);
			}
			else if (property == PositionProperty)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
			else if (property == SizeProperty)
			{
				this.OnSizeChanged(EventArgs.Empty);
			}
		}

		protected virtual void OnBackgroundColorChanged(EventArgs e)
		{
		}

		protected virtual void OnForegroundColorChanged(EventArgs e)
		{
		}

		protected virtual void OnFontChanged(EventArgs e)
		{
		}

		protected virtual void OnPositionChanged(EventArgs e)
		{
		}

		protected virtual void OnSizeChanged(EventArgs e)
		{
		}

		public void Update(TimingState time, IControlInputHandler input)
		{									
			this.UpdateControl(time, input);
		}

		protected virtual void UpdateControl(TimingState time, IControlInputHandler input)
		{
			if (time == null)
				throw new ArgumentNullException("time");

			if (input == null)
				throw new ArgumentNullException("input");

			Rectangle rectangle = this.Rectangle;
			this.ContainsCursor = rectangle.Contains(input.CursorPosition);
		}

		protected virtual void OnCursorEnter(EventArgs e)
		{
			if (this.CursorEnter != null)
				this.CursorEnter(this, e);
		}

		protected virtual void OnCursorLeave(EventArgs e)
		{
			if (this.CursorLeave != null)
				this.CursorLeave(this, e);
		}

		public void Draw(IControlRenderer renderer)
		{
			if (renderer == null)
				throw new ArgumentNullException("renderer");

			renderer.PushScissor(this.Rectangle);

			this.DrawControl(renderer);

			renderer.PopScissor();
		}

		protected virtual void DrawControl(IControlRenderer renderer)
		{
		}
	}
}
