using Engine;
using static OpenGL.GL;
using Maths = Engine.Maths;

// We can now move around objects. However, how can we move our "camera", or modify our perspective?
// In this tutorial, I'll show you how to setup a full projection/view/model (PVM) matrix.
// In addition, we'll make the rectangle rotate over time.
public class CubeTest2 : EngineMain
{
    Cube cube;

    Cube cube2;

    // We create a double to hold how long has passed since the program was opened.
    private float _time;

    // Then, we create two matrices to hold our view and projection. They're initialized at the bottom of OnLoad.
    // The view matrix is what you might consider the "camera". It represents the current viewport in the window.
    private Matrix4x4 _view;

    // This represents how the vertices will be projected. It's hard to explain through comments,
    // so check out the web version for a good demonstration of what this does.
    private Matrix4x4 _projection;


    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        // We enable depth testing here. If you try to draw something more complex than one plane without this,
        // you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
        // Obviously, we don't want this, so we enable depth testing. We also clear the depth buffer in GL.Clear over in OnRenderFrame.
        glEnable(GL_DEPTH_TEST);

        cube = new Cube();
        cube2 = new Cube();

        // For the view, we don't do too much here. Next tutorial will be all about a Camera class that will make it much easier to manipulate the view.
        // For now, we move it backwards three units on the Z axis.
        _view = Matrix4x4.CreateTranslation(0.0f, 0.0f, -3.0f);

        // For the matrix, we use a few parameters.
        //   Field of view. This determines how much the viewport can see at once. 45 is considered the most "realistic" setting, but most video games nowadays use 90
        //   Aspect ratio. This should be set to Width / Height.
        //   Near-clipping. Any vertices closer to the camera than this value will be clipped.
        //   Far-clipping. Any vertices farther away from the camera than this value will be clipped.
        //_projection = Matrix4x4.CreatePerspectiveFieldOfView(Engine.Math.DegreesToRadians(45), DisplayManager.windowSize.X / DisplayManager.windowSize.Y, 0.1f, 100.0f);

        _projection = Matrix4x4.CreatePerspectiveFieldOfView(Maths.DegreesToRadians(45f), Window.windowSize.x / Window.windowSize.y, 0.1f, 100.0f);



        // Now, head over to OnRenderFrame to see how we setup the model matrix.
    }

    public override void Update()
    {
        if (Input.GetKey(Actions.Down))
        {
            Console.Write("test");
        }
    }

    public unsafe override void Draw()
    {
        // We add the time elapsed since last frame, times 4.0 to speed up animation, to the total amount of time passed.
        _time += 0.5f * Time.deltaTime;

        // We clear the depth buffer in addition to the color buffer.
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        // Finally, we have the model matrix. This determines the position of the model.
        //var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX((float)Engine.Math.DegreesToRadians(_time));

        var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(_time) * Matrix4x4.CreateRotationY(_time);

        var model2 = Matrix4x4.Identity;

        // Then, we pass all of these matrices to the vertex shader.
        // You could also multiply them here and then pass, which is faster, but having the separate matrices available is used for some advanced effects.

        // IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
        // They are then transposed properly when passed to the shader. 
        // This means that we retain the same multiplication order in both OpenTK c# code and GLSL shader code.
        // If you pass the individual matrices to the shader and multiply there, you have to do in the order "model * view * projection".
        // You can think like this: first apply the modelToWorld (aka model) matrix, then apply the worldToView (aka view) matrix, 
        // and finally apply the viewToProjectedSpace (aka projection) matrix.
        //cube.GetShader().SetMatrix4("model", model);
        //cube.GetShader().SetMatrix4("view", _view);
        //cube.GetShader().SetMatrix4("projection", _projection);

        cube.SetRotation(new Vector3 (_time, _time, 0));

        cube.DrawCubeTest();

        cube2.DrawCubeTest();

        //cube2.GetShader().SetMatrix4("model", model2);
        //cube2.GetShader().SetMatrix4("view", _view);
        //cube2.GetShader().SetMatrix4("projection", _projection);

        //cube2.DrawCube();

        //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);



        //SwapBuffers();
    }
}