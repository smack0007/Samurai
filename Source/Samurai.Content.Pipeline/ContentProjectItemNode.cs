using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Samurai.Content.Pipeline
{
	public class ContentProjectItemNode
	{
		public string Name
		{
			get;
			private set;
		}

		public ContentProjectImportNode Import
		{
			get;
			set;
		}

		public List<ContentProjectProcessorNode> Processors
		{
			get;
			set;
		}

		public ContentProjectItemNode()
		{
			this.Processors = new List<ContentProjectProcessorNode>();
		}

		internal static ContentProjectItemNode FromXElement(XElement element)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			ContentProjectItemNode item = new ContentProjectItemNode();

			item.Name = element.GetRequiredAttributeValue("Name");
			
			var importNode = element.Element("Import");

			if (importNode != null)
				item.Import = ContentProjectImportNode.FromXElement(importNode);

			var processorsNode = element.Element("Processors");

			if (processorsNode != null)
			{
				foreach (var processorNode in processorsNode.Elements())
				{
					if (processorNode.Name == "Processor")
					{
						item.Processors.Add(ContentProjectProcessorNode.FromXElement(processorNode));
					}
					else
					{
					}
				}
			}

			return item;
		}

		internal void Build(ContentProjectContext context)
		{
			context.Logger.BeginSection(this.Name);

			object content = null;

			if (this.Import != null)
				content = this.Import.Build(context);

			foreach (var processor in this.Processors)
				processor.Build(context, content);

			context.Logger.EndSection();
		}
	}
}
