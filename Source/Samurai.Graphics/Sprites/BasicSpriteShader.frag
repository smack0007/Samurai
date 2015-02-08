uniform sampler2D fragSampler;

smooth in vec4 fragColor;
smooth in vec2 fragUV; 

out vec4 outColor; 

void main() 
{ 
	outColor = texture(fragSampler, vec2(fragUV.x, fragUV.y)) * fragColor;
}