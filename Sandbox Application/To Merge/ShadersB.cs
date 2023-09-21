using System;
using Engine;
using GLFW;
// glfw3;
using static OpenGL.GL;

public class ShadersB : EngineMain
{
    uint vao;
    uint vbo;

    Shader shader;

    private void CreateProgram()
    {
        string vertexShader = @"#version 330 core
                              layout (location = 0) in vec3 aPos;
                              layout (location = 1) in vec3 aColor;

                              out vec3 ourColor;

                              void main()
                              {
                                    gl_Position = vec4(aPos, 1.0);
                                    ourColor = aColor;
                              }";

        string fragmentShader = @"#version 330 core
                                out vec4 FragColor;
                                in vec3 ourColor;

                                void main()
                                {
                                        FragColor = vec4(ourColor, 1.0f);
                                } ";

        shader = new Shader(vertexShader, fragmentShader);
        //shader.Load();

        shader.Use();
    }

    public override void Initialize()
    {
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        // Create shader program
        CreateProgram();

        var vertices = new[] {
    // positions         // colors
     0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 1.0f,   // bottom right
    -0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 1.0f,   // bottom left
     0.0f,  0.5f, 0.0f,  1.0f, 1.0f, 1.0f    // top 
        };

        vao = glGenVertexArray();
        vbo = glGenBuffer();

        glBindVertexArray(vao);

        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        fixed (float* v = &vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
        }

        glVertexAttribPointer(0, 3, GL_FLOAT, false, 6 * sizeof(float), NULL);
        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 3, GL_FLOAT, false, 6 * sizeof(float), NULL);
        glEnableVertexAttribArray(1);

        glUseProgram(shader.Handle);
    }

    public override void Update()
    {

    }

    public override void Draw()
    {
        glClear(GL_COLOR_BUFFER_BIT);

        glBindVertexArray(vao);
        glDrawArrays(GL_TRIANGLES, 0, 3);
    }
}