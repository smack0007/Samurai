using System;

namespace Samurai
{
	public class BlendState
	{
		public static readonly BlendState Disabled = new BlendState(
			enabled: false,
			sourceFactor: SourceBlendFactor.One,
			destinationFactor: DestinationBlendFactor.Zero
		);

		public static readonly BlendState AlphaBlend = new BlendState(
			enabled: true,
			sourceFactor: SourceBlendFactor.SourceAlpha,
			destinationFactor: DestinationBlendFactor.OneMinusSourceAlpha
		);

		public bool Enabled
		{
			get;
			private set;
		}

		public SourceBlendFactor SourceFactor
		{
			get;
			private set;
		}

		public DestinationBlendFactor DestinationFactor
		{
			get;
			private set;
		}

		public BlendState(
			bool enabled = false,
			SourceBlendFactor sourceFactor = SourceBlendFactor.One,
			DestinationBlendFactor destinationFactor = DestinationBlendFactor.Zero)
		{
			this.Enabled = enabled;
			this.SourceFactor = sourceFactor;
			this.DestinationFactor = destinationFactor;
		}
	}
}
