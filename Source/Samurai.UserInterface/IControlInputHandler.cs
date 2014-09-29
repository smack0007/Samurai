using System;

namespace Samurai.UserInterface
{
	public interface IControlInputHandler
	{
		Point CursorPosition { get; }

		bool IsPrimaryButtonPressed { get; }
	}
}
