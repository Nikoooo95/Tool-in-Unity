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
Vector3f get(Exporter* exporter)
{
	return exporter->get_data();
}

extern "C" __declspec(dllexport)
void set(Exporter* exporter, Vector3f vector)
{
	exporter->set_data(vector);
}


extern "C" __declspec(dllexport)
bool export_obj(Exporter * exporter, const char * path)
{
	return exporter->export_obj(path);
}

extern "C" __declspec(dllexport)
const char* get_path(Exporter * exporter)
{
	return exporter->get_path();
}






