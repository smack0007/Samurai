using System;
using System.Reflection;

namespace Samurai.Canvas2D
{
    /// <summary>
    /// A simple brush that applies a solid color.
    /// </summary>
    public class SolidColorBrush : CanvasBrush
    {
        ShaderProgram shader;
        Color4 color;

        public Color4 Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="graphics">Handle to the GraphicsContext.</param>
        public SolidColorBrush(GraphicsContext graphics)
            : base (graphics)
		{
            this.shader = typeof(SolidColorBrush).Assembly.LoadShaderProgram(
                graphics,
                "Samurai.Canvas2D.BasicCanvasShader.vert",
                "Samurai.Canvas2D.SolidColorBrush.frag");
		}

		protected override void DisposeManagedResources()
		{
            this.shader.Dispose();
            this.shader = null;
		}
		
        public override void Apply(ref Matrix4 transform)
        {
            this.Grahpics.ShaderProgram = this.shader;
            this.shader.SetValue("vertTransform", ref transform);
            this.shader.SetValue("fragColor", ref this.color);
        }
    }
}
