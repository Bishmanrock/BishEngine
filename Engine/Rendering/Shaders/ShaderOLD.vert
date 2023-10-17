#version 330 core

layout (location = 0) in vec3 position;

// INPUTS
uniform vec4 colour;
uniform vec2 texture;

// OUTPUTS
out vec4 shaderColor;
out vec2 textureCoord;

void main()
{
    gl_Position = vec4(position.x, position.y, position.z, 1.0);
    shaderColor = colour;
    textureCoord = texture;
}