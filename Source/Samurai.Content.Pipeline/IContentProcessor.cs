using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public interface IContentProcessor
	{
		Type InputType { get; }

		Type OutputType { get; }

		object Process(object input, ContentProcessorContext context);
	}
}
