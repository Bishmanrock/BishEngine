using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Mesh is a parent class for multiple other mesh objects. Basically these objects store the vertice data for meshes, both for use in rendering the object and sometimes (for less complicated objects) rendering the collider shape.

namespace Engine
{
    public class Mesh
    {
        public float[] vertices; // The vertices data
        public int[] indices { get; set; }

        public string text;

        public float[] GetMeshVertices()
        {
            return vertices;
        }
    }
}