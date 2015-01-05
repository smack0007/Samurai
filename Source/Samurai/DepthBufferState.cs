using System;

namespace Samurai
{
	public class DepthBufferState
	{
		public static readonly DepthBufferState Disabled = new DepthBufferState();

		public static readonly DepthBufferState LessThanOrEqual = new DepthBufferState(
			enabled: true,
			depthFunc: DepthFunc.LessThanOrEqual
		);

		public bool Enabled
		{
			get;
			private set;
		}

		public DepthFunc DepthFunc
		{
			get;
			private set;
		}

		public DepthBufferState(
			bool enabled = false,
			DepthFunc depthFunc = DepthFunc.LessThanOrEqual)
		{
			this.Enabled = enabled;
			this.DepthFunc = depthFunc;
		}
	}
}
