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
			string name = context.ReplaceVariables(this.Name);
			Dictionary<string, string> parameters = context.ReplaceVariables(this.Parameters);

			context.Logger.BeginSection(string.Format("Processor: {0}", name));

			var processor = context.GetContentProcessor(name);

			ReflectionHelper.ApplyParameters(context, processor, parameters);
			
			processor.Process(content, new ContentProcessorContext(context));

			context.Logger.EndSection();
		}
	}
}
