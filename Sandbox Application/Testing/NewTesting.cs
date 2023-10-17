using Engine;
using static OpenGL.GL;

public class NewTesting : EngineMain
{
    Cube cube;
    Cube cube2;

    private bool _firstMove = true;

    private Vector2 _lastPos;

    private CameraFPS _camera;

    private Mouse mouse; // Mouse input

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
        mouse = new Mouse();
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        // We enable depth testing here. If you try to draw something more complex than one plane without this,
        // you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
        // Obviously, we don't want this, so we enable depth testing. We also clear the depth buffer in GL.Clear over in OnRenderFrame.
        glEnable(GL_DEPTH_TEST);

        cube = new Cube();
        cube.SetPosition(new Vector3(0, 0, 0));

        cube2 = new Cube();
        cube2.SetPosition(new Vector3(2, 2, 2));

        // We initialize the camera so that it is 3 units back from where the rectangle is.
        // We also give it the proper aspect ratio.
        _camera = new CameraFPS();
        _camera.SetUp(Vector3.UnitZ * 3, Window.windowSize.x / (float)Window.windowSize.y);

        // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
        //CursorState = CursorState.Grabbed;

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
    }

    public override void Update()
    {
        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;

        if (Input.GetKeyDown(Actions.Up))
        {
            _camera.Position += _camera.Front * cameraSpeed * Time.deltaTime; // Forward
            Console.Write("W pressed");
/*            cube.SetPosition(new Vector3(cube.transform.position.x + 1, cube.transform.position.y, cube.transform.position.z));

            cube.SetRotation(new Vector3(cube.transform.rotation.x + 1, cube.transform.rotation.y, cube.transform.rotation.z));

            cube.SetScale(new Vector3(cube.transform.scale.x + 1, cube.transform.scale.y, cube.transform.scale.z));*/
        }
        if (Input.GetKeyDown(Actions.Down))
        {
            _camera.Position -= _camera.Front * cameraSpeed * Time.deltaTime; // Backwards
            Console.Write("S pressed");

        }
        if (Input.GetKeyDown(Actions.Left))
        {
            _camera.Position -= _camera.Right * cameraSpeed * Time.deltaTime; // Left
            Console.Write("A pressed");
        }
        if (Input.GetKeyDown(Actions.Right))
        {
            _camera.Position += _camera.Right * cameraSpeed * Time.deltaTime; // Right
            Console.Write("D pressed");
        }
        if (Input.GetKeyDown(Actions.Action))
        {
            _camera.Position += _camera.Up * cameraSpeed * Time.deltaTime; // Up
            Console.Write("Space pressed");
        }
        //if (Glfw.GetKey(Engine.Window.window, Keys.LeftShift) == InputState.Press)
        //{
        //    _camera.Position -= _camera.Up * cameraSpeed * Time.deltaTime; // Down
        //    Console.Write("Left shift pressed");
        //}

        // Get the mouse state
        //var mouse = Mouse;

        if (_firstMove) // This bool variable is initially set to true.
        {
            _lastPos = new Vector2(mouse.X, mouse.Y);
            _firstMove = false;
        }
        else
        {
            // Calculate the offset of the mouse position
            var deltaX = mouse.X - _lastPos.x;
            var deltaY = mouse.Y - _lastPos.y;
            _lastPos = new Vector2(mouse.X, mouse.Y);

            // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
            _camera.Yaw += deltaX * sensitivity;
            _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
        }
    }

    public unsafe override void Draw()
    {
        // We add the time elapsed since last frame, times 4.0 to speed up animation, to the total amount of time passed.
        _time += 2.5f * Time.deltaTime;

        // Finally, we have the model matrix. This determines the position of the model.
        var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX((float)Maths.DegreesToRadians(_time));

        //var model = Matrix4x4.Identity;

        // Then, we pass all of these matrices to the vertex shader.
        // You could also multiply them here and then pass, which is faster, but having the separate matrices available is used for some advanced effects.

        // IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
        // They are then transposed properly when passed to the shader. 
        // This means that we retain the same multiplication order in both OpenTK c# code and GLSL shader code.
        // If you pass the individual matrices to the shader and multiply there, you have to do in the order "model * view * projection".
        // You can think like this: first apply the modelToWorld (aka model) matrix, then apply the worldToView (aka view) matrix, 
        // and finally apply the viewToProjectedSpace (aka projection) matrix.
        cube.GetShader().SetMatrix4("model", model);
        cube.GetShader().SetMatrix4("view", _camera.GetViewMatrix());
        cube.GetShader().SetMatrix4("projection", _camera.GetProjectionMatrix());

        cube.DrawCube();

         //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        //SwapBuffers();
    }
}