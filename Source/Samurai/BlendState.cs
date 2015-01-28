using System;

namespace Samurai
{
	public class BlendState : GraphicsState
	{		
		bool enabled = false;
		SourceBlendFactor sourceFactor = SourceBlendFactor.One;
		DestinationBlendFactor destinationFactor = DestinationBlendFactor.Zero;

		public bool Enabled
		{
			get { return this.enabled; }

			set
			{
				if (value != this.enabled)
				{
					this.enabled = value;

					if (this.Graphics != null)
						this.ApplyBlendCap();
				}
			}
		}

		public SourceBlendFactor SourceFactor
		{
			get { return this.sourceFactor; }
			
			set
			{
				if (value != this.sourceFactor)
				{
					this.sourceFactor = value;
					
					if (this.Graphics != null)
						this.ApplyBlendFunc();
				}
			}
		}

		public DestinationBlendFactor DestinationFactor
		{
			get { return this.destinationFactor; }
			
			set
			{
				if (value != this.destinationFactor)
				{
					this.destinationFactor = value;

					if (this.Graphics != null)
						this.ApplyBlendFunc();
				}
			}
		}

		public BlendState()
			: base()
		{
		}

		protected override void Apply()
		{
			this.ApplyBlendCap();
			this.ApplyBlendFunc();			
		}

		private void ApplyBlendCap()
		{
			this.Graphics.ToggleCap(GLContext.BlendCap, this.Enabled);
		}

		private void ApplyBlendFunc()
		{
			this.Graphics.GL.BlendFunc((uint)this.SourceFactor, (uint)this.DestinationFactor);
		}
	}
}
