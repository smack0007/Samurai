#version 330 

uniform sampler2D uniTexture;

smooth in vec4 vertColor;
smooth in vec2 vertUV; 

out vec4 outColor; 

void main() 
{ 
	outColor = texture(uniTexture, vec2(vertUV.x, vertUV.y)) * vertColor;
}
