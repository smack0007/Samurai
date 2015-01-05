using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	/// <summary>
	/// Contains helper methods for System.Drawing.Bitmap.
	/// </summary>
	internal static class BitmapHelper
	{
		public static byte[] GetBytes(Bitmap bitmap)
		{
			byte[] bytes = new byte[bitmap.Width * bitmap.Height * 4];

			BitmapData bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Marshal.Copy(bitmapData.Scan0, bytes, 0, bytes.Length);
			bitmap.UnlockBits(bitmapData);

			// Pixel format for little-endian machines is [B][G][R][A]. We need to convert to [R][G][B][A].
			// http://stackoverflow.com/questions/8104461/pixelformat-format32bppargb-seems-to-have-wrong-byte-order

			for (int i = 0; i < bytes.Length; i += 4)
			{
				byte b = bytes[i];
				bytes[i] = bytes[i + 2];
				bytes[i + 2] = b;
			}

			return bytes;
		}
	}
}
