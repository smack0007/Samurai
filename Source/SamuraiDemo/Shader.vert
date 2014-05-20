#version 330 core

uniform mat4 projection;

layout(location = 0) in vec3 inPosition; 
layout(location = 1) in vec3 inColor; 
layout(location = 2) in vec2 inUV; 

out vec3 fragColor;
out vec2 fragUV;

void main() 
{ 
   gl_Position = projection * vec4(inPosition, 1.0);
   fragColor = inColor;
   fragUV = inUV;
}
