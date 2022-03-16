// Given a 3d position as a seed, compute a smooth procedural noise
// value: "Perlin Noise", also known as "Gradient noise".
//
// Inputs:
//   st  3D seed
// Returns a smooth value between (-1,1)
//
// expects: random_direction, smooth_step
float perlin_noise( vec3 st) 
{
  vec3 f = floor(st);
  vec3 c = ceil(st);
  vec3 s = fract(st);

  vec3 w = smooth_step(s);

  vec3 x0 = vec3(f.x, f.y, f.z);	// 000
  vec3 x1 = vec3(f.x, c.y, f.z);	// 010
  vec3 x2 = vec3(f.x, f.y, c.z);	// 001
  vec3 x3 = vec3(f.x, c.y, c.z);	// 011
  vec3 x4 = vec3(c.x, f.y, f.z);	// 100
  vec3 x5 = vec3(c.x, c.y, f.z);	// 110
  vec3 x6 = vec3(c.x, f.y, c.z);	// 101
  vec3 x7 = vec3(c.x, c.y, c.z);	// 111

  float n0 = dot(random_direction(x0), st-x0);	// 000 
  float n1 = dot(random_direction(x1), st-x1);	// 010
  float n2 = dot(random_direction(x2), st-x2);	// 001
  float n3 = dot(random_direction(x3), st-x3);	// 011
  float n4 = dot(random_direction(x4), st-x4);	// 100
  float n5 = dot(random_direction(x5), st-x5);	// 110
  float n6 = dot(random_direction(x6), st-x6);	// 101
  float n7 = dot(random_direction(x7), st-x7);	// 111

  float ix0 = mix(n0, n4, w.x);	// 000 & 100
  float ix1 = mix(n1, n5, w.x);	// 010 & 110
  float ix2 = mix(n2, n6, w.x);	// 001 & 101
  float ix3 = mix(n3, n7, w.x);	// 011 & 111

  float iy0 = mix(ix0, ix1, w.y);	// 00 & 10
  float iy1 = mix(ix2, ix3, w.y);	// 01 & 11

  float iz = mix(iy0, iy1, w.z);

  return iz;
 
}

