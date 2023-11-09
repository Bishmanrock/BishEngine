using System.Reflection;
using static OpenGL.GL;

namespace Engine
{
    public class Cube : GameObject
    {
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

        private Shader shader;

        private uint _vertexBufferObject;
        private uint _vertexArrayObject;

        private Texture _texture;
        private Texture _texture2;

        private uint _elementBufferObject;

        private float testTime; // Temporary value, allows external commands to set movements for Draw. Needs a better way to control.

        public unsafe Cube()
        {
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

            // shader.vert has been modified. Take a look at it after the explanation in OnRenderFrame.
            shader = new Shader(ShaderType.TEMP_TRANSFORMATION);
            shader.Use();

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

        public void SetTime(float time)
        {
            testTime = time;
        }


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

        public void SetPosition(Vector3 newPosition)
        {
            //transform.position = newPosition;
            //transform.position = Matrix4x4.Identity * Matrix4x4.CreateTranslation(newPosition.x, newPosition.y, newPosition.z);

            //_time += 0.5f * Time.deltaTime;

            //var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(_time) * Matrix4x4.CreateRotationY(_time);

            //shader.SetMatrix4("model", model);
            //shader.SetMatrix4("view", CameraManager.GetActiveCamera().GetViewMatrix());
            //shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        }

        public void SetRotation(Vector3 newPosition)
        {
            transform.rotation = newPosition;
            //position = Matrix4x4.Identity * Matrix4x4.CreateTranslation(newPosition);

            //_time += 0.5f * Time.deltaTime;

            //var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(_time) * Matrix4x4.CreateRotationY(_time);

            //shader.SetMatrix4("model", model);
            //shader.SetMatrix4("view", CameraManager.GetActiveCamera().GetViewMatrix());
            //shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        }

        public void SetScale(Vector3 newPosition)
        {
            transform.scale = newPosition;
            //position = Matrix4x4.Identity * Matrix4x4.CreateTranslation(newPosition);

            //_time += 0.5f * Time.deltaTime;

            //var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(_time) * Matrix4x4.CreateRotationY(_time);

            //shader.SetMatrix4("model", model);
            //shader.SetMatrix4("view", CameraManager.GetActiveCamera().GetViewMatrix());
            //shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        }

        public Shader GetShader()
        {
            return shader;
        }

        //public void DrawCube() // REPLACED BY DRAW
        //{
        //    glBindVertexArray(_vertexArrayObject);
        //    glDrawArrays(GL_TRIANGLES, 0, 36);
        //}

        public unsafe void Draw()
        {
            // Finally, we have the model matrix. This determines the position of the model.
            //var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX((float)Engine.Math.DegreesToRadians(_time));

            var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(testTime) * Matrix4x4.CreateRotationY(testTime);

            // Then, we pass all of these matrices to the vertex shader.
            // You could also multiply them here and then pass, which is faster, but having the separate matrices available is used for some advanced effects.

            // IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
            // They are then transposed properly when passed to the shader. 
            // This means that we retain the same multiplication order in both OpenTK c# code and GLSL shader code.
            // If you pass the individual matrices to the shader and multiply there, you have to do in the order "model * view * projection".
            // You can think like this: first apply the modelToWorld (aka model) matrix, then apply the worldToView (aka view) matrix, 
            // and finally apply the viewToProjectedSpace (aka projection) matrix.
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", CameraManager.activeCamera.GetView());
            shader.SetMatrix4("projection", CameraManager.activeCamera.GetProjection());

            glBindVertexArray(_vertexArrayObject);

            glDrawArrays(GL_TRIANGLES, 0, 36);
        }

        public void DrawCubeTest()
        {
            GetShader().SetMatrix4("model", Matrix4x4.Identity * Matrix4x4.CreateRotationX(transform.rotation.x) * Matrix4x4.CreateRotationY(transform.rotation.y));

            GetShader().SetMatrix4("view", Matrix4x4.CreateTranslation(0.0f, 0.0f, -3.0f));

            GetShader().SetMatrix4("projection", Matrix4x4.CreatePerspectiveFieldOfView(Maths.DegreesToRadians(45f), Window.windowSize.x / Window.windowSize.y, 0.1f, 100.0f));

            glBindVertexArray(_vertexArrayObject);
            glDrawArrays(GL_TRIANGLES, 0, 36);
        }
    }
}