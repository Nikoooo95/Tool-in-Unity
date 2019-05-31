#pragma once
#include "SimplexNoise.h"
#include "Model.hpp"
#include <string>



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
Model* createModel() {
	return new Model();
}

extern "C" __declspec(dllexport)
double initializeModel(Model* model, IntPtr path) {
	return 6.0; // model->getString(path);
	char* pchar =re
}