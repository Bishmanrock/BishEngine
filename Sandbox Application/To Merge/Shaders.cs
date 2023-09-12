using System;
using System.Numerics;
using Engine;
using GLFW;
// glfw3;
using static OpenGL.GL;

public class Shaders : EngineMain
{
    private long n;
    private static Random rand;

    private int location;

    uint vao;
    uint vbo;

    Shader shader;

    private static void SetRandomColor(int location)
    {
        var r = (float)rand.NextDouble();
        var g = (float)rand.NextDouble();
        var b = (float)rand.NextDouble();
        glUniform3f(location, r, g, b);
    }

    /// <summary>
    /// Creates an extremely basic shader program that is capable of displaying a triangle on screen.
    /// </summary>
    /// <returns>The created shader program. No error checking is performed for this basic example.</returns>
    private void CreateProgram()
    {
        string vertexShader = @"#version 330 core
                              layout (location = 0) in vec3 pos;

                              //out vec4 vertexColour;

                              void main()
                              {
                                    gl_Position = vec4(pos, 1.0);
                                   // vertexColor = vec4(0.5f, 0.0f, 0.0f, 1.0f);
                              }";

        string fragmentShader = @"#version 330 core
                                out vec4 result;

                                uniform vec4 vertexColor;

                                void main()
                                {
                                        result = vertexColor;
                                } ";

        shader = new Shader(vertexShader, fragmentShader);
        //shader.Load();

        shader.Use();
    }

    /// <summary>
    /// Creates a VBO and VAO to store the vertices for a triangle.
    /// </summary>
    /// <param name="vao">The created vertex array object for the triangle.</param>
    /// <param name="vbo">The created vertex buffer object for the triangle.</param>
    private static unsafe void CreateVertices(out uint vao, out uint vbo)
    {
        var vertices = new[] {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };

        vao = glGenVertexArray();
        vbo = glGenBuffer();

        // Bind vertex array object
        glBindVertexArray(vao);

        // Copy our vertices array in a buffer for OpenGL to use
        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        fixed (float* v = &vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
        }

        // And then set the vertex attributors pointers
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);
        glEnableVertexAttribArray(0);

        glBindVertexArray(vao);
    }

    public override void Initialize()
    {
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public override void LoadContent()
    {
        // Create shader program
        CreateProgram();

        // Define a simple triangle
        CreateVertices(out var vao, out var vbo);
        rand = new Random();

        //location = glGetUniformLocation(program, "color");
        //SetRandomColor(location);


        n = 0;
    }

    public override void Update()
    {

    }

    private float green = 0;

    public override void Draw()
    {
        glClear(GL_COLOR_BUFFER_BIT);

        glUseProgram(shader.Handle);

        green = green + 0.001f;

        int vertexColorLocation = glGetUniformLocation(shader.Handle, "vertexColor");
        glUniform4f(vertexColorLocation, 0.0f, green, 0.0f, 1.0f);

        // Draw the triangle.
        glDrawArrays(GL_TRIANGLES, 0, 3);
    }
}