using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Samurai;
using Samurai.Graphics;
using Samurai.Wpf;

namespace FragmentShaderPhotoStudio
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static readonly string vertexShader =
@"#version 330 

uniform vec2 vertSize; 
uniform mat4 vertTransform; 

layout(location = 0) in vec2 vertPosition; 
layout(location = 1) in vec2 vertTexCoords; 

smooth out vec2 texCoords;

void main() 
{ 
	gl_Position = vertTransform * vec4(vertPosition.x * vertSize.x, vertPosition.y * vertSize.y, 1.0, 1.0); 
    texCoords = vertTexCoords; 
}";

		private static readonly string defaultFragmentShader =
@"#version 330

smooth in vec2 texCoords; 

uniform sampler2D sampler;

out vec4 outColor; 

void main() 
{ 
	outColor = texture(sampler, vec2(texCoords.x, texCoords.y));
}";

		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector2 Position;
			public Vector2 TexCoords;
		}

		StaticVertexBuffer<Vertex> vertexBuffer;
		ShaderProgram shaderProgram;
		Texture2D texture;
		Matrix4 transform;

		public MainWindow()
		{
			this.InitializeComponent();

			using (Stream file = Assembly.GetExecutingAssembly().GetManifestResourceStream("FragmentShaderPhotoStudio.glsl.xshd"))
			using (XmlTextReader reader = new XmlTextReader(file))
			{
				var xshd = HighlightingLoader.LoadXshd(reader);
				this.ShaderCodeBox.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
			}

			this.ShaderCodeBox.Text = defaultFragmentShader;

			this.GraphicsBox.GraphicsContextCreated += this.GraphicsBox_GraphicsContextCreated;
			this.GraphicsBox.Render += this.GraphicsBox_Render;

			this.CompileButton.Click += this.CompileButton_Click;

			this.transform = new Matrix4()
			{
				M33 = 1f,
				M44 = 1f,
				M41 = -1f,
				M42 = 1f
			};
		}
		
		private void GraphicsBox_GraphicsContextCreated(object sender, GraphicsContextEventArgs e)
		{
			e.Graphics.ClearColor = Color4.CornflowerBlue;

			this.vertexBuffer = new StaticVertexBuffer<Vertex>(e.Graphics, new Vertex[]
			{
				new Vertex() { Position = Vector2.Zero, TexCoords = Vector2.Zero },
				new Vertex() { Position = Vector2.UnitX, TexCoords = Vector2.UnitX },
				new Vertex() { Position = Vector2.UnitY, TexCoords = Vector2.UnitY },
				
				new Vertex() { Position = Vector2.UnitY, TexCoords = Vector2.UnitY },
				new Vertex() { Position = Vector2.UnitX, TexCoords = Vector2.UnitX },
				new Vertex() { Position = Vector2.One, TexCoords = Vector2.One },
			});

			this.shaderProgram = new ShaderProgram(
				e.Graphics,
				VertexShader.Compile(e.Graphics, vertexShader),
				FragmentShader.Compile(e.Graphics, defaultFragmentShader));

			this.texture = Texture2D.LoadFromFile(e.Graphics, "SamuraiLogo.png", new TextureParams());
		}

		private void GraphicsBox_Render(object sender, GraphicsContextEventArgs e)
		{
			e.Graphics.Clear();

			e.Graphics.SetShaderProgram(this.shaderProgram);

			Vector2 size = new Vector2(256, 256);

			Samurai.Rectangle viewport = e.Graphics.Viewport;
			this.transform.M11 = 2f / viewport.Width;
			this.transform.M22 = -2f / viewport.Height;
			
			this.shaderProgram.SetValue("vertSize", ref size);
			this.shaderProgram.SetValue("vertTransform", ref this.transform);
			this.shaderProgram.SetValue("sampler", this.texture);
			
			e.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer);
		}

		private void CompileButton_Click(object sender, EventArgs e)
		{
			ShaderProgram program = null;

			try
			{
				program = new ShaderProgram(
					this.GraphicsBox.Graphics,
					VertexShader.Compile(this.GraphicsBox.Graphics, vertexShader),
					FragmentShader.Compile(this.GraphicsBox.Graphics, this.ShaderCodeBox.Text));
			}
			catch (SamuraiException ex)
			{
				program = null;
			}

			if (program != null)
			{
				this.shaderProgram.Dispose();
				this.shaderProgram = program;
			}
		}
	}
}
