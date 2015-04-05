using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public class ContentProjectLogger : IContentProjectLogger
	{
		private readonly TextWriter writer;
		
		private string padding;

		public ContentProjectLogger(TextWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			this.writer = writer;
			this.padding = string.Empty;
		}

		public void BeginSection(string name)
		{
			this.writer.WriteLine("{0}=== {1} ===", padding, name);
			this.padding += "\t";
		}

		public void EndSection()
		{
			this.padding = this.padding.Substring(0, this.padding.Length - 1);
		}

		public void LogInfo(string value)
		{
			this.writer.WriteLine("{0}{1}", padding, value);
		}
	}
}
