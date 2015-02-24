using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public class ContentProjectContext
	{
		Dictionary<string, Type> importers;
		Dictionary<string, Type> processors;
		
		Dictionary<Type, Func<string, object>> typeParsers;

		public ContentProjectLogger Logger
		{
			get;
			private set;
		}

		public ContentProjectContext(ContentProjectLogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");

			this.Logger = logger;

			this.typeParsers = new Dictionary<Type, Func<string, object>>()
			{
				{ typeof(string), (x) => x },

				{ typeof(bool), (x) => bool.Parse(x) },
				{ typeof(byte), (x) => byte.Parse(x) },
				{ typeof(char), (x) => char.Parse(x) },
				{ typeof(decimal), (x) => decimal.Parse(x) },
				{ typeof(double), (x) => double.Parse(x) },
				{ typeof(float), (x) => float.Parse(x) },
				{ typeof(int), (x) => int.Parse(x) },
				{ typeof(long), (x) => long.Parse(x) },
				{ typeof(sbyte), (x) => sbyte.Parse(x) },
				{ typeof(short), (x) => short.Parse(x) },
				{ typeof(uint), (x) => uint.Parse(x) },
				{ typeof(ulong), (x) => ulong.Parse(x) },
				{ typeof(ushort), (x) => ushort.Parse(x) },

				{ typeof(Color4), (x) => Color4.FromHexString(x) }
			};
		}

		public IContentImporter GetContentImporter(string fileName)
		{
			if (this.importers == null)
			{
				this.importers = new Dictionary<string, Type>();

				var importerTypes = AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(x => x.GetTypes())
					.Where(x => !x.IsAbstract && typeof(IContentImporter).IsAssignableFrom(x));

				foreach (Type importerType in importerTypes)
				{
					ContentImporterAttribute attribute = (ContentImporterAttribute)importerType.GetCustomAttribute(typeof(ContentImporterAttribute));

					if (attribute != null)
					{
						foreach (string fileExtension in attribute.FileExtensions)
						{
							// TODO: Check for conflicting file extensions.

							this.importers[fileExtension] = importerType;
						}
					}
				}
			}

			string ext = Path.GetExtension(fileName);

			// TODO: Extension not in dictionary.

			return (IContentImporter)Activator.CreateInstance(this.importers[ext]);
		}

		public IContentProcessor GetContentProcessor(string name)
		{
			if (this.processors == null)
			{
				this.processors = new Dictionary<string, Type>();

				var processorTypes = AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(x => x.GetTypes())
					.Where(x => !x.IsAbstract && typeof(IContentProcessor).IsAssignableFrom(x));

				foreach (Type processorType in processorTypes)
				{
					ContentProcessorAttribute attribute = (ContentProcessorAttribute)processorType.GetCustomAttribute(typeof(ContentProcessorAttribute));

					if (attribute != null)
					{
						// TODO: Check for conflicting names.

						this.processors[attribute.Name] = processorType;
					}
				}
			}

			// TODO: Processor not in dictionary.

			return (IContentProcessor)Activator.CreateInstance(this.processors[name]);
		}

		public Func<string, object> GetTypeParser(Type type)
		{
			// TODO: Throw exception if parser not registerd.

			return this.typeParsers[type];
		}

		public void SetTypeParser(Type type, Func<string, object> parser)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			if (parser == null)
				throw new ArgumentNullException("parser");

			this.typeParsers[type] = parser;
		}
	}
}
