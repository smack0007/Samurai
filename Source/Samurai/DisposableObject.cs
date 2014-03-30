using System;

namespace Samurai
{
	public class DisposableObject : IDisposable
	{
		public bool IsDisposed
		{
			get;
			private set;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing)
				{
					this.DisposeManagedResources();
				}

				this.DisposeUnmanagedResources();

				this.IsDisposed = true;
			}
		}

		protected virtual void DisposeManagedResources()
		{
		}

		protected virtual void DisposeUnmanagedResources()
		{
		}
	}
}
