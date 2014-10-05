#version 330 

uniform mat4 inProjection; 

layout(location = 0) in vec3 inPosition; 
layout(location = 1) in vec4 inColor; 
layout(location = 2) in vec2 inUV; 

smooth out vec4 fragColor;
smooth out vec2 fragUV;

void main() 
{ 
   gl_Position = inProjection * vec4(inPosition, 1.0); 
   fragColor = inColor;
   fragUV = inUV; 
}