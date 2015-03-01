using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	internal static class ReflectionHelper
	{
		public static void ApplyParameters(ContentProjectContext context, object obj, Dictionary<string, string> parameters)
		{
			Type objType = obj.GetType();

			foreach (var pair in parameters)
			{
				var property = objType.GetProperty(pair.Key);

				if (property != null)
				{
					var parser = context.GetTypeParser(property.PropertyType);
					property.SetValue(obj, parser(pair.Value));
				}
				else
				{
					// TODO: Property doesn't exist.
				}
			}
		}
	}
}
