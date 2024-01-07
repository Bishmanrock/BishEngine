

// The Rendering Manager is a static class which is called every frame as part of the game loop. When objects are created they are added to the Rendering Manager, and when they are destroyed/disabled/etc. they are removed. The Rendering Manager cycles through every object in its list to draw them per frame. This works on a small scale, but may need to be expanded on if it becomes too cumbersome if projects ever get large enough. It's possible this may be a sufficient solution for the size games I make though.

using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Engine
{
    public static class RenderingManager
    {
        // OpenGL function definitions
        private const uint GL_FRONT_AND_BACK = 0x0408;
        private const uint GL_LINE = 0x1B01;
        private const uint GL_FILL = 0x1B02;

        [DllImport("opengl32.dll")]
        private static extern void glPolygonMode(uint face, uint mode);

        private static bool wireframeMode = false;

        private static List<GameObject> renderList = new List<GameObject>();

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
            foreach(IRenderable renderable in renderList)
            {
                renderable.renderData.Draw();
            }
        }

        public static void SetWireframeMode()
        {
            wireframeMode = !wireframeMode;

            if (wireframeMode == true)
            {
                glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
            }
            else
            {
                glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
            }
        }
    }
}