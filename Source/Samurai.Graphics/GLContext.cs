﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Graphics
{
	internal class GLContext
	{
		public const int VersionMajor = 3;
		public const int VersionMinor = 3;
		
		public readonly string ShaderVersionDirective = "#version 330";

#if WINDOWS
		private const string Library = "opengl32.dll";
		IGraphicsHostContext host;
#endif
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

		// Buffer
		public const uint ArrayBuffer = 0x8892;
		public const uint DynamicCopy = 0x88EA;
		public const uint DynamicDraw = 0x88E8;
		public const uint DynamicRead = 0x88E9;
		public const uint ElementArrayBuffer = 0x8893;
		public const uint StaticCopy = 0x88E6;
		public const uint StaticDraw = 0x88E4;
		public const uint StaticRead = 0x88E5;
		public const uint StreamCopy = 0x88E2;
		public const uint StreamDraw = 0x88E0;
		public const uint StreamRead = 0x88E1;

		// Caps
		public const uint BlendCap = 0x0BE2;
		public const uint CullFaceCap = 0x0B44;
		public const uint DepthTestCap = 0x0B71;
		public const uint ScissorTestCap = 0x0C11;
		public const uint StencilTestCap = 0x0B90;

		// Clear
		public const uint ColorBufferBit = 0x00004000;
		public const uint DepthBufferBit = 0x00000100;
		public const uint StencilBufferBit = 0x00000400;

		// DepthFunc
		public const uint Always = 0x0207;
		public const uint Equal = 0x0202;
		public const uint Gequal = 0x0206;
		public const uint Greater = 0x0204;
		public const uint Lequal = 0x0203;
		public const uint Less = 0x0201;
		public const uint Never = 0x0200;
		public const uint Notequal = 0x0205;

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

		// Faces
		public const uint Back = 0x0405;
		public const uint Cw = 0x0900;
		public const uint Ccw = 0x0901;
		public const uint Front = 0x0404;
		public const uint FrontAndBack = 0x0408;

		// Framebuffers
		public const uint DrawFramebuffer = 0x8CA9;
		public const uint Framebuffer = 0x8D40;
		public const uint FramebufferComplete = 0x8CD5;
		public const uint ReadFramebuffer = 0x8CA8;

		// Get
		public const uint NumExtensions = 0x821D;

        // GetString
        public const uint Vendor = 0x1F00;
        public const uint Renderer = 0x1F01;
        public const uint Version = 0x1F02;
        public const uint Extensions = 0x1F03;
        public const uint ShadingLanguageVersion = 0x8B8C;

		// Pixels
		public const uint Rgba = 0x1908;
		public const uint Rgba8 = 0x8058;

		// Primitive Types
		public const uint Lines = 0x0001;
		public const uint Points = 0x0000;
		public const uint Triangles = 0x0004;
        public const uint TriangleStrip = 0x0005;
        public const uint TriangleFan = 0x0006;

		// Shaders
		public const uint CompileStatus = 0x8B81;
		public const uint FragmentShader = 0x8B30;
		public const uint InfoLogLength = 0x8B84;
		public const uint VertexShader = 0x8B31;

		// StencilOp
		public const uint Keep = 0x1E00;
		public const uint Replace = 0x1E01;
		public const uint Incr = 0x1E02;
		public const uint IncrWrap = 0x8507;
		public const uint Decr = 0x1E03;
		public const uint DecrWrap = 0x8508;
		public const uint Invert = 0x150A;

		// Textures
		public const uint ClampToEdge = 0x812F;
		public const uint Linear = 0x2601;
		public const uint Nearest = 0x2600;
		public const uint Repeat = 0x2901;
		public const uint Texture0 = 0x84C0;
		public const uint Texture1D = 0x0DE0;
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

		private delegate void __BindFramebuffer(uint target, uint framebuffer);
		private __BindFramebuffer _BindFramebuffer;

		[DllImport(Library, EntryPoint = "glBindTexture")]
		private static extern void _BindTexture(uint target, uint texture);

		private delegate void __BindVertexArray(uint array);
		private __BindVertexArray _BindVertexArray;

		[DllImport(Library, EntryPoint = "glBlendFunc")]
		private static extern void _BlendFunc(uint sfactor, uint dfactor);

		private delegate void __BufferData(uint target, IntPtr size, IntPtr data, uint usage);
		private __BufferData _BufferData;

		private delegate uint __CheckFramebufferStatus(uint target);
		private __CheckFramebufferStatus _CheckFramebufferStatus;

		[DllImport(Library, EntryPoint = "glClear")]
		private static extern void _Clear(uint mask);

		[DllImport(Library, EntryPoint = "glClearColor")]
		private static extern void _ClearColor(float red, float green, float blue, float alpha);

		[DllImport(Library, EntryPoint = "glColorMask")]
		private static extern void _ColorMask(bool red, bool green, bool blue, bool alpha);

		private delegate void __CompileShader(uint shader);
		private __CompileShader _CompileShader;

		private delegate uint __CreateProgram();
		private __CreateProgram _CreateProgram;

		private delegate uint __CreateShader(uint shaderType);
		private __CreateShader _CreateShader;

		[DllImport(Library, EntryPoint = "glCullFace")]
		private static extern void _CullFace(uint mode);

		private delegate void __DeleteBuffers(int n, uint[] buffers);
		private __DeleteBuffers _DeleteBuffers;

		private delegate void __DeleteFramebuffers(int n, uint[] framebuffers);
		private __DeleteFramebuffers _DeleteFramebuffers;
				
		private delegate void __DeleteProgram(uint program);
		private __DeleteProgram _DeleteProgram;

		[DllImport(Library, EntryPoint = "glDeleteTextures")]
		private static extern void _DeleteTextures(int n, uint[] textures);

		private delegate void __DeleteShader(uint shader);
		private __DeleteShader _DeleteShader;

		private delegate void __DeleteVertexArrays(int n, uint[] arrays);
		private __DeleteVertexArrays _DeleteVertexArrays;

		[DllImport(Library, EntryPoint = "glDepthFunc")]
		private static extern void _DepthFunc(uint func);

		[DllImport(Library, EntryPoint = "glDepthMask")]
		private static extern void _DepthMask(bool mask);

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

		[DllImport(Library, EntryPoint = "glFrontFace")]
		private static extern void _FrontFace(uint mode);
				
		[DllImport(Library, EntryPoint = "glGetIntegerv")]
		private static extern void _GetIntegerv(uint pname, out int value);

        [DllImport(Library, EntryPoint = "glGetString")]
        private static extern IntPtr _GetString(uint name);

		private delegate IntPtr __GetStringi(uint name, uint index);
		private __GetStringi _GetStringi;

		private delegate void __GenBuffers(int n, [Out] uint[] buffers);
		private __GenBuffers _GenBuffers;

		private delegate void __GenFramebuffers(int n, [Out] uint[] buffers);
		private __GenFramebuffers _GenFramebuffers;

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

		[DllImport(Library, EntryPoint = "glScissor")]
		private static extern void _Scissor(int x, int y, int width, int height);

		private delegate void __ShaderSource(uint shader, int count, ref string @string, ref int length);
		private __ShaderSource _ShaderSource;

		[DllImport(Library, EntryPoint = "glStencilFunc")]
		private static extern void _StencilFunc(uint func, int @ref, uint mask);

		[DllImport(Library, EntryPoint = "glStencilMask")]
		private static extern void _StencilMask(uint mask);

		[DllImport(Library, EntryPoint = "glStencilOp")]
		private static extern void _StencilOp(uint fail, uint zfail, uint zpass);

		[DllImport(Library, EntryPoint = "glTexImage1D")]
		private static extern void _TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, byte[] pixels);

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

        private delegate void __Uniform2f(int location, float v0, float v1);
        private __Uniform2f _Uniform2f;

        private delegate void __Uniform3f(int location, float v0, float v1, float v2);
        private __Uniform3f _Uniform3f;

        private delegate void __Uniform4f(int location, float v0, float v1, float v2, float v3);
        private __Uniform4f _Uniform4f;

		private delegate void __UniformMatrix4fv(int location, int count, bool transpose, ref float value);
		private __UniformMatrix4fv _UniformMatrix4fv;

		private delegate void __UseProgram(uint program);
		private __UseProgram _UseProgram;

		private delegate void __VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
		private __VertexAttribPointer _VertexAttribPointer;

		[DllImport(Library, EntryPoint = "glViewport")]
		private static extern void _Viewport(int x, int y, int width, int height);

		public GLContext(IGraphicsHostContext host)
		{
			this.host = host;

			_ActiveTexture = (__ActiveTexture)GetProcAddress<__ActiveTexture>(host, "glActiveTexture");
			_AttachShader = (__AttachShader)GetProcAddress<__AttachShader>(host, "glAttachShader");
			_BindAttribLocation = (__BindAttribLocation)GetProcAddress<__BindAttribLocation>(host, "glBindAttribLocation");
			_BindBuffer = (__BindBuffer)GetProcAddress<__BindBuffer>(host, "glBindBuffer");
			_BindFramebuffer = (__BindFramebuffer)GetProcAddress<__BindFramebuffer>(host, "glBindFramebuffer");
			_BindVertexArray = (__BindVertexArray)GetProcAddress<__BindVertexArray>(host, "glBindVertexArray");
			_BufferData = (__BufferData)GetProcAddress<__BufferData>(host, "glBufferData");
			_CheckFramebufferStatus = (__CheckFramebufferStatus)GetProcAddress<__CheckFramebufferStatus>(host, "glCheckFramebufferStatus");
			_CompileShader = (__CompileShader)GetProcAddress<__CompileShader>(host, "glCompileShader");
			_CreateProgram = (__CreateProgram)GetProcAddress<__CreateProgram>(host, "glCreateProgram");
			_CreateShader = (__CreateShader)GetProcAddress<__CreateShader>(host, "glCreateShader");
			_DeleteBuffers = (__DeleteBuffers)GetProcAddress<__DeleteBuffers>(host, "glDeleteBuffers");
			_DeleteFramebuffers = (__DeleteFramebuffers)GetProcAddress<__DeleteFramebuffers>(host, "glDeleteFramebuffers");
			_DeleteProgram = (__DeleteProgram)GetProcAddress<__DeleteProgram>(host, "glDeleteProgram");
			_DeleteShader = (__DeleteShader)GetProcAddress<__DeleteShader>(host, "glDeleteShader");
			_DeleteVertexArrays = (__DeleteVertexArrays)GetProcAddress<__DeleteVertexArrays>(host, "glDeleteVertexArrays");
			_EnableVertexAttribArray = (__EnableVertexAttribArray)GetProcAddress<__EnableVertexAttribArray>(host, "glEnableVertexAttribArray");
			_GenBuffers = (__GenBuffers)GetProcAddress<__GenBuffers>(host, "glGenBuffers");
			_GenFramebuffers = (__GenFramebuffers)GetProcAddress<__GenFramebuffers>(host, "glGenFramebuffers");
			_GenVertexArrays = (__GenVertexArrays)GetProcAddress<__GenVertexArrays>(host, "glGenVertexArrays");
			_GetShaderInfoLog = (__GetShaderInfoLog)GetProcAddress<__GetShaderInfoLog>(host, "glGetShaderInfoLog");
			_GetShaderiv = (__GetShaderiv)GetProcAddress<__GetShaderiv>(host, "glGetShaderiv");
			_GetStringi = (__GetStringi)GetProcAddress<__GetStringi>(host, "glGetStringi");
			_GetUniformLocation = (__GetUniformLocation)GetProcAddress<__GetUniformLocation>(host, "glGetUniformLocation");
			_LinkProgram = (__LinkProgram)GetProcAddress<__LinkProgram>(host, "glLinkProgram");
			_ShaderSource = (__ShaderSource)GetProcAddress<__ShaderSource>(host, "glShaderSource");
			_Uniform1f = (__Uniform1f)GetProcAddress<__Uniform1f>(host, "glUniform1f");
			_Uniform1i = (__Uniform1i)GetProcAddress<__Uniform1i>(host, "glUniform1i");
			_Uniform2f = (__Uniform2f)GetProcAddress<__Uniform2f>(host, "glUniform2f");
			_Uniform3f = (__Uniform3f)GetProcAddress<__Uniform3f>(host, "glUniform3f");
			_Uniform4f = (__Uniform4f)GetProcAddress<__Uniform4f>(host, "glUniform4f");
			_UniformMatrix4fv = (__UniformMatrix4fv)GetProcAddress<__UniformMatrix4fv>(host, "glUniformMatrix4fv");
			_UseProgram = (__UseProgram)GetProcAddress<__UseProgram>(host, "glUseProgram");
			_VertexAttribPointer = (__VertexAttribPointer)GetProcAddress<__VertexAttribPointer>(host, "glVertexAttribPointer");
		}

		private static object GetProcAddress<T>(IGraphicsHostContext host, string name)
		{
			Type delegateType = typeof(T);

			IntPtr proc = host.GetProcAddress(name);

			if (proc == IntPtr.Zero)
				throw new SamuraiException(string.Format("Failed to load GL extension function: {0}.", name));

			return Marshal.GetDelegateForFunctionPointer(proc, delegateType);
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

		public void BindFramebuffer(uint target, uint framebuffer)
		{
			_BindFramebuffer(target, framebuffer);
			CheckErrors("BindFramebuffer");
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

		public void BufferData(uint target, IntPtr data, int offset, int length, uint usage)
		{
			IntPtr ptr = IntPtr.Add(data, offset);
			_BufferData(target, (IntPtr)(length), ptr, usage);
			CheckErrors("BufferData");
		}

		public void BufferData<T>(uint target, T[] data, int index, int count, uint usage)
		{
			int sizeOfT = Marshal.SizeOf(typeof(T));
			GCHandle dataPtr = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr ptr = IntPtr.Add(dataPtr.AddrOfPinnedObject(), index * sizeOfT);

			try
			{
				_BufferData(target, (IntPtr)(sizeOfT * count), ptr, usage);
			}
			finally
			{
				dataPtr.Free();
			}

			CheckErrors("BufferData");
		}

		public uint CheckFramebufferStatus(uint target)
		{
			uint result = _CheckFramebufferStatus(target);
			CheckErrors("CheckFramebufferStatus");
			return result;
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

		public void ColorMask(bool red, bool blue, bool green, bool alpha)
		{
			_ColorMask(red, green, blue, alpha);
			CheckErrors("ColorMask");
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

		public void CullFace(uint mode)
		{
			_CullFace(mode);
			CheckErrors("CullFace");
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

		public void DepthFunc(uint func)
		{
			_DepthFunc(func);
			CheckErrors("DepthFunc");
		}

		public void DepthMask(bool mask)
		{
			_DepthMask(mask);
			CheckErrors("DepthMask");
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

		public void FrontFace(uint mode)
		{
			_FrontFace(mode);
			CheckErrors("FrontFace");
		}

		public int GetIntegerv(uint pname)
		{
			int value;
			_GetIntegerv(pname, out value);
			return value;
		}

        public string GetString(uint name)
        {
            IntPtr data = _GetString(name);
            CheckErrors("GetString");
            return Marshal.PtrToStringAnsi(data);
        }

		public string GetStringi(uint name, uint index)
		{
			IntPtr data = _GetStringi(name, index);
			CheckErrors("GetStringi");
			return Marshal.PtrToStringAnsi(data);
		}

		public uint GenBuffer()
		{
			_GenBuffers(1, UintArraySizeOne);
			CheckErrors("GenBuffers");
			return UintArraySizeOne[0];
		}

		public uint GenFramebuffer()
		{
			_GenFramebuffers(1, UintArraySizeOne);
			CheckErrors("GenFramebuffers");
			return UintArraySizeOne[0];
		}

		public uint GenTexture()
		{
			_GenTextures(1, UintArraySizeOne);
			CheckErrors("GenTextures");
			return UintArraySizeOne[0];
		}
				
		public uint GenVertexArray()
		{
			_GenVertexArrays(1, UintArraySizeOne);
			CheckErrors("GenVertexArrays");
			return UintArraySizeOne[0];
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

		public void Scissor(int x, int y, int width, int height)
		{
			_Scissor(x, y, width, height);
			CheckErrors("Scissor");
		}

		public void ShaderSource(uint shader, string source)
		{
            int length = source.Length;
			_ShaderSource(shader, 1, ref source, ref length);
			CheckErrors("ShaderSource");
		}

		public void StencilFunc(uint func, int @ref, uint mask)
		{
			_StencilFunc(func, @ref, mask);
			CheckErrors("StencilFunc");
		}

		public void StencilMask(uint mask)
		{
			_StencilMask(mask);
			CheckErrors("StencilMask");
		}

		public void StencilOp(uint fail, uint zfail, uint zpass)
		{
			_StencilOp(fail, zfail, zpass);
			CheckErrors("StencilOp");
		}

		public void TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, byte[] pixels)
		{
			_TexImage1D(target, level, internalformat, width, border, format, type, pixels);
			CheckErrors("TexImage1D");
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

        public void Uniform2f(int location, float v0, float v1)
        {
            _Uniform2f(location, v0, v1);
            CheckErrors("Uniform2f");
        }

        public void Uniform3f(int location, float v0, float v1, float v2)
        {
            _Uniform3f(location, v0, v1, v2);
            CheckErrors("Uniform3f");
        }

        public void Uniform4f(int location, float v0, float v1, float v2, float v3)
        {
            _Uniform4f(location, v0, v1, v2, v3);
            CheckErrors("Uniform4f");
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

        public bool MakeCurrent()
        {
            return this.host.MakeCurrent();
        }

		public void SwapBuffers()
		{
			this.host.SwapBuffers();
		}
	}
}
