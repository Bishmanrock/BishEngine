using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// This static class holds the colour data for the palette to be used in the game. This restricts the colours used when rendering to show a more retro look.

namespace Engine
{
    public static class Palette
    {
        public static Vector3[] palette;

        static Palette()
        {
            palette = new Vector3[]
            {
                new Vector3(1.0f, 0.0f, 0.0f), // Red
                new Vector3(0.0f, 1.0f, 0.0f), // Green
                new Vector3(0.0f, 0.0f, 1.0f)  // Blue
            };
        }
    }
}