using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Samurai.Content.Pipeline.Graphics;

namespace Samurai.Content.Pipeline.Console
{
	class Program
	{
		public static int Main(string[] args)
		{	
			if (args.Length < 1)
			{
				System.Console.Error.WriteLine("Please provide a content project file.");
				return 1;
			}
						
			ContentProject project = ContentProject.Load(args[0]);

			var context = new ContentProjectContext(new ContentProjectLogger(System.Console.Out));

			context.Variables["ProjectPath"] = Path.GetFullPath(args[0]);
			context.Variables["ProjectFileName"] = Path.GetFileName(args[0]);
			context.Variables["ProjectDirectory"] = Path.GetDirectoryName(Path.GetFullPath(args[0]));

			Directory.SetCurrentDirectory(Path.GetDirectoryName(Path.GetFullPath(args[0])));

			project.Build(context);

			System.Console.ReadKey();

			return 0;
		}
	}
}
