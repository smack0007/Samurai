using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public class ShaderCompilationException : SamuraiException
	{
		public string ErrorText
		{
			get;
			private set;
		}

		public ShaderCompilationException(string message, string errorText)
			: base(message)
		{
			if (errorText == null)
				throw new ArgumentNullException("errorText");

			this.ErrorText = errorText;
		}
	}
}
