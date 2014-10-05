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

            Line,

            Triangle,
   
            TriangleStrip,

            TriangleFan
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
            this.DrawLine(this.pixel, start, 0, end, 0, width, tint);
        }

        public void DrawLine(Texture2D texture, Vector2 start, float startT, Vector2 end, float endT, float width, Color4 tint)
        {
            this.SetState(State.Line, 6, texture);

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

        public void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, Color4 tint)
        {
            this.DrawTriangle(this.pixel, v1, Vector2.Zero, v2, Vector2.Zero, v3, Vector2.Zero, tint);
        }

        public void DrawTriangle(Texture2D texture, Vector2 v1, Vector2 uv1, Vector2 v2, Vector2 uv2, Vector2 v3, Vector2 uv3, Color4 tint)
        {
            this.SetState(State.Triangle, 3, texture);

            this.AddVertex(ref v1, ref tint, ref uv1);
            this.AddVertex(ref v2, ref tint, ref uv2);
            this.AddVertex(ref v3, ref tint, ref uv3);
        }

        private void DrawTriangleSet(State state, IList<Vector2> positions, Color4 tint)
        {
            if (positions == null)
                throw new ArgumentNullException("positions");

            this.SetState(state, positions.Count, this.pixel);

            Vector2 uv = Vector2.Zero;
            for (int i = 0; i < positions.Count; i++)
            {
                Vector2 position = positions[i];
                this.AddVertex(ref position, ref tint, ref uv);
            }
        }

        private void DrawTriangleSet(State state, Texture2D texture, IList<Vector2> positions, IList<Vector2> texCoords, Color4 tint)
        {
            if (positions == null)
                throw new ArgumentNullException("positions");

            if (texCoords == null)
                throw new ArgumentNullException("texCoords");

            if (positions.Count != texCoords.Count)
                throw new InvalidOperationException("positions.Count and texCoords.Count must be equal.");

            this.SetState(state, positions.Count, texture);

            for (int i = 0; i < positions.Count; i++)
            {
                Vector2 position = positions[i];
                Vector2 uv = texCoords[i];
                this.AddVertex(ref position, ref tint, ref uv);
            }
        }

        public void DrawTriangleStrip(IList<Vector2> positions, Color4 tint)
        {
            this.DrawTriangleSet(State.TriangleStrip, positions, tint);
        }

        public void DrawTriangleStrip(Texture2D texture, IList<Vector2> positions, IList<Vector2> texCoords, Color4 tint)
        {
            this.DrawTriangleSet(State.TriangleStrip, texture, positions, texCoords, tint);
        }

        public void DrawTriangleFan(IList<Vector2> positions, Color4 tint)
        {
            this.DrawTriangleSet(State.TriangleFan, positions, tint);
        }

        public void DrawTriangleFan(Texture2D texture, IList<Vector2> positions, IList<Vector2> texCoords, Color4 tint)
        {
            this.DrawTriangleSet(State.TriangleFan, texture, positions, texCoords, tint);
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

                PrimitiveType primitiveType = PrimitiveType.Triangles;

                switch (this.state)
                {
                    case State.TriangleStrip:
                        primitiveType = PrimitiveType.TriangleStrip;
                        break;

                    case State.TriangleFan:
                        primitiveType = PrimitiveType.TriangleFan;
                        break;
                }

                this.graphics.Draw(primitiveType, this.vertexBuffer, 0, this.vertexCount);

                this.vertexCount = 0;
            }
        }
    }
}
