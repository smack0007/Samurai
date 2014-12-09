using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Win32;
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
		private static readonly string vertexShaderCode =
@"#version 330 

uniform vec2 vertSize; 
uniform mat4 vertTransform; 

layout(location = 0) in vec2 vertPosition; 
layout(location = 1) in vec2 vertTexCoords; 

smooth out vec2 pixelCoords;

void main() 
{ 
	gl_Position = vertTransform * vec4(vertPosition.x * vertSize.x, vertPosition.y * vertSize.y, 1.0, 1.0); 
    pixelCoords = vertTexCoords; 
}";

		private static readonly string versionHeader = "#version 330" + Environment.NewLine;

		private static readonly string defaultFragmentShaderCode =
@"uniform sampler2D picture;
uniform vec2 pictureSize;
uniform float time;

smooth in vec2 pixelCoords;

out vec4 pixel; 

void main() 
{ 
	pixel = texture(picture, vec2(pixelCoords.x, pixelCoords.y));
	
	// TODO: Do stuff here...
}";

		[StructLayout(LayoutKind.Sequential)]
		struct Vertex
		{
			public Vector2 Position;
			public Vector2 TexCoords;
		}

		StaticVertexBuffer<Vertex> vertexBuffer;
		ShaderProgram currentShaderProgram;
		Texture2D currentPicture;
		Matrix4 transform;
		Stopwatch stopwatch;

		public MainWindow()
		{
			this.InitializeComponent();

			this.FileExitButton.Click += this.FileExitButton_Click;

			this.CompileButton.Click += this.CompileButton_Click;
			this.ImportPictureButton.Click += this.ImportPictureButton_Click;

			using (Stream file = Assembly.GetExecutingAssembly().GetManifestResourceStream("FragmentShaderPhotoStudio.glsl.xshd"))
			using (XmlTextReader reader = new XmlTextReader(file))
			{
				var xshd = HighlightingLoader.LoadXshd(reader);
				this.ShaderCodeBox.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
			}

			this.ShaderCodeBox.Text = defaultFragmentShaderCode;

			this.GraphicsBox.GraphicsContextCreated += this.GraphicsBox_GraphicsContextCreated;
			this.GraphicsBox.Render += this.GraphicsBox_Render;

			this.transform = new Matrix4()
			{
				M33 = 1f,
				M44 = 1f,
				M41 = -1f,
				M42 = 1f
			};

			this.stopwatch = new Stopwatch();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Key == Key.F5)
			{
				this.Compile();
			}
		}

		private void FileExitButton_Click(object sender, EventArgs e)
		{
			this.Exit();
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

			this.currentShaderProgram = new ShaderProgram(
				e.Graphics,
				VertexShader.Compile(e.Graphics, vertexShaderCode),
				FragmentShader.Compile(e.Graphics, versionHeader + defaultFragmentShaderCode));

			this.currentPicture = Texture2D.LoadFromFile(e.Graphics, "SamuraiLogo.png", new TextureParams());

			this.stopwatch.Start();
		}

		private void GraphicsBox_Render(object sender, GraphicsContextEventArgs e)
		{
			e.Graphics.Clear();

			e.Graphics.SetShaderProgram(this.currentShaderProgram);

			Vector2 size = new Vector2(this.currentPicture.Width, this.currentPicture.Height);

			Samurai.Rectangle viewport = e.Graphics.Viewport;
			this.transform.M11 = 2f / viewport.Width;
			this.transform.M22 = -2f / viewport.Height;
			
			this.currentShaderProgram.SetValue("vertSize", ref size);
			this.currentShaderProgram.SetValue("vertTransform", ref this.transform);
			this.currentShaderProgram.SetValue("picture", this.currentPicture);

			Vector2 pictureSize = new Vector2(this.currentPicture.Width, this.currentPicture.Height);
			this.currentShaderProgram.SetValue("pictureSize", ref pictureSize);

			this.currentShaderProgram.SetValue("time", (float)this.stopwatch.ElapsedMilliseconds);
			
			e.Graphics.Draw(PrimitiveType.Triangles, this.vertexBuffer);
		}

		private void CompileButton_Click(object sender, EventArgs e)
		{
			this.Compile();
		}

		private void ImportPictureButton_Click(object sender, EventArgs e)
		{
			this.ImportPicture();
		}

		private void Exit()
		{
			this.Close();
		}

		private void Compile()
		{
			ShaderProgram shaderProgram = null;
			VertexShader vertexShader = VertexShader.Compile(this.GraphicsBox.Graphics, vertexShaderCode);
			FragmentShader fragmentShader = null;

			this.OutputBox.Text = string.Format("Compile started at {0}:", DateTime.Now) + Environment.NewLine;
			this.OutputBox.Foreground = Brushes.Black;

			try
			{
				fragmentShader = FragmentShader.Compile(this.GraphicsBox.Graphics, versionHeader + this.ShaderCodeBox.Text);

				shaderProgram = new ShaderProgram(this.GraphicsBox.Graphics, vertexShader, fragmentShader);

				this.OutputBox.Text += "Shader compiled successfully.";

				this.stopwatch.Restart();
			}
			catch (ShaderCompilationException ex)
			{
				if (fragmentShader != null)
				{
					fragmentShader.Dispose();
				}

				shaderProgram = null;

				this.OutputBox.Text += "Failed to compile shader:" + Environment.NewLine + ex.ErrorText;
				this.OutputBox.Foreground = Brushes.Red;
			}

			if (shaderProgram != null)
			{
				this.currentShaderProgram.Dispose();
				this.currentShaderProgram = shaderProgram;
			}
		}

		private void ImportPicture()
		{
			OpenFileDialog dialog = new OpenFileDialog()
			{
				Filter = "Image Files (*.jpg,*.png)|*.jpg;*.png"
			};

			if (dialog.ShowDialog() == true)
			{
				Texture2D picture = null;

				try
				{
					picture = Texture2D.LoadFromFile(this.GraphicsBox.Graphics, dialog.FileName, new TextureParams());
				}
				catch (Exception)
				{
					picture = null;
				}

				if (picture != null)
				{
					this.currentPicture.Dispose();
					this.currentPicture = picture;
				}
			}
		}
	}
}
