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
	/*string workStr(tool->parse(test));

	//string workStr(cacaca);
	int lenStr = workStr.length() + 1;
	char* answer = new char[lenStr];
	const char * constAnswer = new char[lenStr];
	strcpy_s(answer, lenStr, workStr.c_str());
	constAnswer = answer;
	return constAnswer;*/
	return tool->parse(test);
}

extern "C" __declspec(dllexport)
const char* getLayer(Tool* tool) {
	string nameLayer = tool->getLayerName();
	
	int lenStr = nameLayer.length() + 1;
	char* answer = new char[lenStr];
	const char * constAnswer = new char[lenStr];
	strcpy_s(answer, lenStr, nameLayer.c_str());
	constAnswer = answer;
	return constAnswer;
}
