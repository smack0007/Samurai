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
			private set;
		}

		public ContentProjectSerializeNode Serialize
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
					item.Processors.Add(ContentProjectProcessorNode.FromXElement(processorNode));
				}
			}

			var serializeNode = element.Element("Serialize");

			if (serializeNode != null)
				item.Serialize = ContentProjectSerializeNode.FromXElement(serializeNode);

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

			if (this.Serialize != null)
				content = this.Serialize.Build(context, content);

			context.Logger.EndSection();
		}
	}
}
