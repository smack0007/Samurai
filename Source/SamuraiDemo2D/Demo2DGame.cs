using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using Samurai.Input;
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
		Keyboard keyboard;
		Mouse mouse;

		public Demo2DGame()
		{
			this.Window.Title = "Samurai 2D Demo";

			this.Graphics.BlendEnabled = true;
			this.Graphics.SetBlendFunction(SourceBlendFactor.SourceAlpha, DestinationBlendFactor.OneMinusSourceAlpha);

			this.spriteBatch = new SpriteBatch(this.Graphics);
			this.shaderProgram = new BasicSpriteBatchShaderProgram(this.Graphics);

			this.planesTexture = Texture.LoadFromFile(this.Graphics, "Planes.png", new TextureParams()
				{
				});

			this.planes = SpriteSheet.BuildFromGrid(this.planesTexture, Color4.Magenta);

			this.font = TextureFont.Build(this.Graphics, "Arial", 72, new TextureFontParams()
				{
					Color = Color4.Blue,
					ColorKey = Color4.Black
				});

			this.keyboard = new Keyboard();
			this.mouse = new Mouse(this.Window);
		}

		protected override void Update(TimeSpan elapsed)
		{
			this.keyboard.Update();
			this.mouse.Update(elapsed);

			if (this.keyboard.IsKeyPressed(Key.Escape))
				this.Exit();
		}

		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.Black);

			this.spriteBatch.Begin(this.shaderProgram);

			this.spriteBatch.Draw(this.planes, 5, new Color4(255, 255, 255, 255), new Vector2(this.mouse.X, this.mouse.Y));
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
