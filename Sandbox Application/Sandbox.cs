using Engine;
using System.Runtime.ConstrainedExecution;
using static OpenGL.GL;
using Maths = Engine.Maths;

// We can now move around objects. However, how can we move our "camera", or modify our perspective?
// In this tutorial, I'll show you how to setup a full projection/view/model (PVM) matrix.
// In addition, we'll make the rectangle rotate over time.
public class Sandbox : EngineMain
{
    Camera camera;
    CubeTest2 cube;

    // We create a double to hold how long has passed since the program was opened.
    private float _time;

    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        cube = new CubeTest2();
        camera = new Camera();
    }

    public override void Update()
    {

    }

    public unsafe override void Draw()
    {
        // We add the time elapsed since last frame, times 4.0 to speed up animation, to the total amount of time passed.
        _time += 0.05f * Time.deltaTime;

        //cube.transform.position = new Vector3(_time * 1.00001, _time * 1.00001, _time * 1.00001);

        cube.transform.SetPosition(new Vector3(_time, _time, _time));

        // We clear the depth buffer in addition to the color buffer.
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        cube.Draw();

        //SwapBuffers();
    }
}