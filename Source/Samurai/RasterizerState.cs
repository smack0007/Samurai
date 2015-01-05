using System;

namespace Samurai
{
	public class RasterizerState
	{
		public static readonly RasterizerState Default = new RasterizerState();

		public FrontFace FrontFace
		{
			get;
			private set;
		}

		public CullMode CullMode
		{
			get;
			private set;
		}

		public RasterizerState(
			FrontFace frontFace = FrontFace.Clockwise,
			CullMode cullMode = CullMode.None)
		{
			this.FrontFace = frontFace;
			this.CullMode = cullMode;
		}
	}
}
