using Engine;
using static OpenGL.GL;
using Timer = Engine.Timer;

public class TexturedSquareOld : EngineMain
{
    Shader2 shader;

    private Engine.Timer _timer;

    private uint _vertexBufferObject;

    private uint _vertexArrayObject;

    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };

        _vertexBufferObject = glGenBuffer();

        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);

        fixed (float* v = &_vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * _vertices.Length, v, GL_STATIC_DRAW);
        }

        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);

        glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);
        glEnableVertexAttribArray(0);

        glGetInteger(GL_MAX_VERTEX_ATTRIBS);
        //Debug.WriteLine($"Maximum number of vertex attributes supported: {maxAttributeCount}");

        shader = new Shader2(
    "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\MonoBehaviour\\Shaders\\texturedsquare.vert",
    "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\MonoBehaviour\\Shaders\\texturedsquare.frag");

        shader.Use();

        _timer = new Timer();
        _timer.Start();
    }

    public override void Update()
    {

    }

    public override void Draw()
    {
        glClear(GL_COLOR_BUFFER_BIT); // Clear the framebuffer to defined background color

        shader.Use();

        double timeValue = Time.totalElapsedSeconds;

        float greenValue = (float)Maths.Sin(timeValue) / 2.0f + 0.5f;

        int vertexColorLocation = glGetUniformLocation(shader.Handle, "ourColor");

        glUniform4f(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

        glBindVertexArray(_vertexArrayObject);

        glDrawArrays(GL_TRIANGLES, 0, 3);
    }
}