﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Samurai.GLFW;
using Samurai.Graphics;

namespace Samurai.Samples.MiniTri
{
    public class MiniTriSample : Game
    {
		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector3 Position;
			public Color4 Color;
		};

		//static Vertex[] verticies = new Vertex[]
		//{
		//	new Vertex() { Position = new Vector3(0.0f, 0.5f, 0.5f), Color = new Color4(255, 0, 0, 255) },
		//	new Vertex() { Position = new Vector3(0.5f, -0.5f, 0.5f), Color = new Color4(0, 255, 0, 255) },
		//	new Vertex() { Position = new Vector3(-0.5f, -0.5f, 0.5f), Color = new Color4(0, 0, 255, 255) }
		//};

		static float[] verticies = new float[]
		{
			// Positions
			0.0f, 0.5f, 0.5f, 
			0.5f, -0.5f, 0.5f, 
			-0.5f, -0.5f, 0.5f, 

			// Colors
			1f, 0, 0, 1f,
			0, 1f, 0, 1f,
			0, 0, 1f, 1f
		};
		
		const string vertexShaderCode = @"
layout(location = 0) in vec3 inPosition; 
layout(location = 1) in vec4 inColor; 

out vec4 fragColor;

void main() 
{ 
	gl_Position = vec4(inPosition, 1.0);
	fragColor = inColor;
}";

		const string fragmentShaderCode = @"
in vec4 fragColor;

out vec4 outColor; 

void main() 
{ 
	outColor = fragColor;
}";

		//StaticVertexBuffer<Vertex> vertexBuffer;
		StaticVertexBuffer vertexBuffer;
		ShaderProgram shaderProgram;

		public MiniTriSample()
		{
			this.Window.Title = "Samurai Mini Tri Sample";

			this.Graphics.ClearColor = Color4.CornflowerBlue;

			//this.vertexBuffer = new StaticVertexBuffer<Vertex>(this.Graphics, verticies);

			VertexElement[] elements = new VertexElement[]
			{
				new VertexElement(VertexElementType.Float, 3, 0, 12),
				new VertexElement(VertexElementType.Float, 4, 36, 16)
			};

			using (DataBuffer data = DataBuffer.Create(verticies))
			{
				this.vertexBuffer = new StaticVertexBuffer(this.Graphics, elements, data);
			}

			this.shaderProgram = new ShaderProgram(this.Graphics,
				VertexShader.Compile(this.Graphics, vertexShaderCode),
				FragmentShader.Compile(this.Graphics, fragmentShaderCode)	
			);

			this.Graphics.ShaderProgram = this.shaderProgram;
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

			this.Graphics.SwapBuffers();
		}

		public static void Main()
		{
			using (var app = new MiniTriSample())
				app.Run();
		}
    }
}
