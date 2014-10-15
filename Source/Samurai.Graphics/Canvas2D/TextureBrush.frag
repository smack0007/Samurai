#version 330 

uniform sampler2D fragTexture;
uniform int fragTextureWidth;
uniform int fragTextureHeight;
uniform int fragTextureSourceX;
uniform int fragTextureSourceY;
uniform int fragTextureSourceWidth;
uniform int fragTextureSourceHeight;
uniform vec4 fragTint;

smooth in vec2 fragModelPosition;
smooth in vec2 fragTexCoord; 

out vec4 outColor; 

void main() 
{ 
	vec2 source = vec2(
		(fragModelPosition.x + fragTextureSourceX) / fragTextureWidth,
		(fragModelPosition.y + fragTextureSourceY) / fragTextureHeight
	);
	outColor = texture(fragTexture, source) * fragTint;
}