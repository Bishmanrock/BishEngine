using Engine;
using static OpenGL.GL;

// This script is used purely for testing engine functionality. It does not actually provide anything to the engine itself and should not be included as part of the project build.

public class Sandbox : EngineMain
{
    Camera camera;
    Cube leftPaddle;
    Cube rightPaddle;

    Cube ball;

    Quad text;

    private float ballSpeed = 0.1f;

    // We create a double to hold how long has passed since the program was opened.
    private float _time;

    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        leftPaddle = new Cube();
        leftPaddle.transform.SetPosition(new Vector3(-1, 0, 0));

        rightPaddle = new Cube();
        rightPaddle.transform.SetPosition(new Vector3(1, 0, 0));

        text = new Quad();

        ball = new Cube();
        ball.transform.SetScale(new Vector3(0.1f, 0.1f, 0.1f));

        camera = new Camera();
    }

    public override void Update()
    {
        MoveBall();
        CheckCollisions();


        if (Input.GetKey(KeyCode.Left))
        {
            leftPaddle.transform.SetPosition(new Vector3(leftPaddle.transform.position.x - 0.01f * Time.deltaTime, _time, _time));
        }
    }

    public unsafe override void Draw()
    {
        // We add the time elapsed since last frame, times 4.0 to speed up animation, to the total amount of time passed.
        _time += 0.05f * Time.deltaTime;

        leftPaddle.transform.SetRotation(new Vector3(_time, _time, _time));
        //cube.transform.SetScale(new Vector3(_time, _time, _time));


        // We clear the depth buffer in addition to the color buffer.
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        leftPaddle.Draw();
        rightPaddle.Draw();
        ball.Draw();
        text.Draw();

        //SwapBuffers();
    }

    private void MoveBall()
    {
        // Move the ball
        ball.transform.SetPosition(new Vector3(ball.transform.position.x - ballSpeed * Time.deltaTime, ball.transform.position.y, ball.transform.position.z));
    }

    // Checks if the ball is colliding with one of the paddles
    private void CheckCollisions()
    {

    }
}