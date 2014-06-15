using Samurai;
using System;

namespace SamuraiDemo2D
{
	public class Demo2DGame : Game
	{
		SpriteBatch spriteBatch;
		BasicSpriteBatchShaderProgram shaderProgram;
		Texture planesTexture;

		public Demo2DGame()
		{
			this.Window.Title = "Samurai 2D Demo";

			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
			this.shaderProgram = new BasicSpriteBatchShaderProgram(this.GraphicsDevice);

			this.planesTexture = Texture.FromFile(this.GraphicsDevice, "Planes.png", new TextureParams()
				{
					ColorKey = GLHelper.MakePixelRGBA(255, 0, 255, 255),
					WrapS = TextureWrap.Repeat,
					WrapT = TextureWrap.Repeat
				});
		}

		protected override void Draw(TimeSpan elapsed)
		{
			this.GraphicsDevice.Clear();

			this.spriteBatch.Begin(this.shaderProgram);

			this.spriteBatch.Draw(this.planesTexture, Color4.White, Vector2.Zero);	

			this.spriteBatch.End();

			this.GraphicsDevice.SwapBuffers();
		}

		protected override void Shutdown()
		{
			this.spriteBatch.Dispose();
			this.shaderProgram.Dispose();
			this.planesTexture.Dispose();
		}

		private static void Main(string[] args)
		{
			using (Demo2DGame game = new Demo2DGame())
				game.Run();
		}
	}
}
