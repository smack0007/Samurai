using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content
{
	public interface IContentStorage
	{
		Stream GetStream(string fileName);
	}
}
