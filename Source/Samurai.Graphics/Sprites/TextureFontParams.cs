using System;

namespace Samurai.Graphics.Sprites
{
	public class TextureFontParams
	{
		public static readonly TextureFontParams Default = new TextureFontParams();

		public char MinChar
		{
			get;
			set;
		}
		
		public char MaxChar
		{
			get;
			set;
		}

		public TextureFontStyle Style
		{
			get;
			set;
		}

		public bool Antialias
		{
			get;
			set;
		}

		public Color4 Color
		{
			get;
			set;
		}

		public Color4 BackgroundColor
		{
			get;
			set;
		}

		public Color4? ColorKey
		{
			get;
			set;
		}

		public TextureFontParams()
		{
			this.MinChar = (char)32;
			this.MaxChar = (char)127;
			this.Antialias = true;
			this.Color = Color4.White;
			this.BackgroundColor = Color4.Black;
		}
	}
}
