#pragma once

#include <string>
#include <iostream>
#include <stdio.h>


#include "SimplexNoise.h"
#include "Tool.hpp"

using namespace tool;

extern "C" __declspec(dllexport)
SimplexNoise* createSimplexNoise() {
	return new SimplexNoise();
}

extern "C" __declspec(dllexport)
void destroySimplexNoise(SimplexNoise* sn) {
	delete sn;
}

extern "C" __declspec(dllexport)
void initializeSimplexNoise(SimplexNoise* sn) {
	sn->init();
}

extern "C" __declspec(dllexport)
double simplex(SimplexNoise* sn, double x, double y) {
	return sn->noise(x, y);
}

extern "C" __declspec(dllexport)
Tool* createTool() {
	return new Tool();
}

extern "C" __declspec(dllexport)
const bool parseFile(Tool* tool, char* path) {
	string test(path);
	return tool->parse(test);
}

/*
extern "C" __declspec(dllexport)
const char* getLayer(Tool* tool) {
	string nameLayer = tool->getLayerName();
	
	int lenStr = nameLayer.length() + 1;
	char* answer = new char[lenStr];
	const char * constAnswer = new char[lenStr];
	strcpy_s(answer, lenStr, nameLayer.c_str());
	constAnswer = answer;
	return constAnswer;
}*/

extern "C" __declspec(dllexport)
const Vector2 * getVertex() {
	
	Vector2* vertice = new Vector2(0.0f, 11.0f);
	return vertice;

}


extern "C" __declspec(dllexport)
void getVector(Tool * tool, Vector2 prueba[]) {

	prueba[0].set(1.0f, 2.0f);
	prueba[1].set(3.0f, 4.0f);

}

void fillVectors(Tool * tool, int layer, int model, Vector2 vectors []) {
	tool->fillVectors(layer, model, vectors);
}

extern "C" __declspec(dllexport)
int getLayersAmount(Tool * tool) {
	return tool->getLayersAmount();
}


extern "C" __declspec(dllexport)
const char* getLayerName(Tool * tool, int layer) {
	string workStr(tool->getLayerName(layer));
	int lenStr = workStr.length() + 1;
	char* answer = new char[lenStr];
	const char * constAnswer = new char[lenStr];
	strcpy_s(answer, lenStr, workStr.c_str());
	constAnswer = answer;
	return constAnswer;
}

extern "C" __declspec(dllexport)
int getModelsInLayerAmount(Tool * tool, int layer) {
	return tool->getModelsInLayerAmount(layer);
}

extern "C" __declspec(dllexport)
int getVectorsAmount(Tool * tool, int layer, int model) {
	return tool->getVectorsAmount(layer, model);
}

extern "C" __declspec(dllexport)
const char* getModelNameInLayer(Tool * tool, int layer, int model) {
	string workStr(tool->getModelNameInLayer(layer, model));
	int lenStr = workStr.length() + 1;
	char* answer = new char[lenStr];
	const char * constAnswer = new char[lenStr];
	strcpy_s(answer, lenStr, workStr.c_str());
	constAnswer = answer;
	return constAnswer;
}



