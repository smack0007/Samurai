#version 330 

uniform mat4 vertTransform; 

layout(location = 0) in vec2 vertModelPosition; 
layout(location = 1) in vec2 vertScreenPosition; 
layout(location = 2) in vec2 vertTexCoord; 

smooth out vec2 fragModelPosition;
smooth out vec2 fragTexCoord;

void main() 
{ 
   gl_Position = vertTransform * vec4(vertScreenPosition, 0, 1.0); 
   
   fragModelPosition = vertModelPosition;
   fragTexCoord = vertTexCoord;
}