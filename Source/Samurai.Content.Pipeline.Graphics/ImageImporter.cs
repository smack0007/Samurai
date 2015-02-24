using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline.Graphics
{
	[ContentImporter(new string[] { ".png" })]
    public class ImageImporter : ContentImporter<ImageData>
    {
		public ImageImporter()
		{
		}

		public override ImageData Import(string fileName, ContentImporterContext context)
		{
			Bitmap bitmap = (Bitmap)Bitmap.FromFile(fileName);

			byte[] bytes = new byte[bitmap.Width * bitmap.Height * 4];
			Color4[] pixels = new Color4[bitmap.Width * bitmap.Height];

			BitmapData bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Marshal.Copy(bitmapData.Scan0, bytes, 0, bytes.Length);
			bitmap.UnlockBits(bitmapData);

			// Pixel format for little-endian machines is [B][G][R][A]. We need to convert to [R][G][B][A].
			// http://stackoverflow.com/questions/8104461/pixelformat-format32bppargb-seems-to-have-wrong-byte-order

			int j = 0;
			for (int i = 0; i < bytes.Length; i += 4)
			{
				pixels[j] = new Color4(bytes[i + 2], bytes[i + 1], bytes[i], bytes[i + 3]);
				j++;
			}

			return new ImageData(bitmap.Width, bitmap.Height, pixels);
		}
	}
}
