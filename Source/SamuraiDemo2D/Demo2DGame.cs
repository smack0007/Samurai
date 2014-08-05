using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using Samurai.Input;
using System;
using System.Collections.Generic;

namespace SamuraiDemo2D
{
	public class Demo2DGame : Game
	{
		SpriteBatch spriteBatch;
		BasicSpriteBatchShaderProgram shaderProgram;
		Texture2D planesTexture;
		SpriteSheet planeSpriteSheet;
		TextureFont font;
		Keyboard keyboard;
		Mouse mouse;
		
		List<Plane> planes;

		const int PlaneCount = 100;

		int fps = 0;
		int fpsCount;
		float fpsTimer;

		public Demo2DGame()
			: base(new GameOptions()
			{
				WindowResizable = true
			})
		{
			this.Window.Title = "Samurai 2D Demo";

			this.Graphics.BlendEnabled = true;
			this.Graphics.SetBlendFunction(SourceBlendFactor.SourceAlpha, DestinationBlendFactor.OneMinusSourceAlpha);

			this.spriteBatch = new SpriteBatch(this.Graphics);
			this.shaderProgram = new BasicSpriteBatchShaderProgram(this.Graphics);

			this.planesTexture = Texture2D.LoadFromFile(this.Graphics, "Planes.png", new TextureParams()
				{
				});

			this.planeSpriteSheet = SpriteSheet.Build(this.planesTexture, 64, 64);

			this.font = TextureFont.Build(this.Graphics, "Arial", 72, new TextureFontParams()
				{
					Color = Color4.Black,
					BackgroundColor = Color4.White,
					//ColorKey = Color4.Black
				});

			this.keyboard = new Keyboard();
			this.mouse = new Mouse(this.Window);

			Random random = new Random();

			this.planes = new List<Plane>();

			for (int i = 0; i < PlaneCount; i++)
			{
				this.planes.Add(new Plane(
					random.Next(4) * 3,
					new Vector2(random.Next(this.Window.Width), random.Next(this.Window.Height)),
					(float)random.Next(360),
					this.Window.Size
				));
			}
		}

		protected override void Update(TimeSpan elapsed)
		{
			this.keyboard.Update();
			this.mouse.Update(elapsed);

			if (this.keyboard.IsKeyPressed(Key.Escape))
				this.Exit();

			foreach (Plane plane in this.planes)
			{
				plane.Update(elapsed);
			}
		}

		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.Black);

			this.spriteBatch.Begin(this.shaderProgram);

			foreach (Plane plane in this.planes)
			{
				this.spriteBatch.Draw(
					this.planeSpriteSheet,
					plane.StartFrame + plane.FrameOffset,
					Color4.White,
					plane.Position,
					new Vector2(32, 32),
					Vector2.One,
					MathHelper.ToRadians(plane.Rotation)
				);
			}

			this.spriteBatch.DrawString(this.font, string.Join("FPS: ", this.fps.ToString()), Color4.White, new Vector2(50, 50));

			this.spriteBatch.End();

			this.Graphics.SwapBuffers();

			this.fpsCount++;
			this.fpsTimer += (float)elapsed.TotalSeconds;
			if (this.fpsTimer >= 1.0f)
			{
				this.fps = this.fpsCount;
				this.fpsTimer -= 1.0f;
				this.fpsCount = 0;
			}
		}

		private static void Main(string[] args)
		{
			using (Demo2DGame game = new Demo2DGame())
				game.Run();
		}
	}
}
