﻿using Samurai.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Canvas.GraphicsContextCreated += this.Canvas_ContextCreated;
            this.Canvas.Render += this.Canvas_Render;
        }

        private void Canvas_ContextCreated(object sender, GraphicsContextEventArgs e)
        {
        }

        private void Canvas_Render(object sender, GraphicsContextEventArgs e)
        {
            e.Graphics.Clear();
        }
    }
}
