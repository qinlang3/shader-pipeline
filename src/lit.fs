// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: PI, blinn_phong
void main()
{
  vec3 ka, kd, ks;
  float p;
  vec3 v = -normalize(view_pos_fs_in.xyz);
  float theta = animation_seconds*((2*M_PI)/4);
  vec4 l_p = view*vec4(10*cos(theta), 5.0, 10*sin(theta), 1.0);
  vec3 l = normalize((l_p-view_pos_fs_in).xyz);
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
  color = blinn_phong(ka, kd, ks, p, normalize(normal_fs_in), v, l);
}
