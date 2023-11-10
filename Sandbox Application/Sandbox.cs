using Engine;
using static OpenGL.GL;

public class Sandbox : EngineMain
{
    Camera camera;
    Cube cube;
    Cube cube2;

    // We create a double to hold how long has passed since the program was opened.
    private float _time;

    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        cube = new Cube();
        cube2 = new Cube();
        camera = new Camera();
    }

    public override void Update()
    {

    }

    public unsafe override void Draw()
    {
        // We add the time elapsed since last frame, times 4.0 to speed up animation, to the total amount of time passed.
        _time += 0.05f * Time.deltaTime;

        cube.transform.SetPosition(new Vector3(_time, _time, _time));
        cube.transform.SetRotation(new Vector3(_time, _time, _time));
        //cube.transform.SetScale(new Vector3(_time, _time, _time));


        // We clear the depth buffer in addition to the color buffer.
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        cube.Draw();
        cube2.Draw();

        //SwapBuffers();
    }
}