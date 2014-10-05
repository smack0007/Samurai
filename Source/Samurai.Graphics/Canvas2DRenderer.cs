using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
    public class Canvas2DRenderer : DisposableObject
    {
        [StructLayout(LayoutKind.Sequential)]
        struct Vertex
        {
            public Vector2 Position;
            public Color4 Color;
            public Vector2 UV;
        }

        enum State
        {
            None,

            Lines,

            Triangles           
        }

        GraphicsContext graphics;
        
        Vertex[] vertices;
        int vertexCount;
        Texture2D texture;

        DynamicVertexBuffer<Vertex> vertexBuffer;
        ShaderProgram shader;
        Texture2D pixel;

        bool drawInProgress;

        Matrix4 projection;

        BlendState oldBlendState;
        DepthBufferState oldDepthBufferState;
        RasterizerState oldRasterizerState;

        State state;

        public Canvas2DRenderer(GraphicsContext graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            this.graphics = graphics;

            this.vertices = new Vertex[1024 * 4];

            this.vertexBuffer = new DynamicVertexBuffer<Vertex>(this.graphics);

            this.projection = new Matrix4()
            {
                M33 = 1f,
                M44 = 1f,
                M41 = -1f,
                M42 = 1f
            };

            Assembly assembly = typeof(Canvas2DRenderer).Assembly;
			this.shader = new ShaderProgram(
				this.graphics,
                VertexShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.Graphics.Canvas2DShader.vert")),
                FragmentShader.Compile(graphics, assembly.GetManifestResourceStream("Samurai.Graphics.Canvas2DShader.frag")));

            this.pixel = Texture2D.LoadFromBytes(this.graphics, new byte[] { 255, 255, 255, 255 }, 1, 1, new TextureParams());
        }
                
        protected override void DisposeManagedResources()
        {
            this.vertexBuffer.Dispose();
            this.vertexBuffer = null;
        }

        private void EnsureDrawInProgress()
        {
            if (!this.drawInProgress)
                throw new InvalidOperationException("Draw not currently in progress.");
        }

        public void Begin()
        {
            if (this.drawInProgress)
                throw new InvalidOperationException("Draw already in progress.");
                        
            this.oldBlendState = this.graphics.BlendState;
            this.graphics.BlendState = BlendState.AlphaBlend;

            this.oldDepthBufferState = this.graphics.DepthBufferState;
            this.graphics.DepthBufferState = DepthBufferState.LessThanOrEqual;

            this.oldRasterizerState = this.graphics.RasterizerState;
            this.graphics.RasterizerState = RasterizerState.Default;

            this.state = State.None;

            this.drawInProgress = true;
        }

        public void End()
        {
            this.EnsureDrawInProgress();

            this.Flush();

            this.graphics.BlendState = this.oldBlendState;
            this.oldBlendState = null;

            this.graphics.DepthBufferState = this.oldDepthBufferState;
            this.oldDepthBufferState = null;

            this.graphics.RasterizerState = this.oldRasterizerState;
            this.oldRasterizerState = null;

            this.drawInProgress = false;
        }

        private void SetState(State state, int requiredVertices, Texture2D texture)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");

            this.EnsureDrawInProgress();

            if (state != this.state ||
                (this.vertexCount + requiredVertices >= this.vertexBuffer.Count) ||
                texture != this.texture)
            {
                this.Flush();
            }

            this.state = state;
            this.texture = texture;
        }

        private void AddVertex(ref Vector2 position, ref Color4 color, ref Vector2 uv)
        {
            this.vertices[this.vertexCount].Position = position;
            this.vertices[this.vertexCount].Color = color;
            this.vertices[this.vertexCount].UV = uv;
            this.vertexCount++;
        }

        public void DrawLine(Vector2 start, Vector2 end, float width, Color4 tint)
        {
            this.DrawLine(start, end, width, tint, this.pixel, 0, 1);
        }

        public void DrawLine(Vector2 start, Vector2 end, float width, Color4 tint, Texture2D texture, float startT, float endT)
        {
            this.SetState(State.Lines, 6, texture);

            float distance;
            Vector2.Distance(ref start, ref end, out distance);

            float angle = -(float)Math.Atan2(start.X - end.X, start.Y - end.Y);

            float halfWidth = width / 2.0f;

            Vector2 topLeftTemp = new Vector2(start.X - halfWidth, start.Y);
            Vector2 topLeft;
            Vector2.RotateAboutOrigin(ref topLeftTemp, ref start, angle, out topLeft);

            Vector2 topRightTemp = new Vector2(start.X + halfWidth, start.Y);
            Vector2 topRight;
            Vector2.RotateAboutOrigin(ref topRightTemp, ref start, angle, out topRight);

            Vector2 bottomRightTemp = new Vector2(start.X + halfWidth, start.Y - distance);
            Vector2 bottomRight;
            Vector2.RotateAboutOrigin(ref bottomRightTemp, ref start, angle, out bottomRight);

            Vector2 bottomLeftTemp = new Vector2(start.X - halfWidth, start.Y - distance);
            Vector2 bottomLeft;
            Vector2.RotateAboutOrigin(ref bottomLeftTemp, ref start, angle, out bottomLeft);

            Vector2 topLeftUV = new Vector2(startT, 0);
            Vector2 topRightUV = new Vector2(startT, 1);
            Vector2 bottomRightUV = new Vector2(endT, 1);
            Vector2 bottomLeftUV = new Vector2(endT, 0);

            this.AddVertex(ref topLeft, ref tint, ref topLeftUV);
            this.AddVertex(ref topRight, ref tint, ref topRightUV);
            this.AddVertex(ref bottomLeft, ref tint, ref bottomLeftUV);

            this.AddVertex(ref topRight, ref tint, ref topRightUV);
            this.AddVertex(ref bottomRight, ref tint, ref bottomRightUV);
            this.AddVertex(ref bottomLeft, ref tint, ref bottomLeftUV);
        }

        public void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, Color4 color)
        {
            this.SetState(State.Triangles, 3, this.pixel);

            Vector2 uv = Vector2.Zero;

            this.AddVertex(ref v1, ref color, ref uv);
            this.AddVertex(ref v2, ref color, ref uv);
            this.AddVertex(ref v3, ref color, ref uv);
        }

        private void Flush()
        {
            if (this.vertexCount > 0)
            {
                this.vertexBuffer.SetData(this.vertices, 0, this.vertexCount);

                this.graphics.SetShaderProgram(this.shader);

                Rectangle viewport = this.graphics.Viewport;
                this.projection.M11 = 2f / viewport.Width;
                this.projection.M22 = -2f / viewport.Height;

                this.shader.SetValue("inTransform", ref this.projection);
                this.shader.SetSampler("fragSampler", this.texture);

                this.graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer, 0, this.vertexCount);

                this.vertexCount = 0;
            }
        }
    }
}
