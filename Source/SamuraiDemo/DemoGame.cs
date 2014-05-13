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
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.0f), Color = new Color3(0.0f, 0.0f, 1.0f), UV = new Vector2(0.0f, 1.0f) },

            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.0f), Color = new Color3(1.0f, 0.0f, 0.0f), UV = new Vector2(0.0f, 1.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.0f), Color = new Color3(0.0f, 1.0f, 0.0f), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.0f), Color = new Color3(0.0f, 0.0f, 1.0f), UV = new Vector2(1.0f, 0.0f) },
        };

		ShaderProgram shaderProgram;
		DrawBuffer<Vertex> drawBuffer;

		public DemoGame()
		{
			this.Window.Title = "Samurai Demo";

			this.shaderProgram = new ShaderProgram(this.GraphicsDevice);
			this.shaderProgram.AttachShader(Shader.Compile(this.GraphicsDevice, ShaderType.Vertex, File.ReadAllText("Shader.vert")));
			this.shaderProgram.AttachShader(Shader.Compile(this.GraphicsDevice, ShaderType.Fragment, File.ReadAllText("Shader.frag")));
			this.shaderProgram.Link();

			this.drawBuffer = new DrawBuffer<Vertex>(this.GraphicsDevice);
			this.drawBuffer.SetData(this.vertexData);
		}

		protected override void Draw()
		{
			this.GraphicsDevice.Begin();

			this.shaderProgram.Use();

			Matrix4 projection;
			Matrix4.CreateRotationZ(10, out projection);
			this.shaderProgram.SetMatrix("projection", ref projection);

			this.GraphicsDevice.Draw(this.drawBuffer);

			this.GraphicsDevice.End();
		}

		protected override void Shutdown()
		{
			this.shaderProgram.Dispose();
			this.drawBuffer.Dispose();
		}

		private static void Main(string[] args)
		{
			using (DemoGame game = new DemoGame())
				game.Run();
		}
	}
}
