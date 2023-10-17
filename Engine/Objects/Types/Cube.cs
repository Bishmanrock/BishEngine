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

        public unsafe Cube()
        {
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

            shader = new Shader(
                "F:\\GameDev\\Engine\\Engine\\Rendering\\Shaders\\Standard.vert",
                "F:\\GameDev\\Engine\\Engine\\Rendering\\Shaders\\Transformation.frag");
            shader.Use();

            var vertexLocation = shader.GetAttribLocation("aPosition");
            glEnableVertexAttribArray((uint)vertexLocation);
            glVertexAttribPointer((uint)vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            glEnableVertexAttribArray((uint)texCoordLocation);
            glVertexAttribPointer((uint)texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

            _texture = Texture.LoadFromFile("F:\\GameDev\\Engine\\Sandbox Application\\Texture\\Untitled.png");
            _texture2 = Texture.LoadFromFile("F:\\GameDev\\Engine\\Sandbox Application\\Texture\\awesomeface.png");

            _texture.Use(GL_TEXTURE0);
            _texture2.Use(GL_TEXTURE1);

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            shader.Use();
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

        public void DrawCube()
        {
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