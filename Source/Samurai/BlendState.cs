using System;

namespace Samurai
{
	public class BlendState : GraphicsState
	{
		public static readonly BlendState Disabled = new BlendState();

		public static readonly BlendState AlphaBlend = new BlendState()
		{
			Enabled = true,
			SourceFactor = SourceBlendFactor.SourceAlpha,
			DestinationFactor = DestinationBlendFactor.OneMinusSourceAlpha
		};

		bool enabled = false;
		SourceBlendFactor sourceFactor = SourceBlendFactor.One;
		DestinationBlendFactor destinationFactor = DestinationBlendFactor.Zero;

		public bool Enabled
		{
			get { return this.enabled; }

			set
			{
				this.EnsureNotFrozen();
				this.enabled = value;
			}
		}

		public SourceBlendFactor SourceFactor
		{
			get { return this.sourceFactor; }
			
			set
			{
				this.EnsureNotFrozen();
				this.sourceFactor = value;
			}
		}

		public DestinationBlendFactor DestinationFactor
		{
			get { return this.destinationFactor; }
			
			set
			{
				this.EnsureNotFrozen();
				this.destinationFactor = value;
			}
		}

		public BlendState()
			: base()
		{
		}
	}
}
