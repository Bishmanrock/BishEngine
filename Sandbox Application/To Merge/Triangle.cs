using Engine;
using static OpenGL.GL;

public class Triangle : EngineMain
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
        shader = new Shader("F:\\GameDev\\MonoBehaviour - Merge Into\\MonoBehaviour\\MonoBehaviour\\Shaders\\hellotriangle.vert", "F:\\GameDev\\MonoBehaviour - Merge Into\\MonoBehaviour\\MonoBehaviour\\Shaders\\hellotrianglealt.frag");
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

    public override void Draw()
    {
        // Clear the framebuffer to defined background color
        glClear(GL_COLOR_BUFFER_BIT);

        if (n++ % 60 == 0)
        {
            SetRandomColor(location);
        }

        // Draw the triangle.
        glDrawArrays(GL_TRIANGLES, 0, 3);
    }
}