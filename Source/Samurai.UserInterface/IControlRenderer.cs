using System;
using Samurai.Graphics;

namespace Samurai.UserInterface
{
	public interface IControlRenderer
	{
		void DrawString(TextureFont font, string text, Vector2 position, Color4 color);
	}
}
