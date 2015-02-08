using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	[Flags]
	public enum ColorMask
	{
		None = 0,

		Red = 1,

		Green = 2,

		Blue = 4,

		Alpha = 8,
		
		All = Red | Green | Blue | Alpha
	}
}
