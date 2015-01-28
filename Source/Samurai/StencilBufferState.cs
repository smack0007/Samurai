using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public class StencilBufferState : GraphicsState
	{
		public static readonly StencilBufferState Disabled = new StencilBufferState();

		bool stencilBufferEnabled = false;
		StencilFunction stencilFunction = StencilFunction.Always;
		int stencilReferenceValue = 0;
		uint stencilMask = 0;
		StencilOperation stencilFail = StencilOperation.Keep;
		StencilOperation stencilDepthFail = StencilOperation.Keep;
		StencilOperation stencilPass = StencilOperation.Keep;
		uint stencilWriteMask = 0xFFFFFFFF;

		public bool StencilBufferEnabled
		{
			get { return this.stencilBufferEnabled; }

			set
			{
				if (value != this.stencilBufferEnabled)
				{
					this.stencilBufferEnabled = value;
					this.ApplyStencilTestCap();
				}
			}
		}

		public StencilFunction StencilFunction
		{
			get { return this.stencilFunction; }

			set
			{
				if (value != this.stencilFunction)
				{
					this.stencilFunction = value;
					this.ApplyStencilFunc();
				}
			}
		}

		public int StencilReferenceValue
		{
			get { return this.stencilReferenceValue; }

			set
			{
				if (value != this.stencilReferenceValue)
				{
					this.stencilReferenceValue = value;
					this.ApplyStencilFunc();
				}
			}
		}

		public uint StencilMask
		{
			get { return this.stencilMask; }

			set
			{
				if (value != this.stencilMask)
				{
					this.stencilMask = value;
					this.ApplyStencilFunc();
				}
			}
		}

		public StencilOperation StencilFail
		{
			get { return this.stencilFail; }

			set
			{
				if (value != this.stencilFail)
				{
					this.stencilFail = value;
					this.ApplyStencilOp();
				}
			}
		}

		public StencilOperation StencilDepthFail
		{
			get { return this.stencilDepthFail; }

			set
			{
				if (value != this.stencilDepthFail)
				{
					this.stencilDepthFail = value;
					this.ApplyStencilOp();
				}
			}
		}

		public StencilOperation StencilPass
		{
			get { return this.stencilPass; }

			set
			{
				if (value != this.stencilPass)
				{
					this.stencilPass = value;
					this.ApplyStencilOp();
				}
			}
		}

		public uint StencilWriteMask
		{
			get { return this.stencilWriteMask; }

			set
			{
				if (value != this.stencilWriteMask)
				{
					this.stencilWriteMask = value;
					this.ApplyStencilWriteMask();
				}
			}
		}

		public StencilBufferState()
			: base()
		{
		}

		protected override void Apply()
		{
			this.ApplyStencilTestCap();
			this.ApplyStencilFunc();
			this.ApplyStencilOp();
			this.ApplyStencilWriteMask();
		}

		private void ApplyStencilTestCap()
		{
			this.Graphics.ToggleCap(GLContext.StencilTestCap, this.StencilBufferEnabled);
		}

		private void ApplyStencilFunc()
		{
			this.Graphics.GL.StencilFunc((uint)this.StencilFunction, this.StencilReferenceValue, this.StencilMask);
		}

		private void ApplyStencilOp()
		{
			this.Graphics.GL.StencilOp((uint)this.StencilFail, (uint)this.StencilDepthFail, (uint)this.StencilPass);
		}

		private void ApplyStencilWriteMask()
		{
			this.Graphics.GL.StencilMask(this.StencilWriteMask);
		}
	}
}
