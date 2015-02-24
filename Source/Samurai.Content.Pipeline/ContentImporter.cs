using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{	
    public abstract class ContentImporter<T> : IContentImporter
    {
		protected ContentImporter()
			: base()
		{
		}
				
		public abstract T Import(string fileName, ContentImporterContext context);

		object IContentImporter.Import(string fileName, ContentImporterContext context)
		{
			return this.Import(fileName, context);
		}
	}
}
