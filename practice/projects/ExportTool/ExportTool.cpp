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
int get(Exporter* exporter)
{
	return exporter->get_data();
}




