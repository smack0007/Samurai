#version 330 

uniform vec4 fragColor;

smooth in vec2 fragUV; 

out vec4 outColor; 

void main() 
{ 
	outColor = fragColor;
}