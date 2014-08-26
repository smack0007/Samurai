using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using Samurai.Input;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Samurai.UserInterface;

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
			// Front
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, -0.5f), Color = new Color3(255, 0, 0), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, -0.5f), Color = new Color3(255, 0, 0), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, -0.5f), Color = new Color3(255, 0, 0), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, -0.5f), Color = new Color3(255, 0, 0), UV = new Vector2(0.0f, 1.0f) },

			// Back
			new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.5f), Color = new Color3(0, 0, 255), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.5f), Color = new Color3(0, 0, 255), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.5f), Color = new Color3(0, 0, 255), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.5f), Color = new Color3(0, 0, 255), UV = new Vector2(0.0f, 1.0f) },

			// Left
			new Vertex() { Position = new Vector3(-0.5f, -0.5f, -0.5f), Color = new Color3(0, 255, 0), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.5f), Color = new Color3(0, 255, 0), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.5f), Color = new Color3(0, 255, 0), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, -0.5f), Color = new Color3(0, 255, 0), UV = new Vector2(0.0f, 1.0f) },

			// Right
			new Vertex() { Position = new Vector3(0.5f, -0.5f, -0.5f), Color = new Color3(0, 192, 192), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.5f), Color = new Color3(0, 192, 192), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.5f), Color = new Color3(0, 192, 192), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, -0.5f), Color = new Color3(0, 192, 192), UV = new Vector2(0.0f, 1.0f) },

			// Top
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.5f), Color = new Color3(255, 0, 255), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.5f), Color = new Color3(255, 0, 255), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, -0.5f), Color = new Color3(255, 0, 255), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, -0.5f), Color = new Color3(255, 0, 255), UV = new Vector2(0.0f, 1.0f) },

			// Bottom
			new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.5f), Color = new Color3(255, 255, 255), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.5f), Color = new Color3(255, 255, 255), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, -0.5f), Color = new Color3(255, 255, 255), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, -0.5f), Color = new Color3(255, 255, 255), UV = new Vector2(0.0f, 1.0f) },
        };

		byte[] indexData = new byte[]
		{
			0, 1, 3, 1, 2, 3,
			
			7, 5, 4, 7, 6, 5,

			11, 9, 8, 11, 10, 9,
			
			12, 13, 15, 13, 14, 15,

			16, 17, 19, 17, 18, 19,
			
			23, 21, 20, 23, 22, 21,
		};

		float rotationZ;

		float translationZ = 5.0f;
		float translationX;

		ShaderProgram shaderProgram;
		StaticVertexBuffer<Vertex> vertexBuffer;
		StaticIndexBuffer<byte> indexBuffer;
		Texture2D texture0;
		Texture2D texture1;
		GamePad gamePad1;

		ControlRenderer controlRenderer;
		Label label;
		
		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";

			this.Graphics.FrontFace = FrontFace.Clockwise;

			this.Graphics.BlendState = BlendState.AlphaBlend;
						
			this.shaderProgram = new ShaderProgram(
				this.Graphics,
				VertexShader.Compile(this.Graphics, File.ReadAllText("Shader.vert")),
				FragmentShader.Compile(this.Graphics, File.ReadAllText("Shader.frag")));

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

			this.controlRenderer = new ControlRenderer(this.Graphics);
			this.controlRenderer.DefaultFont = TextureFont.Build(this.Graphics, "Segoe UI", 72, new TextureFontParams()
				{
					BackgroundColor = Color4.Transparent
				});

			this.label = new Label()
			{
				Text = "Hello World!",
				Position = new Vector2(100, 100),
				Font = TextureFont.Build(this.Graphics, "Segoe UI", 24, new TextureFontParams() { BackgroundColor = Color4.Transparent })
			};
		}

		protected override void Update(TimeSpan elapsed)
		{
			this.gamePad1.Update();

			Vector2 leftThumbStick = this.gamePad1.LeftThumbStick;
			this.rotationZ += (float)(2.0f * elapsed.TotalSeconds * -leftThumbStick.X);
			this.translationX += (float)(5.0f * elapsed.TotalSeconds * Math.Sin(this.rotationZ) * leftThumbStick.Y);
			this.translationZ += (float)(5.0f * elapsed.TotalSeconds * Math.Cos(this.rotationZ) * leftThumbStick.Y);
		}
		
		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.CornflowerBlue);

			this.Graphics.DepthTestEnabled = true;
			this.Graphics.CullMode = CullMode.Back;

			Vector3 eye = new Vector3(-this.translationX, 0, this.translationZ);
			Vector3 target = new Vector3(-this.translationX + (float)Math.Sin(this.rotationZ), 0, this.translationZ + (float)-Math.Cos(this.rotationZ));
			Vector3 up = Vector3.UnitY;

			Matrix4 projection =
				Matrix4.LookAt(ref eye, ref target, ref up) * 
				Matrix4.PerspectiveFOV(120.0f, (float)this.Window.Width / (float)this.Window.Height, 0.1f, 100.0f);

			this.Graphics.SetShaderProgram(this.shaderProgram);
			this.shaderProgram.SetProjection("projection", ref projection);
			this.shaderProgram.SetSampler("texture0", this.texture1);

			this.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer, this.indexBuffer);

			this.Graphics.DepthTestEnabled = false;
			this.Graphics.CullMode = CullMode.None;

			this.controlRenderer.Begin();
			this.label.Draw(this.controlRenderer);
			this.controlRenderer.End();

			this.Graphics.SwapBuffers();
		}

		private static void Main(string[] args)
		{
			using (DemoGame game = new DemoGame())
				game.Run();
		}
	}
}
