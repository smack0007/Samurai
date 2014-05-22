using Samurai;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.0f), Color = new Color3(1.0f, 0.0f, 0.0f), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.0f), Color = new Color3(0.0f, 1.0f, 0.0f), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.0f), Color = new Color3(0.0f, 0.0f, 1.0f), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.0f), Color = new Color3(1.0f, 1.0f, 0.0f), UV = new Vector2(0.0f, 1.0f) }
        };

		byte[] indexData = new byte[]
		{
			0, 1, 3, 1, 3, 2
		};

		ShaderProgram shaderProgram;
		StaticVertexBuffer<Vertex> vertexBuffer;
		StaticIndexBuffer<byte> indexBuffer;
		Texture texture0;
		Texture texture1;

		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";

			this.shaderProgram = new ShaderProgram(
				this.GraphicsDevice,
				VertexShader.Compile(this.GraphicsDevice, File.ReadAllText("Shader.vert")),
				FragmentShader.Compile(this.GraphicsDevice, File.ReadAllText("Shader.frag")));

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.GraphicsDevice, this.vertexData);
			this.indexBuffer = new StaticIndexBuffer<byte>(this.GraphicsDevice, this.indexData);

			this.texture0 = Texture.FromFile(this.GraphicsDevice, "Texture0.png", new TextureParams()
			{
				WrapS = TextureWrap.Repeat,
				WrapT = TextureWrap.Repeat
			});

			this.texture1 = Texture.FromFile(this.GraphicsDevice, "Texture1.png", new TextureParams()
			{
				WrapS = TextureWrap.Clamp,
				WrapT = TextureWrap.Clamp
			});
		}

		protected override void Draw()
		{
			this.GraphicsDevice.Begin();

			this.shaderProgram.Use();

			Matrix4 projection = 
				Matrix4.CreateRotationZ(MathHelper.ToRadians(45)) *
				Matrix4.InvertedYAxis;
					
			this.shaderProgram.SetMatrix("projection", ref projection);
			this.shaderProgram.SetSampler("texture0", this.texture0);

			this.GraphicsDevice.Draw(this.vertexBuffer, this.indexBuffer);

			this.GraphicsDevice.End();
		}

		protected override void Shutdown()
		{
			this.shaderProgram.Dispose();
			this.vertexBuffer.Dispose();
			this.indexBuffer.Dispose();
			this.texture0.Dispose();
		}

		private static void Main(string[] args)
		{
			using (DemoGame game = new DemoGame())
				game.Run();
		}
	}
}
