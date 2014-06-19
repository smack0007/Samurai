using System;

namespace Samurai
{
	/// <summary>
	/// Contains possible values for the destination factor of the blend function.
	/// </summary>
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
