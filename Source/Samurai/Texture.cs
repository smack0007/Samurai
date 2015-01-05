using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public abstract class Texture : GraphicsObject
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

		protected abstract int PixelCount
		{
			get;
		}

		internal Texture(GraphicsContext graphics)
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

		public byte[] GetBytes()
		{
			byte[] bytes = new byte[this.PixelCount * 4];
			this.Graphics.GL.GetTexImage(GLContext.Texture2D, 0, GLContext.Rgba, (int)GLContext.UnsignedByte, bytes);
			return bytes;
		}

		public Color4[] GetPixels()
		{
			byte[] bytes = this.GetBytes();
			Color4[] pixels = new Color4[this.PixelCount];

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

		internal static void Initialize<T>(T texture, GraphicsContext graphics, uint target, byte[] bytes, TextureParams parameters)
			where T : Texture
		{
			graphics.GL.ActiveTexture(GLContext.Texture0 + texture.Index);
			graphics.GL.BindTexture(target, texture.Handle);
			
			if (parameters.ColorKey != null)
			{
				for (int i = 0; i < bytes.Length; i += 4)
				{
					uint pixel = GLHelper.MakePixelRGBA(bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3]);

					if (pixel == parameters.ColorKey.Value.ToRgba())
						GLHelper.DecomposePixelRGBA(parameters.TransparentPixel.ToRgba(), out bytes[i], out bytes[i + 1], out bytes[i + 2], out bytes[i + 3]);
				}
			}

			graphics.GL.TexParameteri(target, GLContext.TextureMagFilter, (int)parameters.MagFilter);
			graphics.GL.TexParameteri(target, GLContext.TextureMinFilter, (int)parameters.MinFilter);
			graphics.GL.TexParameteri(target, GLContext.TextureWrapS, (int)parameters.WrapS);
			graphics.GL.TexParameteri(target, GLContext.TextureWrapT, (int)parameters.WrapT);
		}
	}
}
