using System;

namespace Samurai.Graphics
{
	public interface IGraphicsHostContext
	{
		/// <summary>
		/// Gets the width of the display area of the host.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Gets the height of the display area of the host.
		/// </summary>
		int Height { get; }

		IntPtr GetProcAddress(string name);

		bool MakeCurrent();

		void SwapBuffers();
	}
}
