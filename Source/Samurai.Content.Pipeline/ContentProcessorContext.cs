using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai.Content.Pipeline
{
	public class ContentProcessorContext
	{
		ContentProjectContext context;

		public ContentProcessorContext(ContentProjectContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			this.context = context;
		}
	}
}
