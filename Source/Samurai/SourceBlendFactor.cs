using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public enum SourceBlendFactor : uint	
	{
		DestinationAlpha = GL.DstAlpha,
				
		DestinationColor = GL.DstColor,
		
		One = GL.One,
				
		OneMinusDestinationAlpha = GL.OneMinusDstAlpha,
				
		OneMinusDestinationColor = GL.OneMinusDstColor,
				
		OneMinusSourceAlpha = GL.OneMinusSrcAlpha,
				
		SourceAlpha = GL.SrcAlpha,
		
		SourceAlphaSaturate = GL.SrcAlphaSaturate,

		Zero = 0,
	}
}
