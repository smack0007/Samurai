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

		public string InputFileName
		{
			get;
			set;
		}

		public string OutputFileName
		{
			get;
			set;
		}

		public List<ContentProjectProcessorNode> Processors
		{
			get;
			private set;
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
			item.InputFileName = element.GetOptionalAttributeValue("InputFileName");
			item.OutputFileName = element.GetRequiredAttributeValue("OutputFileName");

			foreach (var processorNode in element.Elements())
			{
				item.Processors.Add(ContentProjectProcessorNode.FromXElement(processorNode));
			}

			return item;
		}

		internal void Build(ContentProjectContext context)
		{
			context.Logger.BeginSection(this.Name);

			object content = null;

			if (this.InputFileName != null)
			{
				var importer = context.GetContentImporter(this.InputFileName);
				content = importer.Import(this.InputFileName, new ContentImporterContext(context));
			}

			foreach (var processor in this.Processors)
				processor.Build(context, content);

			var serializer = context.GetContentSerializer(content.GetType());
			serializer.Serialize(content, new ContentWriter());

			context.Logger.EndSection();
		}
	}
}
