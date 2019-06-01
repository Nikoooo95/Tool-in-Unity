

#include "stdafx.h"

#include "Exporter.h"

Exporter::Exporter()
{
	
}

Exporter::~Exporter()
{
	
}

void Exporter::set_data(Vector3f new_data)
{
	data = new_data;
}

Vector3f Exporter::get_data()
{
	return data;
}

bool Exporter::export_obj(const char path[])
{
	

	return true;
}

const char* Exporter::get_path() const
{
	return path.c_str();
}

void Exporter::set_vertex(Vector3f vertex)
{
	this->vertex = vertex;
}

void Exporter::set_normals(Vector3f normals)
{
	this->normals = normals;
}

void Exporter::set_texcoord(Vector3f texcoord)
{
	this->texcoord = texcoord;
}
