using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDemo
{
	public class DemoGame : Game
	{
		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";
		}

		private static void Main(string[] args)
		{
			DemoGame game = new DemoGame();
			game.Run();
		}
	}
}
