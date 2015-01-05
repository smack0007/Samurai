using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2 : IEquatable<Vector2>
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Vector2));

		public static readonly Vector2 Zero = new Vector2();

		public static readonly Vector2 UnitX = new Vector2(1.0f, 0.0f);

		public static readonly Vector2 UnitY = new Vector2(0.0f, 1.0f);

		public static readonly Vector2 One = new Vector2(1.0f, 1.0f);

		/// <summary>
		/// X component.
		/// </summary>
		public float X;

		/// <summary>
		/// Y component.
		/// </summary>
		public float Y;

		/// <summary>
		/// Initializes a new Vector2.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}
				
		public override string ToString()
		{
			return string.Format("{{ {0}, {1} }}", this.X, this.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Vector2))
				return false;

			return this.Equals((Vector2)obj);
		}

		public bool Equals(Vector2 other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		public float Angle()
		{
			return (float)Math.Atan2(this.X, -this.Y);
		}

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            float result;
            Distance(ref v1, ref v2, out result);
            return result;
        }

        public static void Distance(ref Vector2 v1, ref Vector2 v2, out float result)
        {
            float x = v1.X - v2.X;
            float y = v1.Y - v2.Y;

            result = (float)Math.Sqrt((x * x) + (y * y));
        }

		/// <summary>
		/// Calculates the length of the Vector2.
		/// </summary>
		/// <returns></returns>
		public float Length()
		{
			return (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y));
		}

		/// <summary>
		/// Normalizes the Vector2.
		/// </summary>
		/// <returns></returns>
		public Vector2 Normalize()
		{
			float length = this.Length();
			return new Vector2(this.X / length, this.Y / length);
		}

		public Vector2 Transform(ref Matrix4 matrix)
		{
			Vector2 result;
			Transform(ref this, ref matrix, out result);
			return result;
		}

		public static Vector2 Transform(ref Vector2 position, ref Matrix4 matrix)
		{
			Vector2 result;
			Transform(ref position, ref matrix, out result);
			return result;
		}

		public static void Transform(ref Vector2 position, ref Matrix4 matrix, out Vector2 result)
		{
			result = new Vector2((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
								 (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
		}

		#region Operator Overloads

		public static bool operator ==(Vector2 v1, Vector2 v2)
		{
			return v1.Equals(v2);
		}

		public static bool operator !=(Vector2 v1, Vector2 v2)
		{
			return !v1.Equals(v2);
		}

		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static Vector2 operator *(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
		}

		public static Vector2 operator *(Vector2 v, float val)
		{
			return new Vector2(v.X * val, v.Y * val);
		}

		public static Vector2 operator /(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
		}

		public static Vector2 operator /(Vector2 v, float val)
		{
			return new Vector2(v.X / val, v.Y / val);
		}

		#endregion
	}
}
