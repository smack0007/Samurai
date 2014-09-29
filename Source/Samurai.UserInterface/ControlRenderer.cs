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
		
		Stack<Rectangle> scissorStack;
		Rectangle? oldScissor;

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

			this.scissorStack = new Stack<Rectangle>();
		}

		public void Begin()
		{
			this.oldScissor = this.graphics.Scissor;
			this.spriteBatch.Begin(this.spriteBatchShader);
		}

		public void End()
		{
			this.spriteBatch.End();
			this.graphics.Scissor = this.oldScissor;
		}

		public void PushScissor(Rectangle scissor)
		{
			scissor.Y = this.graphics.Viewport.Height - scissor.Y - scissor.Height;

			this.graphics.Scissor = scissor;
			this.scissorStack.Push(scissor);
		}

		public void PopScissor()
		{
			if (this.scissorStack.Count == 0)
				throw new InvalidOperationException("Scissor stack is empty.");

			Rectangle scissor = this.scissorStack.Pop();
			this.graphics.Scissor = scissor;
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
