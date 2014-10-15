#version 330 

uniform sampler2D fragTexture;
uniform int fragTextureWidth;
uniform int fragTextureHeight;
uniform vec4 fragTint;

smooth in vec2 fragModelPosition;
smooth in vec2 fragTexCoord; 

out vec4 outColor; 

void main() 
{ 
	outColor = texture(fragTexture, vec2(fragModelPosition.x / fragTextureWidth, fragModelPosition.y / fragTextureHeight)) * fragTint;
}