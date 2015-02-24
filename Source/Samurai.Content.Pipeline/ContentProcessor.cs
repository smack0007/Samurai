using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public abstract class ContentProcessor<TInput, TOutput> : IContentProcessor
	{
		Type IContentProcessor.InputType
		{
			get { return typeof(TInput); }
		}

		Type IContentProcessor.OutputType
		{
			get { return typeof(TOutput); }
		}

		public ContentProcessor()
		{
		}

		object IContentProcessor.Process(object input, ContentProcessorContext context)
		{
			if (input == null)
				throw new ArgumentNullException("input");

			if (context == null)
				throw new ArgumentNullException("context");

			if (!(input is TInput))
			{
				throw new ContentProjectException(string.Format("{0} cannot process input type {1}.", this.GetType(), input.GetType()));
			}

			return this.Process((TInput)input, context);
		}

		public abstract TOutput Process(TInput input, ContentProcessorContext context);
	}
}
