// STANDARD SHADER

#version 330

// INPUTS
in vec2 texCoord;

// OUTPUTS
out vec4 fragColor;

uniform sampler2D texture0;
uniform sampler2D texture1;

uniform sampler1D colorRamp;
in float height;

uniform vec4 colour;

uniform bool isWireframeMode = false;

// COLOUR PALETTE SETTINGS
uniform vec4 palette[256]; // The palette array
uniform int paletteSize; // Sie of the palette
//

vec4 findNearestPaletteColour(vec4 colour)
{
    float minDistance = 1000.0;

    vec4 nearestColour = vec4(0.0, 0.0, 0.0, 1.0);

    for (int i = 0; i < paletteSize; i++) {

        float distance = distance(colour, palette[i]);

        if (distance < minDistance) {
            minDistance = distance;
            nearestColour = palette[i];
        }

    }

    return nearestColour;
}

void main()
{
    if (isWireframeMode)
    {
        // Output white colour
        fragColor = vec4(1.0, 1.0, 1.0, 1.0);
    }
    else
    {
        fragColor = mix(texture(texture0, texCoord), texture(texture1, texCoord), texture(texture1, texCoord).a * 0.2);
    }
}