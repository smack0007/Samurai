using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public class TextureParams
	{
		/// <summary>
		/// A color in the source image which should be replaced with transparent pixels.
		/// </summary>
		public uint? ColorKey
		{
			get;
			set;
		}

		/// <summary>
		/// The pixel to use for transparency when color keying.
		/// </summary>
		public uint TransparentPixel
		{
			get;
			set;
		}

		public TextureFilter MagFilter
		{
			get;
			set;
		}

		public TextureFilter MinFilter
		{
			get;
			set;
		}

		public TextureWrap WrapS
		{
			get;
			set;
		}

		public TextureWrap WrapT
		{
			get;
			set;
		}

		public TextureParams()
		{
			this.MagFilter = TextureFilter.Linear;
			this.MinFilter = TextureFilter.Linear;
			this.WrapS = TextureWrap.Clamp;
			this.WrapT = TextureWrap.Clamp;
		}
	}
}
