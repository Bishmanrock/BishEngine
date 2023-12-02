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

            renderData = new Renderable(mesh.GetMeshVertices(), _indices, TextureManager.GetTexture("border"), TextureManager.GetTexture("dougFace"), this);





            //renderData = new Renderable(mesh.GetMeshVertices(), _indices, "F:\\GameDev\\.Engine\\Engine\\Sandbox Application\\Texture\\Untitled.png", "F:\\GameDev\\.Engine\\Engine\\Sandbox Application\\Texture\\awesomeface.png", this);       
        }

        public Shader GetShader()
        {
            return renderData.shader;
        }
    }
}