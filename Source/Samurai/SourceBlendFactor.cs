using System;

namespace Samurai
{
	/// <summary>
	/// Contains possible values for the source factor of the blend function.
	/// </summary>
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
