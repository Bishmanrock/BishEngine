using System.Net.Http.Headers;
using static OpenGL.GL;

// Class to instantly instantiate a cube GameObject

namespace Engine
{
    public class Cube : IRenderable, ICollideable
    {
        public Renderable renderData { get; }

        private Mesh mesh;
        public Collider collider;

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public Cube()
        {
            mesh = new MeshCube();

            //collider = new Collider(this);
            //collider.SetVertices(mesh.GetMeshVertices());

            renderData = new Renderable(mesh.GetMeshVertices(), _indices);

            renderData.SetTexture("border", 0);
            renderData.SetTexture("dougFace", 1);

            RenderingManager.Add(this);
        }

        public Shader GetShader()
        {
            return renderData.shader;
        }

        public Transform transform { get; set; }
        public bool isActive { get; set; }
    }
}