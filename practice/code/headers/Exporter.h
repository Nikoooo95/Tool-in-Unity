#pragma once

#include "Vector3f.h"
#include <string>

using namespace mathexp;

class Exporter
{
private:

	Vector3f data;
	std::string path = "dubidu";

	Vector3f vertex;
	Vector3f normals;
	Vector3f texcoord;

public:
	Exporter();

	~Exporter();

public:

	void set_data(Vector3f new_data);

	Vector3f get_data();

	bool export_obj(const char path[]);

	const char* get_path() const;

	void set_vertex(Vector3f vertex);

	void set_normals(Vector3f normals);

	void set_texcoord(Vector3f texcoord);
};