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

		const int PlaneCount = 1000;

		int fps = 0;
		int fpsCount;
		float fpsTimer;

		public Demo2DGame()
			: base()
		{
			this.Window.Title = "Samurai 2D Demo";

			this.Graphics.DepthTestEnabled = true;
			this.Graphics.DepthFunc = DepthFunc.LessThanOrEqual;

			this.Graphics.BlendState = BlendState.AlphaBlend;
			this.Graphics.RasterizerState = RasterizerState.Default;
						
			this.spriteBatch = new SpriteBatch(this.Graphics);
			this.shaderProgram = new BasicSpriteBatchShaderProgram(this.Graphics);

			this.planesTexture = Texture2D.LoadFromFile(this.Graphics, "Planes.png", new TextureParams()
				{
				});

			this.planeSpriteSheet = SpriteSheet.Build(this.planesTexture, 64, 64);

			this.font = TextureFont.Build(this.Graphics, "Segoe UI", 72, new TextureFontParams()
				{
					Color = Color4.White,
					BackgroundColor = Color4.Transparent,
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

			string text = string.Format("FPS: {0}", fps);

			Size textSize = this.font.MeasureString(text);
			int halfWindowWidth = this.Window.Width / 2;
			int halfWindowHeight = this.Window.Height / 2;
			int halfTextWidth = textSize.Width / 2;
			int halfTextHeight = textSize.Height / 2;

			this.spriteBatch.DrawString(
				this.font,
				text,
				new Rectangle(halfWindowWidth - halfTextWidth, halfWindowHeight - halfTextHeight, textSize.Width, textSize.Height),
				tint: Color4.CornflowerBlue,
				origin: new Vector2(halfTextWidth, halfTextHeight),
				rotation: MathHelper.ToRadians(45.0f),
				layerDepth: 0.5f);

			foreach (Plane plane in this.planes)
			{
				this.spriteBatch.Draw(
					this.planeSpriteSheet,
					plane.StartFrame + plane.FrameOffset,
					plane.Position,
					origin: new Vector2(32, 32),
					rotation: MathHelper.ToRadians(plane.Rotation)
				);
			}

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
