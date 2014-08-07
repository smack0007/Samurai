using Samurai;
using Samurai.GameFramework;
using Samurai.Graphics;
using Samurai.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FractalSample
{
	public class FractalSample : Game
	{
		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector3 Position;

			public Vector2 UV;
		}

		Vertex[] vertexData = new Vertex[]
        {
            new Vertex() { Position = new Vector3(-1, -1, 0), UV = new Vector2(0, 0) },
            new Vertex() { Position = new Vector3(1, -1, 0), UV = new Vector2(1, 0) },
            new Vertex() { Position = new Vector3(-1, 1, 0), UV = new Vector2(0, 1) },
            new Vertex() { Position = new Vector3(-1, 1, 0), UV = new Vector2(0, 1) },
			new Vertex() { Position = new Vector3(1, -1, 0), UV = new Vector2(1, 0) },
			new Vertex() { Position = new Vector3(1, 1, 0), UV = new Vector2(1, 1) }
        };
				
		ShaderProgram shaderProgram;
		StaticVertexBuffer<Vertex> vertexBuffer;
		Texture1D palette;
		Keyboard keyboard;

		float centerX = 0;
		float centerY = 0;
		float scale = 2f;
		float iterations = 10f;

		public FractalSample()
		{
			this.Window.Title = "Fractals";

			this.shaderProgram = new ShaderProgram(
				this.Graphics,
				VertexShader.Compile(this.Graphics, File.ReadAllText("Mandelbrot.vert")),
				FragmentShader.Compile(this.Graphics, File.ReadAllText("Mandelbrot.frag")));

			this.Graphics.SetShaderProgram(this.shaderProgram);

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.Graphics, this.vertexData);

			this.palette = Texture1D.LoadFromFile(this.Graphics, "Palette.png", new TextureParams()
				{
					WrapS = TextureWrap.Repeat,
					WrapT = TextureWrap.Repeat
				});

			this.keyboard = new Keyboard();
		}

		protected override void Update(TimeSpan elapsed)
		{
			this.keyboard.Update();

			if (this.keyboard.IsKeyDown(Key.Left))
				this.centerX -= 1.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.Right))
				this.centerX += 1.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.Up))
				this.centerY -= 1.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.Down))
				this.centerY += 1.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.A))
				this.scale -= 1.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.Z))
				this.scale += 1.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.Q))
				this.iterations -= 100.0f * (float)elapsed.TotalSeconds;

			if (this.keyboard.IsKeyDown(Key.W))
				this.iterations += 100.0f * (float)elapsed.TotalSeconds;
		}
		
		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear(Color4.CornflowerBlue);
						
			Matrix4 projection = Matrix4.InvertedYAxis;
					
			this.shaderProgram.SetValue("projection", ref projection);
			this.shaderProgram.SetSampler("palette", this.palette);
			this.shaderProgram.SetValue("centerX", this.centerX);
			this.shaderProgram.SetValue("centerY", this.centerY);
			this.shaderProgram.SetValue("scale", this.scale);
			this.shaderProgram.SetValue("iterations", this.iterations);

			this.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer);

			this.Graphics.SwapBuffers();
		}

		public static void Main(string[] args)
		{
			using (FractalSample game = new FractalSample())
				game.Run();
		}
	}
}
