using static OpenGL.GL;

// Class to instantly instantiate a cube GameObject

namespace Engine
{
    public class Quad : IRenderable
    {
        public Renderable renderData { get; }
        public Transform transform { get; set; }
        public bool isActive { get; set; }

        Mesh mesh;

        // Why are so many vertcies needed for a cube??
        private readonly float[] _vertices =
    {
     0.5f,  0.5f, 0.0f,  // top right
     0.5f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left 
            };

        private readonly uint[] _indices =
        {
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
        };

        public unsafe Quad()
        {
            transform = new Transform();

            renderData = new Renderable(_vertices, _indices);

            renderData.SetTexture("border", 0);
            renderData.SetTexture("dougFace", 1);

            //renderData.SetTexture("font", 0);
        }

        public Shader GetShader()
        {
            return renderData.shader;
        }
    }
}