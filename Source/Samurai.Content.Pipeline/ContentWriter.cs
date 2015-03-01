using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Content.Pipeline
{
	public class ContentWriter : IDisposable
	{
		BinaryWriter writer;

		public ContentWriter(string fileName)
		{
			Stream file = File.Open(fileName, FileMode.Create, FileAccess.Write);
			this.writer = new BinaryWriter(file);
		}

		public void Dispose()
		{
			this.writer.Dispose();
		}

		public void Write(bool value)
		{
			this.writer.Write(value);
		}
		
		public void Write(byte value)
		{
			this.writer.Write(value);
		}
		
		public void Write(byte[] buffer)
		{
			this.writer.Write(buffer);
		}
		
		public void Write(char ch)
		{
			this.writer.Write(ch);
		}
		
		public void Write(char[] chars)
		{
			this.writer.Write(chars);
		}
		
		public void Write(decimal value)
		{
			this.writer.Write(value);
		}
		
		public void Write(double value)
		{
			this.writer.Write(value);
		}
		
		public void Write(float value)
		{
			this.writer.Write(value);
		}
		
		public void Write(int value)
		{
			this.writer.Write(value);
		}
		
		public void Write(long value)
		{
			this.writer.Write(value);
		}
		
		public void Write(sbyte value)
		{
			this.writer.Write(value);
		}
		
		public void Write(short value)
		{
			this.writer.Write(value);
		}
				
		public void Write(string value)
		{
			this.writer.Write(value);
		}

		public void Write(uint value)
		{
			this.writer.Write(value);
		}

		public void Write(ulong value)
		{
			this.writer.Write(value);
		}

		public void Write(ushort value)
		{
			this.writer.Write(value);
		}

		public void Write(byte[] buffer, int index, int count)
		{
			this.writer.Write(buffer, index, count);
		}

		public void Write(char[] chars, int index, int count)
		{
			this.writer.Write(chars, index, count);
		}
	}
}
