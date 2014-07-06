using System;

namespace Samurai.GameFramework
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
