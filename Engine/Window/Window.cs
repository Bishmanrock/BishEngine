using GLFW;
using System.Drawing;
using static OpenGL.GL;

namespace Engine
{
    public static class Window
    {
        public static GLFW.Window window { get; set; }
        public static Vector2 windowSize { get; set; }

        private static Vector4 backgroundColour;

        public static void CreateWindow(int width, int height, string title)
        {
            windowSize = new Vector2(width, height);

            StartOpenGL();

            // Create window, make the OpenGL context current on the thread, and import graphics functions
            window = Glfw.CreateWindow(width, height, title, GLFW.Monitor.None, GLFW.Window.None);

            if (window == GLFW.Window.None)
            {
                Console.WriteLine("Failed to create GLFW window");
                CloseWindow();
                return;
            }

            // Centre the window
            Rectangle screen = Glfw.PrimaryMonitor.WorkArea; // Get the resolution size
            int x = (screen.Width = width) / 2;
            int y = (screen.Height - height) / 2;
            Glfw.SetWindowPosition(window, x, y);
            //

            Glfw.MakeContextCurrent(window);            

            Import(Glfw.GetProcAddress);

            SetWindowSize();
            EnableVsync(false);

            Glfw.SetFramebufferSizeCallback(window, FramebufferSizeCallback);
        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }

        private static void EnableVsync(bool active)
        {
            if (active == true)
            {
                Glfw.SwapInterval(1); // 1 is on
            }
            else
            {
                Glfw.SwapInterval(0); // 0 is off
            }
        }

        private static void StartOpenGL()
        {
            Glfw.Init(); // Initialises GLFW

            // Set some common context creation hints for the OpenGL profile creation
            //Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            //Glfw.WindowHint(Hint.Doublebuffer, true);
            //Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, true);
        }

        private static void FramebufferSizeCallback(GLFW.Window window, int width, int height)
        {
            // This call back is registered in the CreateWindow() function, and is triggered whenever the window is resized, stretching the window to the new width and height
            glViewport(0, 0, width, height);
        }

        public static void SetBackgroundColour(Vector4 colour)
        {
            backgroundColour = colour;
        }

        public static void DrawBackground()
        {
            glClearColor(backgroundColour.X, backgroundColour.Y, backgroundColour.Z, backgroundColour.W);
        }

        // Sets the new size of the window and then calls SetWindowSize
        public static void ResizeWindow(int width, int height)
        {
            windowSize = new Vector2(width, height);
            SetWindowSize();
        }

        // Sets the window to the windowSize
        private static void SetWindowSize()
        {
            glViewport(0, 0, (int)windowSize.x, (int)windowSize.y);
        }
    }
}