using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ContentProcessorAttribute : Attribute
	{
		public string Name
		{
			get;
			private set;
		}

		public ContentProcessorAttribute(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			this.Name = name;
		}
	}
}
