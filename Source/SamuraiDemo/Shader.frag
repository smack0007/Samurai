#version 330 core

uniform sampler2D texture1;

in vec2 fragUV;
in vec3 fragColor;

out vec4 outColor; 

void main() 
{ 
	outColor = texture(texture1, vec2(fragUV.x, fragUV.y));
}
