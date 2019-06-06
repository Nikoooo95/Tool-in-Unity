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
int getLayers2dAmount(Tool * tool) {
	return tool->getLayers2dAmount();
}

extern "C" __declspec(dllexport)
const char* getLayer2dName(Tool * tool, int layer) {
	std::string workStr(tool->getLayer2dName(layer));
	return tool->charToString(workStr);
}

extern "C" __declspec(dllexport)
int getModelsInLayer2dAmount(Tool * tool, int layer) {
	return tool->getModelsInLayer2dAmount(layer);
}

extern "C" __declspec(dllexport)
const char* getModelNameInLayer(Tool * tool, int layer, int model) {
	std::string workStr(tool->getModelNameInLayer2d(layer, model));
	return tool->charToString(workStr);
}

//2D
extern "C" __declspec(dllexport)
int getVectorsAmount(Tool * tool, int layer, int model) {
	return tool->getVectorsAmount(layer, model);
}

extern "C" __declspec(dllexport)
void fillVectors2d(Tool * tool, int layer, int model, Vector2 vectors[]) {
	tool->fillVectors2d(layer, model, vectors);
}

extern "C" __declspec(dllexport)
void fillVectors3d(Tool * tool, int layer, int model, Vector3 vectors[]) {
	tool->fillVectors3d(layer, model, vectors);
}

extern "C" __declspec(dllexport)
void getColor(Tool * tool, int layer, int model, Color* color) {
	tool->getColor(layer, model, color);
}

extern "C" __declspec(dllexport)
void generateLayer3d(Tool* tool, int layer) {
	tool->generateLayer3d(layer);
}

extern "C" __declspec(dllexport)
void transform2dTo3d(Tool* tool, int layer, int model) {
	tool->transform2dTo3d(layer, model);
}

extern "C" __declspec(dllexport)
void generateTriangles(Tool* tool, int triangles[], int amount, bool backFaces, bool looped) {
	tool->generateTriangles(triangles, amount, backFaces, looped);
}
