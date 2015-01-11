using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public abstract class GraphicsState
	{
		public bool IsFrozen
		{
			get;
			private set;
		}

		internal GraphicsState()
		{
		}

		internal void Freeze()
		{
			this.IsFrozen = true;
		}

		protected void EnsureNotFrozen()
		{
			if (this.IsFrozen)
				throw new SamuraiException(string.Format("{0} is frozen and cannot be modifieid directly. It must be cloned first.", this.GetType().Name));
		}
	}
}
