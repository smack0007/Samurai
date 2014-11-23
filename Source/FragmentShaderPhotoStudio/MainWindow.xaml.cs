using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Samurai.Wpf;

namespace FragmentShaderPhotoStudio
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static readonly string defaultShader =
@"smooth in vec2 texCoords; 

uniform sampler2D sampler;

out vec4 outColor; 

void main() 
{ 
	outColor = texture(sampler, vec2(texCoords.x, texCoords.y));
}";

		public MainWindow()
		{
			this.InitializeComponent();

			using (Stream file = Assembly.GetExecutingAssembly().GetManifestResourceStream("FragmentShaderPhotoStudio.glsl.xshd"))
			using (XmlTextReader reader = new XmlTextReader(file))
			{
				var xshd = HighlightingLoader.LoadXshd(reader);
				this.ShaderCodeBox.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
			}

			this.ShaderCodeBox.Text = defaultShader;

			this.GraphicsBox.GraphicsContextCreated += this.GraphicsBox_GraphicsContextCreated;
			this.GraphicsBox.Render += this.GraphicsBox_Render;
		}
		
		private void GraphicsBox_GraphicsContextCreated(object sender, GraphicsContextEventArgs e)
		{
			e.Context.ClearColor = Color4.CornflowerBlue;
		}

		private void GraphicsBox_Render(object sender, GraphicsContextEventArgs e)
		{
			e.Context.Clear();
		}
	}
}
