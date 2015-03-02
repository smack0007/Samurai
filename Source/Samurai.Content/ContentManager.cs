using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content
{
    public class ContentManager
    {
		IContentStorage storage;

		Dictionary<Type, IContentLoader> contentLoaders;
		
		Dictionary<string, object> loadedContent;

		public ContentManager(IContentStorage storage)
		{
			if (storage == null)
				throw new ArgumentNullException("storage");

			this.storage = storage;

			this.contentLoaders = new Dictionary<Type, IContentLoader>();
			
			this.loadedContent = new Dictionary<string, object>();
		}

		public void AddLoader<T>(IContentLoader reader)
		{
			if (this.contentLoaders.ContainsKey(typeof(T)))
				throw new ContentException(string.Format("Content reader already registered for type {0}.", typeof(T)));

			this.contentLoaders.Add(typeof(T), reader);
		}

		private IContentLoader GetLoader<T>()
		{
			IContentLoader reader = null;

			if (!this.contentLoaders.TryGetValue(typeof(T), out reader))
				throw new ContentException(string.Format("No content reader registered for type {0}.", typeof(T)));

			return reader;
		}
				
		public T Load<T>(string fileName)
			where T : class
		{			
			object content = null;
			
			if (this.loadedContent.TryGetValue(fileName, out content))
			{
				return (T)content;
			}

			IContentLoader loader = this.GetLoader<T>();

			using (Stream stream = this.storage.GetStream(fileName))
			{
				content = loader.Load(stream);
			}

			this.loadedContent[fileName] = content;

			return (T)content;
		}
    }
}
