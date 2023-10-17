﻿using static OpenGL.GL;
using GLFW;

namespace Engine
{
    internal class RotatingSquare : EngineMain
    {
        uint vao;
        uint vbo;

        Shader shader;

        Camera2D camera;

        public override void Initialize()
        {
            Window.SetBackgroundColour(new Vector4(0.2f, 0.3f, 0.3f, 1.0f));
        }

        public unsafe override void LoadContent()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;

                                    uniform mat4 projection;
                                    uniform mat4 model;
    
                                    void main() 
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";

            string fragmentShader = @"#version 330 core
                                    out vec4 FragColor;
                                    in vec4 vertexColor;

                                    void main() 
                                    {
                                        FragColor = vertexColor;
                                    }";

            shader = new Shader(vertexShader, fragmentShader);
            //shader.Load();

            shader.Use();

            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);

            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] vertices =
            {
                -0.5f, 0.5f, 1f, 0f, 0f, // top left
                0.5f, 0.5f, 0f, 1f, 0f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left

                0.5f, 0.5f, 0f, 1f, 0f,// top right
                0.5f, -0.5f, 0f, 1f, 1f, // bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left
            };

            fixed (float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);

            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));

            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            camera = new Camera2D(Window.windowSize / 2f, 2.5f);
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            glClear(GL_COLOR_BUFFER_BIT);

            Vector2 position = new Vector2(400, 300);
            Vector2 scale = new Vector2(150, 100);
            float rotation = Maths.Sin(Time.totalElapsedSeconds) * Maths.Pi * 2f;

            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.x, position.y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.x, scale.y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);

            shader.SetMatrix4("model", sca * rot * trans);

            shader.Use();
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);
        }
    }
}