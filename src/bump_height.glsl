// Create a bumpy surface by using procedural noise to generate a height (
// displacement in normal direction).
//
// Inputs:
//   is_moon  whether we're looking at the moon or centre planet
//   s  3D position of seed for noise generation
// Returns elevation adjust along normal (values between -0.1 and 0.1 are
//   reasonable.
float bump_height( bool is_moon, vec3 s)
{
  if(is_moon){
	return clamp((2.2*improved_perlin_noise(4.24*s+2.2*s*s)+4.2*improved_perlin_noise(sin(0.5*M_PI)*s))*0.08, -0.1, 0.1);
  }else{
	return clamp((6*improved_perlin_noise(4.24*s+2.2*s*s)+4.2*improved_perlin_noise(sin(0.5*M_PI)*s))*0.06, -0.01, 0.1);
  }
}
