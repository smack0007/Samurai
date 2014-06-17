#version 330 core

uniform mat4 projection;
uniform float startX;
uniform float time;

layout(location = 0) in vec2 inPosition; 
layout(location = 1) in vec2 inUV; 

out vec3 fragColor;
out vec2 fragUV;

void main() 
{ 
	float angle = (inPosition.x - startX) + time;

	vec4 outPosition = vec4(inPosition.x, inPosition.y, 0.0, 1.0);
	outPosition.y += sin(angle) * 5;
	gl_Position = projection * outPosition;
	
	fragUV = inUV;
}
