using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content
{	
	public abstract class ContentLoader<T> : IContentLoader
	{
		public Type ContentType
		{
			get { return typeof(T); }
		}

		object IContentLoader.Load(Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			return this.Load(stream);
		}

		public abstract T Load(Stream stream);
	}
}
