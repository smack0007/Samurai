using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	public sealed class SpriteBatch : DisposableObject
	{
		public static readonly int SizeOfVertex = Marshal.SizeOf(typeof(Vertex));

		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector3 Position;
			public Color4 Color;
			public Vector2 UV;
		}

		GraphicsContext graphics;

		Vertex[] vertices;
		int vertexCount;
		Texture texture;

		DynamicVertexBuffer<Vertex> vertexBuffer;
		StaticIndexBuffer<ushort> indexBuffer;

		ISpriteBatchShaderProgram shader;
		bool drawInProgress;

		Matrix4 projection;
										
		public SpriteBatch(GraphicsContext graphics)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");

			this.graphics = graphics;

			this.vertices = new Vertex[1024 * 4];

			this.vertexBuffer = new DynamicVertexBuffer<Vertex>(this.graphics);

			ushort[] indices = new ushort[1024 * 6];
			for (ushort i = 0, vertex = 0; i < indices.Length; i += 6, vertex += 4)
			{
				indices[i] = vertex;
				indices[i + 1] = (ushort)(vertex + 1);
				indices[i + 2] = (ushort)(vertex + 2);
				indices[i + 3] = (ushort)(vertex + 2);
				indices[i + 4] = (ushort)(vertex + 3);
				indices[i + 5] = (ushort)(vertex + 1);
			}

			this.indexBuffer = new StaticIndexBuffer<ushort>(this.graphics, indices);

			this.projection = new Matrix4()
			{
				M33 = 1f,
				M44 = 1f,
				M41 = -1f,
				M42 = 1f
			};
		}

		~SpriteBatch()
		{
			this.Dispose(false);
		}

		protected override void DisposeManagedResources()
		{
			this.vertexBuffer.Dispose();
			this.vertexBuffer = null;

			this.indexBuffer.Dispose();
			this.indexBuffer = null;
		}
				
		private void EnsureDrawNotInProgressForProperty(string propertyName)
		{
			if (this.drawInProgress)
				throw new InvalidOperationException(string.Format("The property \"{0}\" may not be changed while drawing is in progress.", propertyName));
		}

		private void EnsureDrawInProgress()
		{
			if (!this.drawInProgress)
				throw new InvalidOperationException("Draw not currently in progress.");
		}

		public void Begin(ISpriteBatchShaderProgram shader)
		{
			if (shader == null)
				throw new ArgumentNullException("shader");

			if (this.drawInProgress)
				throw new InvalidOperationException("Draw already in progress.");

			this.shader = shader;
			this.drawInProgress = true;
		}

		public void End()
		{
			this.EnsureDrawInProgress();

			this.Flush();

			this.shader = null;
			this.drawInProgress = false;
		}

		private Vector2 CalculateUV(float x, float y)
		{
			Vector2 uv = Vector2.Zero;

			if (this.texture.Width != 1 || this.texture.Height != 1)
			{
				uv = new Vector2(x / (float)this.texture.Width, y / (float)this.texture.Height);
			}

			return uv;
		}

		private void AddQuad(
			Vector2 topLeft,
			Vector2 topRight,
			Vector2 bottomRight,
			Vector2 bottomLeft,
			Rectangle source,
			Color4 color)
		{
			if (this.vertexCount == this.vertices.Length)
				this.Flush();

			this.vertices[this.vertexCount].Position = new Vector3(topLeft, 0);
			this.vertices[this.vertexCount + 1].Position = new Vector3(topRight, 0);
			this.vertices[this.vertexCount + 2].Position = new Vector3(bottomLeft, 0);
			this.vertices[this.vertexCount + 3].Position = new Vector3(bottomRight, 0);

			this.vertices[this.vertexCount].UV = this.CalculateUV(source.Left, source.Top);
			this.vertices[this.vertexCount + 1].UV = this.CalculateUV(source.Right, source.Top);
			this.vertices[this.vertexCount + 2].UV = this.CalculateUV(source.Left, source.Bottom);
			this.vertices[this.vertexCount + 3].UV = this.CalculateUV(source.Right, source.Bottom);

			this.vertices[this.vertexCount].Color = color;
			this.vertices[this.vertexCount + 1].Color = color;
			this.vertices[this.vertexCount + 2].Color = color;
			this.vertices[this.vertexCount + 3].Color = color;

			this.vertexCount += 4;
		}

		public void Draw(Texture texture, Color4 tint, Vector2 destination)
		{
			this.Draw(texture, tint, destination, new Rectangle(0, 0, texture.Width, texture.Height));
		}

		public void Draw(Texture texture, Color4 tint, Rectangle destination)
		{
			this.Draw(texture, tint, destination, new Rectangle(0, 0, texture.Width, texture.Height));
		}

		public void Draw(Texture texture, Color4 tint, Vector2 destination, Rectangle source)
		{
			this.DrawInternal(texture, tint, destination, source.Width, source.Height, source);
		}

		public void Draw(Texture texture, Color4 tint, Rectangle destination, Rectangle source)
		{
			this.DrawInternal(texture, tint, new Vector2(destination.X, destination.Y), destination.Width, destination.Height, source);
		}

		private void DrawInternal(Texture texture, Color4 tint, Vector2 destination, int width, int height, Rectangle source)
		{
			if (texture == null)
				throw new ArgumentNullException("texture");

			if (texture != this.texture)
				this.Flush();

			this.texture = texture;

			this.AddQuad(
				new Vector2(destination.X, destination.Y),
				new Vector2(destination.X + width, destination.Y),
				new Vector2(destination.X + width, destination.Y + height),
				new Vector2(destination.X, destination.Y + height),
				source,
				tint);
		}

		public void Draw(Texture texture, Color4 tint, Vector2 destination, Rectangle source, Vector2 origin, Vector2 scale, float rotation)
		{
			this.DrawInternal(texture, tint, destination, source.Width, source.Height, source, origin, scale, rotation);
		}

		public void Draw(Texture texture, Color4 tint, Rectangle destination, Rectangle source, Vector2 origin, Vector2 scale, float rotation)
		{
			this.DrawInternal(texture, tint, new Vector2(destination.X, destination.Y), destination.Width, destination.Height, source, origin, scale, rotation);
		}

		private void DrawInternal(Texture texture, Color4 tint, Vector2 destination, int width, int height, Rectangle source, Vector2 origin, Vector2 scale, float rotation)
		{
			if (texture == null)
				throw new ArgumentNullException("texture");

			if (texture != this.texture)
				this.Flush();

			this.texture = texture;

			Matrix4 rotationMatrix;
			Matrix4.CreateRotationZ(rotation, out rotationMatrix);

			Matrix4 scaleMatrix;
			Matrix4.CreateScale(ref scale, out scaleMatrix);

			Matrix4 translationMatrix;
			Matrix4.CreateTranslation(destination.X, destination.Y, out translationMatrix);

			Matrix4 identity = Matrix4.Identity;
			
			Matrix4 transform1;
			Matrix4.Multiply(ref identity, ref scaleMatrix, out transform1);

			Matrix4 transform2;
			Matrix4.Multiply(ref transform1, ref rotationMatrix, out transform2);

			Matrix4 transform3;
			Matrix4.Multiply(ref transform2, ref translationMatrix, out transform3);

			Vector2 topLeft = new Vector2(-origin.X, -origin.Y).Transform(ref transform3);
			Vector2 topRight = new Vector2(width - origin.X, -origin.Y).Transform(ref transform3);
			Vector2 bottomRight = new Vector2(width - origin.X, height - origin.Y).Transform(ref transform3);
			Vector2 bottomLeft = new Vector2(-origin.X, height - origin.Y).Transform(ref transform3);

			this.AddQuad(
				topLeft,
				topRight,
				bottomRight,
				bottomLeft,
				source,
				tint);
		}

		public void Draw(SpriteSheet spriteSheet, int frame, Color4 tint, Vector2 position)
		{
			if (spriteSheet == null)
				throw new ArgumentNullException("spriteSheet");

			Rectangle frameRect = spriteSheet[frame];
			this.DrawInternal(spriteSheet.Texture, tint, position, frameRect.Width, frameRect.Height, frameRect);
		}

		public void Draw(SpriteSheet spriteSheet, int frame, Color4 tint, Vector2 position, Vector2 origin, Vector2 scale, float rotation)
		{
			if (spriteSheet == null)
				throw new ArgumentNullException("spriteSheet");

			Rectangle frameRect = spriteSheet[frame];
			this.DrawInternal(spriteSheet.Texture, tint, position, frameRect.Width, frameRect.Height, frameRect, origin, scale, rotation);
		}

		public void DrawString(TextureFont font, string text, Color4 tint, Vector2 position)
		{
			this.DrawString(font, text, tint, position, Vector2.One);
		}

		public void DrawString(TextureFont font, string text, Color4 tint, Vector2 position, Vector2 scale)
		{
			if (font == null)
				throw new ArgumentNullException("font");

			if (text == null)
				throw new ArgumentNullException("text");

			Size textSize = font.MeasureString(text);

			this.DrawString(font, text, new Rectangle((int)position.X, (int)position.Y, textSize.Width, textSize.Height), scale, tint);
		}

		public void DrawString(TextureFont font, string text, Rectangle destination, Color4 color)
		{
			this.DrawString(font, text, destination, Vector2.One, color);
		}

		public void DrawString(TextureFont font, string text, Rectangle destination, Vector2 scale, Color4 color)
		{
			if (font == null)
				throw new ArgumentNullException("font");

			if (text == null)
				throw new ArgumentNullException("text");

			if (text.Length == 0)
				return;

			float heightOfSingleLine = font.LineHeight * scale.Y;

			if (heightOfSingleLine > destination.Height) // We can't draw anything
				return;

			Vector2 cursor = new Vector2(destination.X, destination.Y);

			for (int i = 0; i < text.Length; i++)
			{
				// Skip characters we can't render.
				if (text[i] == '\r')
					continue;

				float widthOfChar = 0;

				if (text[i] == '\n' || cursor.X + (widthOfChar = font[text[i]].Width * scale.X) > destination.Right)
				{
					cursor.X = destination.X;
					cursor.Y += heightOfSingleLine + font.LineSpacing;

					// If the next line extends past the destination, quit.
					if (cursor.Y + heightOfSingleLine > destination.Bottom)
						return;

					// We can't render a new line.
					if (text[i] == '\n')
						continue;
				}

				Rectangle letterSource = font[text[i]];
				Rectangle letterDestination = new Rectangle((int)cursor.X, (int)cursor.Y, (int)widthOfChar, (int)heightOfSingleLine);

				this.Draw(font.Texture, color, letterDestination, letterSource);

				cursor.X += widthOfChar + font.CharacterSpacing;
			}
		}

		private void Flush()
		{
			if (this.vertexCount > 0)
			{
				this.vertexBuffer.SetData(this.vertices, 0, this.vertexCount);

				this.graphics.SetShaderProgram(this.shader.ShaderProgram);

				Rectangle viewport = this.graphics.Viewport;
				this.projection.M11 = 2f / viewport.Width;
				this.projection.M22 = -2f / viewport.Height;
								
				this.shader.SetProjectionMatrix(ref this.projection);
				this.shader.SetSampler(this.texture);

				this.graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer, this.indexBuffer);

				this.vertexCount = 0;
			}
		}
	}

}
