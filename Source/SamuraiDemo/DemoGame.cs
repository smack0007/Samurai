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
		Texture texture;

		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";

			this.shaderProgram = new ShaderProgram(this.GraphicsDevice);
			this.shaderProgram.AttachShader(Shader.Compile(this.GraphicsDevice, ShaderType.Vertex, File.ReadAllText("Shader.vert")));
			this.shaderProgram.AttachShader(Shader.Compile(this.GraphicsDevice, ShaderType.Fragment, File.ReadAllText("Shader.frag")));
			this.shaderProgram.Link();

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.GraphicsDevice, this.vertexData);
			this.indexBuffer = new StaticIndexBuffer<byte>(this.GraphicsDevice, this.indexData);

			this.texture = Texture.FromFile(this.GraphicsDevice, "Texture.png", new TextureParams() {
				WrapS = TextureWrap.Clamp,
				WrapT = TextureWrap.Clamp
			});
		}

		protected override void Draw()
		{
			this.GraphicsDevice.Begin();

			this.shaderProgram.Use();

			Matrix4 projection = 
				Matrix4.CreateRotationZ(MathHelper.ToRadians(0)) *
				Matrix4.InvertedYAxis;
					
			this.shaderProgram.SetMatrix("projection", ref projection);
			this.shaderProgram.SetSampler("texture1", this.texture);

			this.GraphicsDevice.Draw(this.vertexBuffer, this.indexBuffer);

			this.GraphicsDevice.End();
		}

		protected override void Shutdown()
		{
			this.shaderProgram.Dispose();
			this.vertexBuffer.Dispose();
			this.indexBuffer.Dispose();
			this.texture.Dispose();
		}

		private static void Main(string[] args)
		{
			using (DemoGame game = new DemoGame())
				game.Run();
		}
	}
}
