using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Samurai.GameFramework
{
    internal static class GLFW
    {
#if x86
        private const string Library = "glfw3_x86.dll";
#endif

#if x64
        private const string Library = "glfw3_x64.dll";
#endif

		[DllImport(Library, EntryPoint = "glfwCreateWindow", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr CreateWindow(int width, int height, string title, IntPtr monitor, IntPtr share);

		[DllImport(Library, EntryPoint = "glfwGetPrimaryMonitor", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr GetPrimaryMonitor();
			
        [DllImport(Library, EntryPoint = "glfwGetProcAddress", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr GetProcAddress(string procName);

		[DllImport(Library, EntryPoint = "glfwHideWindow", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void HideWindow(IntPtr window);

        [DllImport(Library, EntryPoint = "glfwInit", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int Init();

        [DllImport(Library, EntryPoint = "glfwMakeContextCurrent", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void MakeContextCurrent(IntPtr window);

        [DllImport(Library, EntryPoint = "glfwPollEvents", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void PollEvents();

        [DllImport(Library, EntryPoint = "glfwSetCharCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern CharFun SetCharCallback(IntPtr window, CharFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetCursorPosCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern CursorPosFun SetCursorPosCallback(IntPtr window, CursorPosFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetErrorCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern ErrorFun SetErrorCallback(ErrorFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetKeyCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern KeyFun SetKeyCallback(IntPtr window, KeyFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetMouseButtonCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern MouseButtonFun SetMouseButtonCallback(IntPtr window, MouseButtonFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetScrollCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern ScrollFun SetScrollCallback(IntPtr window, ScrollFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetWindowPos", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void SetWindowPos(IntPtr window, int xpos, int ypos);

        [DllImport(Library, EntryPoint = "glfwSetWindowPosCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void SetWindowPosCallback(IntPtr window, WindowPosFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetWindowShouldClose", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int SetWindowShouldClose(IntPtr window, int value);

        [DllImport(Library, EntryPoint = "glfwSetWindowSize", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void SetWindowSize(IntPtr window, int width, int height);

        [DllImport(Library, EntryPoint = "glfwSetWindowSizeCallback", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void SetWindowSizeCallback(IntPtr window, WindowSizeFun cbfun);

        [DllImport(Library, EntryPoint = "glfwSetWindowTitle", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void SetWindowTitle(IntPtr window, string title);

		[DllImport(Library, EntryPoint = "glfwShowWindow", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void ShowWindow(IntPtr window);

        [DllImport(Library, EntryPoint = "glfwSwapBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void SwapBuffers(IntPtr window);

        [DllImport(Library, EntryPoint = "glfwTerminate", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int Terminate();

        [DllImport(Library, EntryPoint = "glfwWindowHint", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int WindowHint(int target, int hint);

        [DllImport(Library, EntryPoint = "glfwWindowShouldClose", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int WindowShouldClose(IntPtr window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void CharFun(IntPtr window, uint codepoint);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void CursorEnterFun(IntPtr window, int entered);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void CursorPosFun(IntPtr window, double xpos, double ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void ErrorFun(int error, string description);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void KeyFun(IntPtr window, int key, int scancode, int action, int mods);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void MouseButtonFun(IntPtr window, int button, int action, int mods);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void ScrollFun(IntPtr window, double xoffset, double yoffset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void WindowPosFun(IntPtr window, int xpos, int ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public delegate void WindowSizeFun(IntPtr window, int width, int height);

        public const int RELEASE = 0;
        public const int PRESS = 1;
        public const int REPEAT = 2;

        public const int KEY_UNKNOWN = -1;
        public const int KEY_SPACE = 32;
        public const int KEY_APOSTROPHE = 39;
        public const int KEY_COMMA = 44;
        public const int KEY_MINUS = 45;
        public const int KEY_PERIOD = 46;
        public const int KEY_SLASH = 47;
        public const int KEY_0 = 48;
        public const int KEY_1 = 49;
        public const int KEY_2 = 50;
        public const int KEY_3 = 51;
        public const int KEY_4 = 52;
        public const int KEY_5 = 53;
        public const int KEY_6 = 54;
        public const int KEY_7 = 55;
        public const int KEY_8 = 56;
        public const int KEY_9 = 57;
        public const int KEY_SEMICOLON = 59;
        public const int KEY_EQUAL = 61;
        public const int KEY_A = 65;
        public const int KEY_B = 66;
        public const int KEY_C = 67;
        public const int KEY_D = 68;
        public const int KEY_E = 69;
        public const int KEY_F = 70;
        public const int KEY_G = 71;
        public const int KEY_H = 72;
        public const int KEY_I = 73;
        public const int KEY_J = 74;
        public const int KEY_K = 75;
        public const int KEY_L = 76;
        public const int KEY_M = 77;
        public const int KEY_N = 78;
        public const int KEY_O = 79;
        public const int KEY_P = 80;
        public const int KEY_Q = 81;
        public const int KEY_R = 82;
        public const int KEY_S = 83;
        public const int KEY_T = 84;
        public const int KEY_U = 85;
        public const int KEY_V = 86;
        public const int KEY_W = 87;
        public const int KEY_X = 88;
        public const int KEY_Y = 89;
        public const int KEY_Z = 90;
        public const int KEY_LEFT_BRACKET = 91;
        public const int KEY_BACKSLASH = 92;
        public const int KEY_RIGHT_BRACKET = 93;
        public const int KEY_GRAVE_ACCENT = 96;
        public const int KEY_WORLD_1 = 161;
        public const int KEY_WORLD_2 = 162;
        public const int KEY_ESCAPE = 256;
        public const int KEY_ENTER = 257;
        public const int KEY_TAB = 258;
        public const int KEY_BACKSPACE = 259;
        public const int KEY_INSERT = 260;
        public const int KEY_DELETE = 261;
        public const int KEY_RIGHT = 262;
        public const int KEY_LEFT = 263;
        public const int KEY_DOWN = 264;
        public const int KEY_UP = 265;
        public const int KEY_PAGE_UP = 266;
        public const int KEY_PAGE_DOWN = 267;
        public const int KEY_HOME = 268;
        public const int KEY_END = 269;
        public const int KEY_CAPS_LOCK = 280;
        public const int KEY_SCROLL_LOCK = 281;
        public const int KEY_NUM_LOCK = 282;
        public const int KEY_PRINT_SCREEN = 283;
        public const int KEY_PAUSE = 284;
        public const int KEY_F1 = 290;
        public const int KEY_F2 = 291;
        public const int KEY_F3 = 292;
        public const int KEY_F4 = 293;
        public const int KEY_F5 = 294;
        public const int KEY_F6 = 295;
        public const int KEY_F7 = 296;
        public const int KEY_F8 = 297;
        public const int KEY_F9 = 298;
        public const int KEY_F10 = 299;
        public const int KEY_F11 = 300;
        public const int KEY_F12 = 301;
        public const int KEY_F13 = 302;
        public const int KEY_F14 = 303;
        public const int KEY_F15 = 304;
        public const int KEY_F16 = 305;
        public const int KEY_F17 = 306;
        public const int KEY_F18 = 307;
        public const int KEY_F19 = 308;
        public const int KEY_F20 = 309;
        public const int KEY_F21 = 310;
        public const int KEY_F22 = 311;
        public const int KEY_F23 = 312;
        public const int KEY_F24 = 313;
        public const int KEY_F25 = 314;
        public const int KEY_KP_0 = 320;
        public const int KEY_KP_1 = 321;
        public const int KEY_KP_2 = 322;
        public const int KEY_KP_3 = 323;
        public const int KEY_KP_4 = 324;
        public const int KEY_KP_5 = 325;
        public const int KEY_KP_6 = 326;
        public const int KEY_KP_7 = 327;
        public const int KEY_KP_8 = 328;
        public const int KEY_KP_9 = 329;
        public const int KEY_KP_DECIMAL = 330;
        public const int KEY_KP_DIVIDE = 331;
        public const int KEY_KP_MULTIPLY = 332;
        public const int KEY_KP_SUBTRACT = 333;
        public const int KEY_KP_ADD = 334;
        public const int KEY_KP_ENTER = 335;
        public const int KEY_KP_EQUAL = 336;
        public const int KEY_LEFT_SHIFT = 340;
        public const int KEY_LEFT_CONTROL = 341;
        public const int KEY_LEFT_ALT = 342;
        public const int KEY_LEFT_SUPER = 343;
        public const int KEY_RIGHT_SHIFT = 344;
        public const int KEY_RIGHT_CONTROL = 345;
        public const int KEY_RIGHT_ALT = 346;
        public const int KEY_RIGHT_SUPER = 347;
        public const int KEY_MENU = 348;
        public const int KEY_LAST = KEY_MENU;
        public const int MOD_SHIFT = 0x0001;
        public const int MOD_CONTROL = 0x0002;
        public const int MOD_ALT = 0x0004;
        public const int MOD_SUPER = 0x0008;
        
        public const int MOUSE_BUTTON_1 = 0;
        public const int MOUSE_BUTTON_2 = 1;
        public const int MOUSE_BUTTON_3 = 2;
        public const int MOUSE_BUTTON_4 = 3;
        public const int MOUSE_BUTTON_5 = 4;
        public const int MOUSE_BUTTON_6 = 5;
        public const int MOUSE_BUTTON_7 = 6;
        public const int MOUSE_BUTTON_8 = 7;
        public const int MOUSE_BUTTON_LAST = MOUSE_BUTTON_8;
        public const int MOUSE_BUTTON_LEFT = MOUSE_BUTTON_1;
        public const int MOUSE_BUTTON_RIGHT = MOUSE_BUTTON_2;
        public const int MOUSE_BUTTON_MIDDLE = MOUSE_BUTTON_3;
        
        public const int JOYSTICK_1 = 0;
        public const int JOYSTICK_2 = 1;
        public const int JOYSTICK_3 = 2;
        public const int JOYSTICK_4 = 3;
        public const int JOYSTICK_5 = 4;
        public const int JOYSTICK_6 = 5;
        public const int JOYSTICK_7 = 6;
        public const int JOYSTICK_8 = 7;
        public const int JOYSTICK_9 = 8;
        public const int JOYSTICK_10 = 9;
        public const int JOYSTICK_11 = 10;
        public const int JOYSTICK_12 = 11;
        public const int JOYSTICK_13 = 12;
        public const int JOYSTICK_14 = 13;
        public const int JOYSTICK_15 = 14;
        public const int JOYSTICK_16 = 15;
        public const int JOYSTICK_LAST = JOYSTICK_16;
        
        public const int NOT_INITIALIZED = 0x00010001;
        public const int NO_CURRENT_CONTEXT = 0x00010002;
        public const int INVALID_ENUM = 0x00010003;
        public const int INVALID_VALUE = 0x00010004;
        public const int OUT_OF_MEMORY = 0x00010005;
        public const int API_UNAVAILABLE = 0x00010006;
        public const int VERSION_UNAVAILABLE = 0x00010007;
        public const int PLATFORM_ERROR = 0x00010008;
        public const int FORMAT_UNAVAILABLE = 0x00010009;
        public const int FOCUSED = 0x00020001;
        public const int ICONIFIED = 0x00020002;
        public const int RESIZABLE = 0x00020003;
        public const int VISIBLE = 0x00020004;
        public const int DECORATED = 0x00020005;
        public const int RED_BITS = 0x00021001;
        public const int GREEN_BITS = 0x00021002;
        public const int BLUE_BITS = 0x00021003;
        public const int ALPHA_BITS = 0x00021004;
        public const int DEPTH_BITS = 0x00021005;
        public const int STENCIL_BITS = 0x00021006;
        public const int ACCUM_RED_BITS = 0x00021007;
        public const int ACCUM_GREEN_BITS = 0x00021008;
        public const int ACCUM_BLUE_BITS = 0x00021009;
        public const int ACCUM_ALPHA_BITS = 0x0002100A;
        public const int AUX_BUFFERS = 0x0002100B;
        public const int STEREO = 0x0002100C;
        public const int SAMPLES = 0x0002100D;
        public const int SRGB_CAPABLE = 0x0002100E;
        public const int REFRESH_RATE = 0x0002100F;
        public const int CLIENT_API = 0x00022001;
        public const int CONTEXT_VERSION_MAJOR = 0x00022002;
        public const int CONTEXT_VERSION_MINOR = 0x00022003;
        public const int CONTEXT_REVISION = 0x00022004;
        public const int CONTEXT_ROBUSTNESS = 0x00022005;
        public const int OPENGL_FORWARD_COMPAT = 0x00022006;
        public const int OPENGL_DEBUG_CONTEXT = 0x00022007;
        public const int OPENGL_PROFILE = 0x00022008;
        public const int OPENGL_API = 0x00030001;
        public const int OPENGL_ES_API = 0x00030002;
        public const int NO_ROBUSTNESS = 0;
        public const int NO_RESET_NOTIFICATION = 0x00031001;
        public const int LOSE_CONTEXT_ON_RESET = 0x00031002;
        public const int OPENGL_ANY_PROFILE = 0;
        public const int OPENGL_CORE_PROFILE = 0x00032001;
        public const int OPENGL_COMPAT_PROFILE = 0x00032002;
        public const int CURSOR = 0x00033001;
        public const int STICKY_KEYS = 0x00033002;
        public const int STICKY_MOUSE_BUTTONS = 0x00033003;
        public const int CURSOR_NORMAL = 0x00034001;
        public const int CURSOR_HIDDEN = 0x00034002;
        public const int CURSOR_DISABLED = 0x00034003;
        public const int CONNECTED = 0x00040001;
        public const int DISCONNECTED = 0x00040002;
 
        public static void RegisterErrorCallback()
        {
            SetErrorCallback(errorCallback);
        }

        // It is important to hold a reference to the callback so that the GC does not garbage collect the delegate.
        private static ErrorFun errorCallback = OnError;

        private static void OnError(int error, string description)
        {
            throw new SamuraiException(string.Format("GLFW Error: ({0}) {1}", error, description));
        }

        public static Key ConvertKeyCode(int keyCode)
        {
            switch (keyCode)
            {
                case KEY_SPACE: return Key.Space;
                case KEY_APOSTROPHE: return Key.Apostrophe;
                case KEY_COMMA: return Key.Comma;
                case KEY_MINUS: return Key.Minus;
                case KEY_PERIOD: return Key.Period;
                case KEY_SLASH: return Key.Slash;
                case KEY_0: return Key.Num0;
                case KEY_1: return Key.Num1;
                case KEY_2: return Key.Num2;
                case KEY_3: return Key.Num3;
                case KEY_4: return Key.Num4;
                case KEY_5: return Key.Num5;
                case KEY_6: return Key.Num6;
                case KEY_7: return Key.Num7;
                case KEY_8: return Key.Num8;
                case KEY_9: return Key.Num9;
                case KEY_SEMICOLON: return Key.Semicolon;
                case KEY_EQUAL: return Key.Equal;
                case KEY_A: return Key.A;
                case KEY_B: return Key.B;
                case KEY_C: return Key.C;
                case KEY_D: return Key.D;
                case KEY_E: return Key.E;
                case KEY_F: return Key.F;
                case KEY_G: return Key.G;
                case KEY_H: return Key.H;
                case KEY_I: return Key.I;
                case KEY_J: return Key.J;
                case KEY_K: return Key.K;
                case KEY_L: return Key.L;
                case KEY_M: return Key.M;
                case KEY_N: return Key.N;
                case KEY_O: return Key.O;
                case KEY_P: return Key.P;
                case KEY_Q: return Key.Q;
                case KEY_R: return Key.R;
                case KEY_S: return Key.S;
                case KEY_T: return Key.T;
                case KEY_U: return Key.U;
                case KEY_V: return Key.V;
                case KEY_W: return Key.W;
                case KEY_X: return Key.X;
                case KEY_Y: return Key.Y;
                case KEY_Z: return Key.Z;
                case KEY_LEFT_BRACKET: return Key.LeftBracket;
                case KEY_BACKSLASH: return Key.Backslash;
                case KEY_RIGHT_BRACKET: return Key.RightBracket;
                case KEY_GRAVE_ACCENT: return Key.GraveAccent;
                case KEY_WORLD_1: return Key.World1;
                case KEY_WORLD_2: return Key.World2;
                case KEY_ESCAPE: return Key.Escape;
                case KEY_ENTER: return Key.Enter;
                case KEY_TAB: return Key.Tab;
                case KEY_BACKSPACE: return Key.Backspace;
                case KEY_INSERT: return Key.Insert;
                case KEY_DELETE: return Key.Delete;
                case KEY_RIGHT: return Key.Right;
                case KEY_LEFT: return Key.Left;
                case KEY_DOWN: return Key.Down;
                case KEY_UP: return Key.Up;
                case KEY_PAGE_UP: return Key.PageUp;
                case KEY_PAGE_DOWN: return Key.PageDown;
                case KEY_HOME: return Key.Home;
                case KEY_END: return Key.End;
                case KEY_CAPS_LOCK: return Key.CapsLock;
                case KEY_SCROLL_LOCK: return Key.ScrollLock;
                case KEY_NUM_LOCK: return Key.NumLock;
                case KEY_PRINT_SCREEN: return Key.PrintScreen;
                case KEY_PAUSE: return Key.Pause;
                case KEY_F1: return Key.F1;
                case KEY_F2: return Key.F2;
                case KEY_F3: return Key.F3;
                case KEY_F4: return Key.F4;
                case KEY_F5: return Key.F5;
                case KEY_F6: return Key.F6;
                case KEY_F7: return Key.F7;
                case KEY_F8: return Key.F8;
                case KEY_F9: return Key.F9;
                case KEY_F10: return Key.F10;
                case KEY_F11: return Key.F11;
                case KEY_F12: return Key.F12;
                case KEY_F13: return Key.F13;
                case KEY_F14: return Key.F14;
                case KEY_F15: return Key.F15;
                case KEY_F16: return Key.F16;
                case KEY_F17: return Key.F17;
                case KEY_F18: return Key.F18;
                case KEY_F19: return Key.F19;
                case KEY_F20: return Key.F20;
                case KEY_F21: return Key.F21;
                case KEY_F22: return Key.F22;
                case KEY_F23: return Key.F23;
                case KEY_F24: return Key.F24;
                case KEY_F25: return Key.F25;
                case KEY_KP_0: return Key.Keypad0;
                case KEY_KP_1: return Key.Keypad1;
                case KEY_KP_2: return Key.Keypad2;
                case KEY_KP_3: return Key.Keypad3;
                case KEY_KP_4: return Key.Keypad4;
                case KEY_KP_5: return Key.Keypad5;
                case KEY_KP_6: return Key.Keypad6;
                case KEY_KP_7: return Key.Keypad7;
                case KEY_KP_8: return Key.Keypad8;
                case KEY_KP_9: return Key.Keypad9;
                case KEY_KP_DECIMAL: return Key.KeypadDecimal;
                case KEY_KP_DIVIDE: return Key.KeypadDivide;
                case KEY_KP_MULTIPLY: return Key.KeypadMultiply;
                case KEY_KP_SUBTRACT: return Key.KeypadSubtract;
                case KEY_KP_ADD: return Key.KeypadAdd;
                case KEY_KP_ENTER: return Key.KeypadEnter;
                case KEY_KP_EQUAL: return Key.KeypadEqual;
                case KEY_LEFT_SHIFT: return Key.LeftShift;
                case KEY_LEFT_CONTROL: return Key.LeftControl;
                case KEY_LEFT_ALT: return Key.LeftAlt;
                case KEY_LEFT_SUPER: return Key.LeftSuper;
                case KEY_RIGHT_SHIFT: return Key.RightShift;
                case KEY_RIGHT_CONTROL: return Key.RightControl;
                case KEY_RIGHT_ALT: return Key.RightAlt;
                case KEY_RIGHT_SUPER: return Key.RightSuper;
                case KEY_MENU: return Key.Menu;
            }

            return Key.Unknown;
        }

        public static MouseButton ConvertMouseButton(int button)
        {
            switch (button)
            {
                case MOUSE_BUTTON_1: return MouseButton.Left;
                case MOUSE_BUTTON_2: return MouseButton.Right;
                case MOUSE_BUTTON_3: return MouseButton.Middle;
                case MOUSE_BUTTON_4: return MouseButton.XButton1;
                case MOUSE_BUTTON_5: return MouseButton.XButton2;
                case MOUSE_BUTTON_6: return MouseButton.XButton3;
                case MOUSE_BUTTON_7: return MouseButton.XButton4;
                case MOUSE_BUTTON_8: return MouseButton.XButton5;
            }

            return MouseButton.Unknown;
        }
    }
}
