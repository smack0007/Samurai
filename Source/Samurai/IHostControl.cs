using System;

namespace Samurai
{
	public interface IHostControl
	{
		/// <summary>
		/// Gets a handle to the window.
		/// </summary>
		IntPtr Handle { get; }

		/// <summary>
		/// Gets the width of the display area of the host.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Gets the height of the display area of the host.
		/// </summary>
		int Height { get; }
	}
}
