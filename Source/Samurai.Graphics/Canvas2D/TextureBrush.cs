using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics.Canvas2D
{
    public class TextureBrush : CanvasBrush
    {
        ShaderProgram shader;
        Texture2D texture;
        Color4 tint;

        public Texture2D Texture
        {
            get { return this.texture; }

            set
            {
                if (value != this.texture)
                {
                    this.TriggerStateChanging();
                    this.texture = value;
                }
            }
        }

        public Color4 Tint
        {
            get { return this.tint; }
            
            set
            {
                if (value != this.tint)
                {
                    this.TriggerStateChanging();
                    this.tint = value;
                }
            }
        }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="graphics">Handle to the GraphicsContext.</param>
        public TextureBrush(GraphicsContext graphics)
            : base (graphics)
		{
            this.shader = typeof(SolidColorBrush).Assembly.LoadShaderProgram(
                graphics,
                "Samurai.Graphics.Canvas2D.BasicCanvasShader.vert",
                "Samurai.Graphics.Canvas2D.TextureBrush.frag");

            this.tint = Color4.White;
		}

		protected override void DisposeManagedResources()
		{
            this.shader.Dispose();
            this.shader = null;
		}
		
        public override void Apply(ref Matrix4 transform)
        {
            this.Grahpics.SetShaderProgram(this.shader);
            this.shader.SetValue("vertTransform", ref transform);
            this.shader.SetValue("fragTexture", this.texture);
            this.shader.SetValue("fragTextureWidth", this.texture.Width);
            this.shader.SetValue("fragTextureHeight", this.texture.Height);
            this.shader.SetValue("fragTint", ref this.tint);
        }
    }
}
