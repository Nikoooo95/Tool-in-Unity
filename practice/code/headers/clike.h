/**
 * @file clike.h
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Class for the comunication between C# and the DLL
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#pragma once

#include <string>
#include <iostream>
#include <stdio.h>

#include "Tool.hpp"

using namespace tool;

/**
 * @brief Calls the constructor of the C++ Tool
 * 
 */
extern "C" __declspec(dllexport)
Tool* createTool() {
	return new Tool();
}

/**
 * @brief Parses the .gonic file
 * 
 */
extern "C" __declspec(dllexport)
const bool parseFile(Tool* tool, char* path) {
	std::string test(path);
	return tool->parse(test);
}

/**
 * @brief Gets the amount of 2D Layers in the .gonic file
 * 
 */
extern "C" __declspec(dllexport)
int getLayers2dAmount(Tool * tool) {
	return tool->getLayers2dAmount();
}

/**
 * @brief Gets the name of a concrete layer
 * 
 */
extern "C" __declspec(dllexport)
const char* getLayer2dName(Tool * tool, int layer) {
	std::string workStr(tool->getLayer2dName(layer));
	return tool->charToString(workStr);
}

/**
 * @brief Gets the amount of 2D models in a concrete 2D layer of the .gonic file
 * 
 */
extern "C" __declspec(dllexport)
int getModelsInLayer2dAmount(Tool * tool, int layer) {
	return tool->getModelsInLayer2dAmount(layer);
}

/**
 * @brief Gets the name of a 2D model of a 2d layer
 * 
 */
extern "C" __declspec(dllexport)
const char* getModelNameInLayer(Tool * tool, int layer, int model) {
	std::string workStr(tool->getModelNameInLayer2d(layer, model));
	return tool->charToString(workStr);
}

/**
 * @brief Gets the amount of 2D Vectors of a concrete 2D model
 * 
 */
extern "C" __declspec(dllexport)
int getVectorsAmount(Tool * tool, int layer, int model) {
	return tool->getVectorsAmount(layer, model);
}

/**
 * @brief Fills an array of 2D Vector with the Vertex positions of a 2D model
 * 
 */
extern "C" __declspec(dllexport)
void fillVectors2d(Tool * tool, int layer, int model, Vector2 vectors[]) {
	tool->fillVectors2d(layer, model, vectors);
}

/**
 * @brief Fills an array of 3D Vector with the Vertex positions of a 3D model
 * 
 */
extern "C" __declspec(dllexport)
void fillVectors3d(Tool * tool, int layer, int model, Vector3 vectors[]) {
	tool->fillVectors3d(layer, model, vectors);
}

/**
 * @brief Gets the color of a model
 * 
 */
extern "C" __declspec(dllexport)
void getColor(Tool * tool, int layer, int model, Color* color) {
	tool->getColor(layer, model, color);
}

/**
 * @brief Generates a 3D layer from a 2D layer
 * 
 */
extern "C" __declspec(dllexport)
void generateLayer3d(Tool* tool, int layer) {
	tool->generateLayer3d(layer);
}

/**
 * @brief Transform a 2D model to a 3D model from a Layer
 * 
 */
extern "C" __declspec(dllexport)
void transform2dTo3d(Tool* tool, int layer, int model) {
	tool->transform2dTo3d(layer, model);
}

/**
 * @brief Generates the index positions for triangles of a 3D model
 * 
 */
extern "C" __declspec(dllexport)
void generateTriangles(Tool* tool, int triangles[], int amount, bool backFaces, bool looped) {
	tool->generateTriangles(triangles, amount, backFaces, looped);
}
