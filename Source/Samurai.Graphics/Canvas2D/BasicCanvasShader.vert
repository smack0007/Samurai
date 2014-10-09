#version 330 

uniform mat4 vertTransform; 

layout(location = 0) in vec2 vertPosition; 
layout(location = 1) in vec2 vertUV; 

smooth out vec2 fragUV;

void main() 
{ 
   gl_Position = vertTransform * vec4(vertPosition, 0, 1.0); 
   fragUV = vertUV; 
}