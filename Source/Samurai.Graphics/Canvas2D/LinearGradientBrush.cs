using System;

namespace Samurai.Graphics.Canvas2D
{
    public class LinearGradientBrush : CanvasBrush
    {
        ShaderProgram shader;
        Color4 startColor;
        Color4 endColor;

        public Color4 StartColor
        {
            get { return this.startColor; }
            set { this.startColor = value; }
        }

        public Color4 EndColor
        {
            get { return this.endColor; }
            set { this.endColor = value; }
        }

        public float Angle
        {
            get;
            set;
        }

        public LinearGradientBrush(GraphicsContext graphics)
            : base(graphics)
        {
            this.shader = typeof(LinearGradientBrush).Assembly.LoadShaderProgram(
               graphics,
               "Samurai.Graphics.Canvas2D.BasicCanvasShader.vert",
               "Samurai.Graphics.Canvas2D.LinearGradientBrush.frag");

            this.startColor = Color4.Black;
            this.endColor = Color4.White;
            this.Angle = MathHelper.ToRadians(90);
        }

        public override void Apply(ref Matrix4 transform)
        {
            Vector2 factor = new Vector2((float)Math.Sin(this.Angle), (float)-Math.Cos(this.Angle));
                       
            this.Grahpics.SetShaderProgram(this.shader);
            this.shader.SetValue("vertTransform", ref transform);
            this.shader.SetValue("fragStartColor", ref this.startColor);
            this.shader.SetValue("fragEndColor", ref this.endColor);
            this.shader.SetValue("fragFactor", ref factor);
        }
    }
}
