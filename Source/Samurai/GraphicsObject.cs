using System;

namespace Samurai
{
	/// <summary>
	/// Base class for objects which should be disposed along with the GraphicsContext.
	/// </summary>
	public class GraphicsObject : DisposableObject
	{
		protected GraphicsContext Graphics
		{
			get;
			private set;
		}

		internal GraphicsObject(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.Graphics = graphics;
			this.Graphics.RegisterGraphicsObject(this);
		}

		protected override void Dispose(bool disposing)
		{
			bool isDisposed = this.IsDisposed;

			base.Dispose(disposing);

			if (!isDisposed && !this.Graphics.IsDisposing)
				this.Graphics.UnregisterGraphicsObject(this);
		}
	}
}
