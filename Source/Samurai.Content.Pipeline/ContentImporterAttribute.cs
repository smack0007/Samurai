using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ContentImporterAttribute : Attribute
	{
		public string[] FileExtensions
		{
			get;
			private set;
		}

		public ContentImporterAttribute(string fileExtension)
			: this(new string[] { fileExtension })
		{
			if (fileExtension == null)
				throw new ArgumentNullException("fileExtension");
		}

		public ContentImporterAttribute(string[] fileExtensions)
		{
			if (fileExtensions == null)
				throw new ArgumentNullException("fileExtensions");

			this.FileExtensions = fileExtensions;
		}
	}
}
