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
			string path = Path.GetFullPath(fileName);

			context.Logger.LogInfo(string.Format("Loading assembly {0}...", path));

			if (File.Exists(path))
			{
				Assembly.LoadFile(path);
			}
			else
			{
				throw new ContentProjectException(string.Format("Unable to load referenced assembly {0}.", fileName));
			}
		}
	}
}
