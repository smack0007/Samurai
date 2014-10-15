using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics.Canvas2D
{
    public class CanvasRenderer : DisposableObject
    {
        [StructLayout(LayoutKind.Sequential)]
        struct Vertex
        {
            public Vector2 ModelPosition;

            public Vector2 ScreenPosition;
            
            public Vector2 TexCoord;
        }

        enum State
        {
            None,

            Line,

            Triangle,
   
            TriangleStrip,

            TriangleFan,
            
            Circle,

            Rectangle
        }

        GraphicsContext graphics;
        
        Vertex[] vertices;
        int vertexCount;

        DynamicVertexBuffer<Vertex> vertexBuffer;        
                        
        Matrix4 projection;

        BlendState oldBlendState;
        DepthBufferState oldDepthBufferState;
        RasterizerState oldRasterizerState;

        bool drawInProgress;
        State state;
        CanvasBrush brush;

        public CanvasRenderer(GraphicsContext graphics)
            : this(graphics, 1024)
        {
        }

        public CanvasRenderer(GraphicsContext graphics, int bufferSize)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            if (bufferSize < 512)
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize should be >= 512");

            this.graphics = graphics;

            this.vertices = new Vertex[1024];

            this.vertexBuffer = new DynamicVertexBuffer<Vertex>(this.graphics);

            this.projection = new Matrix4()
            {
                M33 = 1f,
                M44 = 1f,
                M41 = -1f,
                M42 = 1f
            };
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

            this.graphics.BlendState = this.oldBlendState;
            this.oldBlendState = null;

            this.graphics.DepthBufferState = this.oldDepthBufferState;
            this.oldDepthBufferState = null;

            this.graphics.RasterizerState = this.oldRasterizerState;
            this.oldRasterizerState = null;

            this.brush = null;
            this.drawInProgress = false;
        }

        private void SetState(State state, int requiredVertices, CanvasBrush brush)
        {
            if (brush == null)
                throw new ArgumentNullException("brush");

            this.EnsureDrawInProgress();
                        
            this.state = state;
            this.brush = brush;
        }
        
        private void AddVertex(ref Vector2 modelPosition, ref Vector2 screenPosition, ref Vector2 texCoord)
        {
            this.vertices[this.vertexCount].ModelPosition = modelPosition;
            this.vertices[this.vertexCount].ScreenPosition = screenPosition;
            this.vertices[this.vertexCount].TexCoord = texCoord;
            this.vertexCount++;
        }
                
        public void DrawLine(Vector2 start, Vector2 end, float width, CanvasBrush brush)
        {
            this.SetState(State.Line, 6, brush);

            float distance;
            Vector2.Distance(ref start, ref end, out distance);

            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            float halfWidth = width / 2.0f;

            Vector2 modelTopLeft = Vector2.Zero;
            Vector2 modelTopRight = new Vector2(distance, 0);
            Vector2 modelBottomRight = new Vector2(distance, width);
            Vector2 modelBottomLeft = new Vector2(0, width);

            Vector2 screenTopLeftTemp = new Vector2(start.X, start.Y - halfWidth);
            Vector2 screenTopLeft;
            Vector2.RotateAboutOrigin(ref screenTopLeftTemp, ref start, angle, out screenTopLeft);

            Vector2 screenTopRightTemp = new Vector2(start.X + distance, start.Y - halfWidth);
            Vector2 screenTopRight;
            Vector2.RotateAboutOrigin(ref screenTopRightTemp, ref start, angle, out screenTopRight);

            Vector2 screenBottomRightTemp = new Vector2(start.X + distance, start.Y + halfWidth);
            Vector2 screenBottomRight;
            Vector2.RotateAboutOrigin(ref screenBottomRightTemp, ref start, angle, out screenBottomRight);

            Vector2 screenBottomLeftTemp = new Vector2(start.X, start.Y + halfWidth);
            Vector2 screenBottomLeft;
            Vector2.RotateAboutOrigin(ref screenBottomLeftTemp, ref start, angle, out screenBottomLeft);

            Vector2 topLeftTexCoord = Vector2.Zero;
            Vector2 topRightUV = Vector2.UnitX;
            Vector2 bottomRightUV = Vector2.One;
            Vector2 bottomLeftUV = Vector2.UnitY;

            this.AddVertex(ref modelTopLeft, ref screenTopLeft, ref topLeftTexCoord);
            this.AddVertex(ref modelTopRight, ref screenTopRight, ref topRightUV);
            this.AddVertex(ref modelBottomLeft, ref screenBottomLeft, ref bottomLeftUV);

            this.AddVertex(ref modelTopRight, ref screenTopRight, ref topRightUV);
            this.AddVertex(ref modelBottomRight, ref screenBottomRight, ref bottomRightUV);
            this.AddVertex(ref modelBottomLeft, ref screenBottomLeft, ref bottomLeftUV);

            this.Flush();
        }
                
        public void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, CanvasBrush brush)
        {
            this.SetState(State.Triangle, 3, brush);

            Vector2 min = v1;
            Vector2 max = v1;

            if (v2.X < min.X)
                min.X = v2.X;

            if (v2.X > max.X)
                max.X = v2.X;

            if (v3.X < min.X)
                min.X = v3.X;

            if (v3.X > max.X)
                max.X = v3.X;

            if (v2.Y < min.Y)
                min.Y = v2.Y;

            if (v2.Y > max.Y)
                max.Y = v2.Y;

            if (v3.Y < min.Y)
                min.Y = v3.Y;

            if (v3.Y > max.Y)
                max.Y = v3.Y;

            Vector2 modelPosition1 = new Vector2(v1.X - min.X, v1.Y - min.Y);
            Vector2 modelPosition2 = new Vector2(v2.X - min.X, v2.Y - min.Y);
            Vector2 modelPosition3 = new Vector2(v3.X - min.X, v3.Y - min.Y);

            float width = max.X - min.X;
            float height = max.Y - min.Y;

            Vector2 texCoord1 = new Vector2(v1.X / width, v1.Y / height);
            Vector2 texCoord2 = new Vector2(v2.X / width, v2.Y / height);
            Vector2 texCoord3 = new Vector2(v3.X / width, v3.Y / height);

            this.AddVertex(ref modelPosition1, ref v1, ref texCoord1);
            this.AddVertex(ref modelPosition2, ref v2, ref texCoord2);
            this.AddVertex(ref modelPosition3, ref v3, ref texCoord3);

            this.Flush();
        }

        private void DrawTriangleSet(State state, IList<Vector2> positions, CanvasBrush brush)
        {
            if (positions == null)
                throw new ArgumentNullException("positions");

            if (positions.Count < 3)
                return;

            this.SetState(state, positions.Count, brush);

            Vector2 min = positions[0];
            Vector2 max = positions[0];

            for (int i = 1; i < positions.Count; i++)
            {
                if (positions[i].X < min.X)
                    min.X = positions[i].X;

                if (positions[i].Y < min.Y)
                    min.Y = positions[i].Y;

                if (positions[i].X > max.X)
                    max.X = positions[i].X;

                if (positions[i].Y > max.Y)
                    max.Y = positions[i].Y;
            }

            float width = max.X - min.X;
            float height = max.Y - min.Y;
            
            for (int i = 0; i < positions.Count; i++)
            {
                Vector2 modelPosition = new Vector2(positions[i].X - min.X, positions[i].Y - min.Y);
                Vector2 screenPosition = positions[i];
                Vector2 texCoord = new Vector2(positions[i].X / width, positions[i].Y / height);
                this.AddVertex(ref modelPosition, ref screenPosition, ref texCoord);
            }

            this.Flush();
        }

        public void DrawTriangleStrip(IList<Vector2> positions, CanvasBrush brush)
        {
            this.DrawTriangleSet(State.TriangleStrip, positions, brush);
        }

        public void DrawTriangleFan(IList<Vector2> positions, CanvasBrush brush)
        {
            this.DrawTriangleSet(State.TriangleFan, positions, brush);
        }

        public void DrawCircle(Vector2 center, float radius, CanvasBrush brush)
        {
            this.SetState(State.Circle, 362, brush);

            Vector2 modelPositionCenter = new Vector2(radius, radius);
            Vector2 texCoordCenter = new Vector2(0.5f, 0.5f);
            this.AddVertex(ref modelPositionCenter, ref center, ref texCoordCenter);

            Vector2 topLeft = new Vector2(center.X - radius, center.Y - radius);

            for (int i = 0; i <= 360; i++)
            {
                Vector2 screenPositionTemp = new Vector2(center.X, center.Y - radius);
                Vector2 screenPosition;
                Vector2.RotateAboutOrigin(ref screenPositionTemp, ref center, MathHelper.ToRadians(i), out screenPosition);

                Vector2 modelPosition = new Vector2(screenPosition.X - topLeft.X, screenPosition.Y - topLeft.Y);

                Vector2 texCoordTemp = new Vector2(0.5f, 0.0f);
                Vector2 texCoord;
                Vector2.RotateAboutOrigin(ref screenPositionTemp, ref texCoordCenter, MathHelper.ToRadians(i), out texCoord);

                this.AddVertex(ref modelPosition, ref screenPosition, ref texCoord);
            }

            this.Flush();
        }

        public void DrawRectangle(Vector2 topLeft, Vector2 bottomRight, CanvasBrush brush)
        {
            this.SetState(State.Rectangle, 4, brush);

            Vector2 topLeftModelPosition = Vector2.Zero;
            Vector2 topLeftScreenPosition = topLeft;
            Vector2 topLeftTexCoords = Vector2.Zero;

            Vector2 topRightModelPosition = new Vector2(bottomRight.X - topLeft.X, 0);
            Vector2 topRightScreenPosition = new Vector2(bottomRight.X, topLeft.Y);
            Vector2 topRightTexCoords = Vector2.UnitX;

            Vector2 bottomRightModelPosition = new Vector2(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
            Vector2 bottomRightScreenPosition = bottomRight;
            Vector2 bottomRightTexCoords = Vector2.One;

            Vector2 bottomLeftModelPosition = new Vector2(0, bottomRight.Y - topLeft.Y);
            Vector2 bottomLeftScreenPosition = new Vector2(topLeft.X, bottomRight.Y);
            Vector2 bottomLeftTexCoords = Vector2.UnitY;

            this.AddVertex(ref topLeftModelPosition, ref topLeftScreenPosition, ref topLeftTexCoords);
            this.AddVertex(ref topRightModelPosition, ref topRightScreenPosition, ref topRightTexCoords);
            this.AddVertex(ref bottomLeftModelPosition, ref bottomLeftScreenPosition, ref bottomLeftTexCoords);
            this.AddVertex(ref bottomRightModelPosition, ref bottomRightScreenPosition, ref bottomRightTexCoords);
            
            this.Flush();
        }

        public void DrawRectangle(Rectangle rectangle, CanvasBrush brush)
        {
            this.DrawRectangle(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Right, rectangle.Bottom), brush);
        }

        private void Flush()
        {
            if (this.vertexCount > 0)
            {
                this.vertexBuffer.SetData(this.vertices, 0, this.vertexCount);

                Rectangle viewport = this.graphics.Viewport;
                this.projection.M11 = 2f / viewport.Width;
                this.projection.M22 = -2f / viewport.Height;

                this.brush.Apply(ref this.projection);

                PrimitiveType primitiveType = PrimitiveType.Triangles;

                switch (this.state)
                {
                    case State.TriangleStrip:
                    case State.Rectangle:
                        primitiveType = PrimitiveType.TriangleStrip;
                        break;

                    case State.TriangleFan:
                    case State.Circle:
                        primitiveType = PrimitiveType.TriangleFan;
                        break;
                }

                this.graphics.Draw(primitiveType, this.vertexBuffer, 0, this.vertexCount);

                this.vertexCount = 0;
            }
        }
    }
}
