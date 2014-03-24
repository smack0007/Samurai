using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public class SamuraiException : Exception
	{
		public SamuraiException(string message)
			: base(message)
		{
		}
	}
}
