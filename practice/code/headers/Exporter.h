#pragma once

#include "Vector3f.h"
#include "Transform.h"
#include <string>
#include <vector>
#include <algorithm>
#include <iostream>

#include <stdio.h>

using namespace mathexp;

class Exporter
{
private:
	std::string path;
	std::string log;

	std::vector<Vector3f> vertex;
	std::vector<Vector3f> normals;
	std::vector<Vector3f> texcoord;
	Transform mesh_transform;

public:
	Exporter();

	~Exporter() = default;

public:

	bool export_obj(std::string & path);

	const char * get_log();
	const char * get_path();
	void set_path(const std::string & path);

	void set_vertex(Vector3f v []);
	void set_normals(Vector3f * normals);
	void set_texcoord(Vector3f * texcoord);

	int get_size() { return vertex.size(); }

	void set_transform(Vector3f position, Vector3f rotation, Vector3f scale);

private:

	const char * string_to_char(const std::string & s);
};