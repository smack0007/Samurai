using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public class ContentSerializerContext
	{
		ContentProjectContext context;

		public ContentSerializerContext(ContentProjectContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			this.context = context;
		}
	}
}
