using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas2DSandbox
{
    public class Canvas2DGame : Game
    {
        Canvas2DRenderer canvas;
        Texture2D lineStripe;

        float rotation;

        public Canvas2DGame()
        {
            this.Window.Title = "Canvas2D Sandbox";

            this.canvas = new Canvas2DRenderer(this.Graphics);

            this.lineStripe = Texture2D.LoadFromFile(this.Graphics, "LineStripe.png", new TextureParams()
                {
                    WrapS = TextureWrap.Repeat,
                    WrapT = TextureWrap.Repeat
                });
        }
                
        protected override void Draw(TimingState time)
        {
            this.Graphics.Clear();

            this.canvas.Begin();

            Vector2 center = new Vector2(this.Window.Width / 2, this.Window.Height / 2);

            this.canvas.DrawLine(Vector2.Zero, new Vector2(this.Window.Width, this.Window.Height), 1, Color4.Yellow);
            this.canvas.DrawLine(new Vector2(0, this.Window.Height), new Vector2(this.Window.Width, 0), 1, Color4.Yellow);

            this.canvas.DrawTriangle(new Vector2(center.X - 100, center.Y + 50), new Vector2(center.X + 100, center.Y + 50), new Vector2(center.X, center.Y - 50), Color4.White);
                                  
            this.rotation += (float)time.ElapsedTime.TotalSeconds * 36.0f;

            Vector2 end = new Vector2(center.X, center.Y - 300);
            Vector2.RotateAboutOrigin(ref end, ref center, MathHelper.ToRadians(this.rotation), out end);
            float distance = Vector2.Distance(center, end);
            this.canvas.DrawLine(center, end, 5.0f, Color4.Red, this.lineStripe, 0, distance / this.lineStripe.Width);

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
