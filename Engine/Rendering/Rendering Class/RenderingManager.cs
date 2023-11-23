// The Rendering Manager is a static class which is called every frame as part of the game loop. When objects are created they are added to the Rendering Manager, and when they are destroyed/disabled/etc. they are removed. The Rendering Manager cycles through every object in its list to draw them per frame. This works on a small scale, but may need to be expanded on if it becomes too cumbersome if projects ever get large enough. It's possible this may be a sufficient solution for the size games I make though.

namespace Engine
{
    public static class RenderingManager
    {
        //private List

        public static void AddToManager()
        {

        }

        public static void Draw()
        {

        }
    }
}