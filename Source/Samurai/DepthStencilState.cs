using System;

namespace Samurai
{
	public class DepthStencilState : GraphicsState
	{
		public static readonly DepthStencilState Disabled = new DepthStencilState();

		public static readonly DepthStencilState DepthLessThanOrEqual = new DepthStencilState()
		{
			DepthBufferEnabled = true,
			DepthFunction = DepthFunction.LessThanOrEqual
		};

		bool depthBufferEnabled = false;
		DepthFunction depthFunction = DepthFunction.Less;
		bool depthWriteEnabled = true;
		bool stencilBufferEnabled = false;
		StencilFunction stencilFunction = StencilFunction.Always;
		int stencilReferenceValue = 0;
		uint stencilMask = 0;
		StencilOperation stencilFail = StencilOperation.Keep;
		StencilOperation stencilDepthFail = StencilOperation.Keep;
		StencilOperation stencilPass = StencilOperation.Keep;
		uint stencilWriteMask = 0xFFFFFFFF;

		public bool DepthBufferEnabled
		{
			get { return this.depthBufferEnabled; }
			
			set
			{
				this.EnsureNotFrozen();
				this.depthBufferEnabled = value;
			}
		}

		public DepthFunction DepthFunction
		{
			get { return this.depthFunction; }
			
			set
			{
				this.EnsureNotFrozen();
				this.depthFunction = value;
			}
		}

		public bool DepthWriteEnabled
		{
			get { return this.depthWriteEnabled; }

			set
			{
				this.EnsureNotFrozen();
				this.depthWriteEnabled = value;
			}
		}

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

		public DepthStencilState()
		{
		}
	}
}
