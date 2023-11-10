using System.Reflection;
using static OpenGL.GL;

// Class to instantly instantiate a cube game object

namespace Engine
{
    public class CubeTest2 : GameObject, IRenderable
    {

        public Renderable renderData { get; }

        private readonly float[] _vertices =
    {
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private uint _vertexBufferObject;
        private uint _vertexArrayObject;

        private Texture _texture;
        private Texture _texture2;

        Shader shader;

        private uint _elementBufferObject;

        public float testTime; // Temporary value, allows external commands to set movements for Draw. Needs a better way to control.

        public unsafe CubeTest2()
        {
            transform.SetScale(new Vector3(1, 1, 1));

            // We enable depth testing here. If you try to draw something more complex than one plane without this,
            // you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
            // Obviously, we don't want this, so we enable depth testing. We also clear the depth buffer in GL.Clear over in OnRenderFrame.
            glEnable(GL_DEPTH_TEST);

            _vertexArrayObject = glGenVertexArray();
            glBindVertexArray(_vertexArrayObject);

            _vertexBufferObject = glGenBuffer();
            glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);

            fixed (float* v = &_vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, _vertices.Length * sizeof(float), v, GL_STATIC_DRAW);
            }

            _elementBufferObject = glGenBuffer();
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _elementBufferObject);

            fixed (uint* i = &_indices[0])
            {
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indices.Length * sizeof(uint), i, GL_STATIC_DRAW);
            }

            bool testing = false;

            if (testing == false)
            {
                shader = new Shader(ShaderType.TEMP_TRANSFORMATION);
                shader.Use();
            }
            else if (testing == true)
            {
                renderData.shader = new Shader(ShaderType.TEMP_TRANSFORMATION);
                renderData.shader.Use();
            }



            var vertexLocation = shader.GetAttribLocation("aPosition");
            glEnableVertexAttribArray((uint)vertexLocation);
            glVertexAttribPointer((uint)vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            glEnableVertexAttribArray((uint)texCoordLocation);
            glVertexAttribPointer((uint)texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

            _texture = Texture.LoadFromFile("F:\\GameDev\\.Engine\\Games\\OpenGL Tutorials\\01. Hello Window\\Hello Window\\Hello Window\\Graphics\\Untitled.png");
            _texture2 = Texture.LoadFromFile("F:\\GameDev\\.Engine\\Games\\OpenGL Tutorials\\01. Hello Window\\Hello Window\\Hello Window\\Graphics\\awesomeface.png");

            _texture.Use(GL_TEXTURE0);
            _texture2.Use(GL_TEXTURE1);

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            shader.Use();


        }

        public Shader GetShader()
        {
            return shader;
        }

        public unsafe void Draw()
        {
            // Then, we pass all of these matrices to the vertex shader.
            // You could also multiply them here and then pass, which is faster, but having the separate matrices available is used for some advanced effects.

            // IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
            // They are then transposed properly when passed to the shader. 
            // This means that we retain the same multiplication order in both OpenTK c# code and GLSL shader code.
            // If you pass the individual matrices to the shader and multiply there, you have to do in the order "model * view * projection".
            // You can think like this: first apply the modelToWorld (aka model) matrix, then apply the worldToView (aka view) matrix, 
            // and finally apply the viewToProjectedSpace (aka projection) matrix.

            // For some reason you need to set the scale as 1. I imagine this is because the gameobject is initialising at 0, 0, 0.


            //shader.SetMatrix4("model", model);
            shader.SetMatrix4("model", TransformToModel(transform));
            shader.SetMatrix4("view", CameraManager.activeCamera.GetView());
            shader.SetMatrix4("projection", CameraManager.activeCamera.GetProjection());

            glBindVertexArray(_vertexArrayObject);

            glDrawArrays(GL_TRIANGLES, 0, 36);
        }

        /// <summary>
        /// Creates the matrix which transforms vertex positions into world positions.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns>The transformation matrix.</returns>
        private Matrix4x4 TransformToModel(Transform transform)
        {
            return Matrix4x4.CreateRotationX(transform.rotation.x) *
                Matrix4x4.CreateRotationX(transform.rotation.y) *
                 Matrix4x4.CreateRotationX(transform.rotation.z) *
                Matrix4x4.CreateScale(transform.scale) *
                 Matrix4x4.CreateTranslation(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}