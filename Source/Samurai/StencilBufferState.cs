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
				this.EnsureNotFrozen();
				this.stencilBufferEnabled = value;
			}
		}

		public StencilFunction StencilFunction
		{
			get { return this.stencilFunction; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilFunction = value;
			}
		}

		public int StencilReferenceValue
		{
			get { return this.stencilReferenceValue; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilReferenceValue = value;
			}
		}

		public uint StencilMask
		{
			get { return this.stencilMask; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilMask = value;
			}
		}

		public StencilOperation StencilFail
		{
			get { return this.stencilFail; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilFail = value;
			}
		}

		public StencilOperation StencilDepthFail
		{
			get { return this.stencilDepthFail; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilDepthFail = value;
			}
		}

		public StencilOperation StencilPass
		{
			get { return this.stencilPass; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilPass = value;
			}
		}

		public uint StencilWriteMask
		{
			get { return this.stencilWriteMask; }

			set
			{
				this.EnsureNotFrozen();
				this.stencilWriteMask = value;
			}
		}

		public StencilBufferState()
			: base()
		{
		}
	}
}
