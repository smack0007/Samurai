using Samurai.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Wpf
{
    public class GraphicsContextEventArgs : EventArgs
    {
        public GraphicsContext Context
        {
            get;
            private set;
        }

        public GraphicsContextEventArgs(GraphicsContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.Context = context;
        }
    }
}
