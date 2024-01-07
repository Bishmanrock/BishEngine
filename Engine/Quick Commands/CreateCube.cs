using static OpenGL.GL;

namespace Engine
{
    class CreateCube
    {
        Shader shader;

        public CreateCube()
        {
            CreateShaderProgram();
            CreateVertices(); // Define a simple square
        }

        private void CreateShaderProgram()
        {
            shader = new Shader(ShaderType.STANDARD);
            shader.Use();
        }

        /// <summary>
        /// Creates a VBO and VAO to store the vertices for a triangle.
        /// </summary>
        /// <param name="vao">The created vertex array object for the triangle.</param>
        /// <param name="vbo">The created vertex buffer object for the triangle.</param>
        private unsafe void CreateVertices()
        {
            var vertices = new[] {
     0.5f,  0.5f, 0.0f,  // top right
     0.5f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left 
        };

            uint[] indices = new uint[]
            {
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };

            uint vao = glGenVertexArray();
            uint vbo = glGenBuffer();
            uint ebo = glGenBuffer();

            glBindVertexArray(vao); // Bind vertex array object

            glBindBuffer(GL_ARRAY_BUFFER, vbo); // Copy our vertices array in a buffer for OpenGL to use

            fixed (float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }

            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);

            fixed (uint* v = &indices[0])
            {
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(float) * indices.Length, v, GL_STATIC_DRAW);
            }

            // And then set the vertex attributors pointers
            glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);
            glEnableVertexAttribArray(0);

            shader.SetColor(1.0f, 0.5f, 0.2f, 1.0f);
        }

        public unsafe void Draw()
        {
            glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, NULL); // Draw the square
        }
    }
}
