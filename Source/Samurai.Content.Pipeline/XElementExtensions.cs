using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Samurai.Content.Pipeline
{
	internal static class XElementExtensions
	{
		public static string GetRequiredAttributeValue(this XElement element, string attributeName)
		{
			XAttribute attribute = element.Attribute(attributeName);

			if (attribute == null)
				throw new XmlException(string.Format("{0} node is missing required attribute {1}.", element.Name, attributeName));

			return attribute.Value;
		}

		public static string GetOptionalElementValue(this XElement element, string elementName)
		{
			XElement child = element.Element(elementName);

			if (child != null)
				return child.Value;

			return null;
		}
	}
}
