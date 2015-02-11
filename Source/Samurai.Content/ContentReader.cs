using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content
{
	public abstract class ContentReader
	{
		public abstract void EnsureParams(object parameters);

		public abstract object Load(IContentStorage storage, object parameters); 
	}

	public abstract class ContentReader<TContentType, TContentParams> : ContentReader
	{
		public Type ContentType
		{
			get { return typeof(TContentType); }
		}

		public override void EnsureParams(object parameters)
		{
			if (!(parameters is TContentParams))
				throw new ContentException(string.Format("Content type {0} requires content parameters of type {1}", typeof(TContentType), typeof(TContentParams)));

			this.EnsureContentParams((TContentParams)parameters);
		}

		protected abstract void EnsureContentParams(TContentParams parameters);

		public override object Load(IContentStorage storage, object parameters)
		{
			if (storage == null)
				throw new ArgumentNullException("storage");

			this.EnsureParams(parameters);
			return this.LoadContent(storage, (TContentParams)parameters);
		}

		protected abstract TContentType LoadContent(IContentStorage storage, TContentParams parameters);
	}
}
