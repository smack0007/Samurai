using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ContentSerializerAttribute : Attribute
	{
		public Type ContentType
		{
			get;
			private set;
		}

		public ContentSerializerAttribute(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			this.ContentType = type;
		}
	}
}
