// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  vec3 ka, kd, ks;
  float p;
  vec3 v = -normalize(view_pos_fs_in.xyz);
  float theta = animation_seconds*((2*M_PI)/4);
  vec4 l_p = view*vec4(10*cos(theta), 5.0, 10*sin(theta), 1.0);
  vec3 l = normalize((l_p-view_pos_fs_in).xyz);

  vec3 pos_bump = bump_position(is_moon, sphere_fs_in);
  vec3 T, B;
  tangent(normal_fs_in, T, B);
  float e = 0.00001;
  vec3 n = cross((bump_position(is_moon, sphere_fs_in+e*T)-pos_bump)/e, (bump_position(is_moon, sphere_fs_in+e*B)-pos_bump)/e);
  if(is_moon){
	ka = vec3(0.90, 0.90, 0.90);
	kd = vec3(0.7, 0.7, 0.7);
	ks = vec3(0.2,0.2,0.2);
	p = 200.0;
  }else{
	ka = vec3(0.215686,0.494118,0.721569);
	kd = vec3(0.2,0.3,0.721569);
	ks = vec3(0.7,0.7,0.7);
	p = 500.0;
  }
  color = blinn_phong(ka, kd, ks, p, normalize(n), v, l);
}
