using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	internal class GLContext
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
		public const uint ElementArrayBuffer = 0x8893;
		public const uint FragmentShader = 0x8B30;
		public const uint InfoLogLength = 0x8B84;
		public const uint StaticCopy = 0x88E6;
		public const uint StaticDraw = 0x88E4;
		public const uint StaticRead = 0x88E5;
		public const uint StencilBufferBit = 0x00000400;
		public const uint StreamCopy = 0x88E2;
		public const uint StreamDraw = 0x88E0;
		public const uint StreamRead = 0x88E1;
		public const uint VertexShader = 0x8B31;
		
		// Blend
		public const uint DstAlpha = 0x0304;
		public const uint DstColor = 0x0306;
		public const uint One = 1;
		public const uint OneMinusDstAlpha = 0x0305;
		public const uint OneMinusDstColor = 0x0307;
		public const uint OneMinusSrcAlpha = 0x0303;
		public const uint OneMinusSrcColor = 0x0301;
		public const uint SrcAlpha = 0x0302;
		public const uint SrcAlphaSaturate = 0x0308;
		public const uint SrcColor = 0x0300;
		public const uint Zero = 0;

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

		// Pixels
		public const uint Rgba = 0x1908;

		// Primitive Types
		public const uint Lines = 0x0001;
		public const uint Points = 0x0000;
		public const uint Triangles = 0x0004;

		// Textures
		public const uint ClampToEdge = 0x812F;
		public const uint Linear = 0x2601;
		public const uint Nearest = 0x2600;
		public const uint Repeat = 0x2901;
		public const uint Texture0 = 0x84C0;
		public const uint Texture2D = 0x0DE1;
		public const uint TextureMagFilter = 0x2800;
		public const uint TextureMinFilter = 0x2801;
		public const uint TextureWrapS = 0x2802;
		public const uint TextureWrapT = 0x2803;

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

		private delegate void __ActiveTexture(uint texture);
		private __ActiveTexture _ActiveTexture;

		private delegate void __AttachShader(uint program, uint shader);
		private __AttachShader _AttachShader;

		private delegate void __BindAttribLocation(uint program, uint index, string name);
		private __BindAttribLocation _BindAttribLocation;

		private delegate void __BindBuffer(uint target, uint buffer);
		private __BindBuffer _BindBuffer;

		[DllImport(Library, EntryPoint = "glBindTexture")]
		private static extern void _BindTexture(uint target, uint texture);

		private delegate void __BindVertexArray(uint array);
		private __BindVertexArray _BindVertexArray;

		[DllImport(Library, EntryPoint = "glBlendFunc")]
		private static extern void _BlendFunc(uint sfactor, uint dfactor);

		private delegate void __BufferData(uint target, IntPtr size, IntPtr data, uint usage);
		private __BufferData _BufferData;

		[DllImport(Library, EntryPoint = "glClear")]
		private static extern void _Clear(uint mask);

		[DllImport(Library, EntryPoint = "glClearColor")]
		private static extern void _ClearColor(float red, float green, float blue, float alpha);

		private delegate void __CompileShader(uint shader);
		private __CompileShader _CompileShader;

		private delegate uint __CreateProgram();
		private __CreateProgram _CreateProgram;

		private delegate uint __CreateShader(uint shaderType);
		private __CreateShader _CreateShader;

		private delegate void __DeleteBuffers(int n, uint[] buffers);
		private __DeleteBuffers _DeleteBuffers;
				
		private delegate void __DeleteProgram(uint program);
		private __DeleteProgram _DeleteProgram;

		[DllImport(Library, EntryPoint = "glDeleteTextures")]
		private static extern void _DeleteTextures(int n, uint[] textures);

		private delegate void __DeleteShader(uint shader);
		private __DeleteShader _DeleteShader;

		private delegate void __DeleteVertexArrays(int n, uint[] arrays);
		private __DeleteVertexArrays _DeleteVertexArrays;

		[DllImport(Library, EntryPoint = "glDisable")]
		private static extern void _Disable(uint cap);

		[DllImport(Library, EntryPoint = "glDrawArrays")]
		private static extern void _DrawArrays(uint mode, int first, int count);

		[DllImport(Library, EntryPoint = "glDrawElements")]
		private static extern void _DrawElements(uint mode, int count, uint type, IntPtr indices);

		[DllImport(Library, EntryPoint = "glEnable")]
		private static extern void _Enable(uint cap);

		private delegate void __EnableVertexAttribArray(uint index);
		private __EnableVertexAttribArray _EnableVertexAttribArray;

		private delegate void __GenBuffers(int n, [Out] uint[] buffers);
		private __GenBuffers _GenBuffers;

		[DllImport(Library, EntryPoint = "glGenTextures")]
		private static extern void _GenTextures(int n, [Out] uint[] textures);

		private delegate void __GenVertexArrays(int n, [Out] uint[] arrays);
		private __GenVertexArrays _GenVertexArrays;

		[DllImport(Library, EntryPoint = "glGetError")]
		public static extern uint GetError();

		private delegate void __GetShaderInfoLog(uint shader, int bufSize, out int length, [Out] StringBuilder infoLog);
		private __GetShaderInfoLog _GetShaderInfoLog;

		private delegate void __GetShaderiv(uint shader, uint pname, out int @params);
		private __GetShaderiv _GetShaderiv;

		[DllImport(Library, EntryPoint = "glGetTexImage")]
		private static extern void _GetTexImage(uint target, int level, uint format, uint type, [Out] byte[] img);

		private delegate int __GetUniformLocation(uint program, string name);
		private __GetUniformLocation _GetUniformLocation;

		private delegate void __LinkProgram(uint program);
		private __LinkProgram _LinkProgram;

		private delegate void __ShaderSource(uint shader, int count, string[] @string, ref int length);
		private __ShaderSource _ShaderSource;

		[DllImport(Library, EntryPoint = "glTexImage2D")]
		private static extern void _TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, byte[] pixels);

		[DllImport(Library, EntryPoint = "glTexParameterf")]
		private static extern void _TexParameterf(uint target, uint pname, float param);
				
		[DllImport(Library, EntryPoint = "glTexParameteri")]
		private static extern void _TexParameteri(uint target, uint pname, int param);

		private delegate void __Uniform1f(int location, float v0);
		private __Uniform1f _Uniform1f;

		private delegate void __Uniform1i(int location, int v0);
		private __Uniform1i _Uniform1i;

		private delegate void __UniformMatrix4fv(int location, int count, bool transpose, ref float value);
		private __UniformMatrix4fv _UniformMatrix4fv;

		private delegate void __UseProgram(uint program);
		private __UseProgram _UseProgram;

		private delegate void __VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
		private __VertexAttribPointer _VertexAttribPointer;

		[DllImport(Library, EntryPoint = "glViewport")]
		private static extern void _Viewport(int x, int y, int width, int height);

		private static object GetProcAddress<T>(Func<string, IntPtr> getProcFunc, string name)
		{
			Type delegateType = typeof(T);

			IntPtr proc = getProcFunc(name);

			if (proc == IntPtr.Zero)
				throw new SamuraiException(string.Format("Failed to load GL extension function: {0}.", name));

			return Marshal.GetDelegateForFunctionPointer(proc, delegateType);
		}

		public GLContext(Func<string, IntPtr> getProcFunc)
		{
			if (getProcFunc == null)
				throw new ArgumentNullException("getProcFunc");

			_ActiveTexture = (__ActiveTexture)GetProcAddress<__ActiveTexture>(getProcFunc, "glActiveTexture");
			_AttachShader = (__AttachShader)GetProcAddress<__AttachShader>(getProcFunc, "glAttachShader");
			_BindAttribLocation = (__BindAttribLocation)GetProcAddress<__BindAttribLocation>(getProcFunc, "glBindAttribLocation");
			_BindBuffer = (__BindBuffer)GetProcAddress<__BindBuffer>(getProcFunc, "glBindBuffer");
			_BindVertexArray = (__BindVertexArray)GetProcAddress<__BindVertexArray>(getProcFunc, "glBindVertexArray");
			_BufferData = (__BufferData)GetProcAddress<__BufferData>(getProcFunc, "glBufferData");
			_CompileShader = (__CompileShader)GetProcAddress<__CompileShader>(getProcFunc, "glCompileShader");
			_CreateProgram = (__CreateProgram)GetProcAddress<__CreateProgram>(getProcFunc, "glCreateProgram");
			_CreateShader = (__CreateShader)GetProcAddress<__CreateShader>(getProcFunc, "glCreateShader");
			_DeleteBuffers = (__DeleteBuffers)GetProcAddress<__DeleteBuffers>(getProcFunc, "glDeleteBuffers");
			_DeleteProgram = (__DeleteProgram)GetProcAddress<__DeleteProgram>(getProcFunc, "glDeleteProgram");
			_DeleteShader = (__DeleteShader)GetProcAddress<__DeleteShader>(getProcFunc, "glDeleteShader");
			_DeleteVertexArrays = (__DeleteVertexArrays)GetProcAddress<__DeleteVertexArrays>(getProcFunc, "glDeleteVertexArrays");
			_EnableVertexAttribArray = (__EnableVertexAttribArray)GetProcAddress<__EnableVertexAttribArray>(getProcFunc, "glEnableVertexAttribArray");
			_GenBuffers = (__GenBuffers)GetProcAddress<__GenBuffers>(getProcFunc, "glGenBuffers");
			_GenVertexArrays = (__GenVertexArrays)GetProcAddress<__GenVertexArrays>(getProcFunc, "glGenVertexArrays");
			_GetShaderInfoLog = (__GetShaderInfoLog)GetProcAddress<__GetShaderInfoLog>(getProcFunc, "glGetShaderInfoLog");
			_GetShaderiv = (__GetShaderiv)GetProcAddress<__GetShaderiv>(getProcFunc, "glGetShaderiv");
			_GetUniformLocation = (__GetUniformLocation)GetProcAddress<__GetUniformLocation>(getProcFunc, "glGetUniformLocation");
			_LinkProgram = (__LinkProgram)GetProcAddress<__LinkProgram>(getProcFunc, "glLinkProgram");
			_ShaderSource = (__ShaderSource)GetProcAddress<__ShaderSource>(getProcFunc, "glShaderSource");
			_Uniform1f = (__Uniform1f)GetProcAddress<__Uniform1f>(getProcFunc, "glUniform1f");
			_Uniform1i = (__Uniform1i)GetProcAddress<__Uniform1i>(getProcFunc, "glUniform1i");
			_UniformMatrix4fv = (__UniformMatrix4fv)GetProcAddress<__UniformMatrix4fv>(getProcFunc, "glUniformMatrix4fv");
			_UseProgram = (__UseProgram)GetProcAddress<__UseProgram>(getProcFunc, "glUseProgram");
			_VertexAttribPointer = (__VertexAttribPointer)GetProcAddress<__VertexAttribPointer>(getProcFunc, "glVertexAttribPointer");
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

		private void CheckErrors(string functionName)
		{
			uint errorCode = GetError();
			
			if (errorCode != GLContext.NoError)
				ThrowExceptionForErrorCode(functionName, errorCode);
		}

		public void ActiveTexture(uint texture)
		{
			_ActiveTexture(texture);
			CheckErrors("ActiveTexture");
		}

		public void AttachShader(uint program, uint shader)
		{
			_AttachShader(program, shader);
			CheckErrors("AttachShader");
		}

		public void BindAttribLocation(uint program, uint index, string name)
		{
			_BindAttribLocation(program, index, name);
			CheckErrors("BindAttribLocation");
		}

		public void BindBuffer(uint target, uint buffer)
		{
			_BindBuffer(target, buffer);
			CheckErrors("BindBuffer");
		}

		public void BindTexture(uint target, uint texture)
		{
			_BindTexture(target, texture);
			CheckErrors("BindTexture");
		}

		public void BindVertexArray(uint array)
		{
			_BindVertexArray(array);
			CheckErrors("BindVertexArray");
		}

		public void BlendFunc(uint sfactor, uint dfactor)
		{
			_BlendFunc(sfactor, dfactor);
			CheckErrors("BlendFunc");
		}

		public void BufferData<T>(uint target, T[] data, int index, int length, uint usage)
		{
			int sizeOfT = Marshal.SizeOf(typeof(T));
			GCHandle dataPtr = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr ptr = IntPtr.Add(dataPtr.AddrOfPinnedObject(), index * sizeOfT);

			try
			{
				_BufferData(target, (IntPtr)(sizeOfT * length), ptr, usage);
			}
			finally
			{
				dataPtr.Free();
			}

			CheckErrors("BufferData");
		}

		public void Clear(uint mask)
		{
			_Clear(mask);
			CheckErrors("Clear");
		}

		public void ClearColor(float red, float green, float blue, float alpha)
		{
			_ClearColor(red, green, blue, alpha);
			CheckErrors("ClearColor");
		}

		public void CompileShader(uint shader)
		{
			_CompileShader(shader);
			CheckErrors("CompileShader");
		}

		public uint CreateProgram()
		{
			uint program = _CreateProgram();
			CheckErrors("CreateProgram");
			return program;
		}

		public uint CreateShder(uint shaderType)
		{
			uint shader = _CreateShader(shaderType);
			CheckErrors("CreateShader");
			return shader;
		}

		public void DeleteBuffer(uint buffer)
		{
			UintArraySizeOne[0] = buffer;
			_DeleteBuffers(1, UintArraySizeOne);
			CheckErrors("DeleteBuffers");
		}

		public void DeleteBuffers(uint[] buffers)
		{
			_DeleteBuffers(buffers.Length, buffers);
			CheckErrors("DeleteBuffers");
		}

		public void DeleteProgram(uint program)
		{
			_DeleteProgram(program);
			CheckErrors("DeleteProgram");
		}

		public void DeleteTexture(uint texture)
		{
			UintArraySizeOne[0] = texture;
			_DeleteTextures(1, UintArraySizeOne);
			CheckErrors("DeleteTextures");
		}

		public void DeleteTextures(uint[] textures)
		{
			_DeleteBuffers(textures.Length, textures);
			CheckErrors("DeleteTextures");
		}

		public void DeleteShader(uint shader)
		{
			_DeleteShader(shader);
			CheckErrors("DeleteShader");
		}

		public void DeleteVertexArray(uint array)
		{
			UintArraySizeOne[0] = array;
			_DeleteVertexArrays(1, UintArraySizeOne);
			CheckErrors("DeleteVertexArrays");
		}

		public void DeleteVertexArrays(uint[] arrays)
		{
			_DeleteVertexArrays(arrays.Length, arrays);
			CheckErrors("DeleteVertexArrays");
		}

		public void Disable(uint cap)
		{
			_Disable(cap);
			CheckErrors("Disable");
		}

		public void DrawArrays(uint mode, int first, int count)
		{
			_DrawArrays(mode, first, count);
			CheckErrors("DrawArrays");
		}

		public void DrawElements(uint mode, int count, uint type, IntPtr indices)
		{
			_DrawElements(mode, count, type, indices);
			CheckErrors("DrawElements");
		}

		public void Enable(uint cap)
		{
			_Enable(cap);
			CheckErrors("Enable");
		}

		public void EnableVertexAttribArray(uint index)
		{
			_EnableVertexAttribArray(index);
			CheckErrors("EnableVertexAttribArray");
		}

		public uint GenBuffer()
		{
			_GenBuffers(1, UintArraySizeOne);
			CheckErrors("GenBuffers");
			return UintArraySizeOne[0];
		}

		public uint[] GenBuffers(int n)
		{
			uint[] buffers = new uint[n];
			_GenBuffers(n, buffers);
			CheckErrors("GenBuffers");
			return buffers;
		}

		public uint GenTexture()
		{
			_GenTextures(1, UintArraySizeOne);
			CheckErrors("GenTextures");
			return UintArraySizeOne[0];
		}

		public uint[] GenTextures(int n)
		{
			uint[] textures = new uint[n];
			_GenTextures(n, textures);
			CheckErrors("GenTextures");
			return textures;
		}

		public uint GenVertexArray()
		{
			_GenVertexArrays(1, UintArraySizeOne);
			CheckErrors("GenVertexArrays");
			return UintArraySizeOne[0];
		}

		public uint[] GenVertexArrays(int n)
		{
			uint[] arrays = new uint[n];
			_GenVertexArrays(n, arrays);
			CheckErrors("GenVertexArrays");
			return arrays;
		}

		public string GetShaderInfoLog(uint shader)
		{
			StringBuilder infoLog = new StringBuilder(1024);
			int length;
			_GetShaderInfoLog(shader, infoLog.Capacity, out length, infoLog);
			CheckErrors("GetShaderInfoLog");
			return infoLog.ToString();
		}

		public int GetShader(uint shader, uint pname)
		{
			int @params;
			_GetShaderiv(shader, pname, out @params);
			CheckErrors("GetShaderiv");
			return @params;
		}

		public void GetTexImage(uint target, int level, uint format, uint type, byte[] img)
		{
			_GetTexImage(target, level, format, type, img);
			CheckErrors("GetTexImage");
		}

		public int GetUniformLocation(uint program, string name)
		{
			int location = _GetUniformLocation(program, name);
			CheckErrors("GetUniformLocation");
			return location;
		}

		public void LinkProgram(uint program)
		{
			_LinkProgram(program);
			CheckErrors("LinkProgram");
		}

		public void ShaderSource(uint shader, string source)
		{
			string[] sources = new string[] { source };
            int length = source.Length;

			_ShaderSource(shader, 1, sources, ref length);

			CheckErrors("ShaderSource");
		}

		public void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, byte[] pixels)
		{
			_TexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
			CheckErrors("TexImage2D");
		}

		public void TexParameterf(uint target, uint pname, float param)
		{
			_TexParameterf(target, pname, param);
			CheckErrors("TexParameterf");
		}

		public void TexParameteri(uint target, uint pname, int param)
		{
			_TexParameteri(target, pname, param);
			CheckErrors("TexParameteri");
		}

		public void Uniform1f(int location, float v0)
		{
			_Uniform1f(location, v0);
			CheckErrors("Uniform1f");
		}

		public void Uniform1i(int location, int v0)
		{
			_Uniform1i(location, v0);
			CheckErrors("Uniform1i");
		}

		public void UniformMatrix4(int location, ref Matrix4 matrix4)
		{
			_UniformMatrix4fv(location, 1, false, ref matrix4.M11);
			CheckErrors("UniformMatrix4fv");
		}

		public void UseProgram(uint program)
		{
			_UseProgram(program);
			CheckErrors("UseProgram");
		}
					
		public void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
		{
			_VertexAttribPointer(index, size, type, normalized, stride, pointer);
			CheckErrors("VertexAttribPointer");
		}

		public void Viewport(int x, int y, int width, int height)
		{
			_Viewport(x, y, width, height);
			CheckErrors("Viewport");
		}
	}
}
