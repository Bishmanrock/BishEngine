using System.Net.Http.Headers;
using static OpenGL.GL;

// Class to instantly instantiate a cube GameObject

namespace Engine
{
    public class Cube : GameObject, IRenderable
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

            collider = new Collider(this);
            collider.SetVertices(mesh.GetMeshVertices());

            renderData = new Renderable(mesh.GetMeshVertices(), _indices, this);

            renderData.SetTexture(TextureManager.GetTexture("border"), 0);
            renderData.SetTexture(TextureManager.GetTexture("dougFace"), 1);

            //renderData.SetTexture(TextureManager.GetTexture("dougFace"), 0);
            //renderData.SetTexture(TextureManager.GetTexture("border"), 1);
        }

        public Shader GetShader()
        {
            return renderData.shader;
        }
    }
}