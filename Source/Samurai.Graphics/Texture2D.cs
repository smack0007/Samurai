using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Samurai.Graphics
{
	public class Texture2D : GraphicsObject
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

		private Texture2D(GraphicsContext graphics)
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
			Texture2D texture = new Texture2D(graphics);
			graphics.GL.ActiveTexture(GLContext.Texture0 + texture.Index);
			graphics.GL.BindTexture(GLContext.Texture2D, texture.Handle);

			texture.Width = width;
			texture.Height = height;

			if (parameters.ColorKey != null)
			{
				for (int i = 0; i < bytes.Length; i += 4)
				{	
					uint pixel = GLHelper.MakePixelRGBA(bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3]);

					if (pixel == parameters.ColorKey.Value.ToRgba())
						GLHelper.DecomposePixelRGBA(parameters.TransparentPixel.ToRgba(), out bytes[i], out bytes[i + 1], out bytes[i + 2], out bytes[i + 3]);
				}
			}
						
			graphics.GL.TexImage2D(
				GLContext.Texture2D,
				0,
				(int)GLContext.Rgba,
				width,
				height,
				0,
				GLContext.Rgba,
				(int)GLContext.UnsignedByte,
				bytes);

			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureMagFilter, (int)parameters.MagFilter);
			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureMinFilter, (int)parameters.MinFilter);
			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureWrapS, (int)parameters.WrapS);
			graphics.GL.TexParameteri(GLContext.Texture2D, GLContext.TextureWrapT, (int)parameters.WrapT);

			return texture;
		}

		public byte[] GetBytes()
		{
			byte[] bytes = new byte[this.Width * this.Height * 4];
			this.Graphics.GL.GetTexImage(GLContext.Texture2D, 0, GLContext.Rgba, (int)GLContext.UnsignedByte, bytes);
			return bytes;
		}

		public Color4[] GetPixels()
		{
			byte[] bytes = this.GetBytes();
			Color4[] pixels = new Color4[this.Width * this.Height];

			GCHandle pixelsPtr = GCHandle.Alloc(pixels, GCHandleType.Pinned);

			try
			{
				Marshal.Copy(bytes, 0, pixelsPtr.AddrOfPinnedObject(), bytes.Length);
			}
			finally
			{
				pixelsPtr.Free();
			}
						
			return pixels;
		}
	}
}
