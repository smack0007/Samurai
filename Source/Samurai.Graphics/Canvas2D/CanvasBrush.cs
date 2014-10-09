using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics.Canvas2D
{
    /// <summary>
    /// Base class for canvas brushes.
    /// </summary>
    public abstract class CanvasBrush : DisposableObject
    {
        internal Action StateChanging;

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

        public abstract void Apply(ref Matrix4 matrix);

        protected void TriggerStateChanging()
        {
            if (this.StateChanging != null)
                this.StateChanging();
        }
    }
}
