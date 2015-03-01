using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline.Graphics
{
	[ContentSerializer(typeof(ImageData))]
	public class ImageDataSerializer : ContentSerializer<ImageData>
	{
		public override void Serialize(ImageData content, ContentWriter writer)
		{
			writer.Write(content.Width);
			writer.Write(content.Height);

			for (int i = 0; i < content.Pixels.Length; i++)
			{
				Color4 pixel = content.Pixels[i];
				writer.Write(pixel.R);
				writer.Write(pixel.G);
				writer.Write(pixel.B);
				writer.Write(pixel.A);
			}
		}
	}
}
