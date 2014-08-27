using System;
using Samurai.Graphics;

namespace Samurai.UserInterface
{
	public interface IControlRenderer
	{
		void PushScissor(Rectangle scissor);

		void PopScissor();

		void DrawString(TextureFont font, string text, Vector2 position, Color4 color);
	}
}
