#include "stdafx.h"
#include "Exporter.h"

Exporter::Exporter()
{
	data = 5;
}

Exporter::~Exporter()
{
	data = 0;
}

int Exporter::get_data()
{
	return data;
}
