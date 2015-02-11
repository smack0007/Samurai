using System;
using System.IO;
using System.Reflection;

namespace Samurai.Content
{
	public class FileSystemContentStorage : IContentStorage
	{
		public string RootPath
		{
			get;
			private set;
		}

		public FileSystemContentStorage()
			: this(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
		{
		}

		public FileSystemContentStorage(string rootPath)
		{
			if (rootPath == null)
				throw new ArgumentNullException("rootPath");

			this.RootPath = rootPath;
		}

		public Stream GetStream(string fileName)
		{
			try
			{
				return File.Open(Path.Combine(this.RootPath, fileName), FileMode.Open, FileAccess.Read);
			}
			catch (Exception ex)
			{
				throw new ContentException(string.Format("Failed to open content with file name \"{0}\".", fileName), ex);
			}
		}
	}
}
