using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planes
{
	public class Plane
	{
		private static readonly TimeSpan FrameDuration = TimeSpan.FromMilliseconds(50);
		const int FrameCount = 3;
		const float Speed = 100.0f;
		const int HalfSize = 32;
		const int Size = 64;

		TimeSpan frameTimer;

		Size windowSize;

		public int StartFrame
		{
			get;
			private set;
		}

		public int FrameOffset
		{
			get;
			private set;
		}

		public Vector2 Position
		{
			get;
			private set;
		}

		public float Rotation
		{
			get;
			private set;
		}

		public Plane(int startFrame, Vector2 position, float rotation, Size windowSize)
		{
			this.StartFrame = startFrame;
			this.Position = position;
			this.Rotation = rotation;
			this.windowSize = windowSize;
		}

		private static void RotateAboutOrigin(ref Vector2 point, ref Vector2 origin, float rotation, out Vector2 result)
		{
			Vector2 u = point - origin; // point relative to origin  

			if (u == Vector2.Zero)
			{
				result = point;
				return;
			}

			float a = (float)Math.Atan2(u.Y, u.X); // angle relative to origin  
			a += rotation; // rotate  

			// u is now the new point relative to origin
			float length = u.Length();
			u = new Vector2((float)Math.Cos(a) * length, (float)Math.Sin(a) * length);

			result = u + origin;
		}

		public void Update(TimeSpan elapsed)
		{
			this.frameTimer += elapsed;

			if (this.frameTimer >= FrameDuration)
			{
				this.FrameOffset++;
				this.frameTimer -= FrameDuration;

				if (this.FrameOffset >= FrameCount)
					this.FrameOffset -= FrameCount;
			}

			Vector2 position = this.Position;
			Vector2 newPosition = new Vector2(position.X, position.Y - ((float)elapsed.TotalSeconds * Speed));
			
			RotateAboutOrigin(ref newPosition, ref position, MathHelper.ToRadians(this.Rotation), out newPosition);

			if (newPosition.X < -HalfSize)
			{
				newPosition.X += this.windowSize.Width + Size;
			}
			else if (newPosition.X >= this.windowSize.Width + HalfSize)
			{
				newPosition.X -= this.windowSize.Width + Size;
			}

			if (newPosition.Y < -HalfSize)
			{
				newPosition.Y += this.windowSize.Height + Size;
			}
			else if (newPosition.Y >= this.windowSize.Height + HalfSize)
			{
				newPosition.Y -= this.windowSize.Height + Size;
			}
			
			this.Position = newPosition;
		}
	}
}
