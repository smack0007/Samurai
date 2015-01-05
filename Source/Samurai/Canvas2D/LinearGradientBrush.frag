#version 330 

uniform vec4 fragStartColor;
uniform vec4 fragEndColor;
uniform vec2 fragFactor;

smooth in vec2 fragModelPosition;
smooth in vec2 fragTexCoord; 

out vec4 outColor; 

void main() 
{			
	vec4 colorDiff = fragStartColor - fragEndColor;
	vec4 colorAvg = (fragStartColor + fragEndColor) / 2.0;

	vec2 uv = vec2(
		(0.5 - fragTexCoord.x),
		(0.5 - fragTexCoord.y)
	);

	outColor = vec4(
		colorAvg.r + (colorDiff.r * fragFactor.x * uv.x) + (colorDiff.r * fragFactor.y * uv.y),
		colorAvg.g + (colorDiff.g * fragFactor.x * uv.x) + (colorDiff.g * fragFactor.y * uv.y),
		colorAvg.b + (colorDiff.b * fragFactor.x * uv.x) + (colorDiff.b * fragFactor.y * uv.y),
		colorAvg.a + (colorDiff.a * fragFactor.x * uv.x) + (colorDiff.a * fragFactor.y * uv.y)
	);
}