using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Samurai.Graphics
{
	public class Texture : GraphicsObject
	{
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
			: base(graphics)
		{
			this.Index = this.Graphics.AllocateTextureIndex();

			this.Handle = this.Graphics.GL.GenTexture();
		}

		protected override void DisposeManagedResources()
		{
			if (!this.Graphics.IsDisposed)
				this.Graphics.DeallocateTextureIndex(this.Index);
		}

		protected override void DisposeUnmanagedResources()
		{
			this.Graphics.GL.DeleteTexture(this.Handle);
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
			graphics.GL.ActiveTexture(GLContext.Texture0 + texture.Index);
			graphics.GL.BindTexture(GLContext.Texture2D, texture.Handle);

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
				graphics.GL.TexImage2D(
					GLContext.Texture2D,
					0,
					(int)GLContext.Rgba,
					bitmap.Width,
					bitmap.Height,
					0,
					GLContext.Rgba,
					(int)GLContext.UnsignedByte,
					bytesPtr.AddrOfPinnedObject());
			}
			finally
			{
				bytesPtr.Free();
			}

			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureMagFilter, (int)parameters.MagFilter);
			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureMinFilter, (int)parameters.MinFilter);
			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureWrapS, (int)parameters.WrapS);
			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureWrapT, (int)parameters.WrapT);

			return texture;
		}
	}
}
