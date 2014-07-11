using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Rectangle : IEquatable<Rectangle>
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Rectangle));

		public static readonly Rectangle Empty = new Rectangle();

		public int X;

		public int Y;

		public int Width;

		public int Height;

		/// <summary>
		/// The left side of the Rectangle.
		/// </summary>
		public int Left
		{
			get { return this.X; }
			set { this.X = value; }
		}

		/// <summary>
		/// The top side of the Rectangle.
		/// </summary>
		public int Top
		{
			get { return this.Y; }
			set { this.Y = value; }
		}

		/// <summary>
		/// The right side of the Rectangle.
		/// </summary>
		public int Right
		{
			get { return this.X + this.Width; }
			set { this.X = value - this.Width; }
		}

		/// <summary>
		/// The bottom side of the Rectangle.
		/// </summary>
		public int Bottom
		{
			get { return this.Y + this.Height; }
			set { this.Y = value - this.Height; }
		}

		public Rectangle(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Rectangle))
				return false;

			return this.Equals((Rectangle)obj);
		}

		public bool Equals(Rectangle other)
		{
			return this.X == other.X &&
				   this.Y == other.Y &&
				   this.Width == other.Width &&
				   this.Height == other.Height;
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Width.GetHashCode() ^ this.Height.GetHashCode();
		}

		/// <summary>
		/// Returns true if r1 instersects r2.
		/// </summary>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <returns></returns>
		public static bool Intersects(Rectangle r1, Rectangle r2)
		{
			return Intersects(ref r1, ref r2);
		}

		/// <summary>
		/// Returns true if r1 intersects r2.
		/// </summary>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <returns></returns>
		public static bool Intersects(ref Rectangle r1, ref Rectangle r2)
		{
			if (r2.Left > r1.Right || r2.Right < r1.Left ||
			   r2.Top > r1.Bottom || r2.Bottom < r1.Top)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Returns true if the Rectangle intersects with the other.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Intersects(Rectangle other)
		{
			return Intersects(ref this, ref other);
		}

		/// <summary>
		/// Returns true if the Rectangle intersects with the other.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Intersects(ref Rectangle other)
		{
			return Intersects(ref this, ref other);
		}

		/// <summary>
		/// Returns true if the Rectangle contains the given Point.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool Contains(Point point)
		{
			return point.X >= this.X &&
				   point.X <= this.X + this.Width &&
				   point.Y >= this.Y &&
				   point.Y <= this.Y + this.Height;
		}

		/// <summary>
		/// Returns true if the Rectangle contains the given Vector2.
		/// </summary>
		/// <param name="vec"></param>
		/// <returns></returns>
		public bool Contains(Vector2 vec)
		{
			return vec.X >= this.X &&
				   vec.X <= this.X + this.Width &&
				   vec.Y >= this.Y &&
				   vec.Y <= this.Y + this.Height;
		}

		/// <summary>
		/// Returns true if the Rectangle contains the given X and Y coordinate.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool Contains(int x, int y)
		{
			return x >= this.X &&
				   x <= this.X + this.Width &&
				   y >= this.Y &&
				   y <= this.Y + this.Height;
		}

		#region Operator Overloads

		public static bool operator ==(Rectangle r1, Rectangle r2)
		{
			return r1.Equals(r2);
		}

		public static bool operator !=(Rectangle r1, Rectangle r2)
		{
			return !r1.Equals(r2);
		}

		#endregion
	}
}
