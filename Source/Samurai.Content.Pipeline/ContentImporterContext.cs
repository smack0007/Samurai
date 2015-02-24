using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai.Content.Pipeline
{
	public class ContentImporterContext
	{
		ContentProjectContext context;

		public ContentImporterContext(ContentProjectContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			this.context = context;
		}
	}
}
