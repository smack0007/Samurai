using System;

namespace Samurai
{
	public class RasterizerState : GraphicsState
	{
		FrontFace frontFace = FrontFace.CounterClockwise;
		CullMode cullMode = CullMode.None;
		ColorMask colorMask = ColorMask.All;

		public FrontFace FrontFace
		{
			get { return this.frontFace; }
			
			set
			{
				if (value != this.frontFace)
				{
					this.frontFace = value;
					this.ApplyFrontFace();
				}
			}
		}

		public CullMode CullMode
		{
			get { return this.cullMode; }
			
			set
			{
				if (value != this.cullMode)
				{
					this.cullMode = value;
					this.ApplyCullMode();
				}
			}
		}

		public ColorMask ColorMask
		{
			get { return this.colorMask; }
			
			set
			{
				if (value != this.colorMask)
				{
					this.colorMask = value;
					this.ApplyColorMask();
				}
			}
		}

		public RasterizerState()
			: base()
		{
		}

		protected override void Apply()
		{
			this.ApplyFrontFace();
			this.ApplyCullMode();
			this.ApplyColorMask();			
		}

		private void ApplyFrontFace()
		{
			this.Graphics.GL.FrontFace((uint)this.FrontFace);
		}

		private void ApplyCullMode()
		{
			if (this.CullMode == CullMode.None)
			{
				this.Graphics.ToggleCap(GLContext.CullFaceCap, false);
			}
			else
			{
				this.Graphics.ToggleCap(GLContext.CullFaceCap, true);
				this.Graphics.GL.CullFace((uint)this.CullMode);
			}
		}

		private void ApplyColorMask()
		{
			this.Graphics.GL.ColorMask(
				(this.ColorMask & ColorMask.Red) == ColorMask.Red,
				(this.ColorMask & ColorMask.Green) == ColorMask.Green,
				(this.ColorMask & ColorMask.Blue) == ColorMask.Blue,
				(this.ColorMask & ColorMask.Alpha) == ColorMask.Alpha);
		}
	}
}
