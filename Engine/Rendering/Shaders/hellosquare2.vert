#version 330 core

// INPUTS
uniform vec4 colour;

// OUTPUTS
out vec4 shaderColor;

layout (location = 0) in vec3 pos;

void main()
{
    gl_Position = vec4(pos.x, pos.y, pos.z, 1.0);
    shaderColor = colour;
}