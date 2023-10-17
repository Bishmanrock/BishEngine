using GLFW;

// This script is what the game uses to check if input events are being triggered. It uses the binding names 'Shoot', 'Move', etc. and checks it against the raw input keys such as keyboard or mouse input, depending on what the actions the keys are binded to

namespace Engine
{
    public static class Input
    {
        static Input()
        {
            KeyDictionary.Add(Actions.Up, Keys.W);
            KeyDictionary.Add(Actions.Down, Keys.S);
            KeyDictionary.Add(Actions.Left, Keys.A);
            KeyDictionary.Add(Actions.Right, Keys.D);
            KeyDictionary.Add(Actions.Action, Keys.Enter);
        }

        // Binds a raw input to an action
        public static void MapKey(Actions action, Keys key)
        {
            //KeyDictionary.Add(Actions.Up, Keys.W);
        }

        public static void CheckInputs()
        {
            //foreach(Actions action in KeyDictionary)
            //for (int index = 0; index < KeyDictionary.Count; index++)
           // {
            //    if (GetKeyDown(action))
            //    {
                    //action.KeyState
            //    }
         //   }
        }

        public static bool GetKey(Actions action)
        {
            return (KeyDictionary.GetKey(action));
        }

        public static bool GetKeyDown(Actions action)
        {
            return (KeyDictionary.GetKeyDown(action));
        }
    }
}