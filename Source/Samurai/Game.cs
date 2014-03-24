using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public abstract class Game
	{
		public GameWindow Window
		{
			get;
			private set;
		}

		public Game()
		{
			if (GLFW.Init() == 0)
				throw new SamuraiException("GLFW initialization failed.");

			GLFW.RegisterErrorCallback();

			GLFW.WindowHint(GLFW.CLIENT_API, GLFW.OPENGL_API);
			GLFW.WindowHint(GLFW.CONTEXT_VERSION_MAJOR, 3);
			GLFW.WindowHint(GLFW.CONTEXT_VERSION_MINOR, 3);
			GLFW.WindowHint(GLFW.OPENGL_FORWARD_COMPAT, 1);

			this.Window = new GameWindow();

			GL.Init();

			uint program = GL.CreateProgram();
			uint vertexShader = GL.CreateShder(GL.VertexShader);
			GL.ShaderSource(vertexShader, File.ReadAllText("Shader.vert"));
			GL.CompileShader(vertexShader);

			if (GL.GetShader(vertexShader, GL.CompileStatus) == 0)
			{
				string infoLog = GL.GetShaderInfoLog(vertexShader);
				throw new SamuraiException("Failed to compile shader: " + infoLog);
			}


		}

		public void Run()
		{
			GL.ClearColor(1.0f, 0, 0, 0);

			while (!this.Window.ShouldClose())
			{
				GLFW.PollEvents();

				GL.Clear(GL.ColorBufferBit | GL.DepthBufferBit);

				this.Window.SwapBuffers();
			}

			GLFW.Terminate();
		}

		public void Exit()
		{
			this.Window.SetShouldClose(true);
		}
	}
}
