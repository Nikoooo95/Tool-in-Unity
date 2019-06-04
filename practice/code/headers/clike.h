#pragma once

#include <string>
#include <iostream>
#include <stdio.h>

#include "Tool.hpp"

using namespace tool;


extern "C" __declspec(dllexport)
Tool* createTool() {
	return new Tool();
}

extern "C" __declspec(dllexport)
const bool parseFile(Tool* tool, char* path) {
	std::string test(path);
	return tool->parse(test);
}

extern "C" __declspec(dllexport)
int getLayersAmount(Tool * tool) {
	return tool->getLayersAmount();
}

extern "C" __declspec(dllexport)
const char* getLayerName(Tool * tool, int layer) {
	std::string workStr(tool->getLayerName(layer));
	return tool->charToString(workStr);
}

extern "C" __declspec(dllexport)
int getModelsInLayerAmount(Tool * tool, int layer) {
	return tool->getModelsInLayerAmount(layer);
}

extern "C" __declspec(dllexport)
const char* getModelNameInLayer(Tool * tool, int layer, int model) {
	std::string workStr(tool->getModelNameInLayer(layer, model));
	return tool->charToString(workStr);
}

extern "C" __declspec(dllexport)
int getVectorsAmount(Tool * tool, int layer, int model) {
	return tool->getVectorsAmount(layer, model);
}

extern "C" __declspec(dllexport)
void fillVectors(Tool * tool, int layer, int model, Vector2 vectors[]) {
	tool->fillVectors(layer, model, vectors);
}
