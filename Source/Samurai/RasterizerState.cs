using System;

namespace Samurai
{
	public class RasterizerState : GraphicsState
	{
		public static readonly RasterizerState Default = new RasterizerState();

		FrontFace frontFace = FrontFace.CounterClockwise;
		CullMode cullMode = CullMode.None;
		ColorMask colorMask = ColorMask.All;

		public FrontFace FrontFace
		{
			get { return this.frontFace; }
			
			set
			{
				this.EnsureNotFrozen();
				this.frontFace = value;
			}
		}

		public CullMode CullMode
		{
			get { return this.cullMode; }
			
			set
			{
				this.EnsureNotFrozen();
				this.cullMode = value;
			}
		}

		public ColorMask ColorMask
		{
			get { return this.colorMask; }
			
			set
			{
				this.EnsureNotFrozen();
				this.colorMask = value;
			}
		}

		public RasterizerState()
			: base()
		{
		}
	}
}
