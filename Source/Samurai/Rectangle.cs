using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Rectangle : IEquatable<Rectangle>
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Rectangle));

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

		public static bool operator ==(Rectangle r1, Rectangle r2)
		{
			return r1.Equals(r2);
		}

		public static bool operator !=(Rectangle r1, Rectangle r2)
		{
			return !r1.Equals(r2);
		}
	}
}
