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

		internal void Load()
		{
			string path = Path.GetFullPath(this.FileName);

			if (File.Exists(path))
			{
				Assembly.LoadFile(path);
			}
			else
			{

			}
		}
	}
}
