#version 330 core

// INPUTS
in vec4 shaderColor;
in vec2 textureCoord;

// OUTPUTS
out vec4 colourOutput;

void main()
{
    colourOutput = shaderColor;
}