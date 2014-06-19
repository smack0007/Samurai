using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Samurai
{
	public class Texture : DisposableObject
	{
		GraphicsContext graphics;

		internal uint Index
		{
			get;
			private set;
		}

		internal uint Handle
		{
			get;
			private set;
		}
		
		public TextureFilter MinFilter
		{
			get;
			private set;
		}

		public TextureFilter MagFilter
		{
			get;
			private set;
		}

		public TextureWrap WrapS
		{
			get;
			private set;
		}

		public TextureWrap WrapT
		{
			get;
			private set;
		}

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

		private Texture(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.graphics = graphics;
			this.Index = this.graphics.AllocateTextureIndex();

			this.Handle = GL.GenTexture();
		}

		protected override void DisposeManagedResources()
		{
			if (!this.graphics.IsDisposed)
				this.graphics.DeallocateTextureIndex(this.Index);
		}

		protected override void DisposeUnmanagedResources()
		{
			GL.DeleteTexture(this.Handle);
		}

		public static Texture FromFile(GraphicsContext graphics, string fileName, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (fileName == null)
				throw new ArgumentNullException("fileName");

			using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				return FromStream(graphics, file, parameters);
		}

		public static Texture FromStream(GraphicsContext graphics, Stream stream, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (stream == null)
				throw new ArgumentNullException("stream");

			Texture texture = new Texture(graphics);
			GL.ActiveTexture(GL.Texture0 + texture.Index);
			GL.BindTexture(GL.Texture2D, texture.Handle);

			Bitmap bitmap = (Bitmap)Bitmap.FromStream(stream);

			texture.Width = bitmap.Width;
			texture.Height = bitmap.Height;

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

				if (parameters.ColorKey != null)
				{
					uint pixel = GLHelper.MakePixelRGBA(bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3]);

					if (pixel == parameters.ColorKey.Value.ToUint())
						GLHelper.DecomposePixelRGBA(parameters.TransparentPixel.ToUint(), out bytes[i], out bytes[i + 1], out bytes[i + 2], out bytes[i + 3]);
				}
			}

			GCHandle bytesPtr = GCHandle.Alloc(bytes, GCHandleType.Pinned);

			try
			{
				GL.TexImage2D(
					GL.Texture2D,
					0,
					(int)GL.Rgba,
					bitmap.Width,
					bitmap.Height,
					0,
					GL.Rgba,
					(int)GL.UnsignedByte,
					bytesPtr.AddrOfPinnedObject());
			}
			finally
			{
				bytesPtr.Free();
			}

			GL.TexParameteri(GL.Texture2D, GL.TextureMagFilter, (int)parameters.MagFilter);
			GL.TexParameteri(GL.Texture2D, GL.TextureMinFilter, (int)parameters.MinFilter);
			GL.TexParameteri(GL.Texture2D, GL.TextureWrapS, (int)parameters.WrapS);
			GL.TexParameteri(GL.Texture2D, GL.TextureWrapT, (int)parameters.WrapT);

			return texture;
		}
	}
}
