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

		private delegate void __DeleteProgram(uint program);
		private static __DeleteProgram _DeleteProgram;

		private delegate void __DeleteShader(uint shader);
		private static __DeleteShader _DeleteShader;

		private delegate void __GetShaderInfoLog(uint shader, int bufSize, out int length, [Out] StringBuilder infoLog);
		private static __GetShaderInfoLog _GetShaderInfoLog;

		private delegate void __GetShaderiv(uint shader, uint pname, out int @params);
		private static __GetShaderiv _GetShaderiv;

		private delegate void __LinkProgram(uint program);
		private static __LinkProgram _LinkProgram;

		private delegate void __ShaderSource(uint shader, int count, string[] @string, ref int length);
		private static __ShaderSource _ShaderSource;

		private delegate void __UseProgram(uint program);
		private static __UseProgram _UseProgram;

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
			_CompileShader = (__CompileShader)GetProcAddress<__CompileShader>("glCompileShader");
			_CreateProgram = (__CreateProgram)GetProcAddress<__CreateProgram>("glCreateProgram");
			_CreateShader = (__CreateShader)GetProcAddress<__CreateShader>("glCreateShader");
			_DeleteProgram = (__DeleteProgram)GetProcAddress<__DeleteProgram>("glDeleteProgram");
			_DeleteShader = (__DeleteShader)GetProcAddress<__DeleteShader>("glDeleteShader");
			_GetShaderInfoLog = (__GetShaderInfoLog)GetProcAddress<__GetShaderInfoLog>("glGetShaderInfoLog");
			_GetShaderiv = (__GetShaderiv)GetProcAddress<__GetShaderiv>("glGetShaderiv");
			_LinkProgram = (__LinkProgram)GetProcAddress<__LinkProgram>("glLinkProgram");
			_ShaderSource = (__ShaderSource)GetProcAddress<__ShaderSource>("glShaderSource");
			_UseProgram = (__UseProgram)GetProcAddress<__UseProgram>("glUseProgram");
		}

		public static void Clear(uint mask)
		{
			_Clear(mask);
		}

		public static void ClearColor(float red, float green, float blue, float alpha)
		{
			_ClearColor(red, green, blue, alpha);
		}

		public static void CompileShader(uint shader)
		{
			_CompileShader(shader);
		}

		public static uint CreateProgram()
		{
			return _CreateProgram();
		}

		public static uint CreateShder(uint shaderType)
		{
			return _CreateShader(shaderType);
		}

		public static void DeleteProgram(uint program)
		{
			_DeleteProgram(program);
		}

		public static void DeleteShader(uint shader)
		{
			_DeleteShader(shader);
		}

		public static string GetShaderInfoLog(uint shader)
		{
			StringBuilder infoLog = new StringBuilder(1024);
			int length;
			_GetShaderInfoLog(shader, infoLog.Capacity, out length, infoLog);
			return infoLog.ToString();
		}

		public static int GetShader(uint shader, uint pname)
		{
			int @params;
			_GetShaderiv(shader, pname, out @params);
			return @params;
		}

		public static void LinkProgram(uint program)
		{
			_LinkProgram(program);
		}

		public static void ShaderSource(uint shader, string source)
		{
			string[] sources = new string[] { source };
            int length = source.Length;

			_ShaderSource(shader, 1, sources, ref length);
		}

		public const uint ColorBufferBit = 0x00004000;
		public const uint CompileStatus = 0x8B81;
		public const uint DepthBufferBit = 0x00000100;
		public const uint FragmentShader = 0x8B30;
		public const uint InfoLogLength = 0x8B84;
		public const uint StencilBufferBit = 0x00000400;
		public const uint VertexShader = 0x8B31;
	}
}
