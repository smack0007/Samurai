using Samurai;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.0f), Color = new Color3(255, 0, 0), UV = new Vector2(0.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.0f), Color = new Color3(0, 255, 0), UV = new Vector2(1.0f, 0.0f) },
            new Vertex() { Position = new Vector3(0.5f, 0.5f, 0.0f), Color = new Color3(0, 0, 255), UV = new Vector2(1.0f, 1.0f) },
            new Vertex() { Position = new Vector3(-0.5f, 0.5f, 0.0f), Color = new Color3(255, 255, 0), UV = new Vector2(0.0f, 1.0f) }
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
				this.Graphics,
				VertexShader.Compile(this.Graphics, File.ReadAllText("Shader.vert")),
				FragmentShader.Compile(this.Graphics, File.ReadAllText("Shader.frag")));

			this.Graphics.SetShaderProgram(this.shaderProgram);

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.Graphics, this.vertexData);
			this.indexBuffer = new StaticIndexBuffer<byte>(this.Graphics, this.indexData);

			this.texture0 = Texture.FromFile(this.Graphics, "Texture0.png", new TextureParams()
			{
				WrapS = TextureWrap.Repeat,
				WrapT = TextureWrap.Repeat
			});

			this.texture1 = Texture.FromFile(this.Graphics, "Texture1.png", new TextureParams()
			{
				WrapS = TextureWrap.Clamp,
				WrapT = TextureWrap.Clamp
			});
		}

		float rotation;

		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.CornflowerBlue);

			rotation += (float)(360.0 * elapsed.TotalSeconds);

			if (rotation >= 360.0f)
				rotation -= 360.0f;

			Matrix4 projection = 
				Matrix4.CreateRotationZ(MathHelper.ToRadians(rotation)) *
				Matrix4.InvertedYAxis;
					
			this.shaderProgram.SetMatrix("projection", ref projection);
			this.shaderProgram.SetSampler("texture0", this.texture1);

			this.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer, this.indexBuffer);
			//this.GraphicsDevice.Draw(PrimitiveType.Triangles, this.vertexBuffer, 0, 3);

			this.Graphics.SwapBuffers();
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
