using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

// Class that controls where in the world space an object sits

namespace Engine
{
    public struct Transform
    {
        Vector3 position;
        Vector3 eulerRot;
        Vector3 scale;

        public Transform()
        {
            position = new Vector3(0.0f, 0.0f, 0.0f);
            eulerRot = new Vector3(0.0f, 0.0f, 0.0f);
            scale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}