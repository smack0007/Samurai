using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content
{
	public class ContentException : SamuraiException
	{
		public ContentException(string message)
			: base(message)
		{
		}

		public ContentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
