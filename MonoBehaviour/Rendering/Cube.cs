using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.GL;

namespace Engine
{
    public class Cube
    {
        Transform transform;

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

        private System.Numerics.Matrix4x4 position;

        private Shader shader;

        private float _time;

        private uint _vertexBufferObject;

        private uint _vertexArrayObject;

        private Texture _texture;

        private Texture _texture2;

        private uint _elementBufferObject;

        public unsafe Cube()
        {
            shader = new Shader(
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\Transformation.vert",
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\Transformation.frag");
            shader.Use();

            _vertexArrayObject = glGenVertexArray();
            glBindVertexArray(_vertexArrayObject);

            _vertexBufferObject = glGenBuffer();
            glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);

            _elementBufferObject = glGenBuffer();
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _elementBufferObject);

            fixed (float* v = &_vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, _vertices.Length * sizeof(float), v, GL_STATIC_DRAW);
            }

            fixed (uint* i = &_indices[0])
            {
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indices.Length * sizeof(uint), i, GL_STATIC_DRAW);
            }

            var vertexLocation = shader.GetAttribLocation("aPosition");
            glEnableVertexAttribArray((uint)vertexLocation);
            glVertexAttribPointer((uint)vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            glEnableVertexAttribArray((uint)texCoordLocation);
            glVertexAttribPointer((uint)texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

            _texture = Texture.LoadFromFile("F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\Untitled.png");
            _texture2 = Texture.LoadFromFile("F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\awesomeface.png");

            _texture.Use(GL_TEXTURE0);
            _texture2.Use(GL_TEXTURE1);

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            shader.Use();
        }

        public void SetPosition(Vector3 newPosition)
        {
            //position = Matrix4x4.CreateTranslation(newPosition);

            _time += 0.5f * Time.deltaTime;

            var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(_time) * Matrix4x4.CreateRotationY(_time);

            shader.SetMatrix4("model", model);
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
    }
}