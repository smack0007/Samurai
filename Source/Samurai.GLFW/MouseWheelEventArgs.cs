using System;

namespace Samurai.GLFW
{
    public class MouseWheelEventArgs : MouseEventArgs
    {
        public double WheelDelta
        {
            get;
            set;
        }
    }
}
