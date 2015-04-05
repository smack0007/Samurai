using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Samurai.Content.Pipeline
{
	public class ContentProjectReferenceNode
	{
		public string FileName
		{
			get;
			set;
		}

		internal static ContentProjectReferenceNode FromXElement(XElement element)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			ContentProjectReferenceNode reference = new ContentProjectReferenceNode();

			reference.FileName = element.GetRequiredAttributeValue("FileName");

			return reference;
		}

		internal void Build(ContentProjectContext context)
		{
			string fileName = context.ReplaceVariables(this.FileName);
			bool exists = false;

			context.Logger.LogInfo(string.Format("Loading assembly {0}...", fileName));

			if (File.Exists(fileName))
			{
				exists = true;
			}
			else
			{
				string globalPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
				if (File.Exists(globalPath))
				{
					fileName = globalPath;
					exists = true;
				}
			}

			if (exists)
			{
				Assembly assembly = Assembly.LoadFile(Path.GetFullPath(fileName));
				context.RegisterAssembly(assembly);
			}
			else
			{
				throw new ContentProjectException(string.Format("Unable to load referenced assembly {0}.", fileName));
			}
		}
	}
}
