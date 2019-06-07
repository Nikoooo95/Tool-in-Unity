/**
 * @file Exporter.h
 * @author Gonzalo Perez Chamarro (Gonzalo1810 GitHub.com)
 * @brief Clase exportador
 * @version 0.1
 * @date 2019-06-07
 * 
 * @copyright Copyright (c) 2019
 * 
 */

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

	/**
	 * @brief Ruta de exportacion
	 * 
	 */
	std::string path;

	/**
	 * @brief Mensajes de error
	 * 
	 */
	std::string log;

	/**
	 * @brief Nombre del archivo
	 * 
	 */
	std::string name;

	/**
	 * @brief Conjunto de mallas que se van a exportar
	 * 
	 */
	std::vector<sh_Mesh> meshes;

public:
	/**
	 * @brief Construye el exportador
	 * 
	 */
	Exporter();

	/**
	 * @brief Destructor
	 * 
	 */
	~Exporter() = default;

public:

	bool export_obj(std::string & path, std::string & name);

	const std::string & get_path();
	void set_path(const std::string & path);

	void add_mesh(Vector3f position, Vector3f rotation, Vector3f scale, Vector3f vertex[], Vector3f normals[], Vector2f uvs[], int size_v, int size_n, int size_uv);
	bool set_mesh_transform(int index, Vector3f position, Vector3f rotation, Vector3f scale);
	bool set_mesh_by_index(int index, Vector3f vertex[], Vector3f normals[], Vector2f uvs[], int size_v, int size_n, int size_uv);
	void set_meshes_count(int size);

	bool set_mesh_submeshes_count(int index, int size);
	bool set_submesh_triangles(int index, int submesh, int triangles[], int size);

	const std::string & get_log() { return log; }

	int get_meshes_count() { return meshes.size(); }

private:

	bool generate_file();

	const char * string_to_char(const std::string & s);
};