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
            public Vector2 Position;
            public Vector2 UV;
        }

        enum State
        {
            None,

            Line,

            Triangle,
   
            TriangleStrip,

            TriangleFan,
            
            Circle
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

        Action flushAction;

        public CanvasRenderer(GraphicsContext graphics)
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

            this.flushAction = this.Flush;
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

            this.brush = null;
            this.drawInProgress = false;
        }

        private void SetState(State state, int requiredVertices, CanvasBrush brush)
        {
            if (brush == null)
                throw new ArgumentNullException("brush");

            this.EnsureDrawInProgress();

            if (state != this.state ||
                (this.vertexCount + requiredVertices >= this.vertexBuffer.Count) ||
                brush != this.brush)
            {
                this.Flush();

                if (this.brush != null)
                    this.brush.StateChanging = null;
            }

            this.state = state;
            this.brush = brush;
            this.brush.StateChanging = this.flushAction;
        }
        
        private void AddVertex(ref Vector2 position, ref Vector2 uv)
        {
            this.vertices[this.vertexCount].Position = position;
            this.vertices[this.vertexCount].UV = uv;
            this.vertexCount++;
        }
                
        public void DrawLine(Vector2 start, Vector2 end, float width, CanvasBrush brush)
        {
            this.SetState(State.Line, 6, brush);

            float distance;
            Vector2.Distance(ref start, ref end, out distance);

            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            float halfWidth = width / 2.0f;
                       
            Vector2 v1Temp = new Vector2(start.X, start.Y - halfWidth);
            Vector2 v1;
            Vector2.RotateAboutOrigin(ref v1Temp, ref start, angle, out v1);

            Vector2 v2Temp = new Vector2(start.X + distance, start.Y - halfWidth);
            Vector2 v2;
            Vector2.RotateAboutOrigin(ref v2Temp, ref start, angle, out v2);

            Vector2 v3Temp = new Vector2(start.X + distance, start.Y + halfWidth);
            Vector2 v3;
            Vector2.RotateAboutOrigin(ref v3Temp, ref start, angle, out v3);

            Vector2 v4Temp = new Vector2(start.X, start.Y + halfWidth);
            Vector2 v4;
            Vector2.RotateAboutOrigin(ref v4Temp, ref start, angle, out v4);

            Vector2 v1UV = new Vector2(0, 0);
            Vector2 v2UV = new Vector2(distance, 0);
            Vector2 v3UV = new Vector2(distance, width);
            Vector2 v4UV = new Vector2(0, width);

            this.AddVertex(ref v1, ref v1UV);
            this.AddVertex(ref v2, ref v2UV);
            this.AddVertex(ref v4, ref v4UV);

            this.AddVertex(ref v2, ref v2UV);
            this.AddVertex(ref v3, ref v3UV);
            this.AddVertex(ref v4, ref v4UV);
        }
                
        public void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, CanvasBrush brush)
        {
            this.SetState(State.Triangle, 3, brush);

            Vector2 min = v1;

            if (v2.X < min.X)
                min.X = v2.X;

            if (v3.X < min.X)
                min.X = v3.X;

            if (v2.Y < min.Y)
                min.Y = v2.Y;

            if (v3.Y < min.Y)
                min.Y = v3.Y;

            Vector2 uv1 = new Vector2(v1.X - min.X, v1.Y - min.Y);
            Vector2 uv2 = new Vector2(v2.X - min.X, v2.Y - min.Y); 
            Vector2 uv3 = new Vector2(v3.X - min.X, v3.Y - min.Y);

            this.AddVertex(ref v1, ref uv1);
            this.AddVertex(ref v2, ref uv2);
            this.AddVertex(ref v3, ref uv3);
        }

        //private void DrawTriangleSet(State state, IList<Vector2> positions, CanvasBrush brush)
        //{
        //    if (positions == null)
        //        throw new ArgumentNullException("positions");

        //    this.SetState(state, positions.Count, brush);

        //    Vector2 uv = Vector2.Zero;
        //    for (int i = 0; i < positions.Count; i++)
        //    {
        //        Vector2 position = positions[i];
        //        this.AddVertex(ref position, ref uv);
        //    }
        //}

        //private void DrawTriangleSet(State state, Texture2D texture, IList<Vector2> positions, IList<Vector2> texCoords, Color4 tint)
        //{
        //    if (positions == null)
        //        throw new ArgumentNullException("positions");

        //    if (texCoords == null)
        //        throw new ArgumentNullException("texCoords");

        //    if (positions.Count != texCoords.Count)
        //        throw new InvalidOperationException("positions.Count and texCoords.Count must be equal.");

        //    this.SetState(state, positions.Count, texture);

        //    for (int i = 0; i < positions.Count; i++)
        //    {
        //        Vector2 position = positions[i];
        //        Vector2 uv = texCoords[i];
        //        this.AddVertex(ref position, ref tint, ref uv);
        //    }
        //}

        //public void DrawTriangleStrip(IList<Vector2> positions, Color4 tint)
        //{
        //    this.DrawTriangleSet(State.TriangleStrip, positions, tint);
        //}

        //public void DrawTriangleStrip(Texture2D texture, IList<Vector2> positions, IList<Vector2> texCoords, Color4 tint)
        //{
        //    this.DrawTriangleSet(State.TriangleStrip, texture, positions, texCoords, tint);
        //}

        //public void DrawTriangleFan(IList<Vector2> positions, Color4 tint)
        //{
        //    this.DrawTriangleSet(State.TriangleFan, positions, tint);
        //}

        //public void DrawTriangleFan(Texture2D texture, IList<Vector2> positions, IList<Vector2> texCoords, Color4 tint)
        //{
        //    this.DrawTriangleSet(State.TriangleFan, texture, positions, texCoords, tint);
        //}

        public void DrawCircle(Vector2 center, float radius, CanvasBrush brush)
        {
            this.SetState(State.Circle, 362, brush);

            Vector2 uv = new Vector2(radius, radius);
            this.AddVertex(ref center, ref uv);

            for (int i = 0; i <= 360; i++)
            {
                Vector2 temp = new Vector2(center.X, center.Y - radius);

                Vector2 position;
                Vector2.RotateAboutOrigin(ref temp, ref center, MathHelper.ToRadians(i), out position);

                this.AddVertex(ref position, ref tint, ref uv);
            }
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
