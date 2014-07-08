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

		public static Texture Load(GraphicsContext graphics, string fileName, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (fileName == null)
				throw new ArgumentNullException("fileName");

			using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				return Load(graphics, file, parameters);
		}

		public static Texture Load(GraphicsContext graphics, Stream stream, TextureParams parameters)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			if (stream == null)
				throw new ArgumentNullException("stream");
						
			using (Bitmap bitmap = (Bitmap)Bitmap.FromStream(stream))
			{
				byte[] bytes = BitmapHelper.GetBytes(bitmap);
				return Load(graphics, bytes, bitmap.Width, bitmap.Height, parameters);
			}
		}

		public static Texture Load(GraphicsContext graphics, byte[] bytes, int width, int height, TextureParams parameters)
		{
			Texture texture = new Texture(graphics);
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

			GCHandle bytesPtr = GCHandle.Alloc(bytes, GCHandleType.Pinned);

			try
			{
				graphics.GL.TexImage2D(
					GLContext.Texture2D,
					0,
					(int)GLContext.Rgba,
					width,
					height,
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
