

#include "stdafx.h"

#include "Exporter.h"

Exporter::Exporter()
	:mesh_transform(Transform())
{
	path = "";
	log = "";
}

bool Exporter::export_obj(std::string & path)
{
	set_path(path);

	//.-.-.-.-.-.-.-.-.-.-.-.-.-

	return true;
}

const char * Exporter::get_log()
{
	return string_to_char(log);
}

const char * Exporter::get_path()
{
	return string_to_char(path);
}

void Exporter::set_path(const std::string & path)
{
	this->path = path;
}

void Exporter::set_vertex(Vector3f v[])
{
	size_t size = sizeof(v) & sizeof(v[0]);

	vertex.clear();
	std::vector<Vector3f> vector(v, v + size);
	
	vertex = vector;

}

void Exporter::set_normals(Vector3f * normals)
{
	//this->normals = normals;
}

void Exporter::set_texcoord(Vector3f * texcoord)
{
	//this->texcoord = texcoord;
}


void Exporter::set_transform(Vector3f position, Vector3f rotation, Vector3f scale)
{
	mesh_transform.set(position, rotation, scale);
}

const char * Exporter::string_to_char(const std::string & s)
{
	int length_str = s.length() + 1;
	char* temp = new char[length_str];
	const char * str = new char[length_str];
	strcpy_s(temp, length_str, path.c_str());
	str = temp;

	return str;
}
