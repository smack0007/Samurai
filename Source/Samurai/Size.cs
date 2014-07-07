using System;
using System.Runtime.InteropServices;

namespace Samurai
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Size : IEquatable<Size>
    {
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Size));
                
        public int Width;

        public int Height;

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Size))
                return false;

            return this.Equals((Size)obj);
        }

        public bool Equals(Size other)
        {
            return this.Width == other.Width &&
                   this.Height == other.Height;
        }

		public override int GetHashCode()
		{
			return this.Width.GetHashCode() ^ this.Height.GetHashCode();
		}

        public static bool operator ==(Size s1, Size s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(Size s1, Size s2)
        {
            return !s1.Equals(s2);
        }
    }
}
