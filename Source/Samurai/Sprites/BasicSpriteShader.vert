#version 330 

uniform mat4 vertTransform; 

layout(location = 0) in vec3 vertPosition; 
layout(location = 1) in vec4 vertColor; 
layout(location = 2) in vec2 vertUV; 

smooth out vec4 fragColor;
smooth out vec2 fragUV;

void main() 
{ 
   gl_Position = vertTransform * vec4(vertPosition, 1.0); 
   fragColor = vertColor;
   fragUV = vertUV; 
}