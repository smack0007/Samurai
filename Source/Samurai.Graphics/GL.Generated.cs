using System;
using System.Runtime.InteropServices;
namespace Samurai.Graphics
{
	internal static partial class GL
	{
		public const uint ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
		public const uint ACTIVE_ATTRIBUTES = 0x8B89;
		public const uint ACTIVE_TEXTURE = 0x84E0;
		public const uint ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
		public const uint ACTIVE_UNIFORMS = 0x8B86;
		public const uint ALIASED_LINE_WIDTH_RANGE = 0x846E;
		public const uint ALIASED_POINT_SIZE_RANGE = 0x846D;
		public const uint ALPHA = 0x1906;
		public const uint ALPHA_BITS = 0x0D55;
		public const uint ALWAYS = 0x0207;
		public const uint APIENTRYP = GL_APIENTRY*;
		public const uint ARRAY_BUFFER = 0x8892;
		public const uint ARRAY_BUFFER_BINDING = 0x8894;
		public const uint ATTACHED_SHADERS = 0x8B85;
		public const uint BACK = 0x0405;
		public const uint BLEND = 0x0BE2;
		public const uint BLEND_COLOR = 0x8005;
		public const uint BLEND_DST_ALPHA = 0x80CA;
		public const uint BLEND_DST_RGB = 0x80C8;
		public const uint BLEND_EQUATION = 0x8009;
		public const uint BLEND_EQUATION_ALPHA = 0x883D;
		public const uint BLEND_EQUATION_RGB = 0x8009;
		public const uint BLEND_SRC_ALPHA = 0x80CB;
		public const uint BLEND_SRC_RGB = 0x80C9;
		public const uint BLUE_BITS = 0x0D54;
		public const uint BOOL = 0x8B56;
		public const uint BOOL_VEC2 = 0x8B57;
		public const uint BOOL_VEC3 = 0x8B58;
		public const uint BOOL_VEC4 = 0x8B59;
		public const uint BUFFER_SIZE = 0x8764;
		public const uint BUFFER_USAGE = 0x8765;
		public const uint BYTE = 0x1400;
		public const uint CCW = 0x0901;
		public const uint CLAMP_TO_EDGE = 0x812F;
		public const uint COLOR_ATTACHMENT0 = 0x8CE0;
		public const uint COLOR_BUFFER_BIT = 0x00004000;
		public const uint COLOR_CLEAR_VALUE = 0x0C22;
		public const uint COLOR_WRITEMASK = 0x0C23;
		public const uint COMPILE_STATUS = 0x8B81;
		public const uint COMPRESSED_TEXTURE_FORMATS = 0x86A3;
		public const uint CONSTANT_ALPHA = 0x8003;
		public const uint CONSTANT_COLOR = 0x8001;
		public const uint CULL_FACE = 0x0B44;
		public const uint CULL_FACE_MODE = 0x0B45;
		public const uint CURRENT_PROGRAM = 0x8B8D;
		public const uint CURRENT_VERTEX_ATTRIB = 0x8626;
		public const uint CW = 0x0900;
		public const uint DECR = 0x1E03;
		public const uint DECR_WRAP = 0x8508;
		public const uint DELETE_STATUS = 0x8B80;
		public const uint DEPTH_ATTACHMENT = 0x8D00;
		public const uint DEPTH_BITS = 0x0D56;
		public const uint DEPTH_BUFFER_BIT = 0x00000100;
		public const uint DEPTH_CLEAR_VALUE = 0x0B73;
		public const uint DEPTH_COMPONENT = 0x1902;
		public const uint DEPTH_COMPONENT16 = 0x81A5;
		public const uint DEPTH_FUNC = 0x0B74;
		public const uint DEPTH_RANGE = 0x0B70;
		public const uint DEPTH_TEST = 0x0B71;
		public const uint DEPTH_WRITEMASK = 0x0B72;
		public const uint DITHER = 0x0BD0;
		public const uint DONT_CARE = 0x1100;
		public const uint DST_ALPHA = 0x0304;
		public const uint DST_COLOR = 0x0306;
		public const uint DYNAMIC_DRAW = 0x88E8;
		public const uint ELEMENT_ARRAY_BUFFER = 0x8893;
		public const uint ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;
		public const uint EQUAL = 0x0202;
		public const uint EXTENSIONS = 0x1F03;
		public const uint FALSE = 0;
		public const uint FASTEST = 0x1101;
		public const uint FIXED = 0x140C;
		public const uint FLOAT = 0x1406;
		public const uint FLOAT_MAT2 = 0x8B5A;
		public const uint FLOAT_MAT3 = 0x8B5B;
		public const uint FLOAT_MAT4 = 0x8B5C;
		public const uint FLOAT_VEC2 = 0x8B50;
		public const uint FLOAT_VEC3 = 0x8B51;
		public const uint FLOAT_VEC4 = 0x8B52;
		public const uint FRAGMENT_SHADER = 0x8B30;
		public const uint FRAMEBUFFER = 0x8D40;
		public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
		public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
		public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;
		public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
		public const uint FRAMEBUFFER_BINDING = 0x8CA6;
		public const uint FRAMEBUFFER_COMPLETE = 0x8CD5;
		public const uint FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
		public const uint FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
		public const uint FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
		public const uint FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
		public const uint FRONT = 0x0404;
		public const uint FRONT_AND_BACK = 0x0408;
		public const uint FRONT_FACE = 0x0B46;
		public const uint FUNC_ADD = 0x8006;
		public const uint FUNC_REVERSE_SUBTRACT = 0x800B;
		public const uint FUNC_SUBTRACT = 0x800A;
		public const uint GENERATE_MIPMAP_HINT = 0x8192;
		public const uint GEQUAL = 0x0206;
		public const uint GREATER = 0x0204;
		public const uint GREEN_BITS = 0x0D53;
		public const uint HIGH_FLOAT = 0x8DF2;
		public const uint HIGH_INT = 0x8DF5;
		public const uint IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;
		public const uint IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;
		public const uint INCR = 0x1E02;
		public const uint INCR_WRAP = 0x8507;
		public const uint INFO_LOG_LENGTH = 0x8B84;
		public const uint INT = 0x1404;
		public const uint INT_VEC2 = 0x8B53;
		public const uint INT_VEC3 = 0x8B54;
		public const uint INT_VEC4 = 0x8B55;
		public const uint INVALID_ENUM = 0x0500;
		public const uint INVALID_FRAMEBUFFER_OPERATION = 0x0506;
		public const uint INVALID_OPERATION = 0x0502;
		public const uint INVALID_VALUE = 0x0501;
		public const uint INVERT = 0x150A;
		public const uint KEEP = 0x1E00;
		public const uint LEQUAL = 0x0203;
		public const uint LESS = 0x0201;
		public const uint LINE_LOOP = 0x0002;
		public const uint LINE_STRIP = 0x0003;
		public const uint LINE_WIDTH = 0x0B21;
		public const uint LINEAR = 0x2601;
		public const uint LINEAR_MIPMAP_LINEAR = 0x2703;
		public const uint LINEAR_MIPMAP_NEAREST = 0x2701;
		public const uint LINES = 0x0001;
		public const uint LINK_STATUS = 0x8B82;
		public const uint LOW_FLOAT = 0x8DF0;
		public const uint LOW_INT = 0x8DF3;
		public const uint LUMINANCE = 0x1909;
		public const uint LUMINANCE_ALPHA = 0x190A;
		public const uint MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
		public const uint MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;
		public const uint MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
		public const uint MAX_RENDERBUFFER_SIZE = 0x84E8;
		public const uint MAX_TEXTURE_IMAGE_UNITS = 0x8872;
		public const uint MAX_TEXTURE_SIZE = 0x0D33;
		public const uint MAX_VARYING_VECTORS = 0x8DFC;
		public const uint MAX_VERTEX_ATTRIBS = 0x8869;
		public const uint MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
		public const uint MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
		public const uint MAX_VIEWPORT_DIMS = 0x0D3A;
		public const uint MEDIUM_FLOAT = 0x8DF1;
		public const uint MEDIUM_INT = 0x8DF4;
		public const uint MIRRORED_REPEAT = 0x8370;
		public const uint NEAREST = 0x2600;
		public const uint NEAREST_MIPMAP_LINEAR = 0x2702;
		public const uint NEAREST_MIPMAP_NEAREST = 0x2700;
		public const uint NEVER = 0x0200;
		public const uint NICEST = 0x1102;
		public const uint NO_ERROR = 0;
		public const uint NONE = 0;
		public const uint NOTEQUAL = 0x0205;
		public const uint NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
		public const uint NUM_SHADER_BINARY_FORMATS = 0x8DF9;
		public const uint ONE = 1;
		public const uint ONE_MINUS_CONSTANT_ALPHA = 0x8004;
		public const uint ONE_MINUS_CONSTANT_COLOR = 0x8002;
		public const uint ONE_MINUS_DST_ALPHA = 0x0305;
		public const uint ONE_MINUS_DST_COLOR = 0x0307;
		public const uint ONE_MINUS_SRC_ALPHA = 0x0303;
		public const uint ONE_MINUS_SRC_COLOR = 0x0301;
		public const uint OUT_OF_MEMORY = 0x0505;
		public const uint PACK_ALIGNMENT = 0x0D05;
		public const uint POINTS = 0x0000;
		public const uint POLYGON_OFFSET_FACTOR = 0x8038;
		public const uint POLYGON_OFFSET_FILL = 0x8037;
		public const uint POLYGON_OFFSET_UNITS = 0x2A00;
		public const uint RED_BITS = 0x0D52;
		public const uint RENDERBUFFER = 0x8D41;
		public const uint RENDERBUFFER_ALPHA_SIZE = 0x8D53;
		public const uint RENDERBUFFER_BINDING = 0x8CA7;
		public const uint RENDERBUFFER_BLUE_SIZE = 0x8D52;
		public const uint RENDERBUFFER_DEPTH_SIZE = 0x8D54;
		public const uint RENDERBUFFER_GREEN_SIZE = 0x8D51;
		public const uint RENDERBUFFER_HEIGHT = 0x8D43;
		public const uint RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
		public const uint RENDERBUFFER_RED_SIZE = 0x8D50;
		public const uint RENDERBUFFER_STENCIL_SIZE = 0x8D55;
		public const uint RENDERBUFFER_WIDTH = 0x8D42;
		public const uint RENDERER = 0x1F01;
		public const uint REPEAT = 0x2901;
		public const uint REPLACE = 0x1E01;
		public const uint RGB = 0x1907;
		public const uint RGB5_A1 = 0x8057;
		public const uint RGB565 = 0x8D62;
		public const uint RGBA = 0x1908;
		public const uint RGBA4 = 0x8056;
		public const uint SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
		public const uint SAMPLE_BUFFERS = 0x80A8;
		public const uint SAMPLE_COVERAGE = 0x80A0;
		public const uint SAMPLE_COVERAGE_INVERT = 0x80AB;
		public const uint SAMPLE_COVERAGE_VALUE = 0x80AA;
		public const uint SAMPLER_2D = 0x8B5E;
		public const uint SAMPLER_CUBE = 0x8B60;
		public const uint SAMPLES = 0x80A9;
		public const uint SCISSOR_BOX = 0x0C10;
		public const uint SCISSOR_TEST = 0x0C11;
		public const uint SHADER_BINARY_FORMATS = 0x8DF8;
		public const uint SHADER_COMPILER = 0x8DFA;
		public const uint SHADER_SOURCE_LENGTH = 0x8B88;
		public const uint SHADER_TYPE = 0x8B4F;
		public const uint SHADING_LANGUAGE_VERSION = 0x8B8C;
		public const uint SHORT = 0x1402;
		public const uint SRC_ALPHA = 0x0302;
		public const uint SRC_ALPHA_SATURATE = 0x0308;
		public const uint SRC_COLOR = 0x0300;
		public const uint STATIC_DRAW = 0x88E4;
		public const uint STENCIL_ATTACHMENT = 0x8D20;
		public const uint STENCIL_BACK_FAIL = 0x8801;
		public const uint STENCIL_BACK_FUNC = 0x8800;
		public const uint STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
		public const uint STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
		public const uint STENCIL_BACK_REF = 0x8CA3;
		public const uint STENCIL_BACK_VALUE_MASK = 0x8CA4;
		public const uint STENCIL_BACK_WRITEMASK = 0x8CA5;
		public const uint STENCIL_BITS = 0x0D57;
		public const uint STENCIL_BUFFER_BIT = 0x00000400;
		public const uint STENCIL_CLEAR_VALUE = 0x0B91;
		public const uint STENCIL_FAIL = 0x0B94;
		public const uint STENCIL_FUNC = 0x0B92;
		public const uint STENCIL_INDEX8 = 0x8D48;
		public const uint STENCIL_PASS_DEPTH_FAIL = 0x0B95;
		public const uint STENCIL_PASS_DEPTH_PASS = 0x0B96;
		public const uint STENCIL_REF = 0x0B97;
		public const uint STENCIL_TEST = 0x0B90;
		public const uint STENCIL_VALUE_MASK = 0x0B93;
		public const uint STENCIL_WRITEMASK = 0x0B98;
		public const uint STREAM_DRAW = 0x88E0;
		public const uint SUBPIXEL_BITS = 0x0D50;
		public const uint TEXTURE = 0x1702;
		public const uint TEXTURE_2D = 0x0DE1;
		public const uint TEXTURE_BINDING_2D = 0x8069;
		public const uint TEXTURE_BINDING_CUBE_MAP = 0x8514;
		public const uint TEXTURE_CUBE_MAP = 0x8513;
		public const uint TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
		public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
		public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
		public const uint TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
		public const uint TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
		public const uint TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
		public const uint TEXTURE_MAG_FILTER = 0x2800;
		public const uint TEXTURE_MIN_FILTER = 0x2801;
		public const uint TEXTURE_WRAP_S = 0x2802;
		public const uint TEXTURE_WRAP_T = 0x2803;
		public const uint TEXTURE0 = 0x84C0;
		public const uint TEXTURE1 = 0x84C1;
		public const uint TEXTURE10 = 0x84CA;
		public const uint TEXTURE11 = 0x84CB;
		public const uint TEXTURE12 = 0x84CC;
		public const uint TEXTURE13 = 0x84CD;
		public const uint TEXTURE14 = 0x84CE;
		public const uint TEXTURE15 = 0x84CF;
		public const uint TEXTURE16 = 0x84D0;
		public const uint TEXTURE17 = 0x84D1;
		public const uint TEXTURE18 = 0x84D2;
		public const uint TEXTURE19 = 0x84D3;
		public const uint TEXTURE2 = 0x84C2;
		public const uint TEXTURE20 = 0x84D4;
		public const uint TEXTURE21 = 0x84D5;
		public const uint TEXTURE22 = 0x84D6;
		public const uint TEXTURE23 = 0x84D7;
		public const uint TEXTURE24 = 0x84D8;
		public const uint TEXTURE25 = 0x84D9;
		public const uint TEXTURE26 = 0x84DA;
		public const uint TEXTURE27 = 0x84DB;
		public const uint TEXTURE28 = 0x84DC;
		public const uint TEXTURE29 = 0x84DD;
		public const uint TEXTURE3 = 0x84C3;
		public const uint TEXTURE30 = 0x84DE;
		public const uint TEXTURE31 = 0x84DF;
		public const uint TEXTURE4 = 0x84C4;
		public const uint TEXTURE5 = 0x84C5;
		public const uint TEXTURE6 = 0x84C6;
		public const uint TEXTURE7 = 0x84C7;
		public const uint TEXTURE8 = 0x84C8;
		public const uint TEXTURE9 = 0x84C9;
		public const uint TRIANGLE_FAN = 0x0006;
		public const uint TRIANGLE_STRIP = 0x0005;
		public const uint TRIANGLES = 0x0004;
		public const uint TRUE = 1;
		public const uint UNPACK_ALIGNMENT = 0x0CF5;
		public const uint UNSIGNED_BYTE = 0x1401;
		public const uint UNSIGNED_INT = 0x1405;
		public const uint UNSIGNED_SHORT = 0x1403;
		public const uint UNSIGNED_SHORT_4_4_4_4 = 0x8033;
		public const uint UNSIGNED_SHORT_5_5_5_1 = 0x8034;
		public const uint UNSIGNED_SHORT_5_6_5 = 0x8363;
		public const uint VALIDATE_STATUS = 0x8B83;
		public const uint VENDOR = 0x1F00;
		public const uint VERSION = 0x1F02;
		public const uint VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;
		public const uint VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
		public const uint VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
		public const uint VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
		public const uint VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
		public const uint VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
		public const uint VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
		public const uint VERTEX_SHADER = 0x8B31;
		public const uint VIEWPORT = 0x0BA2;
		public const uint ZERO = 0;

		[DllImport(Library, EntryPoint = "glActiveTexture")]
		public static void ActiveTexture(uint texture);

		[DllImport(Library, EntryPoint = "glAttachShader")]
		public static void AttachShader(uint program, uint shader);

		[DllImport(Library, EntryPoint = "glBindAttribLocation")]
		public static void BindAttribLocation(uint program, uint index, string name);

		[DllImport(Library, EntryPoint = "glBindBuffer")]
		public static void BindBuffer(uint target, uint buffer);

		[DllImport(Library, EntryPoint = "glBindFramebuffer")]
		public static void BindFramebuffer(uint target, uint framebuffer);

		[DllImport(Library, EntryPoint = "glBindRenderbuffer")]
		public static void BindRenderbuffer(uint target, uint renderbuffer);

		[DllImport(Library, EntryPoint = "glBindTexture")]
		public static void BindTexture(uint target, uint texture);

		[DllImport(Library, EntryPoint = "glBlendColor")]
		public static void BlendColor(float red, float green, float blue, float alpha);

		[DllImport(Library, EntryPoint = "glBlendEquation")]
		public static void BlendEquation(uint mode);

		[DllImport(Library, EntryPoint = "glBlendEquationSeparate")]
		public static void BlendEquationSeparate(uint modeRGB, uint modeAlpha);

		[DllImport(Library, EntryPoint = "glBlendFunc")]
		public static void BlendFunc(uint sfactor, uint dfactor);

		[DllImport(Library, EntryPoint = "glBlendFuncSeparate")]
		public static void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);

		[DllImport(Library, EntryPoint = "glBufferData")]
		public static void BufferData(uint target, GLsizeiptr size, byte[] data, uint usage);

		[DllImport(Library, EntryPoint = "glBufferSubData")]
		public static void BufferSubData(uint target, GLintptr offset, GLsizeiptr size, byte[] data);

		[DllImport(Library, EntryPoint = "glCheckFramebufferStatus")]
		public static uint CheckFramebufferStatus(uint target);

		[DllImport(Library, EntryPoint = "glClear")]
		public static void Clear(uint mask);

		[DllImport(Library, EntryPoint = "glClearColor")]
		public static void ClearColor(float red, float green, float blue, float alpha);

		[DllImport(Library, EntryPoint = "glClearDepthf")]
		public static void ClearDepthf(float d);

		[DllImport(Library, EntryPoint = "glClearStencil")]
		public static void ClearStencil(int s);

		[DllImport(Library, EntryPoint = "glColorMask")]
		public static void ColorMask(bool red, bool green, bool blue, bool alpha);

		[DllImport(Library, EntryPoint = "glCompileShader")]
		public static void CompileShader(uint shader);

		[DllImport(Library, EntryPoint = "glCompressedTexImage2D")]
		public static void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, byte[] data);

		[DllImport(Library, EntryPoint = "glCompressedTexSubImage2D")]
		public static void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, byte[] data);

		[DllImport(Library, EntryPoint = "glCopyTexImage2D")]
		public static void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border);

		[DllImport(Library, EntryPoint = "glCopyTexSubImage2D")]
		public static void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

		[DllImport(Library, EntryPoint = "glCreateProgram")]
		public static GLuint CreateProgram();

		[DllImport(Library, EntryPoint = "glCreateShader")]
		public static GLuint CreateShader(uint type);

		[DllImport(Library, EntryPoint = "glCullFace")]
		public static void CullFace(uint mode);

		[DllImport(Library, EntryPoint = "glDeleteBuffers")]
		public static void DeleteBuffers(int n, uint[] buffers);

		[DllImport(Library, EntryPoint = "glDeleteFramebuffers")]
		public static void DeleteFramebuffers(int n, uint[] framebuffers);

		[DllImport(Library, EntryPoint = "glDeleteProgram")]
		public static void DeleteProgram(uint program);

		[DllImport(Library, EntryPoint = "glDeleteRenderbuffers")]
		public static void DeleteRenderbuffers(int n, uint[] renderbuffers);

		[DllImport(Library, EntryPoint = "glDeleteShader")]
		public static void DeleteShader(uint shader);

		[DllImport(Library, EntryPoint = "glDeleteTextures")]
		public static void DeleteTextures(int n, uint[] textures);

		[DllImport(Library, EntryPoint = "glDepthFunc")]
		public static void DepthFunc(uint func);

		[DllImport(Library, EntryPoint = "glDepthMask")]
		public static void DepthMask(bool flag);

		[DllImport(Library, EntryPoint = "glDepthRangef")]
		public static void DepthRangef(float n, float f);

		[DllImport(Library, EntryPoint = "glDetachShader")]
		public static void DetachShader(uint program, uint shader);

		[DllImport(Library, EntryPoint = "glDisable")]
		public static void Disable(uint cap);

		[DllImport(Library, EntryPoint = "glDisableVertexAttribArray")]
		public static void DisableVertexAttribArray(uint index);

		[DllImport(Library, EntryPoint = "glDrawArrays")]
		public static void DrawArrays(uint mode, int first, int count);

		[DllImport(Library, EntryPoint = "glDrawElements")]
		public static void DrawElements(uint mode, int count, uint type, byte[] indices);

		[DllImport(Library, EntryPoint = "glEnable")]
		public static void Enable(uint cap);

		[DllImport(Library, EntryPoint = "glEnableVertexAttribArray")]
		public static void EnableVertexAttribArray(uint index);

		[DllImport(Library, EntryPoint = "glFinish")]
		public static void Finish();

		[DllImport(Library, EntryPoint = "glFlush")]
		public static void Flush();

		[DllImport(Library, EntryPoint = "glFramebufferRenderbuffer")]
		public static void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);

		[DllImport(Library, EntryPoint = "glFramebufferTexture2D")]
		public static void FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level);

		[DllImport(Library, EntryPoint = "glFrontFace")]
		public static void FrontFace(uint mode);

		[DllImport(Library, EntryPoint = "glGenBuffers")]
		public static void GenBuffers(int n, out uint[] buffers);

		[DllImport(Library, EntryPoint = "glGenerateMipmap")]
		public static void GenerateMipmap(uint target);

		[DllImport(Library, EntryPoint = "glGenFramebuffers")]
		public static void GenFramebuffers(int n, out uint[] framebuffers);

		[DllImport(Library, EntryPoint = "glGenRenderbuffers")]
		public static void GenRenderbuffers(int n, out uint[] renderbuffers);

		[DllImport(Library, EntryPoint = "glGenTextures")]
		public static void GenTextures(int n, out uint[] textures);

		[DllImport(Library, EntryPoint = "glGetActiveAttrib")]
		public static void GetActiveAttrib(uint program, uint index, int bufSize, out int[] length, out int[] size, out uint[] type, out GLchar[] name);

		[DllImport(Library, EntryPoint = "glGetActiveUniform")]
		public static void GetActiveUniform(uint program, uint index, int bufSize, out int[] length, out int[] size, out uint[] type, out GLchar[] name);

		[DllImport(Library, EntryPoint = "glGetAttachedShaders")]
		public static void GetAttachedShaders(uint program, int maxCount, out int[] count, out uint[] shaders);

		[DllImport(Library, EntryPoint = "glGetAttribLocation")]
		public static int GetAttribLocation(uint program, string name);

		[DllImport(Library, EntryPoint = "glGetBooleanv")]
		public static void GetBooleanv(uint pname, out bool[] data);

		[DllImport(Library, EntryPoint = "glGetBufferParameteriv")]
		public static void GetBufferParameteriv(uint target, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetError")]
		public static uint GetError();

		[DllImport(Library, EntryPoint = "glGetFloatv")]
		public static void GetFloatv(uint pname, out float[] data);

		[DllImport(Library, EntryPoint = "glGetFramebufferAttachmentParameteriv")]
		public static void GetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetIntegerv")]
		public static void GetIntegerv(uint pname, out int[] data);

		[DllImport(Library, EntryPoint = "glGetProgramInfoLog")]
		public static void GetProgramInfoLog(uint program, int bufSize, out int[] length, out GLchar[] infoLog);

		[DllImport(Library, EntryPoint = "glGetProgramiv")]
		public static void GetProgramiv(uint program, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetRenderbufferParameteriv")]
		public static void GetRenderbufferParameteriv(uint target, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetShaderInfoLog")]
		public static void GetShaderInfoLog(uint shader, int bufSize, out int[] length, out GLchar[] infoLog);

		[DllImport(Library, EntryPoint = "glGetShaderiv")]
		public static void GetShaderiv(uint shader, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetShaderPrecisionFormat")]
		public static void GetShaderPrecisionFormat(uint shadertype, uint precisiontype, out int[] range, out int[] precision);

		[DllImport(Library, EntryPoint = "glGetShaderSource")]
		public static void GetShaderSource(uint shader, int bufSize, out int[] length, out GLchar[] source);

		[DllImport(Library, EntryPoint = "glGetString")]
		public static string GetString(uint name);

		[DllImport(Library, EntryPoint = "glGetTexParameterfv")]
		public static void GetTexParameterfv(uint target, uint pname, out float[] @params);

		[DllImport(Library, EntryPoint = "glGetTexParameteriv")]
		public static void GetTexParameteriv(uint target, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetUniformfv")]
		public static void GetUniformfv(uint program, int location, out float[] @params);

		[DllImport(Library, EntryPoint = "glGetUniformiv")]
		public static void GetUniformiv(uint program, int location, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetUniformLocation")]
		public static int GetUniformLocation(uint program, string name);

		[DllImport(Library, EntryPoint = "glGetVertexAttribfv")]
		public static void GetVertexAttribfv(uint index, uint pname, out float[] @params);

		[DllImport(Library, EntryPoint = "glGetVertexAttribiv")]
		public static void GetVertexAttribiv(uint index, uint pname, out int[] @params);

		[DllImport(Library, EntryPoint = "glGetVertexAttribPointerv")]
		public static void GetVertexAttribPointerv(uint index, uint pname, IntPtr pointer);

		[DllImport(Library, EntryPoint = "glHint")]
		public static void Hint(uint target, uint mode);

		[DllImport(Library, EntryPoint = "glIsBuffer")]
		public static bool IsBuffer(uint buffer);

		[DllImport(Library, EntryPoint = "glIsEnabled")]
		public static bool IsEnabled(uint cap);

		[DllImport(Library, EntryPoint = "glIsFramebuffer")]
		public static bool IsFramebuffer(uint framebuffer);

		[DllImport(Library, EntryPoint = "glIsProgram")]
		public static bool IsProgram(uint program);

		[DllImport(Library, EntryPoint = "glIsRenderbuffer")]
		public static bool IsRenderbuffer(uint renderbuffer);

		[DllImport(Library, EntryPoint = "glIsShader")]
		public static bool IsShader(uint shader);

		[DllImport(Library, EntryPoint = "glIsTexture")]
		public static bool IsTexture(uint texture);

		[DllImport(Library, EntryPoint = "glLineWidth")]
		public static void LineWidth(float width);

		[DllImport(Library, EntryPoint = "glLinkProgram")]
		public static void LinkProgram(uint program);

		[DllImport(Library, EntryPoint = "glPixelStorei")]
		public static void PixelStorei(uint pname, int param);

		[DllImport(Library, EntryPoint = "glPolygonOffset")]
		public static void PolygonOffset(float factor, float units);

		[DllImport(Library, EntryPoint = "glReadPixels")]
		public static void ReadPixels(int x, int y, int width, int height, uint format, uint type, out byte[] pixels);

		[DllImport(Library, EntryPoint = "glReleaseShaderCompiler")]
		public static void ReleaseShaderCompiler();

		[DllImport(Library, EntryPoint = "glRenderbufferStorage")]
		public static void RenderbufferStorage(uint target, uint internalformat, int width, int height);

		[DllImport(Library, EntryPoint = "glSampleCoverage")]
		public static void SampleCoverage(float value, bool invert);

		[DllImport(Library, EntryPoint = "glScissor")]
		public static void Scissor(int x, int y, int width, int height);

		[DllImport(Library, EntryPoint = "glShaderBinary")]
		public static void ShaderBinary(int count, uint[] shaders, uint binaryformat, byte[] binary, int length);

		[DllImport(Library, EntryPoint = "glShaderSource")]
		public static void ShaderSource(uint shader, int count, GLchar ** @string, int[] length);

		[DllImport(Library, EntryPoint = "glStencilFunc")]
		public static void StencilFunc(uint func, int @ref, uint mask);

		[DllImport(Library, EntryPoint = "glStencilFuncSeparate")]
		public static void StencilFuncSeparate(uint face, uint func, int @ref, uint mask);

		[DllImport(Library, EntryPoint = "glStencilMask")]
		public static void StencilMask(uint mask);

		[DllImport(Library, EntryPoint = "glStencilMaskSeparate")]
		public static void StencilMaskSeparate(uint face, uint mask);

		[DllImport(Library, EntryPoint = "glStencilOp")]
		public static void StencilOp(uint fail, uint zfail, uint zpass);

		[DllImport(Library, EntryPoint = "glStencilOpSeparate")]
		public static void StencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass);

		[DllImport(Library, EntryPoint = "glTexImage2D")]
		public static void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, byte[] pixels);

		[DllImport(Library, EntryPoint = "glTexParameterf")]
		public static void TexParameterf(uint target, uint pname, float param);

		[DllImport(Library, EntryPoint = "glTexParameterfv")]
		public static void TexParameterfv(uint target, uint pname, float[] @params);

		[DllImport(Library, EntryPoint = "glTexParameteri")]
		public static void TexParameteri(uint target, uint pname, int param);

		[DllImport(Library, EntryPoint = "glTexParameteriv")]
		public static void TexParameteriv(uint target, uint pname, int[] @params);

		[DllImport(Library, EntryPoint = "glTexSubImage2D")]
		public static void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, byte[] pixels);

		[DllImport(Library, EntryPoint = "glUniform1f")]
		public static void Uniform1f(int location, float v0);

		[DllImport(Library, EntryPoint = "glUniform1fv")]
		public static void Uniform1fv(int location, int count, float[] value);

		[DllImport(Library, EntryPoint = "glUniform1i")]
		public static void Uniform1i(int location, int v0);

		[DllImport(Library, EntryPoint = "glUniform1iv")]
		public static void Uniform1iv(int location, int count, int[] value);

		[DllImport(Library, EntryPoint = "glUniform2f")]
		public static void Uniform2f(int location, float v0, float v1);

		[DllImport(Library, EntryPoint = "glUniform2fv")]
		public static void Uniform2fv(int location, int count, float[] value);

		[DllImport(Library, EntryPoint = "glUniform2i")]
		public static void Uniform2i(int location, int v0, int v1);

		[DllImport(Library, EntryPoint = "glUniform2iv")]
		public static void Uniform2iv(int location, int count, int[] value);

		[DllImport(Library, EntryPoint = "glUniform3f")]
		public static void Uniform3f(int location, float v0, float v1, float v2);

		[DllImport(Library, EntryPoint = "glUniform3fv")]
		public static void Uniform3fv(int location, int count, float[] value);

		[DllImport(Library, EntryPoint = "glUniform3i")]
		public static void Uniform3i(int location, int v0, int v1, int v2);

		[DllImport(Library, EntryPoint = "glUniform3iv")]
		public static void Uniform3iv(int location, int count, int[] value);

		[DllImport(Library, EntryPoint = "glUniform4f")]
		public static void Uniform4f(int location, float v0, float v1, float v2, float v3);

		[DllImport(Library, EntryPoint = "glUniform4fv")]
		public static void Uniform4fv(int location, int count, float[] value);

		[DllImport(Library, EntryPoint = "glUniform4i")]
		public static void Uniform4i(int location, int v0, int v1, int v2, int v3);

		[DllImport(Library, EntryPoint = "glUniform4iv")]
		public static void Uniform4iv(int location, int count, int[] value);

		[DllImport(Library, EntryPoint = "glUniformMatrix2fv")]
		public static void UniformMatrix2fv(int location, int count, bool transpose, float[] value);

		[DllImport(Library, EntryPoint = "glUniformMatrix3fv")]
		public static void UniformMatrix3fv(int location, int count, bool transpose, float[] value);

		[DllImport(Library, EntryPoint = "glUniformMatrix4fv")]
		public static void UniformMatrix4fv(int location, int count, bool transpose, float[] value);

		[DllImport(Library, EntryPoint = "glUseProgram")]
		public static void UseProgram(uint program);

		[DllImport(Library, EntryPoint = "glValidateProgram")]
		public static void ValidateProgram(uint program);

		[DllImport(Library, EntryPoint = "glVertexAttrib1f")]
		public static void VertexAttrib1f(uint index, float x);

		[DllImport(Library, EntryPoint = "glVertexAttrib1fv")]
		public static void VertexAttrib1fv(uint index, float[] v);

		[DllImport(Library, EntryPoint = "glVertexAttrib2f")]
		public static void VertexAttrib2f(uint index, float x, float y);

		[DllImport(Library, EntryPoint = "glVertexAttrib2fv")]
		public static void VertexAttrib2fv(uint index, float[] v);

		[DllImport(Library, EntryPoint = "glVertexAttrib3f")]
		public static void VertexAttrib3f(uint index, float x, float y, float z);

		[DllImport(Library, EntryPoint = "glVertexAttrib3fv")]
		public static void VertexAttrib3fv(uint index, float[] v);

		[DllImport(Library, EntryPoint = "glVertexAttrib4f")]
		public static void VertexAttrib4f(uint index, float x, float y, float z, float w);

		[DllImport(Library, EntryPoint = "glVertexAttrib4fv")]
		public static void VertexAttrib4fv(uint index, float[] v);

		[DllImport(Library, EntryPoint = "glVertexAttribPointer")]
		public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, byte[] pointer);

		[DllImport(Library, EntryPoint = "glViewport")]
		public static void Viewport(int x, int y, int width, int height);

	}
}
