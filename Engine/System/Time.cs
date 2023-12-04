// The Time class provides various fields to track application running time or frame times.

using GLFW;

namespace Engine
{
    public static class Time
    {
        public static float deltaTime { get; private set; } // The interval in seconds from the last frame to the current one

        private static float lastFrame; // Log of the previous frame

        public static float totalElapsedSeconds { get; set; } // The total elapsed seconds since the application started running // REplaced by time

        public static float previousTime { get; set; } // The previously recorded totalElapsedSeconds

        public static float applicationStartTime { get; set; } // The time when the application started

        public static float time { get; set; } // The time at the beginning of this frame

        private static double lastTime = Glfw.Time;

        private static int frames;

        public static void Update()
        {
            double currentTime = Glfw.Time;
            deltaTime = (float)currentTime - lastFrame;
            lastFrame = (float)currentTime;

            frames++;

            if (currentTime - lastTime >= 1.0)
            {
                Console.WriteLine($"{frames} fps");
                frames = 0;
                lastTime += 1.0;
            }
        }
    }
}