using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public abstract class GraphicsState
	{
		public GraphicsContext Graphics
		{
			get;
			private set;
		}

		internal GraphicsState()
		{
		}

		internal void SetGraphicsContext(GraphicsContext context)
		{
			this.Graphics = context;

			if (this.Graphics != null)
				this.Apply();
		}

		protected abstract void Apply();
	}
}
