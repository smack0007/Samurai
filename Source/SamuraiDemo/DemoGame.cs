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
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.0f), Color = new Color3(0.0f, 0.0f, 1.0f), UV = new Vector2(0.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.0f), Color = new Color3(1.0f, 0.0f, 0.0f), UV = new Vector2(0.0f, 1.0f) }
        };

		byte[] indexData = new byte[]
		{
			0, 1, 3, 1, 3, 2
		};

		ShaderProgram shaderProgram;
		VertexBuffer<Vertex> vertexBuffer;
		IndexBuffer<byte> indexBuffer;

		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";

			this.shaderProgram = new ShaderProgram(this.GraphicsDevice);
			this.shaderProgram.AttachShader(Shader.Compile(this.GraphicsDevice, ShaderType.Vertex, File.ReadAllText("Shader.vert")));
			this.shaderProgram.AttachShader(Shader.Compile(this.GraphicsDevice, ShaderType.Fragment, File.ReadAllText("Shader.frag")));
			this.shaderProgram.Link();

			this.vertexBuffer = new VertexBuffer<Vertex>(this.GraphicsDevice);
			this.vertexBuffer.SetData(this.vertexData);

			this.indexBuffer = new IndexBuffer<byte>(this.GraphicsDevice);
			this.indexBuffer.SetData(this.indexData);
		}

		protected override void Draw()
		{
			this.GraphicsDevice.Begin();

			this.shaderProgram.Use();

			Matrix4 projection;
			Matrix4.CreateRotationZ(10, out projection);
			this.shaderProgram.SetMatrix("projection", ref projection);

			this.GraphicsDevice.Draw(this.vertexBuffer, this.indexBuffer);

			this.GraphicsDevice.End();
		}

		protected override void Shutdown()
		{
			this.shaderProgram.Dispose();
			this.vertexBuffer.Dispose();
			this.indexBuffer.Dispose();
		}

		private static void Main(string[] args)
		{
			using (DemoGame game = new DemoGame())
				game.Run();
		}
	}
}
