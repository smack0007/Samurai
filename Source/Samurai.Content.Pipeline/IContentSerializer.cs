using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public interface IContentSerializer
	{
		void Serialize(object content, ContentWriter writer);
	}
}
