using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Samurai.Samples.Common;

namespace Samurai.Samples.MiniTri
{
    public class MiniTriApp : SampleApp
    {
		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector3 Position;
			public Color4 Color;
		};

		static Vertex[] verticies = new Vertex[]
		{
			new Vertex() { Position = new Vector3(0.0f, 0.5f, 0.5f), Color = new Color4(255, 0, 0, 255) },
            new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.5f), Color = new Color4(0, 255, 0, 255) },
			new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.5f), Color = new Color4(0, 0, 255, 255) }
		};
		
		const string vertexShaderCode = @"#version 330 core

layout(location = 0) in vec3 inPosition; 
layout(location = 1) in vec4 inColor; 

out vec4 fragColor;

void main() 
{ 
	gl_Position = vec4(inPosition, 1.0);
	fragColor = inColor;
}";

		const string fragmentShaderCode = @"#version 330

in vec4 fragColor;

out vec4 outColor; 

void main() 
{ 
	outColor = fragColor;
}";

		StaticVertexBuffer<Vertex> vertexBuffer;
		ShaderProgram shaderProgram;

		public MiniTriApp()
		{
			this.Title = "MiniTri Sample";

			this.Graphics.ClearColor = Color4.CornflowerBlue;

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.Graphics, verticies);

			this.shaderProgram = new ShaderProgram(this.Graphics,
				VertexShader.Compile(this.Graphics, vertexShaderCode),
				FragmentShader.Compile(this.Graphics, fragmentShaderCode)	
			);

			this.Graphics.SetShaderProgram(this.shaderProgram);
		}

		protected override void Dispose(bool disposing)
		{
			this.vertexBuffer.Dispose();

			base.Dispose(disposing);
		}

		protected override void Draw(TimeSpan elapsed)
		{
			this.Graphics.Clear();

			this.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer);
		}

		public static void Main()
		{
			using (var app = new MiniTriApp())
				app.Run();
		}
    }
}
