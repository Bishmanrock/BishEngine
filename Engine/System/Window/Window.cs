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

        //public static Action<GLFW.Window, int, int> WindowSizeCallbackDelegate;
        //private static GLFW.SizeCallback sizeCallback; // The callback for window resize

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

            SetWindowSize();
            EnableVsync(false);


            //WindowSizeCallbackDelegate = new Action<GLFW.Window, int, int>(WindowSizeCallback);

            //sizeCallback = OnWindowSizeChanged;

            // Set the Callback for Window resizing
            Glfw.SetWindowSizeCallback((GLFW.Window)window, OnWindowSizeChanged);

            //Glfw.SetFramebufferSizeCallback(window, sizeCallback);

            Glfw.SetFramebufferSizeCallback(window, FramebufferSizeCallback);
        }

        // Handle window size change
        private static void OnWindowSizeChanged(GLFW.Window window, int width, int height)
        {
            // Adjust the viewport to the new window size
            glViewport(0, 0, width, height);
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
            Console.Write("Framebuffersizecallback");
            // This call back is registered in the CreateWindow() function, and is triggered whenever the window is resized, stretching the window to the new width and height
            glViewport(0, 0, width, height);
        }

        // Event used for resizing the window
        public static void WindowSizeCallback(GLFW.Window window, int width, int height)
        {
            Console.Write("Window resize");
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
            SetWindowSize2(width, height);
        }

        // Sets the window to the windowSize
        private static void SetWindowSize()
        {
            glViewport(0, 0, (int)windowSize.x, (int)windowSize.y);
        }

        // Testing only, potential replacement for the above
        public static void SetWindowSize2(int width, int height)
        {
            // Calculate the proper aspect ratio to use based on window ratio
            var ratioX = width / (float)windowSize.x;
            var ratioY = height / (float)windowSize.y;
            var ratio = ratioX < ratioY ? ratioX : ratioY;

            // Calculate the width and height that the will be rendered to
            var viewWidth = Convert.ToInt32(windowSize.x * ratio);
            var viewHeight = Convert.ToInt32(windowSize.y * ratio);

            // Calculate the position, which will apply proper "pillar" or "letterbox" 
            var viewX = Convert.ToInt32((width - windowSize.x * ratio) / 2);
            var viewY = Convert.ToInt32((height - windowSize.y * ratio) / 2);

            glViewport(0, 0, (int)windowSize.x, (int)windowSize.y);
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