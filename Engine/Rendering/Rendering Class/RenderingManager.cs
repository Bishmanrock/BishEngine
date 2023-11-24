// The Rendering Manager is a static class which is called every frame as part of the game loop. When objects are created they are added to the Rendering Manager, and when they are destroyed/disabled/etc. they are removed. The Rendering Manager cycles through every object in its list to draw them per frame. This works on a small scale, but may need to be expanded on if it becomes too cumbersome if projects ever get large enough. It's possible this may be a sufficient solution for the size games I make though.

namespace Engine
{
    public static class RenderingManager
    {
        private static List<GameObject> renderList = new List<GameObject>();

        // Sets up the rendering manager
        public static void StartRenderingManager()
        {

        }

        // Adds a GameObject to the renderList
        public static void Add(GameObject gameObject)
        {
            renderList.Add(gameObject);
        }

        // Removes a GameObject from the renderList
        public static void Remove(GameObject gameObject)
        {
            renderList.Remove(gameObject);
        }

        // Cycles through all active GameObjects in the list and triggers their Draw functions. In future this should be changed to instead go through anything with the IRenderable interface.
        public static void Draw()
        {
            foreach(GameObject gameObject in renderList)
            {
                //gameObject.Draw();
            }
        }
    }
}