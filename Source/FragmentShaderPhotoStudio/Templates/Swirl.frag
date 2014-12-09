uniform sampler2D picture;

smooth in vec2 pixelCoords; 

out vec4 pixel; 

void main() 
{ 
	vec2 uv = pixelCoords.xy - 0.5;
	float angle = atan(uv.y, uv.x);
	float radius = length(uv);
	angle += radius * 8;
	vec2 shifted = radius * vec2(cos(angle), sin(angle));
	pixel = texture(picture, (shifted + 0.5));
}