﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Samurai.Content.Pipeline
{
	public class ContentProjectImportNode
	{
		public string FileName { get; set; }

		internal static ContentProjectImportNode FromXElement(XElement element)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			ContentProjectImportNode import = new ContentProjectImportNode();

			import.FileName = element.GetRequiredAttributeValue("FileName");

			return import;
		}

		internal object Build(ContentProjectContext context)
		{
			string fileName = context.ReplaceVariables(this.FileName);

			context.Logger.BeginSection(string.Format("Import: {0}", fileName));

			var importer = context.GetContentImporter(fileName);
			object content = importer.Import(fileName, new ContentImporterContext(context));

			context.Logger.EndSection();

			return content;
		}
	}
}