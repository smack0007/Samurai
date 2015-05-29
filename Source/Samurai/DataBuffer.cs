using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public sealed class DataBuffer : DisposableObject
	{
		GCHandle gcHandle;

		public IntPtr DataPointer
		{
			get;
			private set;
		}

		public int Length
		{
			get;
			private set;
		}

		private DataBuffer()
		{
		}

		public static DataBuffer Create<T>(T[] data)
		{
			return Create<T>(data, 0, data.Length);
		}

		public static DataBuffer Create<T>(T[] data, int index, int count)
		{
			DataBuffer stream = new DataBuffer();

			int sizeOfT = Marshal.SizeOf(typeof(T));
			stream.gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			stream.DataPointer = IntPtr.Add(stream.gcHandle.AddrOfPinnedObject(), index * sizeOfT);
			stream.Length = count * sizeOfT;

			return stream;
		}

		protected override void DisposeUnmanagedResources()
		{
			 this.gcHandle.Free();
		}
	}
}
