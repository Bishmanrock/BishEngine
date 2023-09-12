using System;
using System.Numerics;
using System.Reflection.Metadata;
using Engine;
using GLFW;
using static OpenGL.GL;

public class TexturedSquare : EngineMain
{
    // Because we're adding a texture, we modify the vertex array to include texture coordinates.
        // Texture coordinates range from 0.0 to 1.0, with (0.0, 0.0) representing the bottom left, and (1.0, 1.0) representing the top right.
        // The new layout is three floats to create a vertex, then two floats to create the coordinates.
        private readonly float[] _vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private uint _elementBufferObject;

        private uint _vertexBufferObject;

        private uint _vertexArrayObject;

        private Shader _shader;

    // For documentation on this, check Texture.cs.
    private Texture _texture;

    public override void Initialize()
    {
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
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

        // The shaders have been modified to include the texture coordinates, check them out after finishing the OnLoad function.
        _shader = new Shader(
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\OpenTK.vert",
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\OpenTK.frag");
        _shader.Use();

        // Because there's now 5 floats between the start of the first vertex and the start of the second,
        // we modify the stride from 3 * sizeof(float) to 5 * sizeof(float).
        // This will now pass the new vertex array to the buffer.
        var vertexLocation = _shader.GetAttribLocation("aPosition");
        glEnableVertexAttribArray((uint)vertexLocation);
        glVertexAttribPointer((uint)vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

        // Next, we also setup texture coordinates. It works in much the same way.
        // We add an offset of 3, since the texture coordinates comes after the position data.
        // We also change the amount of data to 2 because there's only 2 floats for texture coordinates.
        var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
        glEnableVertexAttribArray((uint)texCoordLocation);
        glVertexAttribPointer((uint)texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

        _texture = Texture.LoadFromFile("F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\Untitled.png");
        _texture.Use(GL_TEXTURE0);

        glActiveTexture(GL_TEXTURE0);

        _shader.SetInt("texture0", 0);
    }

    public override void Update()
    {

    }

    public unsafe override void Draw()
    {
        glBindVertexArray(_vertexArrayObject);

        _texture.Use(GL_TEXTURE0);
        _shader.Use();

        glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        //SwapBuffers();
    }
}