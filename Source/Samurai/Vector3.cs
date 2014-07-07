using System;
using System.Runtime.InteropServices;

namespace Samurai
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3 : IEquatable<Vector3>
	{
		public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Vector3));

		public static readonly Vector3 Zero = new Vector3();

		public static readonly Vector3 UnitX = new Vector3(1.0f, 0.0f, 0.0f);

		public static readonly Vector3 UnitY = new Vector3(0.0f, 1.0f, 0.0f);

		public static readonly Vector3 UnitZ = new Vector3(0.0f, 0.0f, 1.0f);

		public static readonly Vector3 One = new Vector3(1.0f, 1.0f, 1.0f);

		/// <summary>
		/// X component.
		/// </summary>
		public float X;

		/// <summary>
		/// Y component.
		/// </summary>
		public float Y;

		/// <summary>
		/// Z component.
		/// </summary>
		public float Z;

		/// <summary>
		/// Initializes a new Vector3.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Initializes a new Vector3.
		/// </summary>
		/// <param name="xy"></param>
		/// <param name="z"></param>
		public Vector3(Vector2 xy, float z)
		{
			this.X = xy.X;
			this.Y = xy.Y;
			this.Z = z;
		}

		public override string ToString()
		{
			return string.Format("{{ {0}, {1}, {2} }}", this.X, this.Y, this.Z);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Vector3))
				return false;

			return this.Equals((Vector3)obj);
		}

		public bool Equals(Vector3 other)
		{
			return (this.X == other.X) && (this.Y == other.Y) && (this.Z == other.Z);
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		/// <summary>
		/// Calculates the length of the Vector2.
		/// </summary>
		/// <returns></returns>
		public float Length()
		{
			return (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z));
		}

		/// <summary>
		/// Normalizes the Vector3.
		/// </summary>
		/// <returns></returns>
		public Vector3 Normalize()
		{
			float length = this.Length();
			return new Vector3(this.X / length, this.Y / length, this.Z / length);
		}

		public Vector3 Transform(ref Matrix4 matrix)
		{
			Vector3 result;
			Transform(ref this, ref matrix, out result);
			return result;
		}

		public static Vector3 Transform(ref Vector3 position, ref Matrix4 matrix)
		{
			Vector3 result;
			Transform(ref position, ref matrix, out result);
			return result;
		}

		public static void Transform(ref Vector3 position, ref Matrix4 matrix, out Vector3 result)
		{
			result = new Vector3((position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
								 (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
								 (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43);
		}

		#region Operator Overloads

		public static bool operator ==(Vector3 v1, Vector3 v2)
		{
			return (v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Z == v2.Z);
		}

		public static bool operator !=(Vector3 v1, Vector3 v2)
		{
			return (v1.X != v2.X) || (v1.Y != v2.Y) || (v1.Z != v2.Z);
		}

		public static Vector3 operator +(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
		}

		public static Vector3 operator -(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
		}

		public static Vector3 operator *(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
		}

		public static Vector3 operator *(Vector3 v, float val)
		{
			return new Vector3(v.X * val, v.Y * val, v.Z * val);
		}

		public static Vector3 operator /(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
		}

		public static Vector3 operator /(Vector3 v, float val)
		{
			return new Vector3(v.X / val, v.Y / val, v.Z / val);
		}

		#endregion
	}
}
