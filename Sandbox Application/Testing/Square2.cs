using System;
using Engine;
using GLFW;
using static OpenGL.GL;
using StbImageSharp;
using System.Runtime.InteropServices;
using OpenGL;
using System.Numerics;

public class Square2 : EngineMain
{
    private Shader shader;

    /// <summary>
    /// Creates an extremely basic shader program that is capable of displaying a triangle on screen.
    /// </summary>
    /// <returns>The created shader program. No error checking is performed for this basic example.</returns>
    private void CreateShaderProgram()
    {
        shader = new Shader(
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\MonoBehaviour\\Shaders\\Square2.vert",
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\MonoBehaviour\\Shaders\\Square2.frag");

        //shader.Load();

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

        float[] texCoords = new float[]
        {
    0.0f, 0.0f,  // lower-left corner  
    1.0f, 0.0f,  // lower-right corner
    0.5f, 1.0f   // top-center corner
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

        // Load and create texture
        uint texture;
        texture = glGenTexture();

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

        int width;
        int height;
        int nrChannels;

        string path = "F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\Untitled.png";

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

        glTexImage2D(
            GL_TEXTURE_2D,
            0,
            GL_RGB,
            64,
            64,
            0,
            GL_RGB,
            GL_UNSIGNED_BYTE,
            Texture.ConvertToIntPtr(image.Data));

        glGenerateMipmap(GL_TEXTURE_2D);

        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, shader.Handle);

        glBindVertexArray(vao);

        int myUniformLocation = glGetUniformLocation(shader.Handle, "aTexCoord");





    }

    public override void Initialize()
    {
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        var vertices = new[]
        {
    // positions          // colors           // texture coords
     0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f,   // top right
     0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f,   // bottom right
    -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f,   // bottom left
    -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f    // top left 
};

        uint[] indices = new uint[]
        {
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
        };

        uint vao = glGenVertexArray();
        glBindVertexArray(vao);

        uint vbo = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        fixed (float* v = &vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
        }

        uint ebo = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);

        fixed (uint* v = &indices[0])
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(float) * indices.Length, v, GL_STATIC_DRAW);
        }

        CreateShaderProgram();

        var vertexLocation = (uint)shader.GetAttribLocation("position");
        glEnableVertexAttribArray(vertexLocation);
        glVertexAttribPointer(vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

        var texCoordLocation = (uint)shader.GetAttribLocation("aTexCoord");
        glEnableVertexAttribArray(texCoordLocation);
        glVertexAttribPointer(texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));



        // texture load from file

        uint handle = glGenTexture();

        glBindTexture(GL_TEXTURE_2D, handle);

        string path = "F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\Untitled.png";

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

        glTexImage2D(
            GL_TEXTURE_2D,
            0,
            GL_RGB,
            64,
            64,
            0,
            GL_RGB,
            GL_UNSIGNED_BYTE,
            Texture.ConvertToIntPtr(image.Data));

        glGenerateMipmap(GL_TEXTURE_2D);

        glVertexAttribPointer(2, 2, GL_FLOAT, false, 8 * sizeof(float), (void*)(6 * sizeof(float)));
        glEnableVertexAttribArray(2);
    }

    public override void Update()
    {

    }

    public override void Draw()
    {
        glClear(GL_COLOR_BUFFER_BIT); // Clear the framebuffer to defined background color
        DrawSquare();
    }

    private unsafe void DrawSquare()
    {
        glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, NULL); // Draw the square
    }
}