using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.GL;
using GLFW;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Numerics;

namespace Engine
{
    public class ShaderOLD
    {
        private string vertexCode;
        private string fragmentCode;

        public uint ProgramID { get; set; }

        public ShaderOLD(string vertexFile, string fragmentFile)
        {
            this.vertexCode = File.ReadAllText(vertexFile);
            this.fragmentCode = File.ReadAllText(fragmentFile);
        }

        public void Load()
        {
            uint vertexShader;
            uint fragmentShader;

            vertexShader = glCreateShader(GL_VERTEX_SHADER); // Create the empty shader
            glShaderSource(vertexShader, vertexCode); // Bind the GLSL source code
            glCompileShader(vertexShader); // Compile

            CheckShaderForErrors(vertexShader);

            fragmentShader = glCreateShader(GL_FRAGMENT_SHADER); // Create the empty shader
            glShaderSource(fragmentShader, fragmentCode); // Bind the GLSL source code
            glCompileShader(fragmentShader); // Compile

            CheckShaderForErrors(fragmentShader);

            ProgramID = glCreateProgram(); // Marge into the shader program
            glAttachShader(ProgramID, vertexShader); // Attach vertex shader
            glAttachShader(ProgramID, fragmentShader); // Attach fragment shader
            glLinkProgram(ProgramID); // Link them together

            int[] status = glGetProgramiv(ProgramID, GL_LINK_STATUS, 1);

            if (status[0] == 0)
            {
                // Failed to compile
                string error = glGetProgramInfoLog(ProgramID);
                Debug.WriteLine("ERROR LINKING SHADER PROGRAM: " + error);
            }

            // Cleanup
            DeleteShader(vertexShader);
            DeleteShader(vertexShader);

            //
        }

        private void DeleteShader(uint shader)
        {
            glDetachShader(ProgramID, shader);
            glDeleteShader(shader);
        }

        public void Use()
        {
            // Draw the object
            glUseProgram(ProgramID);
        }

        public void SetMatrix4x4(string uniformName, System.Numerics.Matrix4x4 mat)
        {
            int location = glGetUniformLocation(ProgramID, uniformName);
            glUniformMatrix4fv(location, 1, false, GetMatrix4x4Values(mat));
        }

        private float[] GetMatrix4x4Values(System.Numerics.Matrix4x4 m)
        {
            return new float[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
        }

        // Call function to set the color
        public void SetColor(float red, float green, float blue, float alpha)
        {
            int shaderLocation = glGetUniformLocation(ProgramID, "colour");
            glUniform4f(shaderLocation, red, green, blue, alpha);
        }

        // The shader sources provided with this project use hardcoded layout(location)-s.If you want to do it dynamically, you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
        public int GetAttribLocation(string attribName)
        {
            return glGetAttribLocation(ProgramID, attribName);
        }

        private void CheckShaderForErrors(uint shader)
        {
            int[] status = glGetShaderiv(shader, GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                // Failed to compile
                string error = glGetShaderInfoLog(shader);
                Debug.WriteLine("ERROR COMPILING SHADER: " + error);
            }
        }
    }
}