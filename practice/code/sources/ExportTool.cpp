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
	return exporter->get_log();
}

extern "C" __declspec(dllexport)
void set_path(Exporter * exporter, char * path)
{
	std::string str(path);
	exporter->set_path(str);
}

extern "C" __declspec(dllexport)
void set_mesh_transform(Exporter * exporter, Vector3f pos, Vector3f rot, Vector3f scl)
{
	exporter->set_transform(pos, rot, scl);
}

extern "C" __declspec(dllexport)
void set_vertex(Exporter * exporter, Vector3f v[])
{
	exporter->set_vertex(v);
}

extern "C" __declspec(dllexport)
int get_size(Exporter * exporter)
{
	return exporter->get_size();
}





