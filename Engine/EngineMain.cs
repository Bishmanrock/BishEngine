using GLFW;
using static OpenGL.GL;

// EngineMain acts as the engines main core, containing the main game loop. This is the class that a main game inherits from. 

// There are multiple fields on here that could probably do with being refactored out into cleaner places. There are also some functionalities around times and FPS that would be better places elsewhere, or in the Time class.

namespace Engine
{
    public abstract class EngineMain
    {
        private const int targetFPS = 60;
        private const int targetFrameTime = 1000 / targetFPS;
        private static bool isRunning = false;

        double t = 0.0;
        double dt = 1.0 / 60.0;

        double currentTime = (float)Glfw.Time;

        private WindowResizeEvent windowResizeEvent;

        private int initialWindowWidth = 960;
        private int initialWindowHeight = 960;
        private string initialWindowName = "Hello!";

        private GameState gameState;

        private List<Event> events;

        public EngineMain()
        {
            events = new List<Event>();

            Initialize();

            CreateWindow();

            LoadContent();

            RenderingManager.StartRenderingManager();

            // This is the main game loop for the engine
            while (Glfw.WindowShouldClose(Window.window) == false)
            {
                SetTimes();

                KeyDictionary.CheckInputs();

                Inputs();
                Events();
                Update();

                Window.Render();

                Glfw.PollEvents(); // Poll for operating system events, such as keyboard or mouse input events

                //RenderingManager.Draw(); // Not in full use yet so commented out

                Draw();

                Window.DrawBackground();
            }

            Window.CloseWindow();
        }

        static void Inputs()
        {
            if (Glfw.GetKey(Window.window, Keys.Escape) == InputState.Press)
            {
                // Note this only sets that the window should close. The window shouldn't actually close until the end of the current game loop rotation, to prevent any mid-rendering issues
                Glfw.SetWindowShouldClose(Window.window, true);
            }
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void Update();
        public abstract void Draw();

        // Creates the program window
        private void CreateWindow()
        {
            Console.WriteLine("Engine start");

            Window.CreateWindow(initialWindowWidth, initialWindowHeight, initialWindowName);
        }

        private void SetTimes()
        {
            // Sets the relevant timers in Time

            Time.deltaTime = (float)Glfw.Time - Time.time;

            Time.time = (float)Glfw.Time;
        }

        private void Events()
        {
            foreach (var e in events)
            {
                if (e.Type == EventType.KeyPressed)
                {
                    if (CheckFunctionKey(e.Key))
                    {
                        Console.WriteLine("KeyPressed");
                        continue;
                    }
                }

                if (gameState == GameState.GAME_ACTIVE)
                {
                    if (e.Key == Key.Pause && e.Type == EventType.KeyPressed)
                    {
                        Console.WriteLine("Paused");
                        continue;
                    }
                }
            }

            events.Clear();
        }

        private bool CheckFunctionKey(Key key)
        {
            switch (key)
            {
                case Key.F1:
                    Console.Write("F1 Pressed");
                    return true;

                case Key.F2:
                    return true;

                case Key.F3:
                    return true;

                case Key.F4:
                    return true;

                case Key.F6:
                    return true;

                case Key.F7:
                    return true;

                default:
                    return false;
            }
        }
    }
}
