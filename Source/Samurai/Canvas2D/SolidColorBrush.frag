uniform vec4 fragColor;

smooth in vec2 fragModelPosition;
smooth in vec2 fragTexCoord; 

out vec4 outColor; 

void main() 
{ 
	outColor = fragColor;
}