#pragma once
#include "SimplexNoise.h"

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
