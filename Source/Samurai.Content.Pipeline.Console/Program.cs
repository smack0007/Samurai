using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Samurai.Content.Pipeline.Graphics;

namespace Samurai.Content.Pipeline.Console
{
	class Program
	{
		public static void Main(string[] args)
		{			
			ContentProject project = ContentProject.Load("test.xml");

			project.Build(new ContentProjectContext(new ContentProjectLogger(System.Console.Out)));

			System.Console.ReadKey();
		}
	}
}
