using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Samurai.Content.Pipeline
{
	public class ContentProjectProcessorNode
	{
		public string Name { get; set; }

		public Dictionary<string, string> Parameters { get; private set; }

		public ContentProjectProcessorNode()
		{
			this.Parameters = new Dictionary<string, string>();
		}

		internal static ContentProjectProcessorNode FromXElement(XElement element)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			ContentProjectProcessorNode processor = new ContentProjectProcessorNode();

			processor.Name = element.Name.ToString();

			foreach (XAttribute attribute in element.Attributes())
			{
				processor.Parameters[attribute.Name.ToString()] = attribute.Value;
			}

			return processor;
		}

		internal void Build(ContentProjectContext context, object content)
		{
			context.Logger.BeginSection(string.Format("Processor: {0}", this.Name));

			var processor = context.GetContentProcessor(this.Name);
			
			foreach (var pair in this.Parameters)
			{
				var property = processor.GetType().GetProperty(pair.Key);

				if (property != null)
				{
					var parser = context.GetTypeParser(property.PropertyType);
					property.SetValue(processor, parser(pair.Value));
				}
				else
				{
					// TODO: Property doesn't exist.
				}
			}
			
			processor.Process(content, new ContentProcessorContext(context));

			context.Logger.EndSection();
		}
	}
}
