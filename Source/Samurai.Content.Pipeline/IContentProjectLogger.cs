using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public interface IContentProjectLogger
	{
		void BeginSection(string name);

		void EndSection();

		void LogInfo(string value);
	}
}
