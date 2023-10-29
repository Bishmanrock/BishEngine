#version 330 core

// INPUTS
in vec4 shaderColor;

// OUTPUTS
out vec4 result;

void main()
{
	result = vec4(shaderColor, 1.0);
}