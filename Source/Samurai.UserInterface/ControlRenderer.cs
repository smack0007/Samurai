using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samurai.Graphics;

namespace Samurai.UserInterface
{
	public class ControlRenderer : IControlRenderer
	{
		GraphicsContext graphics;
		SpriteBatch spriteBatch;
		BasicSpriteBatchShaderProgram spriteBatchShader;

		public TextureFont DefaultFont
		{
			get;
			set;
		}

		public ControlRenderer(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.graphics = graphics;
			this.spriteBatch = new SpriteBatch(this.graphics);
			this.spriteBatchShader = new BasicSpriteBatchShaderProgram(this.graphics);
		}

		public void Begin()
		{
			this.spriteBatch.Begin(this.spriteBatchShader);
		}

		public void End()
		{
			this.spriteBatch.End();
		}

		public void DrawString(TextureFont font, string text, Vector2 position, Color4 color)
		{
			if (font == null)
				font = this.DefaultFont;

			if (font == null)
				throw new SamuraiException("No font set on control and no default font provided.");

			this.spriteBatch.DrawString(font, text, position, color);
		}
	}
}
