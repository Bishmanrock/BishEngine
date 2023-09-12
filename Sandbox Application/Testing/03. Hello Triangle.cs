using System;
using System.Numerics;
using Engine;
using static OpenGL.GL;

public class HelloTriangle : EngineMain
{
    Shader shader;

    public override void Initialize()
    {
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        Shader shader = new Shader(
     "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\ShaderOLD.vert",
     "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\ShaderOLD.frag");

        //shader.Load();
        shader.Use();

        var vertices = new[] {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };

        uint vao = glGenVertexArray();
        uint vbo = glGenBuffer();

        // Bind vertex array object
        glBindVertexArray(vao);

        // Copy our vertices array in a buffer for OpenGL to use
        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        fixed (float* v = &vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
        }

        // And then set the vertex attributors pointers
        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(float) * 3, NULL);
        glEnableVertexAttribArray(0);

        glClear(GL_COLOR_BUFFER_BIT);

        shader.SetColor(1.0f, 0.5f, 0.2f, 1.0f);

       // Vector4 shaderColor = new Vector4(1.0f, 0.5f, 0.2f, 1.0f);

        //int shaderLocation = glGetUniformLocation(shader.ProgramID, "color");

       // glUniform4f(shaderLocation, 1.0f, 0.5f, 0.2f, 1.0f);

        glBindVertexArray(vao);
    }

    public override void Update()
    {
    }

    public override void Draw()
    {
        DrawTriangle();
    }

    private unsafe void DrawTriangle()
    {
        glDrawArrays(GL_TRIANGLES, 0, 3);
    }
}