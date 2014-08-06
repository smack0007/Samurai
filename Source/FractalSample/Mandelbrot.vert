#version 330 core

uniform mat4 projection;

layout(location = 0) in vec3 inPosition; 
layout(location = 1) in vec2 inUV; 

out vec2 fragPosition;
out vec2 fragUV;

void main() 
{ 
   gl_Position = projection * vec4(inPosition, 1.0);
   fragPosition = vec2(inPosition.x, inPosition.y) * vec2(0.5) + vec2(0.5);
   fragUV = inUV;
}