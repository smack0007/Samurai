using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public abstract class ContentSerializer<T> : IContentSerializer
	{
		void IContentSerializer.Serialize(object content, FileStream file, ContentSerializerContext context)
		{
			if (content == null)
				throw new ArgumentNullException("content");

			if (file == null)
				throw new ArgumentNullException("file");

			if (context == null)
				throw new ArgumentNullException("context");

			if (!(content is T))
			{
				throw new ContentProjectException(string.Format("{0} cannot write content type {1}.", this.GetType(), content.GetType()));
			}

			this.Serialize((T)content, file, context);
		}

		public abstract void Serialize(T content, FileStream writer, ContentSerializerContext context);
	}
}
