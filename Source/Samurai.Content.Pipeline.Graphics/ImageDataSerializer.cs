using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline.Graphics
{
	[ContentSerializer(typeof(ImageData))]
	public class ImageDataSerializer : ContentSerializer<ImageData>
	{
		public bool Compress
		{
			get;
			set;
		}

		public override void Serialize(ImageData content, ContentWriter writer)
		{
			writer.Write(content.Width);
			writer.Write(content.Height);
			writer.Write(this.Compress);

			if (!this.Compress)
			{
				writer.Write(content.Width * content.Height * 4);

				for (int i = 0; i < content.Pixels.Length; i++)
				{
					Color4 pixel = content.Pixels[i];
					writer.Write(pixel.R);
					writer.Write(pixel.G);
					writer.Write(pixel.B);
					writer.Write(pixel.A);
				}
			}
			else
			{
				using (MemoryStream ms = new MemoryStream())
				{
					using (DeflateStream deflate = new DeflateStream(ms, CompressionLevel.Optimal))
					using (BinaryWriter br = new BinaryWriter(deflate))
					{
						for (int i = 0; i < content.Pixels.Length; i++)
						{
							Color4 pixel = content.Pixels[i];
							br.Write(pixel.R);
							br.Write(pixel.G);
							br.Write(pixel.B);
							br.Write(pixel.A);
						}
					}

					byte[] bytes = ms.ToArray();
					writer.Write(bytes.Length);
					writer.Write(bytes);
				}
			}
		}
	}
}
