using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenGL;
using System.Numerics;
using static OpenGL.GL;
using GLFW;
using System.Drawing;
using System.Xml.Linq;

// A simple class meant to help create shaders.
    public class Shader
    {
        public readonly uint Handle;

        private readonly Dictionary<string, int> _uniformLocations;

        // This is how you create a simple shader.
        // Shaders are written in GLSL, which is a language very similar to C in its semantics.
        // The GLSL source is compiled *at runtime*, so it can optimize itself for the graphics card it's currently being used on.
        // A commented example of GLSL can be found in shader.vert.
        public unsafe Shader(string vertPath, string fragPath)
        {
            // There are several different types of shaders, but the only two you need for basic rendering are the vertex and fragment shaders.
            // The vertex shader is responsible for moving around vertices, and uploading that data to the fragment shader.
            //   The vertex shader won't be too important here, but they'll be more important later.
            // The fragment shader is responsible for then converting the vertices to "fragments", which represent all the data OpenGL needs to draw a pixel.
            //   The fragment shader is what we'll be using the most here.

        // Load vertex shader and compile
        var shaderSource = File.ReadAllText(vertPath);

        // GL.CreateShader will create an empty shader (obviously). The ShaderType enum denotes which type of shader will be created.
        uint vertexShader = glCreateShader(GL_VERTEX_SHADER);

        // Now, bind the GLSL source code
        glShaderSource(vertexShader, shaderSource);

        // And then compile
        CompileShader(vertexShader);

        // We do the same for the fragment shader.
        shaderSource = File.ReadAllText(fragPath);
        var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fragmentShader, shaderSource);
        CompileShader(fragmentShader);

        // These two shaders must then be merged into a shader program, which can then be used by OpenGL.
        // To do this, create a program...
        Handle = glCreateProgram();

        // Attach both shaders...
        glAttachShader(Handle, vertexShader);
        glAttachShader(Handle, fragmentShader);

        // And then link them together.
        LinkProgram(Handle);

        DeleteShader(vertexShader);
        DeleteShader(fragmentShader);
    }

    private static void CompileShader(uint shader)
    {
        // Try to compile the shader
        glCompileShader(shader);

        // Check for compilation errors
        int[] status = glGetShaderiv(shader, GL_COMPILE_STATUS, 1);

        if (status[0] == 0)
        {
            // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
                var infoLog = glGetShaderInfoLog(shader);
                throw new System.Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
        }
    }

        private static void LinkProgram(uint program)
        {
            // We link the program
            glLinkProgram(program);

        // Check for linking errors
        int[] status =  glGetProgramiv(program, GL_LINK_STATUS, 1);

        if (status[0] == 0)
        {
                // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                throw new System.Exception($"Error occurred whilst linking Program({program})");
            }
        }

        // A wrapper function that enables the shader program.
        public void Use()
        {
            glUseProgram(Handle);
        }

        // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
        // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
        public int GetAttribLocation(string attribName)
        {
            return glGetAttribLocation(Handle, attribName);
        }

        // Uniform setters
        // Uniforms are variables that can be set by user code, instead of reading them from the VBO.
        // You use VBOs for vertex-related data, and uniforms for almost everything else.

        // Setting a uniform is almost always the exact same, so I'll explain it here once, instead of in every method:
        //     1. Bind the program you want to set the uniform on
        //     2. Get a handle to the location of the uniform with GL.GetUniformLocation.
        //     3. Use the appropriate GL.Uniform* function to set the uniform.

        /// <summary>
        /// Set a uniform int on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetInt(string name, int data)
        {
            glUseProgram(Handle);
        //glUniform1i(glGetUniformLocation(Handle, data));
        //GL.Uniform1(_uniformLocations[name], data);

        glUniform1i(glGetUniformLocation(Handle, name), data);
    }

        /// <summary>
        /// Set a uniform float on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetFloat(string name, float data)
        {
            glUseProgram(Handle);
        //GL.Uniform1(_uniformLocations[name], data);

        glUniform1i(glGetUniformLocation(Handle, name), (int)data);
    }

    // Call function to set the colour of the shader
    public void SetColor(float red, float green, float blue, float alpha)
    {
        int shaderLocation = glGetUniformLocation(Handle, "colour");
        glUniform4f(shaderLocation, red, green, blue, alpha);
    }

        /// <summary>
        /// Set a uniform Matrix4 on this shader
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        /// <remarks>
        ///   <para>
        ///   The matrix is transposed before being sent to the shader.
        ///   </para>
        /// </remarks>
        public unsafe void SetMatrix4(string name, Matrix4x4 data)
        {
        glUseProgram(Handle);

        int location = glGetUniformLocation(Handle, name);

        // convert the matrix4x4 to a float[]
        float[] cells = { data.M11, data.M12, data.M13, data.M14,
                            data.M21, data.M22, data.M23, data.M24,
                            data.M31, data.M32, data.M33, data.M34,
                            data.M41, data.M42, data.M43, data.M44};

        glUniformMatrix4fv(
            location,
            1,
            true, // True to transpose matrcies so they're in a column-major format
            cells);


        //glUniformMatrix4fv(glGetUniformLocation(Handle, name), 1, true, (float*)1);


        //GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// Set a uniform Vector3 on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetVector3(string name, Vector3 data)
        {
            glUseProgram(Handle);
            //GL.Uniform3(_uniformLocations[name], data);
        }

    public float[] GetMatrix4x4Values(Matrix4x4 m)
    {
        return new float[]
        {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
        };
    }

    // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program. Detach them, and then delete them.
    private void DeleteShader(uint shader)
    {
        glDetachShader(Handle, shader);
        glDeleteShader(shader);
    }

    public void SetMatrix4x4(string uniformName, Matrix4x4 mat)
    {
        int location = glGetUniformLocation(Handle, uniformName);
        glUniformMatrix4fv(location, 1, false, GetMatrix4x4Values(mat));
    }
}