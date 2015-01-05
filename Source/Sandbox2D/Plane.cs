using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDemo2D
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
			Vector2.RotateAboutOrigin(ref newPosition, ref position, MathHelper.ToRadians(this.Rotation), out newPosition);

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
