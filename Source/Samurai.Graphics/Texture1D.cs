using System;
using System.Drawing;
using System.IO;

namespace Samurai.Graphics
{
	public class Texture1D : Texture
	{
		public int Length
		{
			get;
			private set;
		}

		protected override int PixelCount
		{
			get { return this.Length; }
		}

		private Texture1D(GraphicsContext graphics)
			: base(graphics)
		{
		}

		public static Texture1D LoadFromFile(GraphicsContext graphics, string fileName, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (fileName == null)
				throw new ArgumentNullException("fileName");

			using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				return LoadFromStream(graphics, file, parameters);
		}

		public static Texture1D LoadFromStream(GraphicsContext graphics, Stream stream, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (stream == null)
				throw new ArgumentNullException("stream");

			using (Bitmap bitmap = (Bitmap)Bitmap.FromStream(stream))
			{
				byte[] bytes = BitmapHelper.GetBytes(bitmap);
				return LoadFromBytes(graphics, bytes, bitmap.Width * bitmap.Height, parameters);
			}
		}

		public static Texture1D LoadFromBytes(GraphicsContext graphics, byte[] bytes, int length, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (bytes == null)
				throw new ArgumentNullException("bytes");

			Texture1D texture = new Texture1D(graphics);

			Texture.Initialize(texture, graphics, GLContext.Texture1D, bytes, parameters);

			graphics.GL.TexImage1D(
				GLContext.Texture1D,
				0,
				(int)GLContext.Rgba,
				length,
				0,
				GLContext.Rgba,
				(int)GLContext.UnsignedByte,
				bytes);

			texture.Length = length;

			return texture;
		}
	}
}
