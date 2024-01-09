using Engine;
using static OpenGL.GL;
using System.IO; // For read/writing files

// This script is used purely for testing engine functionality. It does not actually provide anything to the engine itself and should not be included as part of the project build.

public class Sandbox : EngineMain
{
    Camera camera;
    Cube leftPaddle;
    Cube rightPaddle;

    Cube ball;

    Quad text;

    // Sprite testing
    private int spriteTexture;
    private Shader shader;
    //

    Sprite sprite;

    private float ballSpeed = 0.2f;
    private float paddleSpeed = 0.5f;

    private bool wireframe = false;

    // We create a double to hold how long has passed since the program was opened.
    private float _time;

    public override void Initialize()
    {
        Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
    }

    public unsafe override void LoadContent()
    {
        string data;

        StreamReader reader = null;
        StreamWriter writer = null;

        TextureManager.AddTexture("dougFace", "F:\\GameDev\\.Engine\\Engine\\Sandbox Application\\Texture\\awesomeface.png");

        TextureManager.AddTexture("border", "F:\\GameDev\\.Engine\\Engine\\Sandbox Application\\Texture\\Untitled.png");

        TextureManager.AddTexture("font", "F:\\GameDev\\.Engine\\Engine\\Engine\\Graphics\\Fonts\\Font - System 16x16.png");

        try
        {
            reader = new StreamReader("F:\\GameDev\\.Engine\\Engine\\Engine\\Outgoing\\Test.txt");

            //writer = new StreamWriter("F:\\GameDev\\.Engine\\Engine\\Engine\\Outgoing\\Test.txt");

            data = reader.ReadLine();

            while(data != null)
            {
                Console.WriteLine(data);
                data = reader.ReadLine();
            }

            reader.Close();

            Vector3 position = new Vector3();
            int id = 0;

            //foreach (char c in data)
            //{
                //Cube cubeName = new Cube();
                //cubeName.transform.position = new Vector3(0, id, 0);

                //id++;
            //}


            //writer.WriteLine(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            //writer.Close();
        }


        leftPaddle = new Cube();
        leftPaddle.transform.SetPosition(new Vector3(-1, 0, 0));

        rightPaddle = new Cube();
        rightPaddle.transform.SetPosition(new Vector3(1, 0, 0));

        sprite = new Sprite(TextureManager.GetTexture("font"));

        text = new Quad();

        ball = new Cube();
        ball.transform.SetScale(new Vector3(0.1f, 0.1f, 0.1f));

        camera = new Camera();
        //camera.transform.SetPosition(new Vector3(camera.transform.position.x, camera.transform.position.y + 4, camera.transform.position.z));
    }

    public override void Update()
    {
        //CheckCollisions();
        MoveBall();


        if (Input.GetKey(KeyCode.Up))
        {
            leftPaddle.transform.SetPosition(new Vector3(leftPaddle.transform.position.x, leftPaddle.transform.position.y + paddleSpeed * Time.deltaTime, leftPaddle.transform.position.z));
        }
        else if (Input.GetKey(KeyCode.Down))
        {
            leftPaddle.transform.SetPosition(new Vector3(leftPaddle.transform.position.x, leftPaddle.transform.position.y - paddleSpeed * Time.deltaTime, leftPaddle.transform.position.z));
        }

        // Sets the mode as wireframe
        if (Input.GetKeyDown(KeyCode.Left))
        {
            RenderingManager.SetWireframeMode();
        }

        // We add the time elapsed since last frame, times 4.0 to speed up animation, to the total amount of time passed.
        _time += 0.05f * Time.deltaTime;

        //leftPaddle.transform.SetRotation(new Vector3(_time, _time, _time));
        //cube.transform.SetScale(new Vector3(_time, _time, _time));
    }

    public unsafe override void Draw()
    {



        // We clear the depth buffer in addition to the color buffer.
        //glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        //glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);

        // THIS IS NOW HANDLED BY THE RENDERABLE CLASS
        //leftPaddle.Draw();
        //rightPaddle.Draw();
        //ball.Draw();
        //text.Draw();

        //SwapBuffers();
    }

    private void MoveBall()
    {
        // Move the ball
        ball.transform.SetPosition(new Vector3(ball.transform.position.x - ballSpeed * Time.deltaTime, ball.transform.position.y * Time.deltaTime, ball.transform.position.z));
    }

    // Checks if the ball is colliding with one of the paddles
    private void CheckCollisions()
    {
        if (ball.collider.isColliding(leftPaddle.collider) == true)
        {
            Console.WriteLine("COLLISION");
            ballSpeed = -ballSpeed;
        }

        if (ball.collider.isColliding(rightPaddle.collider) == true)
        {
            Console.WriteLine("COLLISION");
            ballSpeed = -ballSpeed;
        }
    }
}