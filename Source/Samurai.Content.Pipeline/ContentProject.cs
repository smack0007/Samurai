using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Samurai.Content.Pipeline
{
	public class ContentProject
	{
		public List<ContentProjectReferenceNode> References
		{
			get;
			private set;
		}

		public List<ContentProjectItemNode> Items
		{
			get;
			private set;
		}

		public ContentProject()
		{
			this.References = new List<ContentProjectReferenceNode>();
			this.Items = new List<ContentProjectItemNode>();
		}

		public static ContentProject Load(string fileName)
		{
			ContentProject project = new ContentProject();

			XDocument doc = XDocument.Load(fileName);

			foreach (var node in doc.Root.Elements())
			{
				string name = node.Name.ToString();

				switch (name)
				{
					case "Reference":
						project.References.Add(ContentProjectReferenceNode.FromXElement(node));
						break;

					case "Item":
						project.Items.Add(ContentProjectItemNode.FromXElement(node));
						break;

					default:
						throw new ContentProjectException(string.Format("Unknown child node {0} in ContentProject node.", name));
				}
			}

			return project;
		}

		public void Build(ContentProjectContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			foreach (ContentProjectReferenceNode reference in this.References)
				reference.Build(context);

			foreach (ContentProjectItemNode item in this.Items)
				item.Build(context);
		}
	}
}
