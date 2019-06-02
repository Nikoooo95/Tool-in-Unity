// ExportTool.cpp : Define las funciones exportadas de la aplicación DLL.
//

#include "stdafx.h"
#include "Exporter.h"


extern "C" __declspec(dllexport)
Exporter* create()
{
	return new Exporter();
}

extern "C" __declspec(dllexport)
void destroy(Exporter* exporter)
{
	delete exporter;
}

extern "C" __declspec(dllexport)
bool export_obj(Exporter * exporter, char * path)
{
	std::string str(path);
	return exporter->export_obj(str);
}

extern "C" __declspec(dllexport)
const char* get_path(Exporter * exporter)
{
	return exporter->get_path();
}

extern "C" __declspec(dllexport)
const char* get_log(Exporter * exporter)
{
	return exporter->get_log().c_str();
}

extern "C" __declspec(dllexport)
void set_path(Exporter * exporter, char * path)
{
	std::string str(path);
	exporter->set_path(str);
}

extern "C" __declspec(dllexport)
void set_mesh_transform(Exporter * exporter, int index, Vector3f position, Vector3f rotation, Vector3f scale)
{
	exporter->set_mesh_transform(index, position, rotation, scale);
}

extern "C" __declspec(dllexport)
bool set_mesh_by_index(Exporter * exporter, int index, Vector3f vertex[], Vector3f normals[], Vector2f uvs[], int size_v, int size_n, int size_uv)
{
	return exporter->set_mesh_by_index(index, vertex, normals, uvs, size_v, size_n, size_uv);
}

extern "C" __declspec(dllexport)
void set_meshes_count(Exporter * exporter, int size)
{
	exporter->set_meshes_count(size);
}





