using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samurai.Graphics;

namespace Samurai.Content
{
	public class Texture2DContentReader : ContentReader<Texture2D, Texture2DContentParams>
	{
		GraphicsContext graphics;

		public Texture2DContentReader(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.graphics = graphics;
		}

		protected override void EnsureContentParams(Texture2DContentParams parameters)
		{
			if (string.IsNullOrEmpty(parameters.FileName))
				throw new ContentException("FileName is required for Texture2D content.");
		}

		protected override Texture2D LoadContent(IContentStorage storage, Texture2DContentParams parameters)
		{
			return Texture2D.LoadFromStream(this.graphics, storage.GetStream(parameters.FileName), parameters);
		}
	}
}
