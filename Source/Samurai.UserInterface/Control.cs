using System;
using System.Collections.Generic;
using Samurai.Graphics;
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
			ControlProperty<Vector2>.Create(computeFunc: ControlProperty<Vector2>.IgnoreParentComputeFunc);

		public static readonly ControlProperty<Size> SizeProperty =
			ControlProperty<Size>.Create();

		ControlPropertyStore values;

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
				this.OnBackgroundColorChanged();
			}
			else if (property == ForegroundColorProperty)
			{
				this.OnForegroundColorChanged();
			}
			else if (property == FontProperty)
			{
				this.OnFontChanged();
			}
			else if (property == PositionProperty)
			{
				this.OnPositionChanged();
			}
			else if (property == SizeProperty)
			{
				this.OnSizeChanged();
			}
		}

		protected virtual void OnForegroundColorChanged()
		{
		}

		protected virtual void OnBackgroundColorChanged()
		{
		}

		protected virtual void OnFontChanged()
		{
		}

		protected virtual void OnPositionChanged()
		{
		}

		protected virtual void OnSizeChanged()
		{
		}

		public virtual void Update(TimeSpan elapsed, IControlInputHandler input)
		{
		}

		public virtual void Draw(IControlRenderer renderer)
		{
		}
	}
}
