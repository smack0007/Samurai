using System;
using System.Runtime.InteropServices;

namespace Samurai
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IEquatable<Point>
    {
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Point));

        public int X;

        public int Y;

        public Point(int width, int height)
        {
            this.X = width;
            this.Y = height;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Point))
                return false;

            return this.Equals((Point)obj);
        }

        public bool Equals(Point other)
        {
            return this.X == other.X &&
                   this.Y == other.Y;
        }

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }
    }
}
