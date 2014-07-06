using System;

namespace Samurai.Graphics
{
	/// <summary>
	/// Contains possible values for the destination factor of the blend function.
	/// </summary>
	public enum DestinationBlendFactor : uint
	{
		DestinationAlpha = GLContext.DstAlpha,
				
		One = GLContext.One,
				
		OneMinusDestinationAlpha = GLContext.OneMinusDstAlpha,

		OneMinusSourceAlpha = GLContext.OneMinusSrcAlpha,
				
		OneMinusSourceColor = GLContext.OneMinusSrcColor,
				
		SourceAlpha = GLContext.SrcAlpha,
				
		SourceColor = GLContext.SrcColor,

		Zero = GLContext.Zero,
	}
}
