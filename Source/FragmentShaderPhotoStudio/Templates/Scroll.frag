uniform sampler2D picture;
uniform vec2 pictureSize;
uniform float time;

smooth in vec2 pixelCoords;

out vec4 pixel; 

void main() 
{ 
	vec2 uv = vec2(
		fract(pixelCoords.x + (time / pictureSize.x)),
		fract(pixelCoords.y + (time / pictureSize.y)));
	
	pixel = texture(picture, uv);
}