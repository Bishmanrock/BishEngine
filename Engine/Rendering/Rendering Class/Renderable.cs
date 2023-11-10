using static OpenGL.GL;

// Contains data for use with the IRenderable interface

namespace Engine
{
    public class Renderable
    {
        private readonly float[] vertices;
        private readonly uint[] indices;
        public Shader shader;
        public uint vertexBufferObject;
        private uint vertexArrayObject;
        private uint elementBufferObject;
        public Texture texture1;
        private Texture texture2;
    }
}