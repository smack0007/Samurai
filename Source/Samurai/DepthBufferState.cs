using System;

namespace Samurai
{
	public class DepthBufferState : GraphicsState
	{
		public static readonly DepthBufferState Disabled = new DepthBufferState();

		public static readonly DepthBufferState LessThanOrEqual = new DepthBufferState()
		{
			Enabled = true,
			Function = DepthFunction.LessThanOrEqual
		};

		bool enabled = false;
		DepthFunction function = DepthFunction.Less;
		bool writeEnabled = true;
		
		public bool Enabled
		{
			get { return this.enabled; }
			
			set
			{
				this.EnsureNotFrozen();
				this.enabled = value;
			}
		}

		public DepthFunction Function
		{
			get { return this.function; }
			
			set
			{
				this.EnsureNotFrozen();
				this.function = value;
			}
		}

		public bool WriteEnabled
		{
			get { return this.writeEnabled; }

			set
			{
				this.EnsureNotFrozen();
				this.writeEnabled = value;
			}
		}

		public DepthBufferState()
		{
		}
	}
}
