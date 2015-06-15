using System;
using System.Drawing;
using System.IO;

namespace Samurai.Graphics
{
	public class Texture2D : Texture
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

		protected override int PixelCount
		{
			get { return this.Width * this.Height; }
		}

		private Texture2D(GraphicsContext graphics)
			: base(graphics)
		{
		}

		public Texture2D(
			GraphicsContext graphics,
			int width,
			int height)
			: base(graphics)
		{
			graphics.GL.ActiveTexture(GLContext.Texture0 + this.Index);
			graphics.GL.BindTexture(GLContext.Texture2D, this.Handle);

			graphics.GL.TexImage2D(
				GLContext.Texture2D,
				0,
				(int)GLContext.Rgba8,
				width,
				height,
				0,
				GLContext.Rgba,
				(int)GLContext.UnsignedByte,
				null);

			this.Width = width;
			this.Height = height;
		}

		public static Texture2D LoadFromFile(GraphicsContext graphics, string fileName, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (fileName == null)
				throw new ArgumentNullException("fileName");

			using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				return LoadFromStream(graphics, file, parameters);
		}

		public static Texture2D LoadFromStream(GraphicsContext graphics, Stream stream, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (stream == null)
				throw new ArgumentNullException("stream");

			using (Bitmap bitmap = (Bitmap)Bitmap.FromStream(stream))
			{
				byte[] bytes = BitmapHelper.GetBytes(bitmap);
				return LoadFromBytes(graphics, bytes, bitmap.Width, bitmap.Height, parameters);
			}
		}

		public static Texture2D LoadFromBytes(GraphicsContext graphics, byte[] bytes, int width, int height, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (bytes == null)
				throw new ArgumentNullException("bytes");

			Texture2D texture = new Texture2D(graphics);

			Texture.Initialize(texture, graphics, GLContext.Texture2D, bytes, parameters);

			graphics.GL.TexImage2D(
				GLContext.Texture2D,
				0,
				(int)GLContext.Rgba8,
				width,
				height,
				0,
				GLContext.Rgba,
				(int)GLContext.UnsignedByte,
				bytes);

			texture.Width = width;
			texture.Height = height;

			return texture;
		}
	}
}
