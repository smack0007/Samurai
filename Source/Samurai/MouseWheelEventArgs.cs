using System;

namespace Samurai
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
