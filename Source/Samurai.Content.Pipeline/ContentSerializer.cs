using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public abstract class ContentSerializer<T> : IContentSerializer
	{
		void IContentSerializer.Serialize(object content, ContentWriter writer)
		{
			if (content == null)
				throw new ArgumentNullException("content");

			if (writer == null)
				throw new ArgumentNullException("writer");

			if (!(content is T))
			{
				throw new ContentProjectException(string.Format("{0} cannot write content type {1}.", this.GetType(), content.GetType()));
			}

			this.Serialize((T)content, writer);
		}

		public abstract void Serialize(T content, ContentWriter writer);
	}
}
