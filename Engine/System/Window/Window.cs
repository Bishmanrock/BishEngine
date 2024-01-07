using GLFW;
using OpenGL;
using System.Drawing;
using static OpenGL.GL;

// The Window static class deals with anything relating to the game window, such as resolution, windowed/fullsize, etc. and provides functions around creating, resizing, closing the window, plus others.

namespace Engine
{
    public static class Window
    {
        public static IntPtr window { get; set; }
        public static Vector2 windowSize { get; set; }

        private static Vector4 backgroundColour;

        // Callback delegates
        private static GLFW.SizeCallback _resizeCallback;

        // Creates the application window
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

            //SetWindowSize();
            EnableVsync(false);

            // Set the Callback for Window resizing
            _resizeCallback = new GLFW.SizeCallback(SetWindowSize);
            Glfw.SetWindowSizeCallback((GLFW.Window)window, _resizeCallback);
        }

        // Handle window size change
        private static void SetWindowSize(GLFW.Window window, int width, int height)
        {
            Console.Write("Resizing window");

            // Adjust the viewport to the new window size
            glViewport(0, 0, width, height);

            SetAspectRatio();
        }

        private static void SetAspectRatio()
        {
            // Recalculate the aspect ratio
            float aspectRatio = windowSize.x / windowSize.y;

            // Update the projection matrix
            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(Maths.PiOver4, aspectRatio, 0.1f, 100f);


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

        public static void SetBackgroundColour(Vector4 colour)
        {
            backgroundColour = colour;
        }

        public static void DrawBackground()
        {
            glClearColor(backgroundColour.X, backgroundColour.Y, backgroundColour.Z, backgroundColour.W);
        }

        // Renders the window. Called every frame.
        public static void Render()
        {
            Glfw.SwapBuffers(Window.window); // Swap fore/back framebuffers

            glClear(GL_COLOR_BUFFER_BIT); // Clear the colour buffer
            glClear(GL_DEPTH_BUFFER_BIT); // Clear the depth buffer
        }
    }
}