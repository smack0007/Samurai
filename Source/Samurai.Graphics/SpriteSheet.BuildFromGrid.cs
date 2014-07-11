using System;
using System.Collections.Generic;

namespace Samurai.Graphics
{
	public sealed partial class SpriteSheet
	{
		public static SpriteSheet BuildFromGrid(Texture texture, Color4 gridColor)
		{
			List<Rectangle> frames = new List<Rectangle>();

			Color4[] pixels = texture.GetPixels();
			DoGridSearch(pixels, texture.Width, texture.Height, frames, gridColor);

			return new SpriteSheet(texture, frames);
		}

		private static bool DoesFrameAlreadyExist(List<Rectangle> frames, int x, int y, out Rectangle frame)
		{
			for (int i = 0; i < frames.Count; i++)
			{
				if (frames[i].Contains(x, y))
				{
					frame = frames[i];
					return true;
				}
			}

			frame = Rectangle.Empty;
			return false;
		}

		private static void DoGridSearch(Color4[] pixels, int width, int height, List<Rectangle> frames, Color4 gridColor)
		{
			int x2, y2;

			Color4 pixel;
			Color4 pixel2;

			Rectangle frame;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					pixel = pixels[y * width + x];

					if (pixel != gridColor)
					{
						if (!DoesFrameAlreadyExist(frames, x, y, out frame))
						{
							x2 = x;
							y2 = y;

							pixel2 = pixel;

							while (x2 < width - 1 && pixel2 != gridColor)
							{
								x2++;
								pixel2 = pixels[y2 * width + x2];
							}

							x2--;

							pixel2 = pixels[y2 * width + x2];

							while (y2 < height - 1 && pixel2 != gridColor)
							{
								y2++;
								pixel2 = pixels[y2 * width + x2];
							}

							y2--;

							frames.Add(new Rectangle(x, y, x2 - x + 1, y2 - y + 1));

							x = x2;
						}
						else
						{
							// If we found a frame already, we can for sure go to the far right of it.
							x = frame.Right;
						}
					}
				}
			}
		}
	}
}
