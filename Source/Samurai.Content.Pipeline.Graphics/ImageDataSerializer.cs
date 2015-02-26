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
		}
	}
}
