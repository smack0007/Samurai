using System;

namespace Samurai.GLFW
{
    public class MouseEventArgs : EventArgs
    {
        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }
    }
}
