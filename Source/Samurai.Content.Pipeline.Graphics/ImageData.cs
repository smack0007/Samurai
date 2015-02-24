using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline.Graphics
{
	public class ImageData
	{
		public int Width
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		public Color4[] Pixels
		{
			get;
			private set;
		}

		public ImageData(int width, int height, Color4[] pixels)
		{
			this.Width = width;
			this.Height = height;
			this.Pixels = pixels;
		}
	}
}
