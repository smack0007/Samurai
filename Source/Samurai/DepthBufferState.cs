using System;

namespace Samurai
{
	public class DepthBufferState : GraphicsState
	{
		bool enabled = false;
		DepthFunction function = DepthFunction.Less;
		bool writeEnabled = true;
		
		public bool Enabled
		{
			get { return this.enabled; }
			
			set
			{
				if (value != this.enabled)
				{
					this.enabled = value;
					this.ApplyDepthTestCap();
				}
			}
		}

		public DepthFunction Function
		{
			get { return this.function; }
			
			set
			{
				if (value != this.function)
				{
					this.function = value;
					this.ApplyDepthFunc();
				}
			}
		}

		public bool WriteEnabled
		{
			get { return this.writeEnabled; }

			set
			{
				this.writeEnabled = value;
				this.ApplyDepthMask();
			}
		}

		public DepthBufferState()
		{
		}

		protected override void Apply()
		{
			this.ApplyDepthTestCap();
			this.ApplyDepthFunc();
			this.ApplyDepthMask();
		}

		private void ApplyDepthTestCap()
		{
			this.Graphics.ToggleCap(GLContext.DepthTestCap, this.Enabled);
		}

		private void ApplyDepthFunc()
		{
			this.Graphics.GL.DepthFunc((uint)this.Function);
		}

		private void ApplyDepthMask()
		{
			this.Graphics.GL.DepthMask(this.WriteEnabled);
		}
	}
}
