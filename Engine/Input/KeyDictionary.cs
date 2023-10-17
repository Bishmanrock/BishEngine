using GLFW;

// The dictionary that holds keys and their bindings

namespace Engine
{
    public static class KeyDictionary
    {
        static Dictionary<Actions, KeyObject> dictionary = new Dictionary<Actions, KeyObject>();

        public static void Add(Actions action, Keys key)
        {
            KeyObject keyObject = new KeyObject();
            keyObject.key = key;
            keyObject.timeHeld = 0;

            dictionary.Add(action, keyObject);
        }

        public static bool Action(Actions action)
        {
            if (Glfw.GetKey(Window.window, dictionary.GetValueOrDefault(action).key) == InputState.Press)
            {
                return true; ;
            }

            return false;
        }

        // Cycles through all the keys and checked if they're down, up, being held, etc.
        public static void CheckInputs()
        {
            foreach (var action in dictionary.Keys)
            {
                Keys key = GetDictionaryValue(action).key;

                if (Glfw.GetKey(Window.window, key) == InputState.Press)
                {
                    AddKeyHoldTimer(action);
                }
                else
                {
                    ResetKeyHoldTimer(action);
                }
            }
        }

        // If key is held, add to the timeHeld for that key
        private static void AddKeyHoldTimer(Actions action)
        {
            KeyObject keyObject = GetDictionaryValue(action);

            keyObject.timeHeld++;

            dictionary[action] = keyObject;
        }

        // If key is up, rest the timeHeld to zero
        private static void ResetKeyHoldTimer(Actions action)
        {
            KeyObject keyObject = GetDictionaryValue(action);

            keyObject.timeHeld = 0;

            dictionary[action] = keyObject;
        }

        private static KeyObject GetDictionaryValue(Actions action)
        {
            return dictionary.GetValueOrDefault(action);
        }

        // Returns true if the key has been pressed this frame
        public static bool GetKeyDown(Actions action)
        {
            if (GetDictionaryValue(action).timeHeld == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Returns true if the key is currently pressed
        public static bool GetKey(Actions action)
        {
            if (GetDictionaryValue(action).timeHeld > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}