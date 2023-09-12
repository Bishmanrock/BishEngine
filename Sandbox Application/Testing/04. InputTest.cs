using System;
using System.Numerics;
using Engine;
using GLFW;
using static OpenGL.GL;

public class InputTest : EngineMain
{
    private bool isPressed = false;
    Shader shader;
    Camera2D camera;

    uint vao;

    /// <summary>
    /// Creates a VBO and VAO to store the vertices for a triangle.
    /// </summary>
    /// <param name="vao">The created vertex array object for the triangle.</param>
    /// <param name="vbo">The created vertex buffer object for the triangle.</param>
    private unsafe void CreateVertices()
    {
        var vertices = new[] {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };

        vao = glGenVertexArray();
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
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);
        glEnableVertexAttribArray(0);
    }

    public override void Initialize()
    {
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public override void LoadContent()
    {
        CreateShaderProgram();

        CreateVertices(); // Define a simple triangle
        CreateCamera();

        //location = glGetUniformLocation(program, "color");
        //SetRandomColor(location);
    }

    /// <summary>
    /// Creates an extremely basic shader program that is capable of displaying a triangle on screen.
    /// </summary>
    /// <returns>The created shader program. No error checking is performed for this basic example.</returns>
    private void CreateShaderProgram()
    {
        shader = new Shader(
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\hellosquare.vert",
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\hellosquare.frag");

        //shader.Load();
        shader.Use();

    }

    public override void Update()
    {
        if (Glfw.GetKey(Engine.Window.window, Keys.Space) == InputState.Press)
        {
            isPressed = true;
            Console.Write("Pressed");
        }
        else
        {
            isPressed = false;
            Console.WriteLine("No press");
        }
    }

    public override void Draw()
    {
        DrawTriangle();
    }

    private unsafe void DrawTriangle()
    {
        Vector2 position = new Vector2(400, 300);
        Vector2 scale = new Vector2(150, 100);
        float rotation = MathF.Sin(Time.totalElapsedSeconds) * MathF.PI * 2f;

        Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
        Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
        Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);

        shader.SetMatrix4("model", sca * rot * trans);

        shader.Use();
        shader.SetMatrix4("projection", camera.GetProjectionMatrix());

        glBindVertexArray(vao);

        if (isPressed == true)
        {
            Engine.Window.SetBackgroundColour(new Vector4(1f, 0f, 0f, 1f));
        }
        else
        {
            Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
        }

        glDrawArrays(GL_TRIANGLES, 0, 3);

        glBindVertexArray(0);
    }

    private void CreateCamera()
    {

        camera = new Camera2D(Engine.Window.windowSize / 2f, 2.5f);
    }
}