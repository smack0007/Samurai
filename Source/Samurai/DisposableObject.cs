using System;

namespace Samurai
{
	/// <summary>
	/// Base class for objects which should be disposed.
	/// </summary>
	public class DisposableObject : IDisposable
	{
		/// <summary>
		/// Gets whether or not the object has already been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get;
			private set;
		}

		/// <summary>
		/// Disposes of the object.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Can be called by child classes to dispose of the class.
		/// </summary>
		/// <param name="disposing">If true, managed resources will be disposed as well as unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
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

		/// <summary>
		/// Disposes of child objects.
		/// </summary>
		protected virtual void DisposeManagedResources()
		{
		}

		/// <summary>
		/// Disposes of unmanaged resources.
		/// </summary>
		protected virtual void DisposeUnmanagedResources()
		{
		}
	}
}
