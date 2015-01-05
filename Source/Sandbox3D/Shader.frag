#version 330 core

uniform sampler2D texture0;

in vec2 fragUV;
in vec3 fragColor;

out vec4 outColor; 

void main() 
{ 
	outColor = texture(texture0, vec2(fragUV.x, fragUV.y));
}
