

#ifndef MESH__HEADER
#define MESH__HEADER

#include "Vector3f.h"
#include "Vector2f.h"
#include "Transform.h"
#include <vector>
#include <string>



class Mesh
{
private:
	Transform mesh_transform;

	std::vector<Vector3f> vertex;
	std::vector<Vector3f> normals;
	std::vector<Vector2f> texcoord;

	std::string log;

public:
	Mesh();
	~Mesh();

public:
	void set_transform(Vector3f position, Vector3f rotation, Vector3f scale);


	void set_vertex(Vector3f v[], int size);
	void set_normals(Vector3f n[], int size);
	void set_texcoord(Vector2f tc[], int size);

	const std::string &  get_log();


};
#endif
