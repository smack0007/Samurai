using System;

namespace Samurai.Graphics
{
	/// <summary>
	/// Contains possible values for the source factor of the blend function.
	/// </summary>
	public enum SourceBlendFactor : uint	
	{
		DestinationAlpha = GLContext.DstAlpha,
				
		DestinationColor = GLContext.DstColor,
		
		One = GLContext.One,
				
		OneMinusDestinationAlpha = GLContext.OneMinusDstAlpha,
				
		OneMinusDestinationColor = GLContext.OneMinusDstColor,
				
		OneMinusSourceAlpha = GLContext.OneMinusSrcAlpha,
				
		SourceAlpha = GLContext.SrcAlpha,
		
		SourceAlphaSaturate = GLContext.SrcAlphaSaturate,

		Zero = 0,
	}
}
