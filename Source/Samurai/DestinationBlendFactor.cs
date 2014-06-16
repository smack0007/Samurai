using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public enum DestinationBlendFactor : uint
	{
		DestinationAlpha = GL.DstAlpha,
				
		One = GL.One,
				
		OneMinusDestinationAlpha = GL.OneMinusDstAlpha,

		OneMinusSourceAlpha = GL.OneMinusSrcAlpha,
				
		OneMinusSourceColor = GL.OneMinusSrcColor,
				
		SourceAlpha = GL.SrcAlpha,
				
		SourceColor = GL.SrcColor,

		Zero = GL.Zero,
	}
}
