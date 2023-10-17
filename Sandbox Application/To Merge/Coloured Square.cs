using static OpenGL.GL;
using Engine;

internal class ColoredSquare : EngineMain
{
    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        CreateShaderProgram();
        CreateVertices();
    }

    private unsafe void CreateVertices()
    {
        var vertices = new[] {
                -0.5f, 0.5f, 1f, 0f, 0f, // top left
                0.5f, 0.5f, 0f, 1f, 0f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left

                0.5f, 0.5f, 0f, 1f, 0f,// top right
                0.5f, -0.5f, 0f, 1f, 1f, // bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left
            };


        uint vao = glGenVertexArray();
        uint vbo = glGenBuffer();

        glBindVertexArray(vao);

        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        fixed (float* v = &vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
        }

        glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);

        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));

        glEnableVertexAttribArray(1);

        glBindVertexArray(0);
    }

    public override void Update()
    {
    }

    public override void Draw()
    {
        glClear(GL_COLOR_BUFFER_BIT);
        DrawShape();

        //glBindVertexArray(vao);

        //glBindVertexArray(0);
    }

    private void CreateShaderProgram()
    {
        Shader shader = new Shader("F:\\GameDev\\MonoBehaviour - Merge Into\\MonoBehaviour\\MonoBehaviour\\Shaders\\colouredsquare.vert", "F:\\GameDev\\MonoBehaviour - Merge Into\\MonoBehaviour\\MonoBehaviour\\Shaders\\colouredsquare.frag");
        //shader.Load();
        shader.Use();
    }

    private void DrawShape()
    {
        glDrawArrays(GL_TRIANGLES, 0, 6);
    }
}