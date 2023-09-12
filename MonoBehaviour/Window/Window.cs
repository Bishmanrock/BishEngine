using GLFW;
using GLFW.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static OpenGL.GL;
using static System.Formats.Asn1.AsnWriter;

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

            glViewport(0, 0, width, height);
            Glfw.SwapInterval(0); // Vsync, 0 off, 1 on

            Glfw.SetFramebufferSizeCallback(window, FramebufferSizeCallback);
        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
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
    }
}
