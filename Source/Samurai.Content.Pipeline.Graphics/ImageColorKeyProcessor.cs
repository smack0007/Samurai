using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline.Graphics
{
	[ContentProcessor("ImageColorKey")]
	public class ImageColorKeyProcessor : ContentProcessor<ImageData, ImageData>
	{
		public Color4 ColorKey
		{
			get;
			set;
		}

		public override ImageData Process(ImageData input, ContentProcessorContext context)
		{			
			return input;
		}
	}
}
