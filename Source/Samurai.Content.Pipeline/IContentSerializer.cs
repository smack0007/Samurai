using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public interface IContentSerializer
	{
		void Serialize(object content, FileStream file, ContentSerializerContext context);
	}
}
