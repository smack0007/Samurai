using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content
{
    public class ContentManager<TKey>
    {
		IContentStorage storage;

		Dictionary<Type, ContentReader> contentReaders;
		
		Dictionary<TKey, object> registeredContent;
		Dictionary<TKey, object> loadedContent;

		public ContentManager(IContentStorage storage)
		{
			if (storage == null)
				throw new ArgumentNullException("storage");

			this.storage = storage;

			this.contentReaders = new Dictionary<Type, ContentReader>();
			this.registeredContent = new Dictionary<TKey, object>();
			this.loadedContent = new Dictionary<TKey, object>();
		}

		public void AddReader<TContentType, TContentParams>(ContentReader<TContentType, TContentParams> reader)
		{
			if (this.contentReaders.ContainsKey(typeof(TContentType)))
				throw new ContentException(string.Format("Content reader already registered for type {0}.", typeof(TContentType)));

			this.contentReaders.Add(typeof(TContentType), reader);
		}

		private ContentReader GetReader<TContentType>()
		{
			ContentReader reader = null;

			if (!this.contentReaders.TryGetValue(typeof(TContentType), out reader))
				throw new ContentException(string.Format("No content reader registered for type {0}.", typeof(TContentType)));

			return (ContentReader)reader;
		}

		public void Register<TContentType>(TKey key, object parameters)
		{
			if (this.registeredContent.ContainsKey(key))
				throw new ContentException(string.Format("Content already registered for the key {0}.", key));

			ContentReader reader = this.GetReader<TContentType>();
			reader.EnsureParams(parameters);
			
			this.registeredContent[key] = parameters;
		}

		public TContentType Load<TContentType>(TKey key)
			where TContentType : class
		{
			if (!this.registeredContent.ContainsKey(key))
				throw new ContentException(string.Format("No content registered for the key {0}.", key));

			object content = null;
			if (this.loadedContent.TryGetValue(key, out content))
			{
				return (TContentType)content;
			}

			ContentReader reader = this.GetReader<TContentType>();
			content = reader.Load(this.storage, this.registeredContent[key]);

			this.loadedContent[key] = content;

			return (TContentType)content;
		}
    }
}
