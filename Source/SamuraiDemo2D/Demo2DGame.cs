using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using System;

namespace SamuraiDemo2D
{
	public class Demo2DGame : Game
	{
		SpriteBatch spriteBatch;
		BasicSpriteBatchShaderProgram shaderProgram;
		Texture planesTexture;
		SpriteSheet planes;
		TextureFont font;

		public Demo2DGame()
		{
			this.Window.Title = "Samurai 2D Demo";

			this.Graphics.BlendEnabled = true;
			this.Graphics.SetBlendFunction(SourceBlendFactor.SourceAlpha, DestinationBlendFactor.OneMinusSourceAlpha);

			this.spriteBatch = new SpriteBatch(this.Graphics);
			this.shaderProgram = new BasicSpriteBatchShaderProgram(this.Graphics);

			this.planesTexture = Texture.Load(this.Graphics, "Planes.png", new TextureParams()
				{
				});

			this.planes = SpriteSheet.Build(this.planesTexture, Color4.Magenta);

			this.font = TextureFont.Build(this.Graphics, "Arial", 72, new TextureFontParams()
				{
					Color = Color4.Blue,
					ColorKey = Color4.Black
				});
		}

		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.Black);

			this.spriteBatch.Begin(this.shaderProgram);

			this.spriteBatch.Draw(this.planes, 5, new Color4(255, 255, 255, 255), Vector2.Zero);
			//this.spriteBatch.DrawString(this.font, "Hello World!", Color4.White, Vector2.Zero);

			this.spriteBatch.End();

			this.Graphics.SwapBuffers();
		}

		private static void Main(string[] args)
		{
			using (Demo2DGame game = new Demo2DGame())
				game.Run();
		}
	}
}
