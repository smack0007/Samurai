using System;
using System.Collections.Generic;

namespace Samurai.Graphics
{
	public sealed class ShaderProgram : GraphicsObject
	{
		VertexShader vertexShader;
		FragmentShader fragmentShader;
        
        Dictionary<string, int> uniformLocations;

		internal uint Handle
		{
			get;
			private set;
		}
		
		public ShaderProgram(GraphicsContext graphics, VertexShader vertexShader, FragmentShader fragmentShader)
			: base(graphics)
		{
			if (vertexShader == null)
				throw new ArgumentNullException("vertexShader");

			if (fragmentShader == null)
				throw new ArgumentNullException("fragmentShader");

			this.Handle = this.Graphics.GL.CreateProgram();

			this.vertexShader = vertexShader;
			this.Graphics.GL.AttachShader(this.Handle, this.vertexShader.Handle);

			this.fragmentShader = fragmentShader;
			this.Graphics.GL.AttachShader(this.Handle, this.fragmentShader.Handle);

			this.Graphics.GL.LinkProgram(this.Handle);

            this.uniformLocations = new Dictionary<string, int>();
		}

		~ShaderProgram()
		{
			this.Dispose(false);
		}

		protected override void DisposeManagedResources()
		{
			if (!this.vertexShader.IsDisposed)
				this.vertexShader.Dispose();

			if (!this.fragmentShader.IsDisposed)
				this.fragmentShader.Dispose();
		}

		protected override void DisposeUnmanagedResources()
		{
			this.Graphics.GL.DeleteProgram(this.Handle);
		}

        private int GetUniformLocation(string name)
        {
            int result;
            
            if (!this.uniformLocations.TryGetValue(name, out result))
            {
                result = this.Graphics.GL.GetUniformLocation(this.Handle, name);
                this.uniformLocations[name] = result;
            }
            
            return result;
        }

        public void SetValue(string name, int value)
        {
            int location = this.GetUniformLocation(name);
            this.Graphics.GL.Uniform1i(location, value);
        }
		
		public void SetValue(string name, float value)
		{
            int location = this.GetUniformLocation(name);
			this.Graphics.GL.Uniform1f(location, value);
		}

        public void SetValue(string name, ref Vector2 value)
        {
            int location = this.GetUniformLocation(name);
            this.Graphics.GL.Uniform2f(location, value.X, value.Y);
        }

        public void SetValue(string name, ref Vector3 value)
        {
            int location = this.GetUniformLocation(name);
            this.Graphics.GL.Uniform3f(location, value.X, value.Y, value.Z);
        }

        public void SetValue(string name, ref Color4 value)
        {
            int location = this.GetUniformLocation(name);
            this.Graphics.GL.Uniform4f(location, value.R / 255, value.G / 255, value.B / 255, value.A / 255);
        }

		public void SetValue(string name, ref Matrix4 value)
		{
            int location = this.GetUniformLocation(name);
			this.Graphics.GL.UniformMatrix4(location, ref value);
		}

		public void SetValue(string name, Texture texture)
		{
            int location = this.GetUniformLocation(name);
			this.Graphics.GL.Uniform1i(location, (int)texture.Index);
		}
	}
}
