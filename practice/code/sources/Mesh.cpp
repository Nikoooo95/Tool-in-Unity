
#include "stdafx.h"
#include "Mesh.h"

Mesh::Mesh()
	:mesh_transform(Transform())
{
	log = "";
}

Mesh::~Mesh()
{
}

void Mesh::set_transform(Vector3f position, Vector3f rotation, Vector3f scale)
{
	mesh_transform.set(position, rotation, scale);

}

void Mesh::set_vertex(Vector3f v[], int size)
{
	vertex.clear();
	vertex.resize(size);
	for (size_t i = 0; i < size; ++i)
	{
		vertex[i] = v[i];
	}
}

void Mesh::set_normals(Vector3f n[], int size)
{
	normals.clear();
	normals.resize(size);
	for (size_t i = 0; i < size; ++i)
	{
		normals[i] = n[i];
	}
}

void Mesh::set_texcoord(Vector2f tc[], int size)
{
	texcoord.clear();
	texcoord.resize(size);
	for (size_t i = 0; i < size; ++i)
	{
		texcoord[i] = tc[i];
	}
}

const std::string & Mesh::get_log()
{
	return log;
}
