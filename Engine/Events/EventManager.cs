// A class that manages and sorts through events

namespace Engine
{
    public static class EventManager
    {
        private static List<Event> events = new List<Event>();

        public static void Events()
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

                if (StateManager.GetState() == GameState.GAME_ACTIVE)
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

        private static bool CheckFunctionKey(Key key)
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
