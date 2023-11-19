#version 330 core

// Positions/Coordinates
layout (location = 0) in vec3 aPos;
// Colors
layout (location = 1) in vec3 aColor;
// Texture Coordinates
layout (location = 2) in vec2 aTex;


// Outputs the color for the Fragment Shader
out vec3 color;
// Outputs the texture coordinates to the fragment shader
out vec2 texCoord;

// Inputs the matrices needed for 3D viewing with perspective
uniform mat4 proj;
uniform mat4 view;
uniform mat4 model;

// Inputs the matrices needed for transformation
uniform mat4 rotationMat;
uniform vec3 objPosition;
uniform vec3 objScale;

void main()
{
   // Apply rotation
   vec4 rotated = rotationMat * vec4(aPos, 1.0);

   // Apply scaling
   vec4 scaled = vec4(rotated.xyz * objScale, rotated.w);

   // Apply translation
   vec4 translated = scaled + vec4(objPosition, 0.0);

   // Output the transformed position
   gl_Position = proj * view * model * translated;

   // Assign the colors from the Vertex Data to "color"
   color = aColor;

   // Assign the texture coordinates from the Vertex Data to "texCoord"
   texCoord = aTex;
}