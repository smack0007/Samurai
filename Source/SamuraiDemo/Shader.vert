#version 330 

uniform mat4 uniProjection;

in vec3 inPosition; 
in vec4 inColor; 
in vec2 inUV; 

smooth out vec4 vertColor;
smooth out vec2 vertUV;

void main() 
{ 
   gl_Position = uniProjection * vec4(inPosition, 1.0); 
   vertColor = inColor;
   vertUV = inUV; 
}
