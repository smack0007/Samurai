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
			context.Logger.BeginSection(string.Format("Import: {0}", this.FileName));

			var importer = context.GetContentImporter(this.FileName);
			object content = importer.Import(this.FileName, new ContentImporterContext(context));

			context.Logger.EndSection();

			return content;
		}
	}
}