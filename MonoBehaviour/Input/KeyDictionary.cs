using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using static System.Collections.Specialized.BitVector32;

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
            keyObject.keyState = KeyState.Up;

            dictionary.Add(action, keyObject);

            //Console.WriteLine(key + " added to " + action);
        }

        public static bool Action(Actions action)
        {
            if (Glfw.GetKey(Window.window, dictionary.GetValueOrDefault(action).key) == InputState.Press)
            {
                return true; ;
            }

            return false;
        }

        //    public static void Bind(Actions action, KeyTest key)
        //     {
        //       key.isDown = true;
        //    }

        // Cycles through all the keys and checked if they're down, up, being held, etc.
        public static void CheckInputs()
        {
            foreach (var action in dictionary.Keys)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", action, GetDictionaryValue(action).key);

                Keys key = GetDictionaryValue(action).key;

                if (Glfw.GetKey(Window.window, key) == InputState.Press)
                {
                    CheckIfHeld(action);
                }
                else
                {
                    //Console.WriteLine("Key up!");
                    SetKeyState(action, KeyState.Up);
                }
            }
        }

        public static void CheckIfHeld(Actions action)
        {
            if (dictionary.GetValueOrDefault(action).keyState == KeyState.Down
                || dictionary.GetValueOrDefault(action).keyState == KeyState.Held)
            {
                //Console.WriteLine("Key held!");
                SetKeyState(action, KeyState.Held);
            }
            else
            {
                SetKeyState(action, KeyState.Down);
                //Console.WriteLine("Key down!");
            }

            //Console.WriteLine(dictionary.GetValueOrDefault(action).keyState);
        }

        private static void SetKeyState(Actions action, KeyState state)
        {
            KeyObject keyObject = GetDictionaryValue(action);

            keyObject.keyState = state;

            dictionary[action] = keyObject;
        }

        private static KeyObject GetDictionaryValue(Actions action)
        {
            return dictionary.GetValueOrDefault(action);
        }

        public static bool GetKeyDown(Actions action)
        {
            if (GetDictionaryValue(action).keyState == KeyState.Down)
            {
                return true;
            }

            return false;

            //if (Glfw.GetKey(Window.window, key.key) == InputState.Press)
            //{
            //    if (key.keyState == KeyState.Down)
            //    {
            //        return true; ;
            //    }
            //}
        }
    }
}
