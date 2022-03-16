// Generate a procedural planet and orbiting moon. Use layers of (improved)
// Perlin noise to generate planetary features such as vegetation, gaseous
// clouds, mountains, valleys, ice caps, rivers, oceans. Don't forget about the
// moon. Use `animation_seconds` in your noise input to create (periodic)
// temporal effects.
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
  float height = bump_height(is_moon, sphere_fs_in);
  float kd_noise = (improved_perlin_noise(4.24*sphere_fs_in+2.2*sphere_fs_in*sphere_fs_in)+4.2*improved_perlin_noise(sin(0.5*M_PI)*sphere_fs_in))*0.5;
  float ks_noise = sin(0.5*M_PI*improved_perlin_noise(17*sphere_fs_in));
  if(is_moon){
	ka = vec3(0.90, 0.90, 0.90);
	kd = clamp(vec3(0.7, 0.7, 0.7)+kd_noise, 0.0, 1.0);
	ks = clamp(vec3(0.2,0.2,0.2)+ks_noise, 0.0, 1.0);
	p = 200.0;
  }else{
	if (height <= 0){
		ka = vec3(0.2,0.3,0.721569);
		kd = clamp(vec3(0.2,0.3,0.721569)+kd_noise, 0.0, 1.0);
		ks = clamp(vec3(0.8,0.8,0.8)+ks_noise, 0.0, 1.0);
		p = 500.0;
	}else{
		ka = vec3(0.251961,0.586275,0.130196);
		kd = clamp(vec3(0.251961,0.586275,0.130196)+kd_noise, 0.0, 1.0);
		ks = clamp(vec3(0.3,0.3,0.3)+ks_noise, 0.0, 1.0);
		p = 50.0;
	}
  }
  color = blinn_phong(ka, kd, ks, p, normalize(n), v, l);
}
