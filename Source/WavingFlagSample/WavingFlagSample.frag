uniform sampler2D texture0;

in vec2 fragUV;

out vec4 outColor; 

void main() 
{ 
	outColor = texture(texture0, fragUV);
}
