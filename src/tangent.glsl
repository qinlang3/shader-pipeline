// Input:
//   N  3D unit normal vector
// Outputs:
//   T  3D unit tangent vector
//   B  3D unit bitangent vector
void tangent(in vec3 N, out vec3 T, out vec3 B)
{
  
  T = normalize(min(cross(N, vec3(0.0, 1.0, 0.0)), cross(N, vec3(0.0, 0.0, 1.0))));
  B = normalize(cross(T, normalize(N)));
 
}
