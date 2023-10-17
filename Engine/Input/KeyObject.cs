using GLFW;

namespace Engine
{
    public struct KeyObject
    {
        public Keys key;
        public float timeHeld; // The amount of time the key has been held down. Query 0 for up, 1 for pressed, and > 1 for held
    }
}