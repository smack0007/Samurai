using Samurai;
using Samurai.GLFW;
using Samurai.Graphics;
using Samurai.Graphics.Canvas2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas2DSandbox
{
    public class Canvas2DGame : Game
    {
        CanvasRenderer canvas;
        SolidColorBrush solidColorBrush;
        LinearGradientBrush linearGradientBrush;
        TextureBrush textureBrush;
                
        Texture2D lineStripe;
        Texture2D samuraiLogo;

        float rotation;

        Vector2[] polygonPositions;

        public Canvas2DGame()
        {
            var info = this.Graphics.GetDescription();

            this.Window.Title = "Canvas2D Sandbox";

            this.canvas = new CanvasRenderer(this.Graphics);
            this.solidColorBrush = new SolidColorBrush(this.Graphics);
            this.textureBrush = new TextureBrush(this.Graphics);
            this.linearGradientBrush = new LinearGradientBrush(this.Graphics);

            this.lineStripe = Texture2D.LoadFromFile(this.Graphics, "LineStripe.png", new TextureParams()
            {
                WrapS = TextureWrap.Repeat,
                WrapT = TextureWrap.Repeat
            });

            this.textureBrush.Texture = lineStripe;

            this.samuraiLogo = Texture2D.LoadFromFile(this.Graphics, "SamuraiLogo.png", new TextureParams()
            {
                WrapS = TextureWrap.Repeat,
                WrapT = TextureWrap.Repeat
            });

            this.polygonPositions = new Vector2[8];
            this.polygonPositions[0] = new Vector2(400, 400);
            this.polygonPositions[1] = new Vector2(450, 300);
            this.polygonPositions[2] = new Vector2(500, 450);
            this.polygonPositions[3] = new Vector2(550, 250);
            this.polygonPositions[4] = new Vector2(600, 500);
            this.polygonPositions[5] = new Vector2(650, 200);
            this.polygonPositions[6] = new Vector2(700, 550);
            this.polygonPositions[7] = new Vector2(750, 150);
        }

        protected override void Draw(TimeSpan elapsed)
        {
            this.Graphics.Clear();

            this.canvas.Begin();

            Vector2 center = new Vector2(this.Window.Width / 2, this.Window.Height / 2);

            this.solidColorBrush.Color = Color4.Yellow;
            this.canvas.DrawLine(Vector2.Zero, new Vector2(this.Window.Width, this.Window.Height), 1, solidColorBrush);
            this.canvas.DrawLine(new Vector2(0, this.Window.Height), new Vector2(this.Window.Width, 0), 1, solidColorBrush);

            this.textureBrush.Texture = this.lineStripe;
            this.textureBrush.Tint = Color4.Blue;
            this.textureBrush.Source = null;

            this.linearGradientBrush.Angle = MathHelper.ToRadians(this.rotation);
            this.linearGradientBrush.StartColor = Color4.Red;
            this.linearGradientBrush.EndColor = Color4.Green;
            //this.canvas.DrawTriangleStrip(this.polygonPositions, this.linearGradientBrush);
            this.canvas.DrawRectangle(new Rectangle(400, 400, 200, 200), this.linearGradientBrush);

            this.textureBrush.Texture = this.samuraiLogo;
            this.textureBrush.Tint = Color4.White;

            this.canvas.DrawCircle(new Vector2(128, 128), 128, this.textureBrush);

            this.canvas.DrawTriangle(
                new Vector2(center.X - 128, center.Y + 128),
                new Vector2(center.X + 128, center.Y + 128),
                new Vector2(center.X, center.Y - 128),
                this.textureBrush);

            this.textureBrush.Source = new Rectangle(96, 96, 64, 64);
            this.canvas.DrawRectangle(new Rectangle((int)center.X - 32, (int)center.Y - 32, 64, 64), this.textureBrush);
                                  
            this.rotation += (float)elapsed.TotalSeconds * 36.0f;

            this.textureBrush.Texture = this.lineStripe;
            this.textureBrush.Tint = Color4.Red;
            this.textureBrush.Source = null;

            Vector2 end = new Vector2(center.X, center.Y - 300);
            Vector2.RotateAboutOrigin(ref end, ref center, MathHelper.ToRadians(this.rotation), out end);
            float distance = Vector2.Distance(center, end);
            this.canvas.DrawLine(center, end, 5.0f, this.textureBrush);

            this.canvas.End();

            this.Graphics.SwapBuffers();
        }

        public static void Main(string[] args)
        {
            using (Canvas2DGame game = new Canvas2DGame())
                game.Run();
        }
    }
}
