using System;

namespace Samurai.Graphics
{
	public class BlendState
	{
		public static readonly BlendState Disabled = new BlendState(
			enabled: false,
			sourceBlendFactor: SourceBlendFactor.One,
			destinationBlendFactor: DestinationBlendFactor.Zero
		);

		public static readonly BlendState AlphaBlend = new BlendState(
			enabled: true,
			sourceBlendFactor: SourceBlendFactor.SourceAlpha,
			destinationBlendFactor: DestinationBlendFactor.OneMinusSourceAlpha
		);

		public bool Enabled
		{
			get;
			private set;
		}

		public SourceBlendFactor SourceBlendFactor
		{
			get;
			private set;
		}

		public DestinationBlendFactor DestinationBlendFactor
		{
			get;
			private set;
		}

		public BlendState(
			bool enabled = false,
			SourceBlendFactor sourceBlendFactor = SourceBlendFactor.One,
			DestinationBlendFactor destinationBlendFactor = DestinationBlendFactor.Zero)
		{
			this.Enabled = enabled;
			this.SourceBlendFactor = sourceBlendFactor;
			this.DestinationBlendFactor = destinationBlendFactor;
		}
	}
}
