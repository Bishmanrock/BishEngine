using GLFW;

// The dictionary that holds keys and their bindings

namespace Engine
{
    public static class KeyDictionary
    {
        static Dictionary<KeyCode, KeyObject> dictionary = new Dictionary<KeyCode, KeyObject>();

        public static void Add(KeyCode action, Keys key)
        {
            KeyObject keyObject = new KeyObject();
            keyObject.key = key;
            keyObject.timeHeld = 0;

            dictionary.Add(action, keyObject);
        }

        public static bool Action(KeyCode action)
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
        private static void AddKeyHoldTimer(KeyCode action)
        {
            KeyObject keyObject = GetDictionaryValue(action);

            keyObject.timeHeld++;

            dictionary[action] = keyObject;
        }

        // If key is up, rest the timeHeld to zero
        private static void ResetKeyHoldTimer(KeyCode action)
        {
            KeyObject keyObject = GetDictionaryValue(action);

            keyObject.timeHeld = 0;

            dictionary[action] = keyObject;
        }

        private static KeyObject GetDictionaryValue(KeyCode action)
        {
            return dictionary.GetValueOrDefault(action);
        }

        // Returns true if the key has been pressed this frame
        public static bool GetKeyDown(KeyCode action)
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
        public static bool GetKey(KeyCode action)
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