using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	internal static class GL
	{
		private const string Library = "opengl32.dll";
				
		public const uint ArrayBuffer = 0x8892;
		public const uint Blend = 0x0BE2;
		public const uint ColorBufferBit = 0x00004000;
		public const uint CompileStatus = 0x8B81;
		public const uint DepthBufferBit = 0x00000100;
		public const uint DynamicCopy = 0x88EA;
		public const uint DynamicDraw = 0x88E8;
		public const uint DynamicRead = 0x88E9;
		public const uint FragmentShader = 0x8B30;
		public const uint InfoLogLength = 0x8B84;
		public const uint OneMinusSrcAlpha = 0x0303;
		public const uint SrcAlpha = 0x0302;
		public const uint StaticCopy = 0x88E6;
		public const uint StaticDraw = 0x88E4;
		public const uint StaticRead = 0x88E5;
		public const uint StencilBufferBit = 0x00000400;
		public const uint StreamCopy = 0x88E2;
		public const uint StreamDraw = 0x88E0;
		public const uint StreamRead = 0x88E1;
		public const uint Triangles = 0x0004;
		public const uint VertexShader = 0x8B31;
		
		// Error Codes
		public const uint InvalidEnum = 0x0500;
		public const uint InvalidFramebufferOperation = 0x0506;
		public const uint InvalidOperation = 0x0502;
		public const uint InvalidValue = 0x0501;
		public const uint NoError = 0;
		public const uint OutOfMemory = 0x0505;
		public const uint StackOverflow = 0x0503;
		public const uint StackUnderflow = 0x0504;
		public const uint TableTooLarge = 0x8031;

		// VertexAttribPointerType
		public const uint Byte = 0x1400;
		public const uint UnsignedByte = 0x1401;
		public const uint Short = 0x1402;
		public const uint UnsignedShort = 0x1403;
		public const uint Int = 0x1404;
		public const uint UnsignedInt = 0x1405;
		public const uint HalfFloat = 0x140B;
		public const uint Float = 0x1406;
		public const uint Double = 0x140A;
		public const uint Fixed = 0x140C;
		
		private static readonly uint[] UintArraySizeOne = new uint[1];

		private delegate void __AttachShader(uint program, uint shader);
		private static __AttachShader _AttachShader;

		private delegate void __BindAttribLocation(uint program, uint index, string name);
		private static __BindAttribLocation _BindAttribLocation;

		private delegate void __BindBuffer(uint target, uint buffer);
		private static __BindBuffer _BindBuffer;

		private delegate void __BindVertexArray(uint array);
		private static __BindVertexArray _BindVertexArray;

		[DllImport(Library, EntryPoint = "glBlendFunc")]
		private static extern void _BlendFunc(uint sfactor, uint dfactor);

		private delegate void __BufferData(uint target, IntPtr size, IntPtr data, uint usage);
		private static __BufferData _BufferData;

		[DllImport(Library, EntryPoint = "glClear")]
		private static extern void _Clear(uint mask);

		[DllImport(Library, EntryPoint = "glClearColor")]
		private static extern void _ClearColor(float red, float green, float blue, float alpha);

		private delegate void __CompileShader(uint shader);
		private static __CompileShader _CompileShader;

		private delegate uint __CreateProgram();
		private static __CreateProgram _CreateProgram;

		private delegate uint __CreateShader(uint shaderType);
		private static __CreateShader _CreateShader;

		private delegate void __DeleteBuffers(int n, uint[] buffers);
		private static __DeleteBuffers _DeleteBuffers;

		private delegate void __DeleteProgram(uint program);
		private static __DeleteProgram _DeleteProgram;

		private delegate void __DeleteShader(uint shader);
		private static __DeleteShader _DeleteShader;

		private delegate void __DeleteVertexArrays(int n, uint[] arrays);
		private static __DeleteVertexArrays _DeleteVertexArrays;

		[DllImport(Library, EntryPoint = "glDrawArrays")]
		private static extern void _DrawArrays(uint mode, int first, int count);

		[DllImport(Library, EntryPoint = "glEnable")]
		private static extern void _Enable(uint cap);

		private delegate void __EnableVertexAttribArray(uint index);
		private static __EnableVertexAttribArray _EnableVertexAttribArray;

		private delegate void __GenBuffers(int n, [Out] uint[] buffers);
		private static __GenBuffers _GenBuffers;

		private delegate void __GenVertexArrays(int n, [Out] uint[] arrays);
		private static __GenVertexArrays _GenVertexArrays;

		[DllImport(Library, EntryPoint = "glGetError")]
		public static extern uint GetError();

		private delegate void __GetShaderInfoLog(uint shader, int bufSize, out int length, [Out] StringBuilder infoLog);
		private static __GetShaderInfoLog _GetShaderInfoLog;

		private delegate void __GetShaderiv(uint shader, uint pname, out int @params);
		private static __GetShaderiv _GetShaderiv;

		private delegate int __GetUniformLocation(uint program, string name);
		private static __GetUniformLocation _GetUniformLocation;

		private delegate void __LinkProgram(uint program);
		private static __LinkProgram _LinkProgram;

		private delegate void __ShaderSource(uint shader, int count, string[] @string, ref int length);
		private static __ShaderSource _ShaderSource;

		private delegate void __UniformMatrix4fv(int location, int count, bool transpose, ref float value);
		private static __UniformMatrix4fv _UniformMatrix4fv;

		private delegate void __UseProgram(uint program);
		private static __UseProgram _UseProgram;

		private delegate void __VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
		private static __VertexAttribPointer _VertexAttribPointer;

		[DllImport(Library, EntryPoint = "glViewport")]
		private static extern void _Viewport(int x, int y, int width, int height);

		private static object GetProcAddress<T>(string name)
		{
			Type delegateType = typeof(T);

			IntPtr proc = GLFW.GetProcAddress(name);

			if (proc == IntPtr.Zero)
				throw new SamuraiException(string.Format("Failed to load GL extension function: {0}.", name));

			return Marshal.GetDelegateForFunctionPointer(proc, delegateType);
		}

		public static void Init()
		{
			_AttachShader = (__AttachShader)GetProcAddress<__AttachShader>("glAttachShader");
			_BindAttribLocation = (__BindAttribLocation)GetProcAddress<__BindAttribLocation>("glBindAttribLocation");
			_BindBuffer = (__BindBuffer)GetProcAddress<__BindBuffer>("glBindBuffer");
			_BindVertexArray = (__BindVertexArray)GetProcAddress<__BindVertexArray>("glBindVertexArray");
			_BufferData = (__BufferData)GetProcAddress<__BufferData>("glBufferData");
			_CompileShader = (__CompileShader)GetProcAddress<__CompileShader>("glCompileShader");
			_CreateProgram = (__CreateProgram)GetProcAddress<__CreateProgram>("glCreateProgram");
			_CreateShader = (__CreateShader)GetProcAddress<__CreateShader>("glCreateShader");
			_DeleteBuffers = (__DeleteBuffers)GetProcAddress<__DeleteBuffers>("glDeleteBuffers");
			_DeleteProgram = (__DeleteProgram)GetProcAddress<__DeleteProgram>("glDeleteProgram");
			_DeleteShader = (__DeleteShader)GetProcAddress<__DeleteShader>("glDeleteShader");
			_DeleteVertexArrays = (__DeleteVertexArrays)GetProcAddress<__DeleteVertexArrays>("glDeleteVertexArrays");
			_EnableVertexAttribArray = (__EnableVertexAttribArray)GetProcAddress<__EnableVertexAttribArray>("glEnableVertexAttribArray");
			_GenBuffers = (__GenBuffers)GetProcAddress<__GenBuffers>("glGenBuffers");
			_GenVertexArrays = (__GenVertexArrays)GetProcAddress<__GenVertexArrays>("glGenVertexArrays");
			_GetShaderInfoLog = (__GetShaderInfoLog)GetProcAddress<__GetShaderInfoLog>("glGetShaderInfoLog");
			_GetShaderiv = (__GetShaderiv)GetProcAddress<__GetShaderiv>("glGetShaderiv");
			_GetUniformLocation = (__GetUniformLocation)GetProcAddress<__GetUniformLocation>("glGetUniformLocation");
			_LinkProgram = (__LinkProgram)GetProcAddress<__LinkProgram>("glLinkProgram");
			_ShaderSource = (__ShaderSource)GetProcAddress<__ShaderSource>("glShaderSource");
			_UniformMatrix4fv = (__UniformMatrix4fv)GetProcAddress<__UniformMatrix4fv>("glUniformMatrix4fv");
			_UseProgram = (__UseProgram)GetProcAddress<__UseProgram>("glUseProgram");
			_VertexAttribPointer = (__VertexAttribPointer)GetProcAddress<__VertexAttribPointer>("glVertexAttribPointer");
		}

		private static void ThrowExceptionForErrorCode(string functionName, uint errorCode)
		{
			string errorName = "Unknown";

			switch (errorCode)
			{
				case InvalidEnum: errorName = "InvalidEnum"; break;
				case InvalidFramebufferOperation: errorName = "InvalidFramebufferOperation"; break;
				case InvalidOperation: errorName = "InvalidOperation"; break;
				case InvalidValue: errorName = "InvalidValue"; break;
				case OutOfMemory: errorName = "OutOfMemory"; break;
				case StackOverflow: errorName = "StackOverflow"; break;
				case StackUnderflow: errorName = "StackUnderflow"; break;
				case TableTooLarge: errorName = "TableTooLarge"; break;
			}

			throw new SamuraiException(string.Format("GL Error: Function gl{0} resulted in error code {1}.", functionName, errorName));
		}

		private static void CheckErrors(string functionName)
		{
			uint errorCode = GetError();
			
			if (errorCode != GL.NoError)
				ThrowExceptionForErrorCode(functionName, errorCode);
		}

		public static void AttachShader(uint program, uint shader)
		{
			_AttachShader(program, shader);
			CheckErrors("AttachShader");
		}

		public static void BindAttribLocation(uint program, uint index, string name)
		{
			_BindAttribLocation(program, index, name);
			CheckErrors("BindAttribLocation");
		}

		public static void BindBuffer(uint target, uint buffer)
		{
			_BindBuffer(target, buffer);
			CheckErrors("BindBuffer");
		}

		public static void BindVertexArray(uint array)
		{
			_BindVertexArray(array);
			CheckErrors("BindVertexArray");
		}

		public static void BlendFunc(uint sfactor, uint dfactor)
		{
			_BlendFunc(sfactor, dfactor);
			CheckErrors("BlendFunc");
		}

		public static void BufferData<T>(uint target, T[] data, uint usage)
		{
			GCHandle dataPtr = GCHandle.Alloc(data, GCHandleType.Pinned);
			try
			{
				_BufferData(target, (IntPtr)(Marshal.SizeOf(typeof(T)) * data.Length), dataPtr.AddrOfPinnedObject(), usage);
			}
			finally
			{
				dataPtr.Free();
			}
			CheckErrors("BufferData");
		}

		public static void Clear(uint mask)
		{
			_Clear(mask);
			CheckErrors("Clear");
		}

		public static void ClearColor(float red, float green, float blue, float alpha)
		{
			_ClearColor(red, green, blue, alpha);
			CheckErrors("ClearColor");
		}

		public static void CompileShader(uint shader)
		{
			_CompileShader(shader);
			CheckErrors("CompileShader");
		}

		public static uint CreateProgram()
		{
			uint program = _CreateProgram();
			CheckErrors("CreateProgram");
			return program;
		}

		public static uint CreateShder(uint shaderType)
		{
			uint shader = _CreateShader(shaderType);
			CheckErrors("CreateShader");
			return shader;
		}

		public static void DeleteBuffer(uint buffer)
		{
			UintArraySizeOne[0] = buffer;
			_DeleteBuffers(1, UintArraySizeOne);
			CheckErrors("DeleteBuffers");
		}

		public static void DeleteBuffers(uint[] buffers)
		{
			_DeleteBuffers(buffers.Length, buffers);
			CheckErrors("DeleteBuffers");
		}

		public static void DeleteProgram(uint program)
		{
			_DeleteProgram(program);
			CheckErrors("DeleteProgram");
		}

		public static void DeleteShader(uint shader)
		{
			_DeleteShader(shader);
			CheckErrors("DeleteShader");
		}

		public static void DeleteVertexArray(uint array)
		{
			UintArraySizeOne[0] = array;
			_DeleteVertexArrays(1, UintArraySizeOne);
			CheckErrors("DeleteVertexArrays");
		}

		public static void DeleteVertexArrays(uint[] arrays)
		{
			_DeleteVertexArrays(arrays.Length, arrays);
			CheckErrors("DeleteVertexArrays");
		}

		public static void DrawArrays(uint mode, int first, int count)
		{
			_DrawArrays(mode, first, count);
			CheckErrors("DrawArrays");
		}

		public static void Enable(uint cap)
		{
			_Enable(cap);
			CheckErrors("Enable");
		}

		public static void EnableVertexAttribArray(uint index)
		{
			_EnableVertexAttribArray(index);
			CheckErrors("EnableVertexAttribArray");
		}

		public static uint GenBuffer()
		{
			_GenBuffers(1, UintArraySizeOne);
			CheckErrors("GenBuffers");
			return UintArraySizeOne[0];
		}

		public static uint[] GenBuffers(int n)
		{
			uint[] buffers = new uint[n];
			_GenBuffers(n, buffers);
			CheckErrors("GenBuffers");
			return buffers;
		}

		public static uint GenVertexArray()
		{
			_GenVertexArrays(1, UintArraySizeOne);
			CheckErrors("GenVertexArrays");
			return UintArraySizeOne[0];
		}

		public static uint[] GenVertexArrays(int n)
		{
			uint[] arrays = new uint[n];
			_GenVertexArrays(n, arrays);
			CheckErrors("GenVertexArrays");
			return arrays;
		}

		public static string GetShaderInfoLog(uint shader)
		{
			StringBuilder infoLog = new StringBuilder(1024);
			int length;
			_GetShaderInfoLog(shader, infoLog.Capacity, out length, infoLog);
			CheckErrors("GetShaderInfoLog");
			return infoLog.ToString();
		}

		public static int GetShader(uint shader, uint pname)
		{
			int @params;
			_GetShaderiv(shader, pname, out @params);
			CheckErrors("GetShaderiv");
			return @params;
		}

		public static int GetUniformLocation(uint program, string name)
		{
			int location = _GetUniformLocation(program, name);
			CheckErrors("GetUniformLocation");
			return location;
		}

		public static void LinkProgram(uint program)
		{
			_LinkProgram(program);
			CheckErrors("LinkProgram");
		}

		public static void ShaderSource(uint shader, string source)
		{
			string[] sources = new string[] { source };
            int length = source.Length;

			_ShaderSource(shader, 1, sources, ref length);

			CheckErrors("ShaderSource");
		}

		public static void UniformMatrix4(int location, ref Matrix4 matrix4)
		{
			_UniformMatrix4fv(location, 1, false, ref matrix4.M11);
			CheckErrors("UniformMatrix4fv");
		}

		public static void UseProgram(uint program)
		{
			_UseProgram(program);
			CheckErrors("UseProgram");
		}
					
		public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
		{
			_VertexAttribPointer(index, size, type, normalized, stride, pointer);
			CheckErrors("VertexAttribPointer");
		}

		public static void Viewport(int x, int y, int width, int height)
		{
			_Viewport(x, y, width, height);
			CheckErrors("Viewport");
		}
	}
}
