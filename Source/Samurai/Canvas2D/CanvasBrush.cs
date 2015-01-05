using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Canvas2D
{
    /// <summary>
    /// Base class for canvas brushes.
    /// </summary>
    public abstract class CanvasBrush : DisposableObject
    {
        public GraphicsContext Grahpics
        {
            get;
            private set;
        }

        public CanvasBrush(GraphicsContext graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            this.Grahpics = graphics;
        }

        public abstract void Apply(ref Matrix4 transform);
    }
}
