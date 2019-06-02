#pragma once

#include "Mesh.h"

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>

#include <stdio.h>


class Exporter
{
private:
	typedef std::shared_ptr<Mesh> sh_Mesh;

	std::string path;
	std::string log;

	std::vector<sh_Mesh> meshes;

public:
	Exporter();

	~Exporter() = default;

public:

	bool export_obj(std::string & path);

	const char * get_path();
	void set_path(const std::string & path);

	void add_mesh(Vector3f position, Vector3f rotation, Vector3f scale, Vector3f vertex[], Vector3f normals[], Vector2f uvs[], int size_v, int size_n, int size_uv);
	bool set_mesh_transform(int index, Vector3f position, Vector3f rotation, Vector3f scale);
	bool set_mesh_by_index(int index, Vector3f vertex[], Vector3f normals[], Vector2f uvs[], int size_v, int size_n, int size_uv);
	void set_meshes_count(int size);

	std::string & get_log() { return log; }

private:

	bool generate_file();

	const char * string_to_char(const std::string & s);
};