using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Wpf
{
    public class GraphicsContextEventArgs : EventArgs
    {
        public GraphicsContext Graphics
        {
            get;
            private set;
        }

        public GraphicsContextEventArgs(GraphicsContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.Graphics = context;
        }
    }
}
