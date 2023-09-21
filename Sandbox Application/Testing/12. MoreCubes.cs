using Engine;
using GLFW;
using static OpenGL.GL;

// We can now move around objects. However, how can we move our "camera", or modify our perspective?
// In this tutorial, I'll show you how to setup a full projection/view/model (PVM) matrix.
// In addition, we'll make the rectangle rotate over time.
public class MoreCubes : EngineMain
{
    private bool _firstMove = true;

    private Vector2 _lastPos;

    private readonly float[] _vertices =
    {
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };

    private Vector3[] cubePositions =
        {
        new Vector3(0.0f,  0.0f,  0.0f),
        new Vector3( 2.0f,  5.0f, -15.0f),
    new Vector3(-1.5f, -2.2f, -2.5f),
    new Vector3(-3.8f, -2.0f, -12.3f),
    new Vector3( 2.4f, -0.4f, -3.5f),
    new Vector3(-1.7f,  3.0f, -7.5f),
    new Vector3( 1.3f, -2.0f, -2.5f),
    new Vector3( 1.5f,  2.0f, -2.5f),
    new Vector3( 1.5f,  0.2f, -1.5f),
    new Vector3(-1.3f,  1.0f, -1.5f)
    };

    private readonly uint[] _indices =
    {
            3, 1, 0,
            1, 2, 3
        };

    private CameraFPS _camera;
    private Camera2D _camera2D;

    private uint _elementBufferObject;

    private uint _vertexBufferObject;

    private uint _vertexArrayObject;

    private Shader _shader;

    private Texture _texture;

    private Texture _texture2;

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
        Engine.Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        // We enable depth testing here. If you try to draw something more complex than one plane without this,
        // you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
        // Obviously, we don't want this, so we enable depth testing. We also clear the depth buffer in GL.Clear over in OnRenderFrame.
        glEnable(GL_DEPTH_TEST);

        // Face culling
        //glEnable(GL_CULL_FACE); // Enables only rendering one side of the face
        //glCullFace(GL_FRONT); // Which face to keep
        //glFrontFace(GL_CCW); // Direction of incides (counter clockwise)
        //        

        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);

        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);

        fixed (float* v = &_vertices[0])
        {
            glBufferData(GL_ARRAY_BUFFER, _vertices.Length * sizeof(float), v, GL_STATIC_DRAW);
        }

        _elementBufferObject = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _elementBufferObject);

        fixed (uint* i = &_indices[0])
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indices.Length * sizeof(uint), i, GL_STATIC_DRAW);
        }

        // shader.vert has been modified. Take a look at it after the explanation in OnRenderFrame.
        _shader = new Shader(
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\Transformation.vert",
            "F:\\GameDev\\MonoBehaviour\\MonoBehaviour\\Rendering\\Shaders\\Transformation.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPosition");
        glEnableVertexAttribArray((uint)vertexLocation);
        glVertexAttribPointer((uint)vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

        var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
        glEnableVertexAttribArray((uint)texCoordLocation);
        glVertexAttribPointer((uint)texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

        _texture = Texture.LoadFromFile("F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\Untitled.png");
        _texture2 = Texture.LoadFromFile("F:\\GameDev\\MonoBehaviour\\Sandbox Application\\Texture\\awesomeface.png");

        _texture.Use(GL_TEXTURE0);
        _texture2.Use(GL_TEXTURE1);

        _shader.SetInt("texture0", 0);
        _shader.SetInt("texture1", 1);

        _shader.Use();

        // We initialize the camera so that it is 3 units back from where the rectangle is.
        // We also give it the proper aspect ratio.
        //_camera = new CameraFPS();
        //_camera.SetUp(Vector3.UnitZ * 3, DisplayManager.windowSize.X / (float)DisplayManager.windowSize.Y);

        Vector2 position = new Vector2(0, 0);
        _camera2D = new Camera2D(position, 0);

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

        _projection = Matrix4x4.CreatePerspectiveFieldOfView(Engine.Math.DegreesToRadians(45f), Engine.Window.windowSize.X / Engine.Window.windowSize.Y, 0.1f, 100.0f);



        // Now, head over to OnRenderFrame to see how we setup the model matrix.
    }

    public override void Update()
    {
        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;

        if (Glfw.GetKey(Engine.Window.window, Keys.W) == InputState.Press)
        {
            _camera.Position += _camera.Front * cameraSpeed * Time.deltaTime; // Forward
            Console.Write("W pressed");
        }
        if (Glfw.GetKey(Engine.Window.window, Keys.S) == InputState.Press)
        {
            _camera.Position -= _camera.Front * cameraSpeed * Time.deltaTime; // Backwards
            Console.Write("S pressed");
        }
        if (Glfw.GetKey(Engine.Window.window, Keys.A) == InputState.Press)
        {
            _camera.Position -= _camera.Right * cameraSpeed * Time.deltaTime; // Left
            Console.Write("A pressed");
        }
        if (Glfw.GetKey(Engine.Window.window, Keys.D) == InputState.Press)
        {
            _camera.Position += _camera.Right * cameraSpeed * Time.deltaTime; // Right
            Console.Write("D pressed");
        }
        if (Glfw.GetKey(Engine.Window.window, Keys.Space) == InputState.Press)
        {
            _camera.Position += _camera.Up * cameraSpeed * Time.deltaTime; // Up
            Console.Write("Space pressed");
        }
        if (Glfw.GetKey(Engine.Window.window, Keys.LeftShift) == InputState.Press)
        {
            _camera.Position -= _camera.Up * cameraSpeed * Time.deltaTime; // Down
            Console.Write("Left shift pressed");
        }

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
            var deltaX = mouse.X - _lastPos.X;
            var deltaY = mouse.Y - _lastPos.Y;
            _lastPos = new Vector2(mouse.X, mouse.Y);

            // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
            _camera.Yaw += deltaX * sensitivity;
            _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
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

        // Then, we pass all of these matrices to the vertex shader.
        // You could also multiply them here and then pass, which is faster, but having the separate matrices available is used for some advanced effects.

        // IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
        // They are then transposed properly when passed to the shader. 
        // This means that we retain the same multiplication order in both OpenTK c# code and GLSL shader code.
        // If you pass the individual matrices to the shader and multiply there, you have to do in the order "model * view * projection".
        // You can think like this: first apply the modelToWorld (aka model) matrix, then apply the worldToView (aka view) matrix, 
        // and finally apply the viewToProjectedSpace (aka projection) matrix.


        glBindVertexArray(_vertexArrayObject);

        //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        //glDrawArrays(GL_TRIANGLES, 0, 36);

        for (int i = 0; i < 10; i++)
        {
            var model = Matrix4x4.Identity * Matrix4x4.CreateRotationX(_time + i) * Matrix4x4.CreateRotationY(_time + i);

            model.Translation = cubePositions[i];

            _shader.SetMatrix4x4("model", model);
            _shader.SetMatrix4x4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4x4("projection", _camera.GetProjectionMatrix());

            glDrawArrays(GL_TRIANGLES, 0, 36);
        }

        //SwapBuffers();
    }
}