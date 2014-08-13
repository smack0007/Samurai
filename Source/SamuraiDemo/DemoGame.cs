using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using Samurai.Input;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SamuraiDemo
{
	public class DemoGame : Game
	{
		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector3 Position;

			public Color3 Color;

			public Vector2 UV;
		}

		Vertex[] vertexData = new Vertex[]
        {
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.0f), Color = new Color3(255, 0, 0), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.0f), Color = new Color3(0, 255, 0), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.0f), Color = new Color3(0, 0, 255), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.0f), Color = new Color3(255, 255, 0), UV = new Vector2(0.0f, 1.0f) }
        };

		byte[] indexData = new byte[]
		{
			0, 1, 3, 1, 2, 3
		};

		ShaderProgram shaderProgram;
		StaticVertexBuffer<Vertex> vertexBuffer;
		StaticIndexBuffer<byte> indexBuffer;
		Texture2D texture0;
		Texture2D texture1;
		GamePad gamePad1;

		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";

			this.Graphics.CullMode = CullMode.Back;

			this.shaderProgram = new ShaderProgram(
				this.Graphics,
				VertexShader.Compile(this.Graphics, File.ReadAllText("Shader.vert")),
				FragmentShader.Compile(this.Graphics, File.ReadAllText("Shader.frag")));

			this.Graphics.SetShaderProgram(this.shaderProgram);

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.Graphics, this.vertexData);
			this.indexBuffer = new StaticIndexBuffer<byte>(this.Graphics, this.indexData);

			this.texture0 = Texture2D.LoadFromFile(this.Graphics, "Texture0.png", new TextureParams()
			{
				WrapS = TextureWrap.Repeat,
				WrapT = TextureWrap.Repeat
			});

			this.texture1 = Texture2D.LoadFromFile(this.Graphics, "Texture1.png", new TextureParams()
			{
				WrapS = TextureWrap.Clamp,
				WrapT = TextureWrap.Clamp
			});

			this.gamePad1 = new GamePad(GamePadIndex.One);
		}

		float rotationZ;
		float rotationX;

		protected override void Update(TimeSpan elapsed)
		{
			this.gamePad1.Update();

			this.rotationZ += (float)(360.0f * elapsed.TotalSeconds * this.gamePad1.LeftThumbStick.X);
			this.rotationX += (float)(360.0f * elapsed.TotalSeconds * this.gamePad1.LeftThumbStick.Y);
		}
		
		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.CornflowerBlue);

			Matrix4 projection =
				Matrix4.CreateRotationZ(MathHelper.ToRadians(this.rotationZ)) *
				Matrix4.CreateRotationX(MathHelper.ToRadians(this.rotationX));
					
			this.shaderProgram.SetProjection("projection", ref projection);
			this.shaderProgram.SetSampler("texture0", this.texture1);

			this.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer, this.indexBuffer);

			this.Graphics.SwapBuffers();
		}

		private static void Main(string[] args)
		{
			using (DemoGame game = new DemoGame())
				game.Run();
		}
	}
}
